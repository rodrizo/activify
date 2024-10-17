USE [ACTIVIFY]
GO
ALTER PROCEDURE [dbo].[sp_crud_alumno]
   @action VARCHAR(3) = NULL,
   @AlumnoId INT = NULL,
   @Carnet VARCHAR(50) = NULL,
   @Nombre VARCHAR(50) = NULL,
   @Telefono VARCHAR(50) = NULL,
   @SeccionId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Alumno VALUES(@Carnet, @Nombre,@Telefono, @SeccionId, 1)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT a.AlumnoId [Id], a.Carnet,a.Nombre,a.Telefono, a.SeccionId [Seccion]
		FROM Alumno a WITH(NOLOCK)  
		INNER JOIN Seccion s WITH(NOLOCK) ON s.SeccionId = a.SeccionId
		WHERE s.SeccionId = ISNULL(@SeccionId, a.SeccionId)
		AND a.IsActive = 1
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Alumno
		SET Carnet = @Carnet, Nombre = @Nombre, Telefono = @Telefono
		WHERE AlumnoId = @AlumnoId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		UPDATE Alumno SET IsActive = 0 FROM Alumno
		--DELETE Alumno where AlumnoId = @AlumnoId
	END

	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_alumno', CONCAT(@action,',',ISNULL(@AlumnoId, 0),',',ISNULL(@Carnet, 'NULL'),',',ISNULL(@nombre, 'NULL'),',',ISNULL(@Telefono, 'NULL'),',',ISNULL(@SeccionId, 0)), 1000, GETDATE())
	END
END