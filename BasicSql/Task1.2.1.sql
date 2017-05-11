SELECT ContactName, Country 
FROM [Northwind].[dbo].[Customers]
WHERE Country IN ('USA', 'Canada')
ORDER BY ContactName, Country