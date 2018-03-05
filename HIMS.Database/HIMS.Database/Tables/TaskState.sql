CREATE TABLE [dbo].[TaskState]
(
	[StateId] INT IDENTITY NOT NULL,
	[StateName] NVARCHAR(25) NOT NULL

	CONSTRAINT [PK_TaskState] PRIMARY KEY ([StateId])
)
