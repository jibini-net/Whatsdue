CREATE PROCEDURE [dbo].[Account_List]
AS

    SELECT * FROM Account
    ORDER BY LastName, FirstName, Id

RETURN 0
