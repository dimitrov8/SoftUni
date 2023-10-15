CREATE TABLE [People] (
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Name] NVARCHAR(MAX) NOT NULL,
[Picture] VARBINARY(200),
[Height] DECIMAL(3,2),
[Weight] DECIMAL(5,2),
[Gender] CHAR(1) NOT NULL,
[Birthdate] DATETIME2 NOT NULL,
[Biography] NVARCHAR(MAX)
)

INSERT INTO [People] VALUES
('Evelyn', NULL, 1.62, 51, 'f', '1998-01-12', 'online'),
('Leona', NULL, 1.65, 55, 'f', '1990-11-23', ')'),
('Thomas', NULL, 1.73, 68, 'm', '2000-05-07', 'xd'),
('George', NULL, 1.75, 60, 'm', '1996-02-05', 'D'),
('Max', NULL, 1.85, 10, 'm', '1995-06-27', 'offline')
