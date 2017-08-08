SELECT 
    c.Id,c.ParentId, cc.*
  FROM 
    dbo.Categories c
	left join Categories cc on cc.Id = c.ParentId
  WHERE 
    c.ParentId is NULL


UNION ALL



SELECT  Id as Id, Id as ParentId FROM dbo.Categories c
                                                                WHERE c.ParentId is null
                                                                UNION ALL
                                                                SELECT  c1.Id, c2.Id  
                                                                FROM dbo.Categories as c1
                                                                INNER JOIN dbo.Categories as c2 ON  c2.Id = c1.ParentId
WHERE c1.ParentId is not null




SELECT  Id as Id, Id as ChildId FROM dbo.Categories c
                                                                WHERE c.ParentId is null
                                                                UNION ALL
                                                                SELECT c2.Id, c1.Id
                                                                FROM dbo.Categories as c1
                                                                INNER JOIN dbo.Categories as c2 ON  c2.Id = c1.ParentId
WHERE c1.ParentId is not null




SELECT c2.Id as Id, c1.Id as ChildId
                                                                    FROM dbo.Categories as c1
                                                                    INNER JOIN dbo.Categories as c2 ON  c2.Id = c1.ParentId
    WHERE c1.ParentId is not null
