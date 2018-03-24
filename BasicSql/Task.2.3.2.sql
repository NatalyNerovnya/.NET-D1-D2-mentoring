SELECT c.CompanyName, COUNT(o.OrderID) as Orders
FROM [Northwind].[dbo].[Customers] c
JOIN [Northwind].[dbo].[Orders] o ON c.CustomerID = o.CustomerID
GROUP BY c.CompanyName
