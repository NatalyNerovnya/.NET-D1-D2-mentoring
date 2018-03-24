IF EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U'))
BEGIN
	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Territories_Region')
	BEGIN
	  ALTER TABLE [FK_Territories_Regions]
	  DROP CONSTRAINT FK_Territories_Region
	END

	IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'Regions')
	BEGIN
	CREATE TABLE Regions (
    [RegionID]          INT        NOT NULL,
    [RegionDescription] NCHAR (50) NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY NONCLUSTERED ([RegionID] ASC))
	END

	IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'FK_Territories_Regions')
	BEGIN
	ALTER TABLE [dbo].[Territories] WITH NOCHECK
    ADD CONSTRAINT [FK_Territories_Regions] FOREIGN KEY ([RegionID]) REFERENCES [dbo].[Regions] ([RegionID]);
	END

	BEGIN
	ALTER TABLE [dbo].[Territories] WITH CHECK CHECK CONSTRAINT [FK_Territories_Regions];
	END

	IF EXISTS (SELECT * FROM sysobjects WHERE name = 'Region')
	BEGIN
	EXECUTE sp_rename'Region', 'Regions'
	END

END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'FoundationDate')
BEGIN
ALTER TABLE [dbo].[Customers]
	ADD FoundationDate DATE NULL        
END