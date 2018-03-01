﻿CREATE PROCEDURE [dbo].[spSetUserTaskAsFail]
	@UserId int,
	@TaskId int
AS
DECLARE @stateId int
SELECT @stateId=StateId FROM TaskState WHERE TaskState.StateName = 'Fail'
UPDATE UserTask
	SET StateId = @stateId
	WHERE UserTask.UserId = @UserId and UserTask.TaskId = @TaskId
GO