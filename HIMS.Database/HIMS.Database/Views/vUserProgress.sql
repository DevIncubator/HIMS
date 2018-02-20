CREATE VIEW [dbo].[vUserProgress]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
              ISNULL(ut.TaskId, -999) AS TaskId,
              ISNULL(tt.TaskTrackId, -999) AS TaskTrackId,
              up.Name AS UserName,
              t.Name AS TaskName,
              tt.TrackNote AS TrackNote,
              tt.TrackDate AS TrackDate
	FROM [UserTask] ut JOIN [TaskTrack] tt ON ut.UserTaskId = tt.UserTaskId
	                   JOIN [UserProfile] up ON ut.UserId = up.UserId
	                   JOIN [Task] t ON ut.TaskId = t.TaskId