IF EXISTS(SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CategoryRelationships]')) 
	DROP VIEW [dbo].[CategoryRelationships]
GO
CREATE VIEW [dbo].[CategoryRelationships] (Id, ChildId)
		AS WITH ChildCategories (Id, ChildId) AS(
		SELECT  Id, Id FROM dbo.Categories
		UNION ALL
		SELECT c2.Id, c1.Id
		FROM dbo.Categories as c1
		INNER JOIN ChildCategories as c2 ON  c2.ChildId = c1.ParentId)
	SELECT * FROM ChildCategories;
GO
