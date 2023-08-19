CREATE PROCEDURE [dbo].[Account_GetById]
    @Id INT
AS

    SELECT * FROM Account
    WHERE Id = @Id

RETURN 0
