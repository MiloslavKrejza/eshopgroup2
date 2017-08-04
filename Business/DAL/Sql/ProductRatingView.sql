IF EXISTS(SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductRating]')) 
	DROP VIEW [dbo].[ProductRating]
GO
CREATE VIEW [dbo].[ProductRating] AS
	SELECT p.Id AS ProductId, r.Average AS AverageRating
	FROM dbo.Products AS p LEFT JOIN
	(SELECT ProductId AS PId, AVG(CONVERT(DECIMAL, Rating)) AS Average
	FROM dbo.Reviews
	GROUP BY ProductId) AS r 
		ON p.Id = r.PId
