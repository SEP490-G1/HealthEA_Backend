USE [DOAN]
GO
/****** Object:  Table [dbo].[DocumentProfiles]    Script Date: 10/29/2024 11:09:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentProfiles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PantientId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[ContentMedical] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastModifyDate] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_DocumentProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentProfiles_PantientId]    Script Date: 10/29/2024 11:09:51 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentProfiles_PantientId] ON [dbo].[DocumentProfiles]
(
	[PantientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DocumentProfiles]  WITH CHECK ADD  CONSTRAINT [FK_DocumentProfiles_HealthProfiles_PantientId] FOREIGN KEY([PantientId])
REFERENCES [dbo].[HealthProfiles] ([Id])
GO
ALTER TABLE [dbo].[DocumentProfiles] CHECK CONSTRAINT [FK_DocumentProfiles_HealthProfiles_PantientId]
GO
