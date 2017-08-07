IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Products]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Products]
GO

CREATE TABLE [dbo].[Products](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[FormatId][int] NOT NULL,
	[ProductStateId][int] NOT NULL,
	[LanguageId][int] NOT NULL,
	[PublisherId][int] NOT NULL,
	[CategoryId][int] NOT NULL,
	[BookId][int] NOT NULL,

	[Name][nvarchar](max) NOT NULL,
	[PicAddress][nvarchar](max) NULL,
	[Text][nvarchar](max) NULL,
	[Price][money] NOT NULL,
	[PageCount][int] NULL,
	[Year][int] NULL,
	[ISBN][nvarchar](max) NULL,
	[EAN][nvarchar](max) NULL,
	[DateAdded][datetime] DEFAULT(GETDATE()) NOT NULL 
)
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Formats] FOREIGN KEY([FormatId]) REFERENCES [dbo].[Formats]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_States] FOREIGN KEY([ProductStateId]) REFERENCES [dbo].[ProductStates]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Languages] FOREIGN KEY([LanguageId]) REFERENCES [dbo].[Languages]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Publishers] FOREIGN KEY([PublisherId]) REFERENCES [dbo].[Publishers]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories]
GO

ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Books] FOREIGN KEY([BookId]) REFERENCES [dbo].[Books]
GO