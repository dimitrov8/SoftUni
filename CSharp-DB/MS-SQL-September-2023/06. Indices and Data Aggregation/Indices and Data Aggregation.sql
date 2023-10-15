-- 01. Records’ Count 

SELECT
    COUNT(*) AS [Count]
FROM
    [WizzardDeposits]

-- 02. Longest Magic Wand 

SELECT TOP (1)
    [MagicWandSize] AS 'LongestMagicWand'
FROM
    [WizzardDeposits]
ORDER BY
    [MagicWandSize] DESC

-- 03. Longest Magic Wand per Deposit Groups

SELECT
    wd.[DepositGroup],
    MAX(wd.[MagicWandSize]) AS 'LongestMagicWand'
FROM
    [WizzardDeposits] AS [wd]
GROUP BY
    wd.[DepositGroup]

-- 04. Smallest Deposit Group per Magic Wand Size 

SELECT TOP (2)
    wd.[DepositGroup]
FROM
    [WizzardDeposits] AS [wd]
GROUP BY
    [DepositGroup]
ORDER BY
    AVG(wd.[MagicWandSize])

-- 05. Deposits Sum

SELECT
    wd.[DepositGroup],
    SUM(wd.[DepositAmount]) AS 'TotalSum'
FROM
    [WizzardDeposits] AS [wd]
GROUP BY
    wd.[DepositGroup]

-- 06. Deposits Sum for Ollivander Family

SELECT
    wd.[DepositGroup],
    SUM(wd.[DepositAmount]) AS 'TotalSum'
FROM
    [WizzardDeposits] AS [wd]
WHERE
    wd.[MagicWandCreator] = 'Ollivander family'
GROUP BY
    wd.[DepositGroup],
    wd.[MagicWandCreator]

-- 07. Deposits Filter

SELECT
    wd.[DepositGroup],
    SUM(wd.[DepositAmount]) AS 'TotalSum'
FROM
    [WizzardDeposits] AS [wd]
WHERE
    wd.[MagicWandCreator] = 'Ollivander family'
GROUP BY
    wd.[DepositGroup],
    wd.[MagicWandCreator]
HAVING
    SUM(wd.[DepositAmount]) < 150000
ORDER BY
    [TotalSum] DESC

-- 08. Deposit Charge 

SELECT
    wd.[DepositGroup],
    wd.[MagicWandCreator],
    MIN(wd.[DepositCharge]) AS 'MinDepositCharge'
FROM
    [WizzardDeposits] AS [wd]
GROUP BY
    wd.[DepositGroup],
    wd.[MagicWandCreator]
ORDER BY
    wd.[MagicWandCreator],
    wd.[DepositGroup]

-- 09. Age Groups 

SELECT
    [AgeGroup],
    COUNT(*) AS 'WizzardCount'
FROM
    (
        SELECT
            wd.[Age],
            CASE
                WHEN wd.[Age]
                     BETWEEN 0 AND 10
                    THEN '[0-10]'
                WHEN wd.[Age]
                     BETWEEN 11 AND 20
                    THEN '[11-20]'
                WHEN wd.[Age]
                     BETWEEN 21 AND 30
                    THEN '[21-30]'
                WHEN wd.[Age]
                     BETWEEN 31 AND 40
                    THEN '[31-40]'
                WHEN wd.[Age]
                     BETWEEN 41 AND 50
                    THEN '[41-50]'
                WHEN wd.[Age]
                     BETWEEN 51 AND 60
                    THEN '[51-60]'
                WHEN wd.[Age] >= 61
                    THEN '[61+]'
            END AS [AgeGroup]
        FROM
            [WizzardDeposits] AS [wd]
    ) AS [AgeGroups]
GROUP BY
    [AgeGroup]

-- 10. First Letter 

SELECT
    SUBSTRING(wd.[FirstName], 1, 1) AS [FirstLetter]
FROM
    [WizzardDeposits] AS [wd]
WHERE
    wd.[DepositGroup] = 'Troll Chest'
GROUP BY
    SUBSTRING(wd.[FirstName], 1, 1)
ORDER BY
    [FirstLetter]

-- 11. Average Interest 

SELECT
    wd.[DepositGroup],
    wd.[IsDepositExpired],
    AVG(wd.[DepositInterest]) AS 'AverageInterest'
FROM
    [WizzardDeposits] AS [wd]
WHERE
    wd.[DepositStartDate] > '1985-01-01'
GROUP BY
    wd.[DepositGroup],
    wd.[IsDepositExpired]
ORDER BY
    wd.[DepositGroup] DESC,
    wd.[IsDepositExpired]

-- 12. *Rich Wizard, Poor Wizard

SELECT
    SUM([Host Wizard Deposit] - [Guest Wizard Deposit]) AS [SumDifference]
FROM
    (
        SELECT
            [FirstName]                  AS [Host Wizard],
            [DepositAmount]              AS [Host Wizard Deposit],
            LEAD([FirstName]) OVER (ORDER BY
                                        [Id]
                                   )     AS [Guest Wizard],
            LEAD([DepositAmount]) OVER (ORDER BY
                                            [Id]
                                       ) AS [Guest Wizard Deposit]
        FROM
            [WizzardDeposits]
    ) AS [HostGuestQuery]

-- 13. Departments Total Salaries

SELECT
    [DepartmentID],
    SUM([Salary])
FROM
    [Employees]
GROUP BY
    [DepartmentID]
ORDER BY
    [DepartmentID]

-- 14. Employees Minimum Salaries 

SELECT
    [DepartmentID],
    MIN([Salary]) AS [MinimumSalary]
FROM
    [Employees]
WHERE
    [DepartmentID] IN (
                          2, 5, 7
                      )
    AND [HireDate] > '2000-01-01'
GROUP BY
    [DepartmentID]

-- 15. Employees Average Salaries 

SELECT
    *
INTO
    [EmployeesAS]
FROM
    [Employees]
WHERE
    [Salary] > 30000

DELETE FROM [EmployeesAS]
WHERE
    [ManagerID] = 42

UPDATE
    [EmployeesAS]
SET
    [Salary] += 5000
WHERE
    [DepartmentID] = 1

SELECT
    [DepartmentID],
    AVG([Salary])
FROM
    [EmployeesAS]
GROUP BY
    [DepartmentID]

-- 16. Employees Maximum Salaries 

SELECT
    [DepartmentID],
    MAX([Salary]) AS [MaxSalary]
FROM
    [Employees]
GROUP BY
    [DepartmentID]
HAVING
    NOT MAX([Salary])
    BETWEEN 30000 AND 70000

-- 17. Employees Count Salaries 

SELECT
    COUNT(*) AS [Count]
FROM
    [Employees]
WHERE
    [ManagerId] IS NULL

--  18. *3rd Highest Salary 

SELECT
    [DepartmentID],
    [Salary]
FROM
    (
        SELECT
            [DepartmentID],
            [Salary],
            DENSE_RANK() OVER (PARTITION BY
                                   [DepartmentID]
                               ORDER BY
                                   [Salary] DESC
                              ) AS [Rank]
        FROM
            [Employees]
    ) AS [SalariesByDepartments]
WHERE
    [SalariesByDepartments].[Rank] = 3
GROUP BY
    [DepartmentID],
    [Salary]

-- 19. **Salary Challenge

SELECT TOP (10)
    e.[FirstName],
    e.[LastName],
    e.[DepartmentID]
FROM
    [Employees] AS [e]
WHERE
    e.[Salary] >
    (
        SELECT
            AVG([Salary]) AS [AvgDepartmentSalary]
        FROM
            [Employees] AS [e2]
        WHERE
            e.[DepartmentID] = e2.[DepartmentID]
    )