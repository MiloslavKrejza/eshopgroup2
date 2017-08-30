IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[FrontPageSlots]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[FrontPageSlots]
GO

CREATE TABLE [dbo].[FrontPageSlots](
	[Id][int] NOT NULL,
	[ItemId][int] NOT NULL,
)
GO

ALTER TABLE [dbo].[FrontPageSlots]
ADD CONSTRAINT [PK_FrontPageSlots]
	PRIMARY KEY CLUSTERED ([Id])
GO

ALTER TABLE [dbo].[FrontPageSlots]
ADD CONSTRAINT [FK_FrontPageSlots]
	FOREIGN KEY ([ItemId]) REFERENCES [dbo].[FrontPageItems]
GO