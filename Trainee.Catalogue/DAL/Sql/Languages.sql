IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Languages]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Languages]
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