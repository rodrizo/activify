USE [ACTIVIFY]
GO

ALTER PROCEDURE [dbo].[sp_crud_gastos]
   @action VARCHAR(3) = NULL,
   @idGasto INT = NULL,
   @descripcion VARCHAR(250) = NULL,
   @monto DECIMAL(12,8)= NULL,
   @idActividad INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
	
		DECLARE @resultado DECIMAL(10,2);
		
		SELECT 
			@resultado = CONVERT(DECIMAL(10,2), (a.Monto - ISNULL(SUM(g.Monto), 0)))
		FROM Actividad a WITH(NOLOCK)  
		INNER JOIN Gasto g WITH(NOLOCK) ON g.ActividadId = a.ActividadId
		WHERE a.ActividadId = ISNULL(@idActividad, a.ActividadId)
		GROUP BY a.Monto

		SET @resultado = @resultado - @monto;
		
		IF(@resultado >= 0)
		BEGIN
			INSERT INTO Gasto VALUES(@descripcion, @monto, GETDATE(), @idActividad, 1)
		END
		ELSE
		BEGIN
			RAISERROR('El presupuesto de la actividad no es suficiente. Favor introducir un gasto menor.', 16, 1)
		END
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT g.GastoId [Id], g.Descripcion, g.Monto, a.Nombre [Actividad], CONCAT(s.Aula, '- ', s.Grado) [Seccion]
		FROM Gasto g WITH(NOLOCK)  
		INNER JOIN Actividad a WITH(NOLOCK) ON a.ActividadId = g.ActividadId
		INNER JOIN Seccion s WITH(NOLOCK) ON s.SeccionId = a.SeccionId
		WHERE g.ActividadId = ISNULL(@idActividad, g.ActividadId)
		AND g.IsActive = 1
	END
	
	IF (@action = 'U') --Update
	BEGIN

		DECLARE @resultado2 DECIMAL(10,2);

		SELECT 
			@resultado2 = CONVERT(DECIMAL(10,2), (a.Monto - ISNULL(SUM(g.Monto), 0)))
		FROM Actividad a WITH(NOLOCK)  
		INNER JOIN Gasto g WITH(NOLOCK) ON g.ActividadId = a.ActividadId
		WHERE a.ActividadId = ISNULL(@idActividad, a.ActividadId)
		GROUP BY a.Monto

		SET @resultado2 = @resultado2 - @monto;

		IF(@resultado2 >= 0)
		BEGIN
			UPDATE Gasto
			SET Descripcion = @descripcion, Monto = @monto
			WHERE GastoId = @idGasto
		END
		ELSE
		BEGIN
			RAISERROR('El presupuesto de la actividad no es suficiente. Favor introducir un gasto menor.', 16, 1)
		END

	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		UPDATE Gasto SET IsActive = 0 WHERE GastoId = @idGasto
	END

	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitacora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_gastos', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@idGasto, 0),',',ISNULL(@descripcion, 'NULL'),',',ISNULL(@monto, 0),',',ISNULL(@idActividad, 0)), 1000, GETDATE())
	END
END