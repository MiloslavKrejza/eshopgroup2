IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[AuthorsBooks]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[AuthorsBooks]
GO

CREATE TABLE [dbo].[AuthorsBooks](
	[AuthorId][int] NOT NULL,
	[BookId][int] NOT NULL
)
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [PK_AuthorsBooks]
	PRIMARY KEY ([AuthorId],[BookId])
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [FK_AuthorsBooks_Books]
	FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id])
GO

ALTER TABLE [dbo].[AuthorsBooks]
ADD CONSTRAINT [FK_AuthorsBooks_Authors]
	FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id])
GO