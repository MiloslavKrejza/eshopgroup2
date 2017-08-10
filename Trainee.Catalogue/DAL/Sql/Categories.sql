IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Categories]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Categories]
GO

CREATE TABLE [dbo].[Categories](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL,
	[ParentId][int] NULL,
	[PicAddress][nvarchar](max) NULL 
)
GO

ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
	PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Categories]
	FOREIGN KEY([ParentId]) REFERENCES [dbo].[Categories]([Id])
GO