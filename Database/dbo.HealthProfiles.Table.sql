USE [DOAN]
GO
/****** Object:  Table [dbo].[HealthProfiles]    Script Date: 10/29/2024 11:09:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthProfiles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ProfileCode] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [int] NULL,
	[Residence] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[SharedStatus] [int] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastModifyDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_HealthProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_HealthProfiles_UserId]    Script Date: 10/29/2024 11:09:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_HealthProfiles_UserId] ON [dbo].[HealthProfiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HealthProfiles]  WITH CHECK ADD  CONSTRAINT [FK_HealthProfiles_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[HealthProfiles] CHECK CONSTRAINT [FK_HealthProfiles_User_UserId]
GO
