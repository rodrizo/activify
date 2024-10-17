USE ACTIVIFY
GO
ALTER PROCEDURE [dbo].[sp_crud_profesor]
   @action VARCHAR(3) = NULL,
   @profesorId INT = NULL,
   @nombre VARCHAR(50) = NULL,
   @email VARCHAR(50) = NULL,
   @telefono VARCHAR(50) = NULL,
   @dpi VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Profesor VALUES(@nombre, @email, @telefono, @dpi, 1)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT p.ProfesorId [Id], p.Nombre, p.Email, p.Telefono, p.DPI
		FROM Profesor p WITH(NOLOCK)  
		WHERE p.IsActive = 1
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Profesor
		SET nombre = @nombre, Email = @email, Telefono = @telefono, DPI = @dpi
		WHERE ProfesorId = @profesorId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		UPDATE Profesor SET IsActive = 0 WHERE ProfesorId = @profesorId
	END
	
	
	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitacora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_profesor', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@profesorId, 0),',',
		ISNULL(@nombre, 'NULL'),ISNULL(@email, 0),',',ISNULL(@telefono, 0),',',ISNULL(@dpi, 0)), 1000, GETDATE())
	END
END