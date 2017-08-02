IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[ProductStates]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[ProductStates]
GO

CREATE TABLE [dbo].[ProductStates](
	[Id][int] IDENTITY(1,1) NOT NULL,
	[Name][nvarchar](max) NOT NULL
)
GO

ALTER TABLE [dbo].[ProductStates]
ADD CONSTRAINT [PK_States]
	PRIMARY KEY CLUSTERED([Id])
GO