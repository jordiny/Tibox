CREATE PROCEDURE CustomerWithOrders
@customerId int
AS
BEGIN
SET NOCOUNT ON;
SELECT Id,FirstName,LastName,City,Country,Phone 
FROM Customer WHERE id=@customerId

SELECT  [Id]
      ,[OrderDate]
      ,[OrderNumber]
      ,[CustomerId]
      ,[TotalAmount]
  FROM  [dbo].[Order] where [CustomerId]=@customerId

  SET NOCOUNT OFF;
  END