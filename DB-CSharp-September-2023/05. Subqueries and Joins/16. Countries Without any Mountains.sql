SELECT COUNT(c.[CountryCode]) AS 'Count'
FROM [Countries]                   AS [c]
    LEFT JOIN [MountainsCountries] AS [mc]
        ON mc.[CountryCode] = c.[CountryCode]
    LEFT JOIN [Mountains]          AS [m]
        ON mc.[MountainId] = m.[Id]
WHERE m.[Id] IS NULL