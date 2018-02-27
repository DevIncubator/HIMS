CREATE VIEW [dbo].[vTask]
	AS SELECT 
	 ISNULL(t.TaskId, -999) AS TaskId,
	 t.Name,
	 t.Description,
	 t.StartDate,
	 t.DeadlineDate
	FROM [Task] t
