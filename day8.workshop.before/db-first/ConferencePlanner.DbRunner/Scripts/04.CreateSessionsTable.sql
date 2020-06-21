CREATE TABLE [dbo].[Sessions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Abstract] [nvarchar](4000) NULL,
	[StartTime] [datetimeoffset](7) NULL,
	[EndTime] [datetimeoffset](7) NULL,
	[TrackId] [int] NULL,
	 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Tracks_TrackId] FOREIGN KEY([TrackId])
REFERENCES [dbo].[Tracks] ([Id])
GO

ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Tracks_TrackId]
GO

