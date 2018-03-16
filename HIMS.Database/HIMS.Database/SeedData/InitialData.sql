INSERT INTO [dbo].[Sample]
		([Name], [Description])
	VALUES
		('Sample Record #1', 'This is a simple test record for testing purposes.'),
		('Sample Record #2', 'This is a simple test record for testing purposes too.');

INSERT INTO [dbo].[TaskState]
			([StateName])
	   VALUES
			('Active'),
			('Success'),
			('Fail')

INSERT INTO [dbo].[Direction]
		([Name], [Description])
	VALUES
		('.NET', 'The .NET direction.'),
		('Java', 'The Java direction'),
		('Front-End', 'The Front-End direction.');

-- Demo data start here --

INSERT INTO [dbo].[UserProfile] ([DirectionId], [Name], [Email], [LastName], [Sex], [Education], [BirthDate], [UniversityAverageScore], [MathScore], [Address], [MobilePhone], [Skype], [StartDate]) VALUES
	(1, N'Xavier', 'alex.meleschenko@gmail.com', N'Simpson', 'M', N'Software engineer', '1992-04-15', 8.27, 7.57, N'Makaenka, 5/35', N'1547595', 'XavierSimpson92', '2018-02-21'),
	(3, N'Jacob', 'jacobtyle74@gmail.com', N'Atcheson', 'M', N'Web designer', '1974-07-21', 8, 8.03, N'Krasnaya, 11/47', N'8736550', 'jacobtyle1', '2018-01-30'),
	(3, N'Nicole', 'ReynoldsNi@gmail.com', N'Reynolds', 'F', N'Web developer', '1997-01-31', 9.3, 7.4, N'Stroitelya, 124', N'1136107', 'reynolds_nicyas', '2017-10-28'),
	(2, N'Nathaniel', 'natan_elmerGH@gmail.com', N'Elmers', 'M', N'Software engineer', '1989-02-13', 7.49, 7.1, N'Shkolnaya, 5/44', N'1288724', 'nathaniel_elmersG', '2017-03-14'),
	(1, N'Isaac', 'isaacliremon@gmail.com', N'Fleming', 'M', N'Welder of 4th grade', '1968-07-08', 6, 8.4, N'Halturina, 87', N'9956520', 'isaacliremon', '2017-08-15'),
	(2, N'Maya', 'KittyMayaKawaii@gmail.com', N'Raleigh', 'F', N'Programmer of Information Systems', '1996-09-18', 9, 8.1, N'Cimiryazeva, 147/23', N'3398010', 'Kawaii_MayaK', '2017-04-08'),
	(3, N'Megan', 'MeganAlmostFox@gmail.com', N'Berrington', 'F', N'Art designer', '1993-08-14', 9.8, 9.3, N'Glagoleva, 9/7', N'6969877', 'MeganYourFoxy', '2017-07-29'),
	(2, N'Elizabeth', 'elizabeth.chapman@gmail.com', N'Chapman', 'F', N'Developer', '1995-02-14', 7.5, 7.1, N'Rofilova, 85', N'6942767', 'elizabeth_dewitt', '2017-10-08'),
	(1, N'John', 'russianjohny@mail.ru', N'Miller', 'M', N'Web Developer', '1973-01-05', 8, 8, N'Kovaleva, 12', N'1337992', 'russianjohny73', '2017-12-10'),
	(1, N'Aaron', 'aaron.stephen@gmail.com', N'Stephen', 'M', N'Software engineer', '1994-11-13', 9.4, 7.68, N'Orjanihidza, 87/29', N'7203602', 'aaron_omegalul', '2017-11-24')

INSERT INTO [dbo].[Task] ([TaskId], [Name], [Description], [StartDate], [DeadlineDate]) VALUES
	(1, N'Разработать class A', N'Необходимо разработать class A', '2018-02-14', '2018-02-24'),
	(2, N'Разработать class B', N'Необходимо разработать class B', '2018-02-15', '2018-02-25'),
	(3, N'Разработать контроллер', N'Необходимо разработать контроллер для работы с пользователём', '2018-03-10', '2018-03-30'),
	(4, N'Разработать view', N'Необходимо разработать view для class A и class B', '2018-03-14', '2018-03-24'),
	(5, N'Тестирование приложения', N'Необходимо написать тесты для контроллера', '2018-03-16', '2018-03-26'),
	(6, N'Развернуть проект', N'Необходимо развернуть приложение на хосте', '2018-03-20', '2018-03-30')

INSERT INTO [dbo].[UserTask] ([TaskId], [UserId], [StateId]) VALUES
	(1, 5, 1),
	(3, 4, 1),
	(6, 1, 1),
	(5, 6, 1),
	(2, 1, 1),
	(1, 9, 1),
	(3, 8, 1),
	(3, 2, 1),
	(1, 7, 1),
	(5, 3, 1),
	(4, 2, 1)

INSERT INTO	[dbo].[TaskTrack] ([TrackDate], [TrackNote], [UserTaskId]) VALUES
	('2018-02-15', N'Начал писать Class A', 1),
	('2018-03-21', N'Начал развёртывать проект на хосте, пока проблем не было', 3),
	('2018-03-18', N'Пишу тесты', 4),
	('2018-03-11', N'Начал разрабатывать контроллер', 7),
	('2018-03-10', N'Приступил к разработке контроллер', 2),
	('2018-03-15', N'Верстаю вьюхи', 11),
	('2018-02-15', N'Начал разрабатывать Class A', 9),
	('2018-02-19', N'Почти закончил разработку Class A', 1)