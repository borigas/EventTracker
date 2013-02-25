SELECT COUNT(*) AS TotalKeys FROM [EventTracker].[dbo].[KeyStrokes]
  
SELECT [Key], COUNT(*) AS KeyCount
FROM [EventTracker].[dbo].[KeyStrokes]
GROUP BY [Key] ORDER BY COUNT(*) DESC

SELECT * FROM [EventTracker].[dbo].[KeyStrokes] ORDER BY Id DESC