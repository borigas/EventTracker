-- Total keys
SELECT COUNT(*) AS TotalKeys FROM [EventTracker].[dbo].[KeyStrokes]

-- Total keys per day
SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime)) AS [Day], COUNT(*) AS TotalKeys
FROM [EventTracker].[dbo].[KeyStrokes]
GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime))
ORDER BY DATEADD(dd, 0, DATEDIFF(dd, 0, EventTime)) DESC
 
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
		JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id WHERE (LEN(a.[Key]) = 1 OR LEN(b.[Key]) = 1) AND DATEDIFF(second, a.EventTime, b.EventTime) < 1) AS Percentage
FROM EventTracker.dbo.[KeyStrokes] a 
JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id
WHERE (LEN(a.[Key]) = 1 OR LEN(b.[Key]) = 1) AND DATEDIFF(second, a.EventTime, b.EventTime) < 1
GROUP BY a.[Key], b.[Key]
ORDER BY COUNT(*) DESC

-- Time between key strokes that were keyed in rapid succession
select a.[Key] AS [1st], b.[Key] AS [2nd], Count(*) AS Occurances, AVG(DATEDIFF(millisecond, a.EventTime, b.EventTime)) AS [AverageTime (ms)]
FROM EventTracker.dbo.[KeyStrokes] a 
JOIN EventTracker.dbo.[KeyStrokes] b ON a.Id + 1 = b.Id
WHERE (LEN(a.[Key]) = 1 OR LEN(b.[Key]) = 1) AND DATEDIFF(millisecond, a.EventTime, b.EventTime) BETWEEN 15 AND 250
GROUP BY a.[Key], b.[Key]
HAVING COUNT(*) > 100
ORDER BY AVG(DATEDIFF(millisecond, a.EventTime, b.EventTime))