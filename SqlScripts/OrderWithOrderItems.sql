CREATE PROCEDURE OrderWithOrderItems
@OrderId int
AS
BEGIN
SET NOCOUNT ON;
SELECT  [Id]
      ,[OrderDate]
      ,[OrderNumber]
      ,[CustomerId]
      ,[TotalAmount]
  FROM  [dbo].[Order]
  WHERE [Id]=@OrderId

  SELECT   [Id]
      ,[OrderId]
      ,[ProductId]
      ,[UnitPrice]
      ,[Quantity]
  FROM  [dbo].[OrderItem]
      WHERE [OrderId]=@OrderId
  SET NOCOUNT OFF;
  END