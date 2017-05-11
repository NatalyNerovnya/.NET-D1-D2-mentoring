DECLARE @date date = '1998-05-06';  
DECLARE @datetime datetime = @date;

SELECT [OrderID],[ShippedDate], [ShipVia] FROM [Northwind].[dbo].[Orders]
WHERE [ShippedDate] >= @datetime AND [ShipVia] >= 2