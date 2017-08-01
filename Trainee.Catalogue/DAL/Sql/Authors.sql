IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Authors]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Authors]
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