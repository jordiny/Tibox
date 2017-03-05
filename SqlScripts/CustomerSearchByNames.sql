CREATE PROCEDURE  [dbo].[CustomerSearchByNames]
@firstName nVarchar(40),
@lastName nVarchar(40)
As
BEGIN
SET NOCOUNT ON;
SELECT Id,FirstName,LastName,City,Country,Phone FROM Customer
where FirstName=@firstName and LastName=@lastName
SET NOCOUNT OFF;
END