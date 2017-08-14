IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Carts]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Carts]
GO

CREATE TABLE [dbo].[Carts](
	[UserId][int] NULL,
	[VisitorId][int] NOT NULL,
	[ProductId][int] NOT NULL,
	[Amount][int] NOT NULL
)
GO

ALTER TABLE [dbo].[Carts]
ADD CONSTRAINT [PK_Carts]
	PRIMARY KEY CLUSTERED ([VisitorId],[ProductId])
GO

ALTER TABLE [dbo].[Carts]
ADD CONSTRAINT [FK_Carts_UserProfiles]
	FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfiles]
GO

ALTER TABLE [dbo].[Carts]
ADD CONSTRAINT [FK_Carts_Products]
	FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]
GO
