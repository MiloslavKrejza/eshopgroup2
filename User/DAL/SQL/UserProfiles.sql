IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[UserProfiles]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[UserProfiles]
GO

CREATE TABLE [dbo].[UserProfiles](
	[Id][int] NOT NULL,
	[Name][nvarchar](50) NOT NULL,
	[Surname][nvarchar] (50)NOT NULL,
	[ProfileStateId][int] NOT NULL,
	[PhoneNumber][nvarchar] (max)NULL,
	[CountryId][int] NULL,
	[City][nvarchar](max) NULL,
	[Postalcode][nvarchar](max)NULL,
	[Address][nvarchar](max) NULL,
	[ProfilePicAddress][nvarchar](max) NULL,
)

ALTER TABLE [dbo].[UserProfiles] ADD CONSTRAINT [FK_UserProfiles_Coutries]
	FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id]) 
GO
ALTER TABLE [dbo].[UserProfiles] ADD CONSTRAINT [FK_UserProfiles_Identities]
	FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id]) 
GO
ALTER TABLE [dbo].[UserProfiles] ADD CONSTRAINT [FK_UserProfiles_States]
	FOREIGN KEY ([ProfileStateId]) REFERENCES [dbo].[ProfileStates] ([Id]) 
GO

