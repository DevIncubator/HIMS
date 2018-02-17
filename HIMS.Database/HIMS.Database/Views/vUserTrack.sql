CREATE VIEW [dbo].[vUserTrack]
	AS SELECT 
		[UserProfile].[UserId],
		[Task].[TaskId],
		[Task].[Name],
		[TaskTrack].[TaskTrackId],
		[TaskTrack].[TrackNote],
		[TaskTrack].[TrackDate]
	 FROM [Task], [TaskTrack], [UserProfile]
