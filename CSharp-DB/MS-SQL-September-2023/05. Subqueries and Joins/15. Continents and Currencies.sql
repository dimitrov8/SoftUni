SELECT [ContinentCode]
     , [CurrencyCode]
     , [CurrencyUsage]
FROM
(
    SELECT *
         , DENSE_RANK() OVER (PARTITION BY [ContinentCode] ORDER BY [CurrencyUsage] DESC) AS [CurrencyRank]
    FROM
    (
        SELECT con.[ContinentCode]
             , cou.[CurrencyCode]
             , COUNT(cou.[CurrencyCode]) AS 'CurrencyUsage'
        FROM [Continents]         AS [con]
            LEFT JOIN [Countries] AS [cou]
                ON con.[ContinentCode] = cou.[ContinentCode]
        GROUP BY con.[ContinentCode]
               , cou.[CurrencyCode]
    ) AS [CurrencyUsage]
    WHERE [CurrencyUsage] > 1
) AS [CurrencyRanking]
WHERE [CurrencyRank] = 1
ORDER BY [ContinentCode]
