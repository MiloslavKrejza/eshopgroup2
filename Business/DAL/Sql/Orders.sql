IF EXISTS (SELECT * FROM [dbo].sysobjects WHERE id = OBJECT_ID('[dbo].[Orders]') AND OBJECTPROPERTY(id,'IsUserTable')=1)
	DROP TABLE [dbo].[Orders]
GO

CREATE TABLE [dbo].[Orders](
	[Id][int] Identity(1,1) NOT NULL,
	[UserId][int] NULL,
	[StateId][int] NOT NULL,
	[PaymentId][int] NOT NULL,
	[ShippingId][int] NOT NULL,
	[CountryId][int] NOT NULL,

	[Name][nvarchar](50) NOT NULL,
	[Surname][nvarchar](50) NOT NULL,
	[PhoneNumber][nvarchar](max) NOT NULL,
	[City][nvarchar](max) NOT NULL,
	[Postalcode][nvarchar](max) NOT NULL,
	[Address][nvarchar](max) NOT NULL

)
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
	PRIMARY KEY CLUSTERED ([Id])
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_UserProfiles]
	FOREIGN KEY([UserId]) REFERENCES [dbo].[UserProfiles]
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_OrderStates]
	FOREIGN KEY([StateId]) REFERENCES [dbo].[OrderStates]
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Payments]
	FOREIGN KEY([PaymentId]) REFERENCES [dbo].[Payments]
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Shippings]
	FOREIGN KEY([ShippingId]) REFERENCES [dbo].[Shippings]
GO

ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_Countries]
	FOREIGN KEY([CountryId]) REFERENCES [dbo].[Countries]
GO

