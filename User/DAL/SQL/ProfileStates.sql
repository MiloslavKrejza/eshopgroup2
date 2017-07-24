IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[ProfileStates]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[ProfileStates]
GO

CREATE TABLE [dbo].[ProfileStates](
	[Id][int] Identity(1,1) NOT NULL,
	[StateName][nvarchar](max) NOT NULL
	
)
ALTER TABLE [dbo].[ProfileStates]
ADD CONSTRAINT [PK_ProfileStates]
	PRIMARY KEY Clustered([Id])
GO