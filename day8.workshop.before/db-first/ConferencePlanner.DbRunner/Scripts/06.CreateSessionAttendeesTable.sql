CREATE TABLE [dbo].[SessionAttendee](
	[SessionId] [int] NOT NULL,
	[AttendeeId] [int] NOT NULL,
	CONSTRAINT [PK_SessionAttendee] PRIMARY KEY CLUSTERED 
	(
		[SessionId] ASC,
		[AttendeeId] ASC
	) ON [PRIMARY]
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SessionAttendee]  WITH CHECK ADD  CONSTRAINT [FK_SessionAttendee_Attendees_AttendeeId] FOREIGN KEY([AttendeeId])
REFERENCES [dbo].[Attendees] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SessionAttendee] CHECK CONSTRAINT [FK_SessionAttendee_Attendees_AttendeeId]
GO

ALTER TABLE [dbo].[SessionAttendee]  WITH CHECK ADD  CONSTRAINT [FK_SessionAttendee_Sessions_SessionId] FOREIGN KEY([SessionId])
REFERENCES [dbo].[Sessions] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SessionAttendee] CHECK CONSTRAINT [FK_SessionAttendee_Sessions_SessionId]
GO