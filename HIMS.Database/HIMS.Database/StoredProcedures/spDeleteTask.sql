CREATE PROCEDURE [dbo].[spDeleteTask]
	@taskId int
AS
	DELETE Task WHERE TaskId=@taskId
GO