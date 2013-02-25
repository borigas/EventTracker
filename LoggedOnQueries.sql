select a.IsLoggedOn, SUM(DATEDIFF(second, a.EventTime, b.EventTime)) / (60.0*60.0) AS TotalHours
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
GROUP BY a.IsLoggedOn
ORDER BY a.IsLoggedOn DESC

select a.*, b.EventTime AS EndTime, DATEDIFF(second, a.EventTime, b.EventTime) / (60.0*60.0) AS HoursEllapsed
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
ORDER BY Id DESC

SELECT *
FROM [EventTracker].[dbo].[LogOnEvents] ORDER BY Id DESC