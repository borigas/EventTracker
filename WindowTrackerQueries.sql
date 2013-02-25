/****** Script for SelectTopNRows command from SSMS  ******/
SELECT *
  FROM [EventTracker].[dbo].[WindowChanges]

  select a.*, b.EventTime AS EndTime, DATEDIFF(second, a.EventTime, b.EventTime) AS SecondsEllapsed
  FROM EventTracker.dbo.WindowChanges a 
  JOIN EventTracker.dbo.WindowChanges b ON a.Id + 1 = b.Id

  select a.ProductName, SUM(DATEDIFF(second, a.EventTime, b.EventTime)) AS TotalSeconds
  FROM EventTracker.dbo.WindowChanges a 
  JOIN EventTracker.dbo.WindowChanges b ON a.Id + 1 = b.Id
  GROUP BY a.ProductName