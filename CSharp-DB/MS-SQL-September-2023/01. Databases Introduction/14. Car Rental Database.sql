CREATE TABLE Categories (
Id INT PRIMARY KEY,
CategoryName VARCHAR(50) NOT NULL,
DailyRate DECIMAL (6, 2),
WeeklyRate DECIMAL (6, 2),
MonthlyRate DECIMAL (6,2),
WeekendRate DECIMAL (6,2)
);

INSERT INTO Categories (Id, CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES
(1, 'SUV', NULL, NULL, NULL, NULL),
(2, 'Sedan', NULL, NULL, NULL, NULL),
(3, 'Sports Car', NULL, NULL, NULL, NULL);

CREATE TABLE Cars  (
Id INT PRIMARY KEY,
PlateNumber VARCHAR(10) NOT NULL,
Manufacturer VARCHAR(12) NOT NULL,
Model VARCHAR(10) NOT NULL,
CategoryId INT REFERENCES Categories(Id),
Doors BIT NOT NULL,
Picture VARBINARY(MAX),
Condition VARCHAR(10) NOT NULL,
Available VARCHAR(3) NOT NULL
);

INSERT INTO Cars (Id, PlateNumber, Manufacturer, Model, CategoryId, Doors, Picture, Condition, Available)
VALUES 
(1, 'CB 1234 AB', 'Audi', 'S8', 2, 4, NULL, 'NEW', 'Yes'),
(2, 'CB 4321 BC', 'Range Rover', 'Sport', 1, 5, NULL, 'Good', 'No'),
(3, 'CB 0000 TE', 'McLaren', 'Artura', 3, 3, NULL, 'NEW', 'No');

CREATE TABLE Employees (
Id INT PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Title VARCHAR(30) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Employees (Id, FirstName, LastName, Title, Notes)
VALUES
(1, 'Olivia', 'Johnson', 'Manager', NULL),
(2, 'Ava', 'Wilson', 'Rental Agent', NULL),
(3, 'George', 'Smith', 'Dispatcher', NULL)

CREATE TABLE Customers (
Id INT PRIMARY KEY,
DriverLicenceNumber BIGINT NOT NULL,
FullName VARCHAR(50) NOT NULL,
Address VARCHAR(50) NOT NULL,
City VARCHAR(50) NOT NULL,
ZIPCode INT NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Customers (Id, DriverLicenceNumber, FullName, Address, City, ZIPCode, Notes)
VALUES
(1, 7654321987, 'Jayce Taylor', 'Test Address 1', 'New York', 10001, NULL),
(2, 5432109876, 'Will Martinez', 'Test Address 2', 'New York', 1004, NULL),
(3, 9876543210, 'Clark Campbell', 'Test Adress 3', 'Los Angeles', 90001, NULL);

CREATE TABLE RentalOrders (
Id INT PRIMARY KEY,
EmployeeId INT REFERENCES Employees(Id) NOT NULL,
CustomerId INT REFERENCES Customers(Id) NOT NULL,
CarId INT REFERENCES Cars(Id) NOT NULL,
TankLevel INT NOT NULL,
KilometrageStart INT NOT NULL,
KilometrageEnd INT NOT NULL, 
TotalKilometrage INT,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
TotalDays INT,
RateApplied DECIMAL (6,2),
TaxRate DECIMAL (6,2),
OrderStatus VARCHAR(15),
Notes VARCHAR(50)
);

INSERT INTO RentalOrders (Id, EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES
(1, 3, 3, 3, 100, 300, 500, NULL, '2023-01-01', '2023-05-01', NULL, NULL, NULL, NULL, NULL),
(2, 2, 2, 2, 80, 625, 900, NULL, '2023-07-02', '2023-08-10', NULL, NULL, NULL, NULL, NULL),
(3, 1, 1, 1, 92, 400, 1250, NULL, '2023-07-03', '2023-07-02', NULL, NULL, NULL, NULL, NULL);