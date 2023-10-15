SELECT [m].MountainRange, [p].PeakName, [p].Elevation
FROM [Mountains] AS [m]
LEFT JOIN [Peaks] AS [p]
ON [p].[MountainId] = [m].[id]
WHERE [MountainRange] = 'Rila'
ORDER BY [Elevation] DESC;