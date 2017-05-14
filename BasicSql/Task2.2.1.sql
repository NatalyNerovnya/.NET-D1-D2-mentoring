SELECT YEAR(OrderDate) AS [Year], COUNT(OrderId) AS [Total]
FROM [Northwind].[dbo].[Orders]
GROUP BY YEAR(OrderDate)

SELECT COUNT(OrderId) AS [Total]
FROM [Northwind].[dbo].[Orders]