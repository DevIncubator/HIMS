INSERT INTO [dbo].[Sample]
		([Name], [Description])
	VALUES
		('Sample Record #1', 'This is a simple test record for testing purposes.'),
		('Sample Record #2', 'This is a simple test record for testing purposes too.');

INSERT INTO [dbo].[Direction]
		([Name], [Description])
	VALUES
		('.NET', 'The .NET direction.'),
		('Java', 'The Java direction'),
		('Front-End', 'The Front-End direction.');

INSERT INTO [dbo].[TaskState]
			([StateName])
	   VALUES
			('Active'),
			('Success'),
			('Fail')