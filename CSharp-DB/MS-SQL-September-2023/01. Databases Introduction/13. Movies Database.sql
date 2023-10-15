CREATE TABLE Directors (
Id INT PRIMARY KEY NOT NULL, 
DirectorName VARCHAR(50) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Directors (Id , DirectorName, Notes)
VALUES 
(1, 'Director 1', NULL),
(2, 'Director 2', NULL),
(3, 'Director 3', NULL),
(4, 'Director 4', NULL),
(5, 'Director 5', 'Test');

CREATE TABLE Genres (
Id INT PRIMARY KEY NOT NULL,
GenreName VARCHAR(50),
Notes VARCHAR(MAX)
);

INSERT INTO Genres (Id , GenreName, Notes)
VALUES 
(1, 'Genre 1', 'Test'),
(2, 'Genre 2', NULL),
(3, 'Genre 3', NULL),
(4, 'Genre 4', NULL),
(5, 'Genre 5', NULL);

CREATE TABLE Categories (
Id INT PRIMARY KEY NOT NULL,
CatergoryName VARCHAR(50) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Categories (Id , CatergoryName, Notes)
VALUES 
(1, 'Catergory 1', NULL),
(2, 'Catergory 2', NULL),
(3, 'Catergory 3', NULL),
(4, 'Catergory 4', 'Test'),
(5, 'Catergory 5', NULL);

CREATE TABLE Movies (
Id INT PRIMARY KEY,
Title VARCHAR(50) NOT NULL,
DirectorId INT REFERENCES Directors(Id),
CopyrightYear INT NOT NULL,
Length TIME NOT NULL,
GenreId INT REFERENCES Genres(Id),
CategoryId INT REFERENCES Categories(Id),
Rating DECIMAL (3,1),
Notes VARCHAR(MAX)
);

INSERT INTO Movies (Id , Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating, Notes)
VALUES 
(1, 'Title 1', 5, 1990, '02:30:00', 4, 1, 5.2, NULL),
(2, 'Title 2', 4, 2000, '01:10:01', 5, 2, 6.0, 'Test'),
(3, 'Title 3', 3, 2010, '03:05:09', 3, 4, 8.1, 'Test'),
(4, 'Title 4', 2, 2023, '02:44:05', 2, 3, 9.1, NULL),
(5, 'Title 5', 1, 2023, '02:44:05', 1, 5, NULL, NULL);