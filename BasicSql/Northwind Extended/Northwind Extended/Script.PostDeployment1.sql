/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
Use [Northwind Extended_2]
GO
INSERT INTO [dbo].[Categories]([CategoryName], [Description]) VALUES ('Category 545', 'Category 656');

GO
INSERT INTO [dbo].[Suppliers]( [CompanyName], [ContactName],[ContactTitle]) VALUES ('CompanyName 1', 'ContactName 1', 'ContactTitle 1')


GO
INSERT INTO [dbo].[Products]([ProductName], [Discontinued]) VALUES ('ProductName 1', 0)
