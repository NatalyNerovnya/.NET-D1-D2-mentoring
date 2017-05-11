DECLARE @date date = '1998-05-06';  
DECLARE @datetime datetime = @date;

SELECT [OrderID] AS [Order Number], COALESCE(CAST([ShippedDate] AS VARCHAR), 'Not Shipped') AS [Shipped Date]
FROM [Northwind].[dbo].[Orders]
WHERE [ShippedDate] IS NULL OR [ShippedDate] > @datetime