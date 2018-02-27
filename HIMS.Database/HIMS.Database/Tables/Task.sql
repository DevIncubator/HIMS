CREATE TABLE [dbo].[Task]
(
	[TaskId] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [StartDate] DATETIME NOT NULL, 
    [DeadlineDate] DATETIME NOT NULL
)
