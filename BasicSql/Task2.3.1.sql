SELECT e.EmployeeID FROM [Northwind].[dbo].[Employees] e
JOIN [Northwind].[dbo].[EmployeeTerritories] et ON et.EmployeeID = e.EmployeeID
JOIN [Northwind].[dbo].[Territories] t ON et.TerritoryID = t.TerritoryID
WHERE t.RegionID = 2
