IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Payments]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Payments]
GO

CREATE TABLE [dbo].[Payments](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
	PRIMARY KEY CLUSTERED([Id])
GO