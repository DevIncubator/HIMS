CREATE PROCEDURE [dbo].[spSetUserTaskAsSuccess]
	@UserId int,
	@TaskId int
AS
UPDATE UserTask
	SET StateId = (SELECT StateId FROM TaskState WHERE TaskState.StateName = 'Success')
	WHERE UserTask.UserId = @UserId and UserTask.TaskId = @TaskId
GO
