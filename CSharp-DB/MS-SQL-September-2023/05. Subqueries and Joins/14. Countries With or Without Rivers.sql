SELECT TOP 5
cou.[CountryName],
r.[RiverName]
FROM [Countries] AS [cou]
LEFT JOIN [Continents] AS [con]
ON cou.[ContinentCode] = con.[ContinentCode]
LEFT JOIN [CountriesRivers] AS [cr]
ON cou.[CountryCode] = cr.[CountryCode]
LEFT JOIN [Rivers] AS [r]
ON cr.[RiverId] = r.[Id] 
WHERE con.[ContinentName] = 'Africa'
ORDER BY cou.[CountryName] ASC