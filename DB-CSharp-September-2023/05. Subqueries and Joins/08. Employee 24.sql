SELECT e.[EmployeeID]
     , e.[FirstName]
     , CASE
           WHEN DATEPART(YEAR, p.[StartDate]) > '2004' THEN
               NULL
           ELSE
               p.[Name]
       END AS 'ProjectName'
FROM [Employees]             AS [e]
    JOIN [EmployeesProjects] AS [ep]
        ON e.[EmployeeID] = ep.[EmployeeID]
    JOIN [Projects]          AS [p]
        ON ep.[ProjectID] = p.[ProjectID]
           AND e.[EmployeeID] = 24
