
-- Find 1st log in of the day
;WITH startOfDay AS (
    SELECT *, 
           ROW_NUMBER() OVER(PARTITION BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime))
                                 ORDER BY Id) AS rk
      FROM [EventTracker].[dbo].[LogOnEvents]
	  WHERE IsLoggedOn = 1)
SELECT EventTime AS StartOfDay
FROM startOfDay
WHERE rk = 1

-- Find last log out of the day
;WITH endOfDay AS (
    SELECT *, 
           ROW_NUMBER() OVER(PARTITION BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime))
                                 ORDER BY Id DESC) AS rk
      FROM [EventTracker].[dbo].[LogOnEvents]
	  WHERE IsLoggedOn = 0)
SELECT EventTime AS EndOfDay
FROM endOfDay
WHERE rk = 1