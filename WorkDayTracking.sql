
-- Find 1st log in of the day
;WITH startOfDay AS (
    SELECT *, 
           ROW_NUMBER() OVER(PARTITION BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime))
                                 ORDER BY Id) AS rk
      FROM [EventTracker].[dbo].[LogOnEvents]
	  WHERE IsLoggedOn = 1)
SELECT EventTime AS StartOfDay INTO #StartOfDays
FROM startOfDay
WHERE rk = 1
ORDER BY EventTime DESC

-- Find last log out of the day
;WITH endOfDay AS (
    SELECT *, 
           ROW_NUMBER() OVER(PARTITION BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime))
                                 ORDER BY Id DESC) AS rk
      FROM [EventTracker].[dbo].[LogOnEvents]
	  WHERE IsLoggedOn = 0)
SELECT EventTime AS EndOfDay INTO #EndOfDays
FROM endOfDay
WHERE rk = 1
ORDER BY EventTime DESC

SELECT AVG(DATEDIFF(MINUTE, s.StartOfDay, e.EndOfDay) / 60.0) AS AverageHours FROM #StartOfDays s
JOIN #EndOfDays e ON DATEADD(dd, 0, DATEDIFF(dd, 0, e.EndOfDay)) = DATEADD(dd, 0, DATEDIFF(dd, 0, s.StartOfDay))

SELECT *, DATEDIFF(MINUTE, s.StartOfDay, e.EndOfDay) / 60.0 AS HoursAtWork FROM #StartOfDays s
JOIN #EndOfDays e ON DATEADD(dd, 0, DATEDIFF(dd, 0, e.EndOfDay)) = DATEADD(dd, 0, DATEDIFF(dd, 0, s.StartOfDay))

DROP TABLE #StartOfDays
DROP TABLE #EndOfDays