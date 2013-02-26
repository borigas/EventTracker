-- Total time queries
select a.IsLoggedOn, SUM(DATEDIFF(second, a.EventTime, b.EventTime)) / (60.0*60.0) AS TotalHours
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
GROUP BY a.IsLoggedOn
ORDER BY a.IsLoggedOn DESC

-- Daily time queries
select a.IsLoggedOn, CAST(a.EventTime AS DATE) AS [Date], SUM(DATEDIFF(second, a.EventTime, b.EventTime)) / (60.0*60.0) AS TotalHours
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
WHERE a.IsLoggedOn = 1
GROUP BY CAST(a.EventTime AS DATE), a.IsLoggedOn
ORDER BY CAST(a.EventTime AS DATE) DESC

-- Hourly time queries
-- This query is not complete
-- See for help: http://stackoverflow.com/questions/13246987/tsql-query-to-sum-time-periods-per-range
--select a.IsLoggedOn, DATEADD(hour, DATEPART(hour, a.EventTime), DATEADD(dd, 0, DATEDIFF(dd, 0, a.EventTime))) AS [Date], 
--	CASE WHEN DATEPART(hour, a.EventTime) = DATEPART(hour, b.EventTime) THEN
--		SUM(DATEDIFF(second, a.EventTime, b.EventTime)) / (60.0) AS TotalMinutes
--		ELSE 
--FROM EventTracker.dbo.[LogOnEvents] a 
--JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
----WHERE a.IsLoggedOn = 1
--GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, a.EventTime)), DATEPART(hour, a.EventTime),
--	DATEADD(dd, 0, DATEDIFF(dd, 0, a.EventTime)), DATEPART(hour, b.EventTime), 
--	DATEPART(hour, a.EventTime), a.IsLoggedOn
--ORDER BY DATEADD(dd, 0, DATEDIFF(dd, 0, a.EventTime)), DATEPART(hour, a.EventTime) DESC

-- Recent times
select a.*, b.EventTime AS EndTime, DATEDIFF(second, a.EventTime, b.EventTime) / (60.0*60.0) AS HoursEllapsed
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
ORDER BY Id DESC