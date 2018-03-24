SELECT emp2.EmployeeID, emp2.FirstName as EmployeeeName, boss.EmployeeID as Boss, boss.FirstName as BossName
FROM [Northwind].[dbo].[Employees] as emp2
 JOIN [Employees]  boss ON  boss.EmployeeID  = emp2.ReportsTo