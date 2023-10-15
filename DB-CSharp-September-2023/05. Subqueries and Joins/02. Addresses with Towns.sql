SELECT TOP 50
    e.[FirstName]
  , e.[LastName]
  , t.[Name]        AS 'Town'
  , a.[AddressText] AS 'AddressText'
FROM [Employees]     AS [e]
    JOIN [Addresses] AS [a]
        ON e.[AddressID] = a.[AddressID]
    JOIN [Towns]     as [t]
        ON a.[TownID] = t.[TownID]
ORDER BY e.[FirstName]
       , e.[LastName]