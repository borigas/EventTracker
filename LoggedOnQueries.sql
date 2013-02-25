/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
FROM [EventTracker].[dbo].[LogOnEvents]

select a.*, b.EventTime AS EndTime, DATEDIFF(second, a.EventTime, b.EventTime) AS SecondsEllapsed
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id

select a.IsLoggedOn, SUM(DATEDIFF(second, a.EventTime, b.EventTime)) AS TotalSeconds
FROM EventTracker.dbo.[LogOnEvents] a 
JOIN EventTracker.dbo.[LogOnEvents] b ON a.Id + 1 = b.Id
GROUP BY a.IsLoggedOn