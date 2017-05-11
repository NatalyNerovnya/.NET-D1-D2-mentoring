SELECT CustomerId
FROM [Northwind].[dbo].[Customers]
WHERE Country > 'B' AND Country < 'H'
ORDER BY Country