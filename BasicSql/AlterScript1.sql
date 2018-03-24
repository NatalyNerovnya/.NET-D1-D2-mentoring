IF NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Card]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Card]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CardNumber] INT NOT NULL, 
    [ExpireDate] DATE NOT NULL, 
    [CardHolder] NVARCHAR(50) NOT NULL, 
    [CustomerId] NCHAR(5) NOT NULL, 
    CONSTRAINT [FK_Card_ToCustomers] FOREIGN KEY ([CustomerID]) REFERENCES [Customers]([CustomerID])
)
END
