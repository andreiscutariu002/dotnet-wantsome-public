CREATE TABLE [dbo].[Attendees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[EmailAddress] [nvarchar](256) NULL,
	 CONSTRAINT [PK_Attendees] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

