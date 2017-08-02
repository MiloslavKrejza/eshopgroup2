IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Formats]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Formats]
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