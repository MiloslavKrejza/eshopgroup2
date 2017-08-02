IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Publishers]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Publishers]
GO

CREATE TABLE [dbo].[Publishers](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[Publishers]
ADD CONSTRAINT [PK_Publishers] PRIMARY KEY CLUSTERED([Id])
GO