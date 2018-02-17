CREATE VIEW [dbo].[vUserTrack]
	AS SELECT 
		ISNULL([UserProfile].[UserId], -999) AS UserId, --for Entity Framework primary key
		[Task].[TaskId],
		[Task].[Name],
		[TaskTrack].[TaskTrackId],
		[TaskTrack].[TrackNote],
		[TaskTrack].[TrackDate]
	 FROM [Task], [TaskTrack], [UserProfile]
