CREATE PROCEDURE [dbo].[spDeleteTask]
	@TaskId int
AS
	DELETE Task WHERE Task.TaskId= @TaskId
GO
