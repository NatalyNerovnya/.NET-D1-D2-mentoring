SELECT ContactName, Country 
FROM [Northwind].[dbo].[Customers]
WHERE Country NOT IN ('USA', 'Canada')
ORDER BY ContactName