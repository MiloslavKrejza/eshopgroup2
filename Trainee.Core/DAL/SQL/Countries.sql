IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Countries]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Countries]
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