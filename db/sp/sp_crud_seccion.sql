USE [ACTIVIFY]
GO
CREATE PROCEDURE sp_crud_seccion
   @action VARCHAR(3) = NULL,
   @seccionId INT = NULL,
   @grado VARCHAR(50) = NULL,
   @aula VARCHAR(50) = NULL,
   @profesorId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Seccion VALUES(@grado, @aula, @profesorId)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT s.SeccionId [Id], s.Grado, s.Aula, p.Nombre [Profesor]
		FROM Seccion s WITH(NOLOCK)  
		INNER JOIN Profesor p WITH(NOLOCK) ON s.ProfesorId = p.ProfesorId
		WHERE s.ProfesorId = ISNULL(@profesorId, s.ProfesorId)
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Seccion
		SET Grado = @grado, Aula = @aula, ProfesorId = @profesorId
		WHERE SeccionId = @seccionId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		DELETE Seccion WHERE SeccionId = @seccionId
	END
	/*
	IF (@action = 'M') --Resultados obtenidos de mejoramiento de seccion
	BEGIN
		SELECT c.Nombre [Comite], a.Nombre [Actividad], a.DetalleActividades [Detalle], a.Observaciones, a.Estimado, ta.Descripcion [Tipo de actividad]
		FROM Comite c WITH(NOLOCK)
		INNER JOIN Actividad a WITH(NOLOCK) ON a.ComiteId = c.ComiteId
		INNER JOIN TipoActividad ta WITH(NOLOCK) ON ta.TipoActividadId = a.TipoActividadId
		WHERE c.ComiteId = ISNULL(@idComite, c.ComiteId)
		AND ta.TipoActividadId = 3 --Mejoramiento
	END
	*/
	
	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_seccion', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@seccionId, 0),',',ISNULL(@grado, 'NULL'),',',ISNULL(@aula, 0.0),',',ISNULL(@profesorId, 0)), 1000, GETDATE())
	END
END