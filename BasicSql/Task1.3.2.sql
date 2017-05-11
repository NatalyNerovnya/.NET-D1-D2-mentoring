SELECT CustomerId, Country
FROM [Northwind].[dbo].[Customers]
WHERE Country BETWEEN 'B' AND 'H'
ORDER BY Country