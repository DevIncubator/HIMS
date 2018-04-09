CREATE TABLE [dbo].[Task]
(
	[TaskId] INT IDENTITY(1,1) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [DeadlineDate] DATE NOT NULL

	CONSTRAINT [PK_Task] PRIMARY KEY ([TaskId])
)
