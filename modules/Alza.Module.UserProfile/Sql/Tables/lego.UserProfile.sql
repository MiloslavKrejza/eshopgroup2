
/* Drop Tables */
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[lego].[UserProfile]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [lego].[UserProfile]
GO

/* Create Tables */
CREATE TABLE [lego].[UserProfile]
(
	[Id] int NOT NULL,
	[Name] nvarchar(200) NOT NULL,
	[BirthDate] datetime NOT NULL,
	[Avatar] nvarchar(50) NOT NULL
)
GO

/* Create Primary Keys, Indexes, Uniques, Checks */
ALTER TABLE [lego].[UserProfile] 
 ADD CONSTRAINT [PK_UserProfile]
	PRIMARY KEY CLUSTERED ([Id])
GO