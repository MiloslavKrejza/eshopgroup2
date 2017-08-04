IF EXISTS(SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProductRating]')) 
	DROP VIEW [dbo].[ProductRating]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Products]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Products]
GO




IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[AuthorsBooks]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[AuthorsBooks]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Books]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Books]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Authors]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Authors]
GO

IF EXISTS(SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CategoryRelationships]')) 
	DROP VIEW [dbo].[CategoryRelationships]
GO


IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Reviews]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Reviews]
GO


IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Publishers]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Publishers]
GO


IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[ProductStates]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[ProductStates]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Languages]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Languages]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Formats]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Formats]
GO


IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Categories]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Categories]
GO


IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[UserProfiles]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[UserProfiles]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Countries]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Countries]
GO

IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[ProfileStates]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[ProfileStates]
GO





IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[AspNetUserRoles]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[AspNetUserRoles]
GO


CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
)
GO

DELETE FROM [dbo].[AspNetUsers]
GO


CREATE TABLE [dbo].[Countries](
	[Id][int] Identity(1,1) NOT NULL,
	[CountryName][nvarchar](max) NOT NULL,
	[CountryCode][nvarchar](2) NOT NULL
)
GO

ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Country]
	PRIMARY KEY CLUSTERED([Id])
GO

CREATE INDEX CountryCode_index on [dbo].[Countries] (CountryCode);
GO
CREATE INDEX CountryId_index on [dbo].[Countries] (Id);
GO



CREATE TABLE [dbo].[ProfileStates](
	[Id][int] Identity(1,1) NOT NULL,
	[StateName][nvarchar](max) NOT NULL
	
)
ALTER TABLE [dbo].[ProfileStates]
ADD CONSTRAINT [PK_ProfileStates]
	PRIMARY KEY Clustered([Id])
GO





CREATE TABLE [dbo].[UserProfiles](
	[Id][int] NOT NULL,
	[Name][nvarchar](50) NOT NULL,
	[Surname][nvarchar] (50)NOT NULL,
	[ProfileStateId][int] NOT NULL,
	[PhoneNumber][nvarchar] (max)NULL,
	[CountryId][int] NULL,
	[City][nvarchar](max) NULL,
	[Postalcode][nvarchar](max)NULL,
	[Address][nvarchar](max) NULL,
	[ProfilePicAddress][nvarchar](max) NULL,
)

ALTER TABLE [dbo].[UserProfiles] 
ADD CONSTRAINT [PK_UserProfiles]
	PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[UserProfiles] ADD CONSTRAINT [FK_UserProfiles_Countries]
	FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]) 
GO

ALTER TABLE [dbo].[UserProfiles] ADD CONSTRAINT [FK_UserProfiles_States]
	FOREIGN KEY ([ProfileStateId]) REFERENCES [dbo].[ProfileStates] ([Id]) 
GO

CREATE INDEX UserProfileID_Index ON [dbo].[UserProfiles] (Id)
GO






CREATE TABLE [dbo].[Categories](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL,
	[ParentId][int] NULL
)
GO

ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
	PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Categories]
	FOREIGN KEY([ParentId]) REFERENCES [dbo].[Categories]([Id])
GO



CREATE TABLE [dbo].[Formats](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[Formats]
ADD CONSTRAINT [PK_Formats]
	PRIMARY KEY CLUSTERED([Id])
GO



CREATE TABLE [dbo].[Languages](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
	PRIMARY KEY CLUSTERED([Id])
GO


CREATE TABLE [dbo].[ProductStates](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[ProductStates]
ADD CONSTRAINT [PK_States]
	PRIMARY KEY CLUSTERED([Id])
GO


CREATE TABLE [dbo].[Publishers](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[Publishers]
ADD CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED([Id])
GO



CREATE TABLE [dbo].[Reviews](
	[ProductId][int] NOT NULL,
	[UserId][int] NOT NULL,
	[Rating][int] NOT NULL,
	[Date][datetime] NOT NULL,
	[Text][nvarchar](max) NULL
)
GO

ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [Reviews_Rating]
CHECK ([Rating] BETWEEN 1 AND 5)

ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
	PRIMARY KEY ([ProductId],[UserId])
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

CREATE TABLE [dbo].[Authors](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL,
	[Surname][nvarchar](max) NOT NULL,
	[CountryId][int] NULL
)
GO

ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [PK_Author]
	PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [FK_Authors_Countries]
	FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
GO

CREATE TABLE [dbo].[Books](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL,
	[Annotation][nvarchar](max) NULL
)
GO

ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
	PRIMARY KEY CLUSTERED([Id])
GO


CREATE TABLE [dbo].[AuthorsBooks](
	[AuthorId][int] NOT NULL,
	[BookId][int] NOT NULL
)
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [PK_AuthorsBooks]
	PRIMARY KEY ([AuthorId],[BookId])
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [FK_AuthorsBooks_Books]
	FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [FK_AuthorsBooks_Authors]
	FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id])
GO






CREATE TABLE [dbo].[Products](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[FormatId][int] NOT NULL,
	[ProductStateId][int] NOT NULL,
	[LanguageId][int] NOT NULL,
	[PublisherId][int] NOT NULL,
	[CategoryId][int] NOT NULL,
	[BookId][int] NOT NULL,

	[Name][nvarchar](max) NOT NULL,
	[PicAddress][nvarchar](max) NULL,
	[Text][nvarchar](max) NULL,
	[Price][money] NOT NULL,
	[PageCount][int] NULL,
	[Year][int] NULL,
	[ISBN][nvarchar](max) NULL,
	[EAN][nvarchar](max) NULL
	
)
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Formats] FOREIGN KEY([FormatId]) REFERENCES [dbo].[Formats]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_States] FOREIGN KEY([ProductStateId]) REFERENCES [dbo].[ProductStates]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Languages] FOREIGN KEY([LanguageId]) REFERENCES [dbo].[Languages]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Publishers] FOREIGN KEY([PublisherId]) REFERENCES [dbo].[Publishers]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Books] FOREIGN KEY([BookId]) REFERENCES [dbo].[Books]
GO


CREATE VIEW [dbo].[ProductRating] AS
	SELECT p.Id, r.Average AS AverageRating
	FROM dbo.Products AS p LEFT JOIN
	(SELECT ProductId AS PId, AVG(CONVERT(DECIMAL, Rating)) AS Average
	FROM dbo.Reviews
	GROUP BY ProductId) AS r 
		ON p.Id = r.PId
