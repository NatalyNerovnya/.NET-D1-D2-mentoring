SELECT SUM([UnitPrice] * (1-[Discount])*[Quantity]) AS 'Totals'
FROM [Northwind].[dbo].[Order Details]