SELECT COUNT(CustomerID) AS [Amount], CustomerID, EmployeeID
FROM [Northwind].[dbo].[Orders]
WHERE YEAR(OrderDate) = 1998
GROUP BY EmployeeID, CustomerID