SELECT o.EmployeeID AS [Seller], COUNT(CustomerID) AS [Amount] , CONCAT(e.LastName ,e.FirstName ) AS [Name]
FROM Orders o
JOIN Employees  e
ON o.EmployeeID = e.EmployeeID
GROUP BY o.EmployeeID, CONCAT(e.LastName ,e.FirstName ) 
ORDER BY [Amount] DESC