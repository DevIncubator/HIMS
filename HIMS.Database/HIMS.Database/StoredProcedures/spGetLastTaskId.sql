CREATE PROCEDURE [dbo].[spGetLastTaskId]
@result int OUTPUT
AS
  BEGIN
	SELECT  TOP 1 @result=TaskId
				FROM [Task]
				ORDER BY TaskId DESC
END
RETURN 0
