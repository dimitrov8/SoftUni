-- 01. Employees with Salary Above 35000

-- GO

CREATE PROC [dbo].[usp_GetEmployeesSalaryAbove35000]
AS
    SELECT
        [FirstName],
        [LastName]
    FROM
        [Employees]
    WHERE
        Salary > 35000

-- GO

-- EXEC [dbo].[usp_GetEmployeesSalaryAbove35000]

-- 02. Employees with Salary Above Number

-- GO

CREATE PROC [dbo].[usp_GetEmployeesSalaryAboveNumber] @inputSalary DECIMAL(18, 4)
AS
    SELECT
        [FirstName],
        [LastName]
    FROM
        [Employees]
    WHERE
        SALARY >= @inputSalary

-- GO

-- EXEC [dbo].[usp_GetEmployeesSalaryAboveNumber] 48100

-- 03. Town Names Starting With

-- GO 

CREATE PROC [dbo].[usp_GetTownsStartingWith] @inputSequence VARCHAR(50)
AS
    SELECT
        [Name]
    FROM
        [Towns]
    WHERE
        LOWER([Name]) LIKE LOWER(@inputSequence + '%')

-- GO

-- EXEC [dbo].[usp_GetTownsStartingWith] 'b'

-- 04. Employees from Town 

-- GO

CREATE PROC [dbo].[usp_GetEmployeesFromTown] @inputTown VARCHAR (50)
AS
SELECT  
e.[FirstName],
e.[LastName]
FROM [Employees] AS [e]
JOIN [Addresses] AS [a]
ON e.[AddressID] = a.[AddressID]
JOIN [Towns] AS [t]
ON a.[TownID] = t.[TownID]
WHERE LOWER(t.[Name]) = LOWER(@inputTown)

-- GO

-- EXEC [dbo].usp_GetEmployeesFromTown 'Sofia'

-- 05. Salary Level Function

-- GO

CREATE FUNCTION [dbo].[ufn_GetSalaryLevel] (@salary DECIMAL(18, 4))
RETURNS VARCHAR(7)
AS
    BEGIN
        DECLARE @result VARCHAR(7)

        IF @salary < 30000
            SET @result = 'Low'
        ELSE IF @salary
                BETWEEN 30000 AND 50000
            SET @result = 'Average'
        ELSE IF @salary > 50000
            SET @result = 'High'

        RETURN @result
    END

-- GO

--SELECT 
--[Salary],
--[dbo].[ufn_GetSalaryLevel]([Salary]) AS 'Salary Level'
--FROM [Employees]

-- 06. Employees by Salary Level

-- GO

CREATE PROC usp_EmployeesBySalaryLevel @inputSalaryLevel VARCHAR(7)
AS
    SELECT
        [FirstName],
        [LastName]
    FROM
        [Employees]
    WHERE
        [dbo].[ufn_GetSalaryLevel]([Salary]) = @inputSalaryLevel

-- GO

-- EXEC [dbo].[usp_EmployeesBySalaryLevel] 'High'

-- 07. Define Function

-- GO

CREATE FUNCTION [dbo].[ufn_IsWordComprised]
    (
        @setOfLetters VARCHAR(50),
        @word         VARCHAR(50)
    )
RETURNS BIT
AS
    BEGIN
        DECLARE @currentIndex INT = 1;

        WHILE @currentIndex <= LEN(@word)
            BEGIN
                DECLARE @currentLetter VARCHAR(1) = SUBSTRING(@word, @currentIndex, 1);

                IF (CHARINDEX(@currentLetter, @setOfLetters)) = 0
                    RETURN 0;

                SET @currentIndex += 1;
            END

        RETURN 1;

    END

-- GO

-- 08. *Delete Employees and Departments 

-- GO

CREATE PROC [dbo].[usp_DeleteEmployeesFromDepartment] (@departmentId INT)
AS
    BEGIN
        DELETE FROM [EmployeesProjects] -- 1. DELETE THE PROJECTS OF THE EMPLOYEES
        WHERE
            [EmployeeID] IN (
                                SELECT
                                    [EmployeeID]
                                FROM
                                    [Employees]
                                WHERE
                                    [DepartmentID] = @departmentId
                            )

        UPDATE -- 2. IF ANY EMPLOYEE IS A MANGER TO OTHER EMPLOYEE -> REMOVE HIM AS A MANAGER OF THE OTHER EMPLOYEE (SET THE OTHER EMPLOYEE [ManagerID] TO NULL)
            [Employees]
        SET
            [ManagerID] = NULL
        WHERE
            [ManagerID] IN (
                               SELECT
                                   [EmployeeID]
                               FROM
                                   [Employees]
                               WHERE
                                   [DepartmentID] = @departmentId
                           )

        ALTER TABLE [Departments] -- 3. ALTER THE [Departments] TABLE 
        ALTER COLUMN [ManagerID] INT -- TO ACCEPT NULL AS [ManagerID]

        UPDATE -- 4. IF THE EMPLOYEE IS A MANGER OF A DEPARTMENT -> REMOVE HIM FROM THE MANAGER POSITION (SET THE [ManagerID] Of that Department to NULL)
            [Departments]
        SET
            [ManagerID] = NULL
        WHERE
            [ManagerID] IN (
                               SELECT
                                   [EmployeeID]
                               FROM
                                   [Employees]
                               WHERE
                                   [DepartmentID] = @departmentId
                           )


        DELETE FROM [Employees] -- 5. REMOVE ALL EMPLOYEES IN THE [Employees] TABLE WHICH ARE IN THAT DEPARTMENT
        WHERE
            [DepartmentID] = @departmentId

        DELETE FROM [Departments] -- 6. DELETE THE DEPARTMENT
        WHERE
            [DepartmentID] = @departmentId

        SELECT -- 7. PRINT THE EMPLOYEE IN THE DELETED DEPARTMENT -> SHOULD ALWAYS RETRUN 0
            COUNT([EmployeeID])
        FROM
            [Employees]
        WHERE
            [DepartmentID] = @departmentId
    END

-- GO

-- EXEC [dbo].[usp_DeleteEmployeesFromDepartment] 4

-- 09. Find Full Name 

-- GO

CREATE PROC [dbo].[usp_GetHoldersFullName]
AS
    SELECT
        CONCAT([FirstName], ' ', [LastName]) AS 'Full Name'
    FROM
        [AccountHolders]

-- GO

-- EXEC [dbo].[usp_GetHoldersFullName] 

-- 10. People with Balance Higher Than 

-- GO

CREATE PROC [dbo].[usp_GetHoldersWithBalanceHigherThan] @inputBalance DECIMAL(19, 4)
AS
    WITH HolderBalances
    AS (
           SELECT
               h.[FirstName],
               h.[LastName],
               SUM(a.[Balance]) AS [TotalBalance]
           FROM
               [AccountHolders] AS [h]
               JOIN
                   [Accounts]   AS [a]
                       ON h.[Id] = a.[AccountHolderId]
           GROUP BY
               h.[Id],
               h.[FirstName],
               h.[LastName]
       )
    SELECT
        [FirstName],
        [LastName]
    FROM
        [HolderBalances]
    WHERE
        [TotalBalance] > @inputBalance
    ORDER BY
        [FirstName],
        [LastName]

-- GO

-- EXEC [dbo].[usp_GetHoldersWithBalanceHigherThan] 10000

-- 11. Future Value Function

-- GO

CREATE FUNCTION [dbo].[ufn_CalculateFutureValue]
    (
        @sum                DECIMAL(19, 4),
        @yearlyInterestRate FLOAT,
        @numberOfYears      INT
    )
RETURNS DECIMAL(19, 4)
AS
    BEGIN
        DECLARE @output DECIMAL(19, 4) = @sum * POWER(1 + @yearlyInterestRate, @numberOfYears);

		RETURN @output;
    END

-- GO

--SELECT [dbo].[ufn_CalculateFutureValue](1000, 0.1, 5) AS 'Future Value Function'

-- 12. Calculating Interest

-- GO

CREATE PROC [dbo].[usp_CalculateFutureValueForAccount]
    @accountId      INT,
    @yearlyInterest FLOAT
AS
    BEGIN
        SELECT
            h.[Id]                                                            AS 'Account Id',
            h.[FirstName],
            h.[LastName],
            a.[Balance]                                                       AS 'Current Balance',
            [dbo].[ufn_CalculateFutureValue](a.[Balance], @yearlyInterest, 5) AS 'Balance in 5 years'
        FROM
            [AccountHolders] AS [h]
            JOIN
                [Accounts]   AS [a]
                    ON h.[Id] = a.[AccountHolderId]
        WHERE
            a.[Id] = @accountId
    END
	
-- GO

-- EXEC [dbo].[usp_CalculateFutureValueForAccount] 1, 0.1

-- 13. *Cash in User Games Odd Rows 

-- GO

CREATE OR ALTER FUNCTION [dbo].[ufn_CashInUsersGames] (@gameName NVARCHAR(50))
RETURNS TABLE
AS
RETURN
    (
        SELECT
            SUM([Cash]) AS 'SumCash'
        FROM
            (
                SELECT
                    ug.[Cash],
                    ROW_NUMBER() OVER (ORDER BY
                                           ug.[Cash] DESC
                                      ) AS [RowNumber]
                FROM
                    [UsersGames] [ug]
                   JOIN
                        [Games]  AS [g]
                            ON ug.[GameId] = g.[Id]
                WHERE
                    g.[Name] = @gameName
            ) AS [RowNumberQuery]
        WHERE
            [RowNumber] % 2 <> 0
    )

-- GO

--SELECT *
--FROM [dbo].[ufn_CashInUsersGames]('Love in a mist')