CREATE TABLE [dbo].[Speakers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Bio] [nvarchar](4000) NULL,
	[WebSite] [nvarchar](1000) NULL,
	 CONSTRAINT [PK_Speakers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO