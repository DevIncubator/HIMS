CREATE PROCEDURE [dbo].[spDeleteUser]
	@userId int
AS
	DELETE UserProfile WHERE UserId=@userId
GO
