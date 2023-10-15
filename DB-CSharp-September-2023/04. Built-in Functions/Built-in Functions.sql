-- 01. Find Names of All Employees by First Name 

SELECT [FirstName],  [LastName]
FROM [Employees]
WHERE LEFT ([FirstName], 2) = 'Sa';

-- 02. Find Names of All Employees by Last Name 

SELECT [FirstName],  [LastName]
FROM [Employees]
WHERE [LastName] LIKE '%ei%';

-- 03. Find First Names of All Employees 

SELECT [FirstName]
FROM [Employees]
WHERE [DepartmentID] IN (3, 10)

-- 04. Find All Employees Except Engineers

SELECT [FirstName], [LastName]
FROM [Employees]
WHERE [JobTitle] NOT LIKE '%engineer%';

-- 05. Find Towns with Name Length 

SELECT [Name]
FROM [Towns]
WHERE DATALENGTH([Name]) IN (5,6)
ORDER BY [Name];

-- 06. Find Towns Starting With

SELECT *
FROM [Towns]
WHERE [Name] LIKE '[MKBE]%'
ORDER BY [Name]

-- 07. Find Towns Not Starting With

SELECT *
FROM [Towns]
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name];

-- 08. Create View Employees Hired After 2000 Year

CREATE VIEW [V_EmployeesHiredAfter2000] AS 
SELECT [FirstName], [LastName]
FROM [Employees]
WHERE YEAR(HireDate) > 2000

-- 09. Length of Last Name

SELECT [FirstName], [LastName]
FROM [Employees]
WHERE DATALENGTH([LastName]) IN (5);

-- 10. Rank Employees by Salary

SELECT [EmployeeID], [FirstName], [LastName], [Salary], 
DENSE_RANK() OVER(PARTITION BY [Salary] ORDER BY [EmployeeID])
AS [RANK]
FROM [Employees]
WHERE [Salary] BETWEEN 10000 AND 50000
ORDER BY [Salary] DESC;

-- 11. Find All Employees with Rank 2

WITH RankedEmployees AS (
    SELECT 
        [EmployeeID],
        [FirstName],
        [LastName],
        [Salary],
        DENSE_RANK() OVER(PARTITION BY [Salary] ORDER BY [EmployeeID]) AS [RANK]
    FROM [Employees]
    WHERE [Salary] BETWEEN 10000 AND 50000
)
SELECT *
FROM RankedEmployees
WHERE [RANK] = 2
ORDER BY [Salary] DESC;

-- 12. Countries Holding 'A' 3 or More Times

SELECT 
[CountryName],
[IsoCode]
FROM [Countries]
WHERE LEN([CountryName]) - LEN(REPLACE([CountryName], 'a', '')) >= 3
ORDER BY [IsoCode];

-- 13. Mix of Peak and River Names

SELECT [PeakName]
     , [RiverName]
     , LOWER(([PeakName]) + SUBSTRING([RiverName], 2, LEN([RiverName]))) AS 'Mix'
FROM [Peaks]
    JOIN [Rivers]
        ON RIGHT([PeakName], 1) = LEFT([RiverName], 1)
ORDER BY [Mix]

-- 14. Games From 2011 and 2012 Year

SELECT TOP 50
    [Name]
  , FORMAT([Start], 'yyyy-MM-dd') AS [d]
FROM [Games]
WHERE YEAR([Start])
BETWEEN 2011 AND 2012
ORDER BY [d]
       , [Name]

-- 15. User Email Providers

WITH EmailProviderCTE
AS (SELECT [Username]
         , [Email]
         , SUBSTRING([Email], CHARINDEX('@', [Email]) + 1, LEN([Email])) AS [Email Provider]
    FROM [Users]
   )
SELECT [Username]
     , [Email Provider]
FROM [EmailProviderCTE]
ORDER BY [Email Provider]
       , [Username]

-- 16. Get Users with IP Address Like Pattern

SELECT [Username]
     , [IpAddress] AS 'IP Address'
FROM [Users]
WHERE [IpAddress] LIKE '___.1%.%.___'
ORDER BY [Username]

-- 17. Show All Games with Duration & Part of the Day

WITH [GameInfo]
AS (SELECT [Name] AS [Game],
           DATEPART(HOUR, [Start]) AS [StartHour],
           [Duration]
    FROM [Games]
   )
SELECT [Game],
       CASE
           WHEN [StartHour]
                BETWEEN 0 AND 11 THEN
               'Morning'
           WHEN [StartHour]
                BETWEEN 12 AND 17 THEN
               'Afternoon'
           WHEN [StartHour]
                BETWEEN 18 AND 23 THEN
               'Evening'
       END AS [Part of The Day],
       CASE
           WHEN [Duration]
                BETWEEN 0 AND 3 THEN
               'Extra Short'
           WHEN [Duration]
                BETWEEN 4 AND 6 THEN
               'Short'
           WHEN [Duration] > 6 THEN
               'Long'
           ELSE
               'Extra Long'
       END AS [Duration]
FROM [GameInfo]
ORDER BY [Game],
         [Duration],
         [Part of The Day]

-- 18. Orders Table

SELECT [ProductName],
       [OrderDate],
       DATEADD(DAY, 3, [OrderDate]) AS [Pay Due],
       DATEADD(MONTH, 1, [OrderDate]) AS [Deliver Due]
FROM [Orders]