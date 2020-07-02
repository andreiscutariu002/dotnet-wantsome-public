CREATE TABLE [dbo].[SessionSpeaker](
	[SessionId] [int] NOT NULL,
	[SpeakerId] [int] NOT NULL,
	CONSTRAINT [PK_SessionSpeaker] PRIMARY KEY CLUSTERED 
	(
		[SessionId] ASC,
		[SpeakerId] ASC
	) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SessionSpeaker]  WITH CHECK ADD  CONSTRAINT [FK_SessionSpeaker_Sessions_SessionId] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SessionSpeaker] CHECK CONSTRAINT [FK_SessionSpeaker_Sessions_SessionId]
GO

ALTER TABLE [dbo].[SessionSpeaker]  WITH CHECK ADD  CONSTRAINT [FK_SessionSpeaker_Speakers_SpeakerId] FOREIGN KEY([SpeakerId])
REFERENCES [dbo].[Speakers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SessionSpeaker] CHECK CONSTRAINT [FK_SessionSpeaker_Speakers_SpeakerId]
GO

