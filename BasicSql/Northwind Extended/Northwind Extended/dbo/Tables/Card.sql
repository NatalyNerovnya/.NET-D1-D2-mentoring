CREATE TABLE [dbo].[Card]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CardNumber] INT NOT NULL, 
    [ExpireDate] DATE NOT NULL, 
    [CardHolder] NVARCHAR(50) NOT NULL, 
    [CustomerId] NCHAR(5) NOT NULL, 
    CONSTRAINT [FK_Card_ToCustomers] FOREIGN KEY ([CustomerID]) REFERENCES [Customers]([CustomerID])
)
