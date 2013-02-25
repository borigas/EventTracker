/****** Script for SelectTopNRows command from SSMS  ******/
SELECT COUNT(*) AS KeyStrokes FROM EventTracker.dbo.KeyStrokes

SELECT [Key], COUNT(*)
  FROM [EventTracker].[dbo].[KeyStrokes]
  GROUP BY [Key] ORDER BY COUNT(*) DESC

