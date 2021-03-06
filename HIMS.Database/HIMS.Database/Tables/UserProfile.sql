﻿CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT IDENTITY NOT NULL, 
    [DirectionId] INT NOT NULL, 
    [Name] NVARCHAR(25) NOT NULL, 
    [Email] NCHAR(125) NOT NULL, 
    [LastName] NVARCHAR(25) NOT NULL, 
    [Sex] CHAR NOT NULL, 
    [Education] NVARCHAR(250) NOT NULL, 
    [BirthDate] DATE NOT NULL, 
    [UniversityAverageScore] FLOAT NULL, 
    [MathScore] FLOAT NULL, 
    [Address] NVARCHAR(100) NOT NULL, 
    [MobilePhone] NVARCHAR(15) NOT NULL, 
    [Skype] VARCHAR(50) NULL, 
    [StartDate] DATE NOT NULL

	CONSTRAINT [PK_UserProfile] PRIMARY KEY ([UserId])
	CONSTRAINT [FK_Direction_To_UserProfile] FOREIGN KEY ([DirectionId]) REFERENCES [Direction]([DirectionId]) ON DELETE CASCADE ON UPDATE CASCADE, 
)
