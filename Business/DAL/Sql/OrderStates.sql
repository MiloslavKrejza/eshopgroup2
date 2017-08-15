IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[OrderStates]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[OrderStates]
GO

CREATE TABLE [dbo].[OrderStates](
	[Id][int] Identity(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[OrderStates]
ADD CONSTRAINT [PK_OrderStates]
	PRIMARY KEY CLUSTERED([Id])
GO