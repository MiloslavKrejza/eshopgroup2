IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Shippings]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Shippings]
GO

CREATE TABLE [dbo].[Shippings](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL,
	[Price][money] NOT NULL
)
GO

ALTER TABLE [dbo].[Shippings]
ADD CONSTRAINT [PK_Shippings]
	PRIMARY KEY CLUSTERED([Id])
GO

