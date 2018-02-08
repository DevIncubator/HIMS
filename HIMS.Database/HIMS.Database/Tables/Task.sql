CREATE TABLE [dbo].[Task]
(
	[TaskId] INT IDENTITY NOT NULL, 
    [Name] NVARCHAR(25) NOT NULL, 
	[Description] NVARCHAR(MAX) NOT NULL,
	[StartDate] DATE NOT NULL,
	[DeadlineDate] DATE NOT NULL

	CONSTRAINT [PK_Task] PRIMARY KEY ([TaskId])
)
