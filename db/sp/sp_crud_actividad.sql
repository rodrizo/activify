USE [ACTIVIFY]
GO

ALTER PROCEDURE sp_crud_actividad
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
		INSERT INTO Actividad VALUES(@nombre, @fecha, @monto, @observaciones, GETDATE(), @tipoActividadId, @seccionId, @alumnoId)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT a.ActividadId [Id], a.Nombre, a.Fecha, a.Monto, a.Observaciones, ta.Descripcion [Actividad], CONCAT(s.Grado, ' - ', s.Aula) [Seccion], al.Nombre [Alumno Responsable]
		FROM Actividad a WITH(NOLOCK)  
		INNER JOIN Seccion s WITH(NOLOCK) ON s.SeccionId = a.SeccionId
		INNER JOIN Profesor p WITH(NOLOCK) ON p.ProfesorId = s.ProfesorId
		INNER JOIN TipoActividad ta WITH(NOLOCK) ON ta.TipoActividadId = a.TipoActividadId
		LEFT JOIN Alumno al WITH(NOLOCK) ON al.AlumnoId = a.AlumnoId
		WHERE a.SeccionId = ISNULL(@seccionId, a.SeccionId)
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Actividad
		SET Nombre = @nombre, Fecha = @fecha, Monto = @monto, Observaciones = @observaciones, TipoActividadId = @tipoActividadId, SeccionId = @seccionId, AlumnoId = @alumnoId, LastModifiedDate = GETDATE()
		WHERE ActividadId = @actividadId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		DELETE Actividad WHERE ActividadId = @actividadId
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