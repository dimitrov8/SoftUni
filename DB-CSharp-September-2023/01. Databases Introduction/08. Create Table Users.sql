CREATE TABLE [Users](
[Id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
[Username] VARCHAR(30) NOT NULL,
[Password] VARCHAR(26) NOT NULL,
[ProfilePicture] VARBINARY(MAX),
[LastLoginTime] TIME,
[IsDeleted] BIT
)

INSERT INTO [Users]
VALUES
('eve1', 'randompassword', NULL, NULL, 0),
('leona0', 'rndpass', NULL, NULL, 0),
('thomaz', 'dbpasstest', NULL, NULL, 0),
('grg', 'grgspass', NULL, NULL, 1),
('maxi', 'maxspassword', NULL, NULL, 0)