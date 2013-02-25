SELECT COUNT(*) AS TotalKeys FROM [EventTracker].[dbo].[KeyStrokes]
  
SELECT [Key], COUNT(*) AS KeyCount
FROM [EventTracker].[dbo].[KeyStrokes]
GROUP BY [Key] ORDER BY COUNT(*) DESC

SELECT * FROM [EventTracker].[dbo].[KeyStrokes] ORDER BY Id DESC

select a.[Key] AS [1st], b.[Key] AS [2nd], Count(*) AS Occurances
FROM EventTracker.dbo.[KeyStrokes] a 
JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id
WHERE LEN(a.[Key]) = 1 AND LEN(b.[Key]) = 1
GROUP BY a.[Key], b.[Key]
ORDER BY COUNT(*) DESC