SELECT TOP 5
    e.[EmployeeID] AS 'EmployeeId'
  , e.[JobTitle]
  , e.[AddressID]  AS 'AddressId'
  , a.[AddressText]
FROM [Employees]     AS [e]
    JOIN [Addresses] AS [a]
        ON e.[AddressID] = a.[AddressID]
ORDER BY e.[AddressID]