CREATE VIEW [dbo].[vUserTrack]
	AS SELECT 
		ISNULL(ut.[UserId], -999) AS UserId, --for Entity Framework primary key
		t.[TaskId],
		t.[Name],
		tt.[TaskTrackId],
		tt.[TrackNote],
		tt.[TrackDate]
	 FROM [Task] t join [UserTask] ut on t.TaskId = ut.TaskId join [TaskTrack] tt on ut.UserTaskId = tt.UserTaskId
