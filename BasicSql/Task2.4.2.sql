SELECT c.CompanyName
FROM [Northwind].[dbo].[Customers] c
WHERE c.CustomerID = ANY(SELECT o.CustomerID
							FROM [Northwind].[dbo].[Orders] o
							GROUP BY o.CustomerID
							HAVING COUNT(o.OrderID) >= 150)