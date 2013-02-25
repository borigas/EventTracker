/****** Script for SelectTopNRows command from SSMS  ******/
SELECT * FROM [EventTracker].[dbo].[KeyStrokes] ORDER BY Id DESC

SELECT COUNT(*) AS [EventTracker].[dbo].[KeyStrokes] FROM [EventTracker].[dbo].[KeyStrokes]

SELECT [Key], COUNT(*)
  FROM [EventTracker].[dbo].[KeyStrokes]
  GROUP BY [Key] ORDER BY COUNT(*) DESC

