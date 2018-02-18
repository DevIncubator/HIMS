CREATE TABLE [dbo].[Direction]
(
	[DirectionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL 
)

GO
