USE [ACTIVIFY]
GO
/*
Creador: Wilson Morales
Funcionalidad: SP para realizar CRUD de escuelas
*/
CREATE PROCEDURE sp_crud_almunno
   @action VARCHAR(3) = NULL,
   @AlmumnoId INT = NULL,
   @Carnet VARCHAR(50) = NULL,
   @Nombre VARCHAR(25) = NULL,
   @Telefono INT = NULL,
   @SeccionId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Alumno VALUES(@Carnet, @Nombre,@Telefono, @SeccionId)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT a.AlumnoId [Id], a.Carnet,a.Nombre,a.Telefono, a.SeccionId [Seccion]
		FROM Alumno a WITH(NOLOCK)  
		INNER JOIN Seccion s WITH(NOLOCK) ON s.SeccionId = a.SeccionId
		WHERE s.SeccionId = ISNULL(@SeccionId, a.SeccionId)
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Alumno
		SET Carnet = @Carnet, Nombre = @Nombre, Telefono = @Telefono
		WHERE AlumnoId = @AlmumnoId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		DELETE Alumno where AlumnoId = @AlmumnoId
	END

	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_alumno', CONCAT(@action,',',ISNULL(@AlmumnoId, 0),',',ISNULL(@Carnet, 'NULL'),',',ISNULL(@nombre, 'NULL'),',',ISNULL(@Telefono, 'NULL'),',',ISNULL(@SeccionId, 0)), 1000, GETDATE())
	END
END