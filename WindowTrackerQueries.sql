-- Find total time
  select a.ProductName, a.ModuleName, SUM(DATEDIFF(second, a.EventTime, b.EventTime)) / (60.0 * 60.0) AS TotalHours
  FROM EventTracker.dbo.WindowChanges a 
  JOIN EventTracker.dbo.WindowChanges b ON a.Id + 1 = b.Id
  WHERE a.ProductName != 'Logged Off' AND a.ModuleName != ''
  GROUP BY a.ProductName, a.ModuleName
  ORDER BY SUM(DATEDIFF(second, a.EventTime, b.EventTime)) DESC
  
  -- Find all windows
  select a.*, b.EventTime AS EndTime, DATEDIFF(second, a.EventTime, b.EventTime) AS SecondsEllapsed
  FROM EventTracker.dbo.WindowChanges a 
  JOIN EventTracker.dbo.WindowChanges b ON a.Id + 1 = b.Id
  ORDER BY Id DESC

  -- Find errors
  SELECT * FROM EventTracker.dbo.WindowChanges WHERE ErrorFetchingInfo = 1