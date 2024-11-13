USE [master]
GO
/****** Object:  Database [DoAn]    Script Date: 11/7/2024 10:02:07 PM ******/
CREATE DATABASE [DoAn]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoAn', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\DoAn.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DoAn_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\DoAn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DoAn] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DoAn].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DoAn] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DoAn] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DoAn] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DoAn] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DoAn] SET ARITHABORT OFF 
GO
ALTER DATABASE [DoAn] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DoAn] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DoAn] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DoAn] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DoAn] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DoAn] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DoAn] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DoAn] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DoAn] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DoAn] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DoAn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DoAn] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DoAn] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DoAn] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DoAn] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DoAn] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DoAn] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DoAn] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DoAn] SET  MULTI_USER 
GO
ALTER DATABASE [DoAn] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DoAn] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DoAn] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DoAn] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DoAn] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DoAn] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DoAn] SET QUERY_STORE = ON
GO
ALTER DATABASE [DoAn] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DoAn]
GO
/****** Object:  Table [dbo].[DailyMetrics]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyMetrics](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Weight] [float] NOT NULL,
	[Height] [float] NOT NULL,
	[SystolicBloodPressure] [int] NOT NULL,
	[DiastolicBloodPressure] [int] NOT NULL,
	[HeartRate] [int] NOT NULL,
	[Steps] [int] NOT NULL,
	[BodyTemperature] [float] NOT NULL,
	[Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceTokens]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeviceToken] [nvarchar](255) NOT NULL,
	[DeviceType] [nvarchar](50) NULL,
	[LastUpdated] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentProfiles]    Script Date: 11/7/2024 10:02:08 PM ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[EventDateTime] [datetime2](7) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[RepeatEndDate] [datetime2](7) NOT NULL,
	[RepeatFrequency] [int] NOT NULL,
	[RepeatInterval] [int] NOT NULL,
	[OriginalEventId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthProfiles]    Script Date: 11/7/2024 10:02:08 PM ******/
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
/****** Object:  Table [dbo].[Images]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[PublicId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invalidated_token]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invalidated_token](
	[id] [varchar](255) NOT NULL,
	[expriry_time] [datetime2](6) NULL,
 CONSTRAINT [PK__invalida__3213E83F8938BA78] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notices]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notices](
	[NoticeId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
	[RecipientId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminders](
	[ReminderId] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[ReminderTime] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[IsSent] [bit] NOT NULL,
	[OffsetUnit] [int] NOT NULL,
	[ReminderOffset] [int] NOT NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[ReminderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [uniqueidentifier] NOT NULL,
	[dob] [date] NULL,
	[email] [varchar](255) NOT NULL,
	[first_name] [varchar](255) NULL,
	[gender] [bit] NULL,
	[last_name] [varchar](255) NULL,
	[password] [varchar](255) NOT NULL,
	[phone] [varchar](255) NULL,
	[role] [varchar](255) NULL,
	[status] [varchar](255) NULL,
	[username] [varchar](255) NOT NULL,
 CONSTRAINT [PK__user__B9BE370F7E22D744] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEvents]    Script Date: 11/7/2024 10:02:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEvents](
	[UserEventId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[IsAccepted] [bit] NOT NULL,
	[IsOrganizer] [bit] NOT NULL,
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[UserEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'e45f1173-038d-43af-b801-08dcfd5bd6a2', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', N'string', N'string', CAST(N'2024-11-05T12:36:46.657' AS DateTime))
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'c9b7d1c0-e179-4dfa-93d5-08dcfdbe105b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'123', N'string', CAST(N'2024-11-06T00:19:54.033' AS DateTime))
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'2f31cb85-e62c-49d5-93d8-08dcfdbe105b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'ee25qzlTnOJGR5A3rGUfX3:APA91bHYw0aY69neaJqaGyJKUMc5O2R0RD_8-fyk9HpIrj42UWG7zqqs62kgxh_eL-NI1BFtW1EGNCA7SjMsuSJscqGNh5YI_koez0Q7WsV6_r3ph9PrcE8', N'web', CAST(N'2024-11-06T00:32:41.457' AS DateTime))
GO
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'9e53a47d-986e-43c4-aa50-0559d3fb6bea', NULL, N'Update this nay thoi event', N'Update initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Updated Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-01T09:00:00.0000000' AS DateTime2), 1, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'32fbe979-08cc-49ab-a7ab-251d38f70ded', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-21T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'c0b719e3-a1fe-40db-b741-3db72e46512a', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-30T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'4ed40c90-5d45-4578-8365-49ea4efe2b69', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-07T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'ff433b15-4d70-467c-b9aa-67dacde89fe2', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2025-01-04T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'41352f60-b363-432a-8ff0-a63dd36d8bbf', NULL, N'Update this event', N'Update initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Updated Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-01T09:00:00.0000000' AS DateTime2), 1, 1, N'8f4f8a68-9fcb-40cb-bf3b-67808382899b')
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId]) VALUES (N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-28T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b')
GO
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'8e79656a-fd46-491d-8657-00dd4fe98a95', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'e4f2e85c-28d5-4a05-ad98-01009f708e66', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'6ab82e12-c751-4455-9816-067a5eaa380e', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'af8ad000-3f81-4f33-bf26-207ccddc438f', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'3f3cf06f-1327-43a2-b5b2-2ac47af403af', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'af88290b-be1d-449a-b220-2aec4c5cc390', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'ed6935f6-7056-4e91-8b58-2ef7535e2d81', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'1b0236be-3244-4c59-b750-3c1892b3ece0', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'11a493df-e181-470f-bae4-402ed0cad07e', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'b4b2f535-0283-48fe-a2cc-42dd3bbec41a', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'30ac7f16-84b8-4d91-bbee-43c4341db1c9', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'7bb79c76-1730-461b-9842-45d1322a5f77', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'ef5db4cf-d467-4819-9af7-46a660db2784', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'77307bee-0997-4074-9cd1-52a01cee8b7d', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'd21aaf24-08c8-49e0-8d1e-5750efece60c', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'7835fa5b-e4d3-494a-8054-5a8936ae2ed8', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'0c806e48-9d67-4c00-84b2-669ffb48816e', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'a0425726-3eae-4033-9444-6e57cf70a196', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'12b1ec5c-47ea-4b89-adf0-e08d665d911d')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'fd2d343d-f429-48ca-a650-6f0088bc0642', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'b3b163c6-a8e1-4e60-8da9-a5c7985c23ae', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'dc57f0de-7b37-42e4-b1bd-b2a228c8cc66', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'd73a942b-8907-4ebf-a631-cbb9f0ac6480', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'5014b9db-95b5-4346-9dd3-da16649e5856', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'a2b55273-cb90-4522-ba6d-ef421c818463', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'c4e3198d-70ea-4ad8-bd3e-f06aa825e66e', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
GO
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'806aecd0-c95d-429c-99fa-02386d6c0690', N'6e675cc5-6dd9-4f82-9414-0819b5101801', CAST(N'2025-01-08T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd993c782-b5ea-40a7-97c6-0322c1036dbb', N'934d7cde-27fb-4868-87d5-ef3218716215', CAST(N'2025-01-10T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e2d274b2-04c4-4d03-9d48-0517d95c6a29', N'033b6dd7-52fc-4040-a4cc-c2e7a5a2b1c8', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'80567b55-4223-4bee-a31c-056095faba6d', N'ff433b15-4d70-467c-b9aa-67dacde89fe2', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ebe9385d-2812-4fa5-8646-06c9c9ddbb8a', N'99389d69-2634-4cb8-8f12-22d0b076a5b7', CAST(N'2024-12-25T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c1813c54-6a1c-4705-b18f-0783852c6edd', N'ca15ea06-e558-4e8b-97e0-b702f7d4bd0f', CAST(N'2024-11-13T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f25e8ecf-ab3a-4823-9897-09e8cbcdf4aa', N'32fbe979-08cc-49ab-a7ab-251d38f70ded', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2e79f4a5-c1a4-4036-b00e-0a53b848818a', N'78e11ec6-2b48-4973-a490-194068a49390', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8ee86c8a-3415-458a-8f65-0ba08975b518', N'25677630-b552-42a0-a697-68fd2da37d12', CAST(N'2025-01-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'80d5852e-17e3-449c-92c2-0dc93d38fce5', N'5e0bc01b-909d-4add-b4e1-bdb9781a08ef', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c2ea6b0b-2fa0-4b0e-9486-0e661c4852aa', N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'be8f8003-97a0-4f16-a27b-0ea81d1485f3', N'cd2d3635-1468-4034-89f1-df7dd564afcb', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c26d30a1-ef38-4119-a875-1051f60d5e1a', N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'52e0287e-c724-42ae-a278-1056ed18d6e8', N'4ed40c90-5d45-4578-8365-49ea4efe2b69', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2f8f4bb9-6cea-44b0-a879-12cd4d2a9e10', N'e0166a53-d195-4984-84fe-4d1db16ae9ad', CAST(N'2024-12-27T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'41bc9e8e-e170-495c-b11c-1355c067c274', N'5e0bc01b-909d-4add-b4e1-bdb9781a08ef', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3d77df9f-b952-42f8-944f-1398d129cebb', N'397d51bc-f224-4446-99df-3dffa08edb27', CAST(N'2024-12-05T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'33c244c9-d4ff-4af4-8dea-178970ab9ff4', N'ff433b15-4d70-467c-b9aa-67dacde89fe2', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'749d1ba3-cd6f-426c-99d2-178c0cd24825', N'8010233f-3a50-487b-9c42-2e4510e97cc4', CAST(N'2024-12-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'25ae8c85-5551-49a0-8ae0-17906ee7af84', N'319e663e-db3f-4169-89fa-bdca2ffea623', CAST(N'2024-12-17T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0bb4896c-b887-4d8d-a9e8-1b24480d4e11', N'e6dc9e43-b9cb-4896-a029-26d5dc35fbe2', CAST(N'2024-11-06T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6011216b-5fbb-40e7-9786-1d946411b685', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2024-12-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1cc23b44-88a6-4307-937f-1f6f3639eef2', N'25b82dab-1849-430b-9c60-4bb0fa77fa5d', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2949eba8-ebba-4dc3-9f9b-2008afec1a7e', N'e47475c3-01ab-42b9-8622-bad03b38a438', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c8222379-2633-409a-8dce-219efcb3756b', N'6b41eed2-891a-4603-8399-3d114994ce6f', CAST(N'2025-01-02T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b70d3d97-4b16-4dff-a34d-241e112830a0', N'5afbf17b-05ad-4fa5-9364-03dbf80a3892', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'864ef026-219d-4088-b770-247e593b0dbe', N'26a6cb90-f1da-4aab-8cb2-0d7be0a4af19', CAST(N'2024-12-09T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a0eb8e90-e7e5-4c5b-abf4-28c7285975d8', N'934d7cde-27fb-4868-87d5-ef3218716215', CAST(N'2025-01-10T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b0ee28a9-e257-47e1-bec5-29872aa42d5e', N'd0833d68-88b1-408c-8a00-0bb670adb04b', CAST(N'2025-01-01T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6c655586-0b05-4cc9-888b-2a2982de27fe', N'92d57edd-f45e-4b8d-b273-769150993cfd', CAST(N'2024-11-29T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'5a9c8584-8fbb-4073-bd0d-2a9bf68d3f3e', N'8565bcc8-7abc-456d-961a-0c6356c172f8', CAST(N'2024-12-29T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd28eba82-0fd8-45a1-a882-2b9b4d49eb0f', N'a7f754ae-9ae0-4fb8-9f50-3e5a4e4fb83b', CAST(N'2024-11-22T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4f784365-7426-475f-a8c9-2c9a9713cc03', N'690fc040-f13a-47a2-9b52-8fa20b3bc8b0', CAST(N'2024-12-10T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'213b7697-e83e-4e63-b520-2ca161807dab', N'6af28ac7-8b43-4c37-bc17-bc407b05fbd2', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a3a83e58-6665-400f-9c08-2e901ab45913', N'b6a0a57d-50a9-4016-992b-0c7d31eafa93', CAST(N'2024-12-16T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'677066d0-e2c7-4a14-bcf5-30904bc3cc7a', N'f4dd1b7a-3d27-426d-bad0-71aec3671616', CAST(N'2024-12-31T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'eef846f5-79c5-4cc3-a7e5-3161932589bc', N'78e11ec6-2b48-4973-a490-194068a49390', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'06e09218-dc4b-41d7-adf8-31aef46d53c7', N'1081b50d-e9fc-4ba6-97da-8f0027938fbf', CAST(N'2024-12-08T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'7f1fe18b-b2d2-4cb1-98b7-32521701b674', N'8cde2c96-8558-42dd-b895-a4c8badc7f33', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8eee9104-6809-4bd9-b2ec-33ebf60d3151', N'42813b7e-75a7-46bc-b670-c9b2f8c1f6b0', CAST(N'2024-12-13T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'5be69421-f706-4d89-bd5a-34c0b11e0053', N'a3566ffc-c1ba-4580-9b43-c0dd51549fa6', CAST(N'2024-11-10T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6a0f89fc-364d-43d0-b0e7-3592ec70fbce', N'26a6cb90-f1da-4aab-8cb2-0d7be0a4af19', CAST(N'2024-12-09T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'03f34c30-8165-4b2d-9d96-360c606c39c0', N'65f81d4c-b3d5-4af1-8369-81d63574a419', CAST(N'2024-11-09T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'7200cef9-01e0-4971-a7a8-38740fe17e81', N'8010233f-3a50-487b-9c42-2e4510e97cc4', CAST(N'2024-11-29T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c0111a33-3969-4c51-bdc3-387d776048d5', N'4040b0db-6226-40a2-9c45-693a80f6f240', CAST(N'2025-01-05T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e0521f83-aadd-46cc-b8fc-3886e64f9b1d', N'946a4841-d67d-4d81-951c-f6754d325738', CAST(N'2024-12-11T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'efe9e2f6-e4d6-45f1-8013-39a0d4c52aaf', N'3ac459b3-9fb6-43b8-b588-09ed8d6e4363', CAST(N'2025-01-06T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e11dd3bb-a788-4d51-a66f-3bd4c0016cb6', N'5afbf17b-05ad-4fa5-9364-03dbf80a3892', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'11c91046-444a-4f94-ba95-40122f16ffe1', N'57d0d33f-c2ec-400f-aee5-73255d7eda1b', CAST(N'2025-01-09T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'dcdba3d0-d1e4-4d93-bbad-40f8830a5b4d', N'34097bf2-f038-4e28-8b94-ab78674b0f54', CAST(N'2024-12-03T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f97b48ba-240c-41af-bb22-4111d2377523', N'b2756a7b-7bfd-4ea0-8de9-0094d7cea021', CAST(N'2024-12-12T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8109d9e3-3814-4ba8-a5b1-432edda891ec', N'd57c0fa1-3c47-4a33-bc85-1e176a7d4542', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2738a26b-a38c-485a-a847-43b6120d4a32', N'25b82dab-1849-430b-9c60-4bb0fa77fa5d', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2afdd86a-38ef-4c85-955c-44bbdf785a4c', N'17dfae92-a19a-4efe-bf30-b9effa3467a2', CAST(N'2025-01-03T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'29de6105-03af-42dd-acd2-450bf11cf752', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2024-11-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4d37c919-c438-47cc-acf0-45b117c1ea70', N'319e663e-db3f-4169-89fa-bdca2ffea623', CAST(N'2024-12-17T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3c091e41-93f9-4d80-bd9d-4af28029d0f7', N'bf8fc7f0-a742-49d3-9906-b210c591b887', CAST(N'2024-12-15T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0a8defa4-f7c5-4407-83a5-4bbf505543dc', N'033b6dd7-52fc-4040-a4cc-c2e7a5a2b1c8', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'26671785-643f-4ea5-b238-4e635fd409cf', N'32fbe979-08cc-49ab-a7ab-251d38f70ded', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'82f5724b-3734-467c-b3eb-50121603365a', N'5da741c0-c55f-40b5-836f-3ef82f76a36f', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e238da10-fc25-45b7-a7b5-51214ad070ba', N'42813b7e-75a7-46bc-b670-c9b2f8c1f6b0', CAST(N'2024-12-13T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a1716594-e69f-45c1-bff9-5163a05babc0', N'd0b3f399-0b63-4b0c-abbc-d8b0d35d5c70', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'7e57709d-0cda-40e6-b39a-5358921b7806', N'6153959e-14fc-4633-b2f4-16f8cd53400b', CAST(N'2024-10-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'458ae969-f8cc-4ca0-9094-56babb9d6717', N'b6a0a57d-50a9-4016-992b-0c7d31eafa93', CAST(N'2024-12-16T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'38b376a0-c7ac-4f0e-a51a-5d3fd3481f08', N'17dfae92-a19a-4efe-bf30-b9effa3467a2', CAST(N'2025-01-03T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'70c0bd0b-72d5-464e-bdf8-5d558417959e', N'0913272c-3bea-4752-8510-a665a80f414f', CAST(N'2024-12-20T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'abd74eac-ef66-4158-b7e6-5d8dab1df1ea', N'6b41eed2-891a-4603-8399-3d114994ce6f', CAST(N'2025-01-02T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'282645bc-e5b2-42f0-945c-5f0469458f23', N'5da741c0-c55f-40b5-836f-3ef82f76a36f', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c0dfd9af-d51d-4b97-ac8b-609e368d2cc5', N'c0b719e3-a1fe-40db-b741-3db72e46512a', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4f0e880e-b5e6-4f4c-bf84-65c363354b85', N'8465ed20-96a5-4981-9441-550bb05eac95', CAST(N'2024-12-26T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'207a5809-7158-43c9-845f-66979777ae9c', N'690fc040-f13a-47a2-9b52-8fa20b3bc8b0', CAST(N'2024-12-10T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e37314b0-2cc5-40c4-aa42-6702b9225d48', N'1d5a9a9c-3d0d-4466-88ac-f2e5dbded54c', CAST(N'2024-12-19T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a122cfad-f74c-4918-a193-67a38b3d20d9', N'7c7266dc-f445-4f52-97bf-c46b3df3104e', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'796c0adf-84f5-4405-ae75-6b60d7891a10', N'8cde2c96-8558-42dd-b895-a4c8badc7f33', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b07299cf-8f09-4a24-bd77-6c8c91ace7ef', N'14a8c510-70c1-4d41-a72e-84a5574a4b7b', CAST(N'2024-10-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'26b24389-0f2e-413c-9620-6c92ae1f0eba', N'ca15ea06-e558-4e8b-97e0-b702f7d4bd0f', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c0e36601-d4ca-4323-9c5b-6cd3a8a6a2e8', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2024-10-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'020f5f6a-40cb-46bb-a421-6f949f7c1d95', N'3d26d09d-b9b7-49e7-8c78-2e63191d668c', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3292ae36-b325-45a9-bd0d-70aeec360104', N'3ea83633-f008-4c81-b4de-5917d99f69a8', CAST(N'2024-11-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'473e8b5f-0542-4c7c-9014-71297ab53a31', N'946a4841-d67d-4d81-951c-f6754d325738', CAST(N'2024-12-11T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'104d56a9-a6ae-43fc-bdff-7646fac78328', N'f15c209e-2dba-4d23-8ad5-bf04d7896bdc', CAST(N'2024-12-18T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'77d38f9e-b01d-45e1-9c92-76f680f91a74', N'7c7266dc-f445-4f52-97bf-c46b3df3104e', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3988ff36-a7d8-4091-9264-7779607f2e1d', N'34097bf2-f038-4e28-8b94-ab78674b0f54', CAST(N'2024-12-03T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'623bcc61-f714-4dea-9133-79a9128b2488', N'41352f60-b363-432a-8ff0-a63dd36d8bbf', CAST(N'2024-11-13T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update this event', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'87dd1d80-a888-4a51-836e-7a4635e65f0b', N'0914d178-9d9d-49d1-a20f-96d58dd6a4ec', CAST(N'2024-12-24T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'48562c74-0349-49a5-8178-7cbea1ae733c', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2024-11-29T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c06892fa-9c62-45b3-a8ff-7e63798daafc', N'b2756a7b-7bfd-4ea0-8de9-0094d7cea021', CAST(N'2024-12-12T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c44bf29f-a4ae-4af7-8e74-7eecdc3321d9', N'6af28ac7-8b43-4c37-bc17-bc407b05fbd2', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f3710128-0f14-4bf9-96b4-7f7eb2f09287', N'e6f90d52-abbd-45cc-9373-3932d1ffd852', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0eabc654-8f3d-4d95-8fbc-81799985ac7b', N'6b03583f-5181-4c54-ae2a-e25d16f57584', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'427e8fcd-2f59-4722-b865-8241b8f3f5a0', N'a3566ffc-c1ba-4580-9b43-c0dd51549fa6', CAST(N'2024-11-10T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'067204d8-cb8f-426e-98ba-85cbf0880875', N'25677630-b552-42a0-a697-68fd2da37d12', CAST(N'2024-12-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'80142f87-e987-41ba-a17a-85e9b8b2f3d2', N'80740fb5-f381-41f7-b0ca-86a3e111bf72', CAST(N'2024-12-20T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4333a8be-e772-4be0-8a31-866f91db267a', N'e47475c3-01ab-42b9-8622-bad03b38a438', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'cef05b8e-614b-43f3-8d27-87654462b63d', N'0322cb2e-458f-4877-b085-2a409d1e0436', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4067218f-0793-4314-b330-8cc6b64bb90f', N'1d5a9a9c-3d0d-4466-88ac-f2e5dbded54c', CAST(N'2024-12-19T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f3b34f75-f19f-49cb-8013-8d44f50aa47b', N'a7f754ae-9ae0-4fb8-9f50-3e5a4e4fb83b', CAST(N'2024-11-20T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6d6f8763-d4b2-4928-90a1-90b77b3c7a7e', N'd5f980d7-f304-461e-984b-fa2f79413302', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b455d9bc-42f9-4c5c-a362-926cb2e500d0', N'65f81d4c-b3d5-4af1-8369-81d63574a419', CAST(N'2024-11-09T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'13e7fef4-5636-476d-bc0e-941b217e07bf', N'e742d736-f5e1-4f8e-bbbe-6077be605a68', CAST(N'2024-12-06T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
GO
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a4b657ab-b286-4f13-ae69-9519be866abc', N'f15c209e-2dba-4d23-8ad5-bf04d7896bdc', CAST(N'2024-12-18T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'485fed1b-2c0c-4ab5-916c-9665d632fc21', N'8b908622-5a46-4053-966a-6bd4ca649039', CAST(N'2024-12-02T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a2053a60-615a-41f3-ac20-9741c21af662', N'8565bcc8-7abc-456d-961a-0c6356c172f8', CAST(N'2024-12-29T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b2f5bb61-8a31-4afd-8463-97443a4d7f8a', N'e22e50fd-d766-4c69-be0f-68cda0948c2a', CAST(N'2024-12-25T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'507205c3-78fd-4a75-a753-987ed258655a', N'92d57edd-f45e-4b8d-b273-769150993cfd', CAST(N'2024-11-27T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'03ecb2e5-c8de-4cb4-8e4b-98bc652074a4', N'0914d178-9d9d-49d1-a20f-96d58dd6a4ec', CAST(N'2024-12-24T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'46fea256-8958-4849-a98f-9ad114f01191', N'9cc7adaa-4e5b-4e9e-a55c-02572599229b', CAST(N'2024-11-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'512454f3-de36-4bea-a8c5-9c7ea819129d', N'df65b70a-bb5d-46a1-a7b6-2cb25fc521ef', CAST(N'2024-11-13T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6e5aff21-c703-4da8-bc16-9e7320f28662', N'e6dc9e43-b9cb-4896-a029-26d5dc35fbe2', CAST(N'2024-11-06T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6d13b5b5-73dd-4219-90cc-9f376d7e9479', N'397d51bc-f224-4446-99df-3dffa08edb27', CAST(N'2024-12-05T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'fb119d43-e267-4f2f-b999-a098c96497d5', N'854e5aa3-68ff-4554-bcdc-794c6586a90f', CAST(N'2024-12-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd287a7fa-ced3-43ef-b441-a281274cc2c3', N'191347d0-4512-4fc5-a750-bd1383dcd71b', CAST(N'2024-12-13T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd408250c-9b03-416d-b4e3-a3ad7592b3d2', N'ff9ec9f8-1459-4e68-a95c-43f0a24e663b', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8374578d-fd8b-4451-b83e-a4ebaaa2871d', N'989cc908-cf94-462b-8ec2-def1d926e752', CAST(N'2024-12-04T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f0f2fd17-69ab-48b9-839e-ab8cad295c57', N'44752116-fbe3-4189-97d9-cb62b2a0b9b7', CAST(N'2024-11-05T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e1db53da-ed7b-40cf-9ada-ac0bc2025325', N'3310f80e-1c9b-4605-b36b-3e4519852216', CAST(N'2025-01-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4abc5c96-5368-419d-bdd2-adaa24d3b0e6', N'df65b70a-bb5d-46a1-a7b6-2cb25fc521ef', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0e6cf2c4-2789-44c4-ba8c-af9b956d470e', N'57d0d33f-c2ec-400f-aee5-73255d7eda1b', CAST(N'2025-01-09T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'051dccff-ca1a-4fdd-a0f6-b255483f2418', N'fc054233-61b1-42c7-817a-2c246d28c8ee', CAST(N'2024-12-22T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'abeea282-be38-4dce-88b8-b55e1bcdfb06', N'ea2852fe-08fc-49b8-948b-75b8dc1b696b', CAST(N'2024-12-23T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8952fc66-fb4c-4f60-bad2-b6560145041c', N'4ed40c90-5d45-4578-8365-49ea4efe2b69', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f17ab28e-ca48-4d7b-a9dd-b783ce9a2330', N'191347d0-4512-4fc5-a750-bd1383dcd71b', CAST(N'2024-12-11T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'292c3533-7179-4fd8-a032-b8af08983f1c', N'd5f980d7-f304-461e-984b-fa2f79413302', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e6929b94-2cf8-444e-aeea-b90cddeabcc9', N'3ea83633-f008-4c81-b4de-5917d99f69a8', CAST(N'2024-10-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1c48eeea-71d9-4b7f-a7ef-b93625994cfd', N'cd2d3635-1468-4034-89f1-df7dd564afcb', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'5a4a987d-3cad-4ef0-92f4-bd1db8e50bcf', N'de70e6bc-6a13-4dc0-93b2-9e0ba547bafc', CAST(N'2024-12-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'938cc56b-e870-440c-a922-bdb1618df9eb', N'd57c0fa1-3c47-4a33-bc85-1e176a7d4542', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4ec7193f-2da0-4e63-957f-be20d1f57b7f', N'e91e3247-8d15-48d9-82d2-14b6129f4915', CAST(N'2024-12-01T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1e755f47-d730-4bdc-bdb9-c0fe58c017d9', N'989cc908-cf94-462b-8ec2-def1d926e752', CAST(N'2024-12-06T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'61546453-d440-4569-a68e-c12770e8f057', N'0322cb2e-458f-4877-b085-2a409d1e0436', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ea479e29-cd0c-4014-bb31-c344bf3860e6', N'1081b50d-e9fc-4ba6-97da-8f0027938fbf', CAST(N'2024-12-08T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e752f80f-f933-4b9f-a21c-c44786530a20', N'44752116-fbe3-4189-97d9-cb62b2a0b9b7', CAST(N'2024-11-05T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'9eac00fc-375e-4b84-bebd-c4785acccfb0', N'8465ed20-96a5-4981-9441-550bb05eac95', CAST(N'2024-12-26T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3df3bf89-3b9e-49bd-b9f7-c53225649ed4', N'bf8fc7f0-a742-49d3-9906-b210c591b887', CAST(N'2024-12-15T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'dd069925-b71f-488f-8d8f-c802f982e1cd', N'e0166a53-d195-4984-84fe-4d1db16ae9ad', CAST(N'2024-12-27T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'42bd7953-adea-473b-810c-cbba8583a69d', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2025-01-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'eb57b7a0-35b3-40f3-a3d1-cd14c53d8153', N'ff9ec9f8-1459-4e68-a95c-43f0a24e663b', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'586b9796-0943-4fd5-b8b5-ce1a42e0b6bf', N'e91e3247-8d15-48d9-82d2-14b6129f4915', CAST(N'2024-12-01T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f0add27c-e8ae-4e18-bc0b-cf61c8d04366', N'8a6810b6-3f2e-4c76-8daa-47a27ff0b67f', CAST(N'2024-11-08T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'892ad2ce-f216-4faa-933a-d0bb81b4c1ee', N'41352f60-b363-432a-8ff0-a63dd36d8bbf', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update this event', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'a7052c9b-4581-450e-b441-d2176e10559d', N'cdf6dcf1-0c3d-46ce-8347-d605899ca309', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c1206000-3543-4182-856c-d31e6e33dc44', N'd0833d68-88b1-408c-8a00-0bb670adb04b', CAST(N'2025-01-01T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e558c304-fbb1-4fc0-8cbd-d6dbd31a0c41', N'c0b719e3-a1fe-40db-b741-3db72e46512a', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'77aa28b5-2847-4984-ac08-d8bea684c519', N'cdf6dcf1-0c3d-46ce-8347-d605899ca309', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3b8fcbe1-1f62-48d0-8ae1-d8cc54f4e9f9', N'3d26d09d-b9b7-49e7-8c78-2e63191d668c', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'dcb60bd2-0dd0-4dcb-8c96-dd642fe10aeb', N'6b03583f-5181-4c54-ae2a-e25d16f57584', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'6b94bb9a-a349-4f2d-a30d-de069a2577bb', N'fc054233-61b1-42c7-817a-2c246d28c8ee', CAST(N'2024-12-22T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'16055c2f-5ae7-45e0-8850-de4fc82228d5', N'9cc7adaa-4e5b-4e9e-a55c-02572599229b', CAST(N'2024-11-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'accf43f0-98c3-4c57-9a72-dfd6cda4201b', N'6153959e-14fc-4633-b2f4-16f8cd53400b', CAST(N'2024-11-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'2c83d0ca-b659-4832-9bfa-e05e7443d7e8', N'8b908622-5a46-4053-966a-6bd4ca649039', CAST(N'2024-12-02T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c66b6785-b942-4ebd-92bd-e14a39da7073', N'854e5aa3-68ff-4554-bcdc-794c6586a90f', CAST(N'2024-12-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'224a91b1-cd4e-46e9-9484-e46724e1e7a8', N'99389d69-2634-4cb8-8f12-22d0b076a5b7', CAST(N'2024-12-27T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'7af7d4b8-9b42-4c99-89b8-e507d73bd962', N'14a8c510-70c1-4d41-a72e-84a5574a4b7b', CAST(N'2024-11-01T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'dc443f28-1ac2-4c05-8762-e804d5543ef5', N'6e675cc5-6dd9-4f82-9414-0819b5101801', CAST(N'2025-01-08T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'08291fb5-2f57-4a52-bb64-e969646e9745', N'e6f90d52-abbd-45cc-9373-3932d1ffd852', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1a75005f-2427-4959-857e-e96aa5f13031', N'0913272c-3bea-4752-8510-a665a80f414f', CAST(N'2024-12-20T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f75ef842-e36d-4a88-a1dc-eb6587018cb6', N'ea2852fe-08fc-49b8-948b-75b8dc1b696b', CAST(N'2024-12-23T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'75b37440-76ca-4cea-8039-eca879c2da99', N'80740fb5-f381-41f7-b0ca-86a3e111bf72', CAST(N'2024-12-18T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1cdc9306-f77c-4ae3-b63b-ed729541de2a', N'3ac459b3-9fb6-43b8-b588-09ed8d6e4363', CAST(N'2025-01-06T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'919e1028-f509-4102-8959-f08166a4af3c', N'4040b0db-6226-40a2-9c45-693a80f6f240', CAST(N'2025-01-05T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'52b2f4a1-de81-419a-87d1-f39ae9215525', N'3310f80e-1c9b-4605-b36b-3e4519852216', CAST(N'2025-01-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd05b6527-09fc-473e-823f-f488a896eda8', N'e742d736-f5e1-4f8e-bbbe-6077be605a68', CAST(N'2024-12-06T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'86023475-bff5-484d-9fd3-f5a27f4d31e8', N'8a6810b6-3f2e-4c76-8daa-47a27ff0b67f', CAST(N'2024-11-08T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c70daa59-2900-4f82-93c5-f92796671be4', N'de70e6bc-6a13-4dc0-93b2-9e0ba547bafc', CAST(N'2024-12-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'11c02bb1-edaf-44dc-9d68-fa62f95c788d', N'a178718f-73eb-4257-83a2-59d1f5524b05', CAST(N'2024-12-30T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update Project Kickoff Meeting', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'bcac51f4-3cbd-4534-95a7-fb854639ee76', N'e22e50fd-d766-4c69-be0f-68cda0948c2a', CAST(N'2024-12-25T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'816b1454-d2a6-46dc-863b-fe59e3c3afa3', N'f4dd1b7a-3d27-426d-bad0-71aec3671616', CAST(N'2024-12-31T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1887012b-721e-4582-8ab8-ffb88f5e245d', N'd0b3f399-0b63-4b0c-abbc-d8b0d35d5c70', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
GO
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', CAST(N'1988-07-21' AS Date), N'manhvv15@gmail.com', N'Jane', 0, N'Smith', N'Password123!', N'0987654321', N'User', N'Inactive', N'janesmith')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', NULL, N'admin@gmail.com', NULL, NULL, NULL, N'$2a$10$vyBxUi1EuGbSrPXADGBAEOxc8kotw/hJfG9OWY/SL5rMiwT8Arbhq', NULL, N'ADMIN', N'ACTIVE', N'admin')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'333e7891-9949-48d7-97e3-6c68cf88f8f1', CAST(N'1990-05-15' AS Date), N'manhvvv15@gmail.com', N'John', 1, N'Doe', N'Password123!', N'1234567890', N'Admin', N'Active', N'johndoe')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'4a5b22dd-cee4-498e-8469-d6b9a6914854', CAST(N'1992-12-12' AS Date), N'manhvu152k2@gmail.com', N'Sara', 0, N'Jones', N'Password123!', N'5566778899', N'Manager', N'Active', N'sarajones')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'12b1ec5c-47ea-4b89-adf0-e08d665d911d', CAST(N'1995-09-05' AS Date), N'michaelbrown@example.com', N'Michael', 1, N'Brown', N'Password123!', N'1122334455', N'User', N'Active', N'michaelbrown')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'a33b30ce-57e2-4da4-98e1-f522824c3c67', CAST(N'1985-01-30' AS Date), N'chrislee@example.com', N'Chris', 1, N'Lee', N'Password123!', N'3344556677', N'User', N'Pending', N'chrislee')
GO
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'7fd79438-54ee-4629-8917-02f45ea1a3d0', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'56f0de48-0367-44ab-aab7-46412467fc63', 1, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'c3e88bf9-2065-4df9-8922-778c95ce6f69', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'56f0de48-0367-44ab-aab7-46412467fc63', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'f438d0eb-c662-4f19-87fd-7feeadde07b2', N'a33b30ce-57e2-4da4-98e1-f522824c3c67', N'ca645240-ae68-4cab-8a01-ac8bd4e34338', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'331d0f5b-4119-4bc1-8177-8012278ba1e6', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'f45e1df9-3b50-44f0-a381-cb01efefced8', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'f52d1286-d092-455f-8b87-b6e12a6c993b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'f45e1df9-3b50-44f0-a381-cb01efefced8', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'157d80d0-e1aa-4810-93e2-fc28a7215c19', N'12b1ec5c-47ea-4b89-adf0-e08d665d911d', N'ca645240-ae68-4cab-8a01-ac8bd4e34338', 0, 0)
GO
/****** Object:  Index [IX_DocumentProfiles_PantientId]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentProfiles_PantientId] ON [dbo].[DocumentProfiles]
(
	[PantientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HealthProfiles_UserId]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE NONCLUSTERED INDEX [IX_HealthProfiles_UserId] ON [dbo].[HealthProfiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_user_email]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK_user_email] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_user_username]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK_user_username] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK5c856itaihtmi69ni04cmpc4m]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK5c856itaihtmi69ni04cmpc4m] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UKhl4ga9r00rh51mdaf20hmnslt]    Script Date: 11/7/2024 10:02:08 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UKhl4ga9r00rh51mdaf20hmnslt] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeviceTokens] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[DeviceTokens] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Notices] ADD  DEFAULT (newid()) FOR [NoticeId]
GO
ALTER TABLE [dbo].[DocumentProfiles]  WITH CHECK ADD  CONSTRAINT [FK_DocumentProfiles_HealthProfiles_PantientId] FOREIGN KEY([PantientId])
REFERENCES [dbo].[HealthProfiles] ([Id])
GO
ALTER TABLE [dbo].[DocumentProfiles] CHECK CONSTRAINT [FK_DocumentProfiles_HealthProfiles_PantientId]
GO
ALTER TABLE [dbo].[HealthProfiles]  WITH CHECK ADD  CONSTRAINT [FK_HealthProfiles_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[HealthProfiles] CHECK CONSTRAINT [FK_HealthProfiles_User_UserId]
GO
ALTER TABLE [dbo].[Notices]  WITH CHECK ADD  CONSTRAINT [FK_Notices_User_Recipient] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[Notices] CHECK CONSTRAINT [FK_Notices_User_Recipient]
GO
ALTER TABLE [dbo].[Notices]  WITH CHECK ADD  CONSTRAINT [FK_Notices_User_Sender] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[Notices] CHECK CONSTRAINT [FK_Notices_User_Sender]
GO
ALTER TABLE [dbo].[UserEvents]  WITH CHECK ADD  CONSTRAINT [FK_UserEvents_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[UserEvents] CHECK CONSTRAINT [FK_UserEvents_user_UserId]
GO
USE [master]
GO
ALTER DATABASE [DoAn] SET  READ_WRITE 
GO
