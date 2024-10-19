USE [ACTIVIFY]
GO
ALTER PROCEDURE [dbo].[sp_crud_actividad]
   @action VARCHAR(3) = NULL,
   @actividadId INT = NULL,
   @nombre VARCHAR(250) = NULL,
   @fecha DATETIME = NULL,
   @monto DECIMAL(12,8) = NULL,
   @observaciones VARCHAR(MAX) = NULL,
   @tipoActividadId INT = NULL,
   @seccionId INT = NULL,
   @alumnoId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Actividad VALUES(@nombre, @fecha, @monto, @observaciones, GETDATE(), @tipoActividadId, @seccionId, @alumnoId, 1)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT 
			a.ActividadId [Id], 
			a.Nombre, 
			a.Fecha, 
			a.Monto, 
			a.Observaciones, 
			ta.Descripcion [Actividad], 
			CONCAT(s.Grado, ' - ', s.Aula) [Seccion], 
			al.Nombre [Alumno Responsable], 
			(a.Monto - ISNULL(SUM(g.Monto), 0)) [Monto Disponible]
		FROM Actividad a WITH(NOLOCK)  
		INNER JOIN Seccion s WITH(NOLOCK) ON s.SeccionId = a.SeccionId
		INNER JOIN Profesor p WITH(NOLOCK) ON p.ProfesorId = s.ProfesorId
		INNER JOIN TipoActividad ta WITH(NOLOCK) ON ta.TipoActividadId = a.TipoActividadId
		LEFT JOIN Alumno al WITH(NOLOCK) ON al.AlumnoId = a.AlumnoId
		LEFT JOIN Gasto g WITH(NOLOCK) ON g.ActividadId = a.ActividadId
			AND g.IsActive = 1
		WHERE a.SeccionId = ISNULL(@seccionId, a.SeccionId)
		AND a.IsActive = 1
		GROUP BY a.ActividadId, a.Nombre, a.Fecha, a.Monto, a.Observaciones, ta.Descripcion, CONCAT(s.Grado, ' - ', s.Aula), al.Nombre
	END
	
	IF (@action = 'U') --Update
	BEGIN

		DECLARE @resultado DECIMAL(10,2);

		SELECT 
			@resultado = CONVERT(DECIMAL(10,2), (@monto - ISNULL(SUM(g.Monto), 0)))
		FROM Actividad a WITH(NOLOCK)  
		LEFT JOIN Gasto g WITH(NOLOCK) ON g.ActividadId = a.ActividadId
		WHERE a.ActividadId = ISNULL(@actividadId, a.ActividadId)
		AND a.IsActive = 1
		GROUP BY a.Monto

		IF(@resultado >= 0)
		BEGIN
			UPDATE Actividad
			SET Nombre = @nombre, Fecha = @fecha, Monto = @monto, Observaciones = @observaciones, TipoActividadId = @tipoActividadId, SeccionId = @seccionId, AlumnoId = @alumnoId, LastModifiedDate = GETDATE()
			WHERE ActividadId = @actividadId
		END
		ELSE
		BEGIN
			RAISERROR('El monto disponible no puede quedar en valores negativos.', 16, 1)

		END
		
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		UPDATE Actividad SET IsActive = 0 WHERE ActividadId = @actividadId
		--DELETE Actividad WHERE ActividadId = @actividadId
	END

	
	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_actividad', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@actividadId, 0),',',ISNULL(@nombre, 'NULL'),',',ISNULL(@fecha, NULL),
		',',ISNULL(@monto, 0.0),',',ISNULL(@observaciones, 'NULL'),',',ISNULL(@tipoActividadId, 0),',',ISNULL(@seccionId, 0),',',ISNULL(@alumnoId, 0)), 1000, GETDATE())
	END
END