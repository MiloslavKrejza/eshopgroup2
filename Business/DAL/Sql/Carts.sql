IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Carts]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Carts]
GO

CREATE TABLE [dbo].[Carts](
	[UserId][int] NULL,
	[VisitorId][varchar](36) NOT NULL,
	[ProductId][int] NOT NULL,
	[Amount][int] NOT NULL,
	[LastTimeUpdated][DateTime] DEFAULT(GETDATE()) NOT NULL
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

CREATE INDEX [VisitorId_index]
	ON [dbo].[Carts] ([VisitorId])
GO