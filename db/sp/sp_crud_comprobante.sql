
USE [ACTIVIFY]
GO
CREATE PROCEDURE sp_crud_comprobante
   @action VARCHAR(3) = NULL,
   @ComprobanteId INT = NULL,
   @Nombre VARCHAR(50) = NULL,
   @Imagen image = NULL,
   @GastoId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@action = 'C') --Create
	BEGIN
		INSERT INTO Comprobante VALUES(@Nombre,@Imagen, GETDATE(), @GastoId)
	END
	
	IF (@action = 'R') --Read
	BEGIN
		SELECT c.ComprobanteId [Id], c.Nombre, ISNULL(c.Imagen, NULL) [Imagen]
		FROM Comprobante c WITH(NOLOCK)  
		INNER JOIN Gasto g WITH(NOLOCK) ON g.GastoId = c.GastoId
		WHERE g.GastoId = ISNULL(@GastoId, g.GastoId)
	END
	
	IF (@action = 'U') --Update
	BEGIN
		UPDATE Comprobante
		SET Nombre = @Nombre, Imagen = @Imagen
		WHERE ComprobanteId = @ComprobanteId
	END
	
	IF (@action = 'D') --DELETE
	BEGIN
		DELETE Comprobante where ComprobanteId = @ComprobanteId
	END
	
	IF (@action = 'S') --See image
	BEGIN
		SELECT Imagen FROM Comprobante WHERE ComprobanteId = @ComprobanteId
	END
	
	IF(@action <> 'R')
	BEGIN
		--Insertando data en bitácora
		DECLARE @actionName VARCHAR(25);
		SELECT @actionName = CASE WHEN @action = 'C' THEN 'Create' WHEN @action = 'U' THEN 'Update' WHEN @action = 'D' THEN 'Delete' ELSE NULL END

		INSERT INTO Bitacora VALUES(@actionName, 'sp_crud_comprobante', CONCAT(ISNULL(@action, 'NULL'),',',ISNULL(@ComprobanteId, 0),',',ISNULL(@Nombre, 'NULL'),',',ISNULL(SUBSTRING(@Imagen,1,10), NULL),',',ISNULL(@GastoId, 0)), 1000, GETDATE())
	END
END