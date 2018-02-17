﻿CREATE TABLE [dbo].[TaskTrack]
(
	[TaskTrackId] INT NOT NULL PRIMARY KEY, 
    [TrackDate] DATETIME NOT NULL, 
    [TrackNote] NVARCHAR(MAX) NOT NULL, 
    [UserTaskId] INT NOT NULL, 
    CONSTRAINT [FK_TaskTrack_ToTask] FOREIGN KEY ([UserTaskId]) REFERENCES [Task]([TaskId]) ON DELETE CASCADE ON UPDATE CASCADE,
)
