-- Total keys
SELECT COUNT(*) AS TotalKeys FROM [EventTracker].[dbo].[KeyStrokes]
 
-- Most common keys including special keys
SELECT [Key], COUNT(*) AS KeyCount
FROM [EventTracker].[dbo].[KeyStrokes]
GROUP BY [Key] ORDER BY COUNT(*) DESC

-- Most common character sequences
select a.[Key] AS [1st], b.[Key] AS [2nd], Count(*) AS Occurances, 
	Count(*) / (SELECT CAST(COUNT(*) AS decimal) FROM EventTracker.dbo.[KeyStrokes] a 
		JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id WHERE LEN(a.[Key]) = 1 AND LEN(b.[Key]) = 1) AS Percentage
FROM EventTracker.dbo.[KeyStrokes] a 
JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id
WHERE LEN(a.[Key]) = 1 AND LEN(b.[Key]) = 1
GROUP BY a.[Key], b.[Key]
ORDER BY COUNT(*) DESC

-- Most common characters
select a.[Key] AS [Key], Count(*) AS Occurances, 
	Count(*) / (SELECT CAST(COUNT(*) AS decimal) FROM EventTracker.dbo.[KeyStrokes] a WHERE LEN(a.[Key]) = 1) AS Percentage
FROM EventTracker.dbo.[KeyStrokes] a 
WHERE LEN(a.[Key]) = 1
GROUP BY a.[Key]
ORDER BY COUNT(*) DESC

-- Most common character with up to 1 special key
select a.[Key] AS [1st], b.[Key] AS [2nd], Count(*) AS Occurances, 
	Count(*) / (SELECT CAST(COUNT(*) AS decimal) FROM EventTracker.dbo.[KeyStrokes] a 
		JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id WHERE LEN(a.[Key]) = 1 OR LEN(b.[Key]) = 1) AS Percentage
FROM EventTracker.dbo.[KeyStrokes] a 
JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id
WHERE LEN(a.[Key]) = 1 OR LEN(b.[Key]) = 1
GROUP BY a.[Key], b.[Key]
ORDER BY COUNT(*) DESC