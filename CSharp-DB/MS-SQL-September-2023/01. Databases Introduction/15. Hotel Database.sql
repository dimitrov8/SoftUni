CREATE TABLE Employees (
Id SMALLINT PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Title VARCHAR(20) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Employees (Id, FirstName, LastName, Title, Notes)
VALUES
(1, 'Janice', 'House', 'Receptionist', NULL),
(2, 'Chris', 'Thomas', 'Manager', NULL),
(3, 'Mark', 'Reese', 'Supervisor', NULL);

CREATE TABLE Customers (
AccountNumber SMALLINT PRIMARY KEY,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
PhoneNumber VARCHAR(17) NOT NULL,
EmergencyName VARCHAR(50) NOT NULL,
EmergencyNumber VARCHAR(17) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Customers (AccountNumber, FirstName, LastName, 
PhoneNumber, EmergencyName, EmergencyNumber, Notes)
VALUES
(1, 'Erica' , 'Harford', '+1 (000) 000-0000', 'Jessie German', '+1 (111) 111-1111', NULL), 
(2, 'Lindsay' , 'Duvall', '+1 (222) 222-2222', ' Rita Bloom', '+1 (333) 333-3333', NULL),
(3, 'Bruce' , 'Hofmann', '+1 (444) 444-4444', 'Evan Huff', '+1 (555) 555-5555', NULL);

CREATE TABLE RoomStatus (
RoomStatus VARCHAR(20) PRIMARY KEY NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO RoomStatus (RoomStatus, Notes)
VALUES
('Vacant', NULL),
('Occupied', NULL),
('Maintenance', NULL);

CREATE TABLE RoomTypes (
RoomType VARCHAR(20) PRIMARY KEY NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO RoomTypes (RoomType, Notes)
VALUES
('Single Room', NULL),
('Double Room', NULL),
('Presidential Suite', NULL);

CREATE TABLE BedTypes (
BedType VARCHAR(20) PRIMARY KEY NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO BedTypes (BedType, Notes)
VALUES
('Single Bed', NULL),
('Double Bed', NULL),
('King-Sized Bed', NULL);

CREATE TABLE Rooms (
RoomNumber SMALLINT PRIMARY KEY,
RoomType VARCHAR(20) REFERENCES RoomTypes(RoomType) NOT NULL,
BedType VARCHAR(20) REFERENCES BedTypes(BedType) NOT NULL,
Rate DECIMAL(5,2) NOT NULL,
RoomStatus VARCHAR(20) REFERENCES RoomStatus(RoomStatus) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus, Notes)
VALUES
(1, 'Single Room', 'Single Bed', 80.00, 'Occupied', NULL),
(2, 'Double Room', 'Double Bed', 160.00, 'Maintenance', NULL),
(3, 'Presidential Suite', 'King-Sized Bed', 800.00, 'Vacant', NULL)

CREATE TABLE Payments (
Id INT PRIMARY KEY,
EmployeeId SMALLINT REFERENCES Employees(Id) NOT NULL,
PaymentDate DATE NOT NULL,
AccountNumber SMALLINT REFERENCES Customers(AccountNumber) NOT NULL,
FirstDateOccupied DATE NOT NULL,
LastDateOccupied DATE NOT NULL,
TotalDays SMALLINT NOT NULL,
AmountCharged DECIMAL(6, 2) NOT NULL,
TaxRate DECIMAL (5, 2) NOT NULL,
TaxAmount DECIMAL(5, 2) NOT NULL,
PaymentTotal DECIMAL(6, 2) NOT NULL,
Notes VARCHAR(MAX)
);

INSERT INTO Payments (Id, EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, 
AmountCharged, TaxRate, TaxAmount, PaymentTotal, Notes)
VALUES
(1, 1, '2023-09-12', 1, '2023-09-12', '2023-09-15', 4, 240, 5, 20, 260, NULL),
(2, 2, '2023-10-10', 2, '2023-10-10', '2023-10-11', 1, 80, 2, 2, 82, NULL),
(3, 3, '2023-12-05', 3, '2023-12-05', '2023-12-10', 5, 4500, 12, 60, 4560, NULL)

CREATE TABLE Occupancies (
Id SMALLINT PRIMARY KEY,
EmployeeId SMALLINT REFERENCES Employees(Id) NOT NULL,
DateOccupied DATE NOT NULL,
AccountNumber SMALLINT REFERENCES Customers(AccountNumber) NOT NULL,
RoomNumber SMALLINT REFERENCES Rooms(RoomNumber) NOT NULL,
RateApplied DECIMAL(5,2),
PhoneCharge DECIMAL(5,2),
Notes VARCHAR(MAX)
);

INSERT INTO Occupancies (Id, EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge,Notes)
VALUES
(1, 1, '2023-09-12', 1, 1, NULL, NULL, NULL),
(2, 2, '2023-10-10', 2, 2, NULL, NULL, NULL),
(3, 3, '2023-12-05', 3, 3, NULL, NULL, NULL);
