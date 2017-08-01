IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Books]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Books]
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