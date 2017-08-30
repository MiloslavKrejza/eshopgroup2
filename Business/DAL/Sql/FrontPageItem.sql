IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[FrontPageItems]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[FrontPageItems]
GO

CREATE TABLE [dbo].[FrontPageItems](
	[Id][int] NOT NULL IDENTITY(1,1),
	[Name][nvarchar](max) NULL,
	[SortingParameter][nvarchar](max) NOT NULL,
	[SortType][nvarchar](4) NOT NULL,
	[Count][int] NOT NULL,
	[CategoryId][int] NOT NULL,
	[TimeOffSet][int] NULL,
	[Active][bit] NOT NULL
)
GO

ALTER TABLE [dbo].[FrontPageItems]
ADD CONSTRAINT [PK_FrontPageItems]
	PRIMARY KEY CLUSTERED([Id])
GO

ALTER TABLE [dbo].[FrontPageItems]
ADD CONSTRAINT [FK_FrontPageItems_Categories]
	FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Categories]
GO