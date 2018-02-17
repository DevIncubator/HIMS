CREATE PROCEDURE [dbo].[spDeleteTaskTrack]
	@TaskTrackId int
AS
	DELETE TaskTrack WHERE TaskTrackId=@TaskTrackId
RETURN 0
