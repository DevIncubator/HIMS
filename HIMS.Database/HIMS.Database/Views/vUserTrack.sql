CREATE VIEW [dbo].[vUserTrack]
	AS SELECT 
		ut.[UserId],
		t.[TaskId],
		t.[Name],
		ISNULL(tt.[TaskTrackId], -999) AS TaskTrackId,
		tt.[TrackNote],
		tt.[TrackDate]
	 FROM [Task] t join [UserTask] ut on t.TaskId = ut.TaskId join [TaskTrack] tt on ut.UserTaskId = tt.UserTaskId
