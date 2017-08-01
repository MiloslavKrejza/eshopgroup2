IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Reviews]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Reviews]
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