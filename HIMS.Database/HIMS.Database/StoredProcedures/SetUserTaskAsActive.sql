CREATE PROCEDURE [dbo].[SetUserTaskAsActive]
	@UserId int,
	@TaskId int
AS
DECLARE @stateId int
SELECT @stateId=StateId FROM TaskState WHERE TaskState.StateName = 'Active'
UPDATE UserTask
	SET StateId = @stateId
	WHERE UserTask.UserId = @UserId and UserTask.TaskId = @TaskId
GO