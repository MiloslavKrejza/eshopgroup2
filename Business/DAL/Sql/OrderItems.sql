IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[OrderItems]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[OrderItems]
GO

CREATE TABLE [dbo].[OrderItems](
	[OrderId][int] NOT NULL,
	[ProductId][int] NOT NULL,
	[Price][money] NOT NULL,
	[Amount][int] NOT NULL
)
GO

ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
	PRIMARY KEY CLUSTERED([OrderId],[ProductId])
GO

ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItems_Order]
	FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]
GO

ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItems_Products]
	FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products]
GO