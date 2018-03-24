SELECT s.CompanyName
FROM [Northwind].[dbo].[Suppliers] s
WHERE s.SupplierID IN (SELECT p.SupplierID 
						FROM [Northwind].[dbo].[Products] p 
						WHERE p.UnitsInStock = 0)
