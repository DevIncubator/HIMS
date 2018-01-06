CREATE PROCEDURE [dbo].[spSetUserTaskAsFail]
	@UserId int,
	@TaskId int
AS
UPDATE UserTask
	SET StateId = (SELECT StateId FROM TaskState WHERE TaskState.StateName = 'Fail')
	WHERE UserTask.UserId = @UserId and UserTask.TaskId = @TaskId
GO
