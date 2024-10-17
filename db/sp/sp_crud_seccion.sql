USE [ACTIVIFY]
GO
ALTER PROCEDURE [dbo].[sp_crud_seccion]
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
		INSERT INTO Seccion VALUES(@grado, @aula, @profesorId, 1)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT s.SeccionId [Id], s.Grado, s.Aula, p.Nombre [Profesor]
		FROM Seccion s WITH(NOLOCK)  
		INNER JOIN Profesor p WITH(NOLOCK) ON s.ProfesorId = p.ProfesorId
		WHERE s.ProfesorId = ISNULL(@profesorId, s.ProfesorId)
		AND s.IsActive = 1
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Seccion
		SET Grado = @grado, Aula = @aula, ProfesorId = @profesorId
		WHERE SeccionId = @seccionId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		UPDATE Seccion SET IsActive = 0 WHERE SeccionId = @seccionId
	END

	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_seccion', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@seccionId, 0),',',ISNULL(@grado, 'NULL'),',',ISNULL(@aula, 0.0),',',ISNULL(@profesorId, 0)), 1000, GETDATE())
	END
END