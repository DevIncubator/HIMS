CREATE VIEW [dbo].[vUserTask]
	AS SELECT ISNULL(ut.UserId, -999) AS UserId,
			  ISNULL(t.TaskId, -999) AS TaskId,
			  t.[Name] as TaskName,
			  t.[Description],
			  t.StartDate,
			  t.DeadlineDate,
			  ts.StateName as [State]
			  FROM [Task] t JOIN [UserTask] ut ON ut.TaskId= t.TaskId
			  JOIN [TaskState] ts on ts.StateId = ut.StateId
