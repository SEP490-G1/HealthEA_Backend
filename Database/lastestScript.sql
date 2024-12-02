USE [master]
GO
/****** Object:  Database [DOAN]    Script Date: 01/12/2024 21:44:07 ******/
CREATE DATABASE [DOAN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DOAN', FILENAME = N'/var/opt/mssql/data/DOAN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DOAN_log', FILENAME = N'/var/opt/mssql/data/DOAN_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DOAN] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DOAN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DOAN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DOAN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DOAN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DOAN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DOAN] SET ARITHABORT OFF 
GO
ALTER DATABASE [DOAN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DOAN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DOAN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DOAN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DOAN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DOAN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DOAN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DOAN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DOAN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DOAN] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DOAN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DOAN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DOAN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DOAN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DOAN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DOAN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DOAN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DOAN] SET RECOVERY FULL 
GO
ALTER DATABASE [DOAN] SET  MULTI_USER 
GO
ALTER DATABASE [DOAN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DOAN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DOAN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DOAN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DOAN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DOAN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DOAN', N'ON'
GO
ALTER DATABASE [DOAN] SET QUERY_STORE = ON
GO
ALTER DATABASE [DOAN] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DOAN]
GO
USE [DOAN]
GO
/****** Object:  Sequence [dbo].[DailyMetrics_SEQ]    Script Date: 01/12/2024 21:44:08 ******/
CREATE SEQUENCE [dbo].[DailyMetrics_SEQ] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 50
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
USE [DOAN]
GO
/****** Object:  Sequence [dbo].[user_SEQ]    Script Date: 01/12/2024 21:44:08 ******/
CREATE SEQUENCE [dbo].[user_SEQ] 
 AS [bigint]
 START WITH 1
 INCREMENT BY 50
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE 
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NULL,
	[Location] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[chatMessage]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chatMessage](
	[messageId] [varchar](255) NOT NULL,
	[created_at] [datetime2](6) NOT NULL,
	[message] [text] NULL,
	[senderType] [varchar](255) NOT NULL,
	[userId] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[messageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyMetrics]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyMetrics](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[SystolicBloodPressure] [int] NULL,
	[DiastolicBloodPressure] [int] NULL,
	[HeartRate] [int] NULL,
	[BloodSugar] [float] NULL,
	[BodyTemperature] [float] NULL,
	[Date] [date] NOT NULL,
	[OxygenSaturation] [float] NULL,
 CONSTRAINT [PK_DailyMetrics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceTokens]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeviceToken] [nvarchar](max) NOT NULL,
	[DeviceType] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DeviceTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ClinicAddress] [nvarchar](max) NULL,
	[ClinicCity] [nvarchar](max) NULL,
	[DisplayName] [nvarchar](max) NOT NULL,
	[HistoryOfWork] [nvarchar](max) NULL,
	[NumberOfAppointments] [int] NULL,
	[NumberOfVideoCalls] [int] NULL,
	[Specialization] [nvarchar](max) NULL,
 CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentProfiles]    Script Date: 01/12/2024 21:44:08 ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[EventId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[EventDateTime] [datetime2](7) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[RepeatFrequency] [int] NOT NULL,
	[RepeatInterval] [int] NOT NULL,
	[RepeatEndDate] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[OriginalEventId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthProfiles]    Script Date: 01/12/2024 21:44:08 ******/
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
/****** Object:  Table [dbo].[Images]    Script Date: 01/12/2024 21:44:08 ******/
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
/****** Object:  Table [dbo].[invalidated_token]    Script Date: 01/12/2024 21:44:08 ******/
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
/****** Object:  Table [dbo].[News]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Author] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notices]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notices](
	[NoticeId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RecipientId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[HasViewed] [bit] NOT NULL,
 CONSTRAINT [PK_Notices] PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminders](
	[ReminderId] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[ReminderOffset] [int] NOT NULL,
	[OffsetUnit] [int] NOT NULL,
	[ReminderTime] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[IsSent] [bit] NOT NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[ReminderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [uniqueidentifier] NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TokenCall]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TokenCall](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[tokenCall] [nvarchar](max) NOT NULL,
	[callerId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TokenCall] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [uniqueidentifier] NOT NULL,
	[dob] [date] NULL,
	[email] [varchar](255) NOT NULL,
	[first_name] [nvarchar](255) NULL,
	[gender] [bit] NULL,
	[last_name] [nvarchar](255) NULL,
	[password] [varchar](255) NOT NULL,
	[phone] [varchar](255) NULL,
	[role] [varchar](255) NULL,
	[status] [varchar](255) NULL,
	[username] [varchar](255) NOT NULL,
	[callerid] [varchar](255) NULL,
	[tokencall] [nvarchar](max) NULL,
	[avatar] [varchar](1000) NULL,
 CONSTRAINT [PK__user__B9BE370F7E22D744] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEvents]    Script Date: 01/12/2024 21:44:08 ******/
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
/****** Object:  Table [dbo].[UserReports]    Script Date: 01/12/2024 21:44:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReports](
	[Id] [uniqueidentifier] NOT NULL,
	[ReporterId] [uniqueidentifier] NOT NULL,
	[ReportType] [nvarchar](max) NOT NULL,
	[ReportDescription] [nvarchar](max) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ResolvedAt] [datetime2](7) NULL,
	[ReportedId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserReports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241103134905_Initial', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241103141151_Doctor-Displayname', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241103142857_DoctorReports', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241103145245_Doctor_Update2', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241104152248_DailyMetric-SpO2', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241107145002_ChangeReport', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241107150345_AddTarget', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241113150303_Schedule', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241113160426_removeAppointmentFK', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241117150522_News-Init', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241126080450_ImageUrl', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241130050026_adddb1', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241130051124_adddb12', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241130051712_adddb123', N'8.0.8')
GO
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'd73cf1d8-c803-409d-8930-00bc25a73ad2', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Consultation for General Check-up với Thanh Pham', N'A general health check-up and consultation', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'12:30:00' AS Time), CAST(N'12:45:00' AS Time), N'https://meet.example.com/cd6bd018-5c5a-4351-899a-1c26f648d730', N'Approved', N'Online', CAST(N'2024-11-30T05:59:54.3360907' AS DateTime2), CAST(N'2024-12-01T06:46:20.2973803' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'4751daf6-ef4b-4db5-96dd-0ced62d2b6b7', N'a83010e0-6450-483a-bf40-55de35daf183', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'Tư vấn về Hoàng với Thanh Pham', N'hoàng', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'12:45:00' AS Time), CAST(N'13:00:00' AS Time), NULL, N'Rejected', N'Online', CAST(N'2024-12-01T06:38:40.7576557' AS DateTime2), CAST(N'2024-12-01T06:49:51.8427954' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'2df83ed3-73b4-4bd8-987f-5d4dc22d58fe', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Đau bụng với Thanh Pham', N'Đau bụng', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:45:00' AS Time), CAST(N'14:00:00' AS Time), NULL, N'Pending', N'Offline', CAST(N'2024-12-01T14:38:23.0901736' AS DateTime2), CAST(N'2024-12-01T21:38:23.0879297' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'ea8ecdf6-e31e-4c42-b681-7372c0e8ab23', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Consultation for General Check-up với Thanh Pham', N'A general health check-up and consultation', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:30:00' AS Time), CAST(N'13:45:00' AS Time), NULL, N'Pending', N'Online', CAST(N'2024-11-30T06:00:01.8460318' AS DateTime2), CAST(N'2024-11-30T13:00:01.8460234' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'5233e77d-4ebb-4346-9631-7b33de565343', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Đau bụng với Thanh Pham', N'Đau bụng', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:45:00' AS Time), CAST(N'15:00:00' AS Time), NULL, N'Pending', N'Offline', CAST(N'2024-12-01T14:39:38.3800512' AS DateTime2), CAST(N'2024-12-01T21:39:38.3800471' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'68efc33c-a422-4b9d-8e1b-ca1421ba0126', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Đặt khám với Thanh Pham', N'Đặt kahm', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:15:00' AS Time), CAST(N'13:30:00' AS Time), NULL, N'Pending', N'Online', CAST(N'2024-11-30T09:10:49.9365180' AS DateTime2), CAST(N'2024-11-30T16:10:49.9360885' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'a1957ace-a2c9-4bc8-91b5-d1a2bbdccc56', N'c7546842-90bd-4f81-9700-6207bb853513', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'Tư vấn về ABC với Dương Nguyễn', N'ABC', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:30:00' AS Time), CAST(N'15:45:00' AS Time), N'https://meet.example.com/85e22acf-e619-4602-9c23-51b5155a32aa', N'Approved', N'Online', CAST(N'2024-12-01T14:01:09.4687507' AS DateTime2), CAST(N'2024-12-01T14:02:39.1768556' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'ca820b84-47d7-4d60-a7aa-d92cdaffa8c8', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Hoang với Thanh Pham', N'', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'14:15:00' AS Time), NULL, N'Pending', N'', CAST(N'2024-11-30T09:12:37.7479639' AS DateTime2), CAST(N'2024-11-30T16:12:37.7479605' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'688647f4-8309-47db-9188-e84be70c5e81', N'a83010e0-6450-483a-bf40-55de35daf183', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'Tư vấn về Chân tay miệng với Thanh Pham', N'Đau, khó chịu', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:00:00' AS Time), CAST(N'13:15:00' AS Time), NULL, N'Pending', N'Offline', CAST(N'2024-11-30T18:13:14.9784032' AS DateTime2), CAST(N'2024-12-01T01:13:14.9761310' AS DateTime2))
INSERT [dbo].[Appointments] ([AppointmentId], [DoctorId], [UserId], [Title], [Description], [Date], [StartTime], [EndTime], [Location], [Status], [Type], [CreatedAt], [UpdatedAt]) VALUES (N'73dc394d-8338-4cac-b78f-f2d6f708b6d4', N'c7546842-90bd-4f81-9700-6207bb853513', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'Tư vấn về abc với Dương Nguyễn', N'aaa', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:45:00' AS Time), CAST(N'16:00:00' AS Time), NULL, N'Rejected', N'Online', CAST(N'2024-12-01T14:04:35.3864733' AS DateTime2), CAST(N'2024-12-01T14:04:49.7788814' AS DateTime2))
GO
INSERT [dbo].[DailyMetrics] ([Id], [UserId], [Weight], [Height], [SystolicBloodPressure], [DiastolicBloodPressure], [HeartRate], [BloodSugar], [BodyTemperature], [Date], [OxygenSaturation]) VALUES (N'0af9287d-33b8-48b1-609c-08dd1214f495', N'f56f167f-af25-4433-ba65-990d64dd0d61', NULL, NULL, 80, 80, 80, 100, NULL, CAST(N'2024-12-01' AS Date), NULL)
INSERT [dbo].[DailyMetrics] ([Id], [UserId], [Weight], [Height], [SystolicBloodPressure], [DiastolicBloodPressure], [HeartRate], [BloodSugar], [BodyTemperature], [Date], [OxygenSaturation]) VALUES (N'5e7668fd-c1ad-42bf-a6b7-7a6f8304cd9d', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', 100, 186, NULL, NULL, 120, NULL, NULL, CAST(N'2024-12-01' AS Date), NULL)
INSERT [dbo].[DailyMetrics] ([Id], [UserId], [Weight], [Height], [SystolicBloodPressure], [DiastolicBloodPressure], [HeartRate], [BloodSugar], [BodyTemperature], [Date], [OxygenSaturation]) VALUES (N'5f9e73f8-16b8-4f6e-82ce-c54f55224bce', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', 30, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-12-01' AS Date), NULL)
GO
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType]) VALUES (N'36004113-03ac-4835-39cc-08dd1121d02a', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'test1', N'string')
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType]) VALUES (N'1cfcdf89-9337-4e46-acc1-08dd11ca114d', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'cnRf8RV1btR2-v9FzgEA7T:APA91bGLA7kieZbKBCQOSeOXAVEOIorncE6464vreIrN35RZMQ0lLba5HATiSdGYICl5eK6rENJysVEm_cgmmF6c_swYJznXtnkYmKqPjBmfVTXQIj8Sz-c', N'web')
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType]) VALUES (N'8e81d3ce-6d03-4038-2853-08dd11d01c77', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'dsKutiR6gON_722-VQ9Zs3:APA91bGoW1c3GMjDSEkvkeavYbteYlALUvRD2R5DtD4pK1uumF2EY6EgaXb-_UTDNodZI2sfGGv8RD2lqPMWSYp1xq8-KA_ZrFD7gYK-NV6_1zPoKcQ2__M', N'web')
GO
INSERT [dbo].[Doctors] ([Id], [UserId], [Description], [ClinicAddress], [ClinicCity], [DisplayName], [HistoryOfWork], [NumberOfAppointments], [NumberOfVideoCalls], [Specialization]) VALUES (N'a83010e0-6450-483a-bf40-55de35daf183', N'bed9177f-c08e-4113-87ff-7546f4cef7be', N'Chuyên đa khoa', N'Cầu giấy', N'Hà Nội', N'Thanh Pham', N'20 năm kinh nghiệm', 10, 5, N'Đa khoa')
INSERT [dbo].[Doctors] ([Id], [UserId], [Description], [ClinicAddress], [ClinicCity], [DisplayName], [HistoryOfWork], [NumberOfAppointments], [NumberOfVideoCalls], [Specialization]) VALUES (N'c7546842-90bd-4f81-9700-6207bb853513', N'ba5c828d-fc25-4ee3-a9d3-e668d706a866', N'Chuyên da liễu', N'Hoàng Mai', N'Hà Nội', N'Dương Nguyễn', N'15 năm kinh nghiệm', 20, 8, N'Da liễu')
GO
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'cf26ed62-7f4c-4f54-89ab-006bfa711d9e', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 1, N'{"title":"Đơn thuốc ngày 01-12-2024","date":"2024-12-01T08:35:32.784Z","diagnose":"a","doctorRecomend":"a","doctor":"a","drug":[{"key":1,"name":"a","quantity":0,"dosage":"a","unit":"a"}]}', N'[]', CAST(N'2024-12-01T15:35:32.5833513' AS DateTime2), CAST(N'2024-12-01T15:35:32.5833534' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'0f5e1c62-de39-4cb0-b759-0e6441e3aca7', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 3, N'{"title":"Xét nghiệm scan từ ảnh","date":"2024-12-01T14:14:20.171Z","doctor":"","drug":[{"name":"WBC","value":"10","unit":"K/uL","result":"normal","key":1},{"name":"NEU","value":80.4,"unit":"%","result":"high","key":2},{"name":"LYM","value":12.6,"unit":"%","result":"low","key":3},{"name":"MONO","value":5.7,"unit":"%","result":"normal","key":4},{"name":"BASO","value":0.33,"unit":"%","result":"normal","key":5},{"name":"EOS","value":0.97,"unit":"%","result":"normal","key":6},{"name":"RBC","value":3.92,"unit":"M/uL","result":"low","key":7},{"name":"HGB","value":11.7,"unit":"g/dL","result":"low","key":8},{"name":"HCT","value":35,"unit":"%","result":"low","key":9},{"name":"MCV","value":89.1,"unit":"fL","result":"normal","key":10},{"name":"MCH","value":32,"unit":"pg","result":"normal","key":11},{"name":"MCHC","value":35.9,"unit":"g/dL","result":"normal","key":12},{"name":"RDW","value":10.7,"unit":"%","result":"low","key":13},{"name":"PLT","value":281,"unit":"K/uL","result":"normal","key":14},{"name":"MPV","value":7.9,"unit":"fL","result":"normal","key":15}]}', N'[]', CAST(N'2024-12-01T21:14:22.8619559' AS DateTime2), CAST(N'2024-12-01T21:14:22.8619931' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'ef72f57b-592d-4cf6-b177-139661c0e0e3', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 1, N'{"title":"Đơn thuốc ngày 01-12-2024","date":"2024-12-01T08:37:48.304Z","diagnose":"a","doctorRecomend":"a","doctor":"a","drug":[]}', N'[]', CAST(N'2024-12-01T15:35:23.4874468' AS DateTime2), CAST(N'2024-12-01T15:35:23.4874489' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'cb36e247-464c-4c25-b070-1cb8d180f449', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 2, N'{}', N'[]', CAST(N'2024-12-01T15:36:55.2563627' AS DateTime2), CAST(N'2024-12-01T15:36:55.2563663' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'a18eca28-a145-445d-8335-21847626c2de', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'bc3c80ec-7a99-4e4d-852f-3e74fb47b613', 3, N'{"title":"Xét nghiệm nước tiểu","date":"2024-12-01T13:38:37.133Z","drug":[{"key":1,"name":"Leukocytes (LEU-BLO)","value":0,"result":"10 - 25","unit":"Leu/UL"},{"key":2,"name":"NIT (Nitrite)","value":0,"result":"0.05 - 0.1","unit":"mg/dL"},{"key":3,"name":"pH (độ pH)","value":0,"result":"0.2 - 1.0","unit":"mg/dL"},{"key":4,"name":"GLU (Glucose - đường huyết)","value":0,"result":"0.4 - 0.8","unit":"mg/dL"},{"key":5,"name":"Ph","value":0,"result":"4,6 - 8","unit":"pH"},{"key":6,"name":"PRO (Protein)","value":0,"result":"7.5 - 20/Âm tính","unit":"mg/dL"},{"key":7,"name":"KET (Ketone)","value":0,"result":"2,5 - 5/ âm tính","unit":"mg/dL"},{"key":8,"name":"ASC","value":0,"result":"5 - 10/âm tính","unit":"mg/dL"},{"key":9,"name":"BIL (Bilirubin)","value":0,"result":"âm tính / 0.4 - 0.8","unit":"mg/dL"}]}', N'[]', CAST(N'2024-12-01T20:38:36.4736143' AS DateTime2), CAST(N'2024-12-01T20:38:36.4736969' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'b6c5725a-1ec5-4983-8978-2e4b8f3cef77', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:47.6870599' AS DateTime2), CAST(N'2024-12-01T01:06:47.6870710' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'00551f0c-76df-44bd-87ed-44083ea73f07', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T21:18:49.2717849' AS DateTime2), CAST(N'2024-12-01T21:18:49.2724661' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'2827e2df-1c39-4f99-880d-760b2b8260be', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:47.3624999' AS DateTime2), CAST(N'2024-12-01T01:06:47.3625117' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'40772c8f-5add-41b8-9304-83ec1de41a16', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 3, N'{"title":"Xét nghiệm ngày 01-12-2024","date":"2024-12-01T08:39:05.564Z","diagnose":"a","doctorRecomend":"a","doctor":"a","drug":[]}', N'[]', CAST(N'2024-12-01T15:39:05.2607177' AS DateTime2), CAST(N'2024-12-01T15:39:05.2607211' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'bf7b2d6a-f750-4d2e-b4de-866c2d857cac', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'bc3c80ec-7a99-4e4d-852f-3e74fb47b613', 3, N'{"date":"2024-12-01T06:20:49.981Z","doctor":"","image":[],"title":"Xét nghiệm scan từ ảnh","drug":[{"name":"Glucose NT","value":"NORM","unit":"mg/dL","result":"<30","key":1},{"name":"Bilirubin","value":"NEG","unit":"mg/dL","result":"<20","key":2},{"name":"Ketone","value":"NEG","unit":"mg/dL","result":"<5","key":3},{"name":"SG","value":"1.006","unit":"","result":"1.005 - 1.022","key":4},{"name":"Blood","value":"200","unit":"u/L","result":"0-5","key":5},{"name":"pH","value":"7","unit":"","result":"4.8 - 7.4","key":6},{"name":"Protein","value":"75","unit":"mg/dL","result":"<10","key":7},{"name":"Urobilinogen","value":"NORM","unit":"","result":"<1mg/dL","key":8},{"name":"Nitrite","value":"NEG","unit":"mg/dL","result":"Negative","key":9},{"name":"Leukocytes","value":"300","unit":"u/L","result":"<10","key":10}]}', N'["3"]', CAST(N'2024-12-01T13:20:51.3464716' AS DateTime2), CAST(N'2024-12-01T13:20:51.3465440' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'8e78b240-0802-40a7-97a5-a762ffa501f2', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:46.7920756' AS DateTime2), CAST(N'2024-12-01T01:06:46.7920838' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'658cd9bb-6284-4ed3-afc8-a7ad02e23cb6', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:47.3750468' AS DateTime2), CAST(N'2024-12-01T01:06:47.3750570' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'1cd369bc-9d1f-47c5-a040-aa4e214b062c', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 3, N'{"date":"2024-12-01T14:11:44.049Z","doctor":"","image":[],"title":"Xét nghiệm scan từ ảnh","drug":[{"name":"WBC","value":9.84,"unit":"K/uL","result":"(4.4 - 10.9)","key":1},{"name":"NEU","value":80.4,"unit":"%","result":"(37.0 - 80.0)","key":2},{"name":"LYM","value":12.6,"unit":"%","result":"(20.0 - 52.0)","key":3},{"name":"MONO","value":5.7,"unit":"%","result":"(4.0 - 8.0)","key":4},{"name":"BASO","value":0.33,"unit":"%","result":"(0.0 - 0.2)","key":5},{"name":"EOS","value":0.97,"unit":"%","result":"(0.0 - 7.0)","key":6},{"name":"RBC","value":3.92,"unit":"M/uL","result":"(4.2 - 6.3)","key":7},{"name":"HGB","value":12.5,"unit":"g/dL","result":"","key":8},{"name":"HCT","value":35,"unit":"%","result":"(36.0 - 52.0)","key":9},{"name":"MCV","value":89.1,"unit":"fL","result":"(80.0 - 97.0)","key":10},{"name":"MCH","value":32,"unit":"pG","result":"(27.0 - 34.0)","key":11},{"name":"MCHC","value":36,"unit":"g/dL","result":"(32.0 - 36.0)","key":12},{"name":"RDW","value":10.7,"unit":"%","result":"(11.5 - 14.5)","key":13},{"name":"PLT","value":281,"unit":"K/uL","result":"(150.0 - 400.0)","key":14},{"name":"MPV","value":7.9,"unit":"fL","result":"(0.0 - 99.8)","key":15},{"name":"Nhóm máu","value":"A","unit":"","result":"","key":16},{"name":"Rh","value":"+","unit":"","result":"","key":17}]}', N'["6"]', CAST(N'2024-12-01T21:11:45.9978528' AS DateTime2), CAST(N'2024-12-01T21:11:45.9978622' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'f7c75a45-2ecf-4a85-8792-aeb52385fe23', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 1, N'{"title":"Đơn thuốc ngày 01-12-2024","date":"2024-11-30T17:07:39.408Z","diagnose":"ABC","doctorRecomend":"A","doctor":"A","drug":[{"key":1,"name":"E","quantity":"1","dosage":"1","unit":"1"},{"key":2,"name":"","quantity":0,"dosage":"","unit":""}]}', N'[]', CAST(N'2024-12-01T00:07:39.0586104' AS DateTime2), CAST(N'2024-12-01T00:07:39.0586776' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'b18347f5-df80-4a3f-9c86-be7bfa54c740', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:47.0718042' AS DateTime2), CAST(N'2024-12-01T01:06:47.0718161' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'2ec8fdad-cac7-4bf6-89ea-caf6793d020e', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:48.1200095' AS DateTime2), CAST(N'2024-12-01T01:06:48.1200131' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'f7fa5b74-f2a1-49de-9260-e2db96968244', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:42.5862001' AS DateTime2), CAST(N'2024-12-01T01:06:42.5868031' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'96732a6d-b111-42a0-84ed-ef057aa54634', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 1, N'{}', N'[]', CAST(N'2024-12-01T01:06:46.1999625' AS DateTime2), CAST(N'2024-12-01T01:06:46.1999744' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'f0e24090-a929-4b63-828f-f772c4b8b90a', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'1d8485b1-c59f-49b5-8347-105753b62d21', 3, N'{"title":"Xét nghiệm scan từ ảnh","date":"2024-12-01T14:10:34.535Z","doctor":"","drug":[{"name":"WBC","value":"9.84","unit":"K/uL","result":"(4.4 - 10.9)","key":1},{"name":"NEU","value":"80.4","unit":"%","result":"(37.0 - 80.0)","key":2},{"name":"LYM","value":"12.6","unit":"%","result":"(12.0 - 50.5)","key":3},{"name":"MONO","value":"5.70","unit":"%","result":"(2.0 - 10.0)","key":4},{"name":"BASO","value":"0.330","unit":"%","result":"(0.0 - 0.2)","key":5},{"name":"EOS","value":"0.970","unit":"%","result":"(0.0 - 5.0)","key":6},{"name":"RBC","value":"3.92","unit":"M/uL","result":"(4.2 - 6.3)","key":7},{"name":"HGB","value":"12.5","unit":"g/dL","result":"(13.0 - 18.0)","key":8},{"name":"HCT","value":"35.0","unit":"%","result":"(40.0 - 54.0)","key":9},{"name":"MCV","value":"89.1","unit":"fL","result":"(80.0 - 97.0)","key":10},{"name":"MCH","value":"32.0","unit":"pg","result":"(28.0 - 32.0)","key":11},{"name":"MCHC","value":"35.9","unit":"g/dL","result":"(32.0 - 36.0)","key":12},{"name":"RDW","value":"10.7","unit":"%","result":"(11.5 - 14.5)","key":13},{"name":"PLT","value":"281","unit":"K/uL","result":"(170 - 380)","key":14},{"name":"MPV","value":"7.90","unit":"fL","result":"(10.0 - 99.8)","key":15},{"key":16,"name":"","value":0,"result":"","unit":""}]}', N'[]', CAST(N'2024-12-01T21:10:38.5500825' AS DateTime2), CAST(N'2024-12-01T21:10:38.5501700' AS DateTime2), 0)
INSERT [dbo].[DocumentProfiles] ([Id], [UserId], [PantientId], [Type], [ContentMedical], [Image], [CreateDate], [LastModifyDate], [Status]) VALUES (N'06ab60b6-ec61-40c3-abc7-f9ffef6b6791', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', 1, N'{"title":"Đơn thuốc ngày 01-12-2024","date":"2024-12-01T08:35:07.094Z","diagnose":"e","doctorRecomend":"e","doctor":"e","drug":[{"key":1,"name":"e","quantity":0,"dosage":"e","unit":"e"}]}', N'[]', CAST(N'2024-12-01T15:35:06.8679171' AS DateTime2), CAST(N'2024-12-01T15:35:06.8679712' AS DateTime2), 0)
GO
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'e3336e8f-408a-43be-8718-006a90ffaa8c', N'Ăn đậu hũ', N'', CAST(N'2024-12-18T00:00:00.0000000' AS DateTime2), CAST(N'12:00:00' AS Time), CAST(N'12:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T13:51:56.1387081' AS DateTime2), NULL, NULL, NULL, N'526da385-4462-4c7b-b0a7-1986e9de5003', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'c78996f5-3ebe-4a3c-b6c2-057a1fe15f05', N'Hoàng Thụy Thái', N'', CAST(N'2024-12-01T00:00:00.0000000' AS DateTime2), CAST(N'13:07:12' AS Time), CAST(N'13:07:12' AS Time), N'', NULL, 0, 1, CAST(N'2024-12-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T06:07:18.4523018' AS DateTime2), NULL, NULL, NULL, N'fa904d31-d827-48a4-8e04-55062a639dfb', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'bf65dfc1-e37d-477e-981a-08dd11d3dd33', N'Cuộc hẹn với bác sĩ Thanh Pham', N'A general health check-up and consultation', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'12:30:00' AS Time), CAST(N'12:45:00' AS Time), N'https://meet.example.com/cd6bd018-5c5a-4351-899a-1c26f648d730', NULL, 0, 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T06:46:20.3305712' AS DateTime2), NULL, NULL, NULL, N'8eb91ef1-4bb1-4c24-a0be-7662fec25586', 2)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'ac617d71-f831-4fe7-2ea1-08dd1210d10c', N'Cuộc hẹn với bác sĩ Dương Nguyễn', N'ABC', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:30:00' AS Time), CAST(N'15:45:00' AS Time), N'https://meet.example.com/85e22acf-e619-4602-9c23-51b5155a32aa', NULL, 0, 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T14:02:39.2147894' AS DateTime2), NULL, NULL, NULL, N'd903941a-dfd4-47bb-a202-aa7a9c751791', 2)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'05dc2c99-0016-4eb4-a5e1-0c389c4d1f2d', N'Giảm cân', N'Ăn ít', CAST(N'2024-11-16T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'08:00:20' AS Time), N'Hoàng Mai, Hà Nội', NULL, 2, 1, CAST(N'2024-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T11:54:20.9712895' AS DateTime2), NULL, NULL, NULL, N'a6243281-262c-46be-b7d2-835e076e63a5', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'86a6ce51-9684-47c1-994e-0d517840b321', N'Đi chạy', N'', CAST(N'2024-12-08T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T18:10:45.7114054' AS DateTime2), NULL, NULL, NULL, N'969f7cce-a667-40b1-94d3-9670c58c9b93', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'3af7eb66-cc6e-4b15-a511-14dc7592bfc8', N'Giảm cân', N'Ăn quá nhiều chất béo', CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Ha Noi', NULL, 2, 1, CAST(N'2024-11-17T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T05:51:08.4052868' AS DateTime2), NULL, NULL, NULL, N'6f732770-2dc9-4a17-8e08-b78f4a5a41ab', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'b57c702f-a9ca-474f-bad8-3fae7682e98e', N'Uống nước', N'Đến giờ uống nước rồi', CAST(N'2024-12-01T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T18:09:40.1661071' AS DateTime2), NULL, NULL, NULL, N'cd5bc055-f4e3-43b8-9d63-040ef1871a46', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'598cc23b-6e39-44e6-8a95-4d848a9b2f9c', N'Đi chạy', N'', CAST(N'2024-12-09T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T18:10:45.7114054' AS DateTime2), NULL, NULL, NULL, N'969f7cce-a667-40b1-94d3-9670c58c9b93', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'd5561b8d-a95e-4cbe-9c30-5cd0cd429bed', N'Ăn kiêng', N'', CAST(N'2024-12-19T12:00:00.0000000' AS DateTime2), CAST(N'12:00:00' AS Time), CAST(N'13:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T13:51:56.1387081' AS DateTime2), NULL, NULL, NULL, N'526da385-4462-4c7b-b0a7-1986e9de5003', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'2288c2ee-3574-496a-a9c3-70d57b923d7c', N'e', N'e', CAST(N'2024-12-03T15:41:58.0000000' AS DateTime2), CAST(N'15:41:58' AS Time), CAST(N'15:42:01' AS Time), N'A', NULL, 2, 1, CAST(N'2024-12-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T08:42:12.8345547' AS DateTime2), NULL, NULL, NULL, N'3586aec6-0c70-45e1-b847-96191924f09a', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'6e7af539-ebc5-494e-87c1-89f6d56165d9', N'Ăn kiêng', N'', CAST(N'2024-12-17T12:00:00.0000000' AS DateTime2), CAST(N'12:00:00' AS Time), CAST(N'13:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T13:51:56.1387081' AS DateTime2), NULL, NULL, NULL, N'526da385-4462-4c7b-b0a7-1986e9de5003', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'7b7098c5-f889-4b4b-b755-ad378ef35d16', N'Giảm cân', N'Ăn ít', CAST(N'2024-11-17T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'08:00:20' AS Time), N'Hoàng Mai, Hà Nội', NULL, 2, 1, CAST(N'2024-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T11:54:20.9712895' AS DateTime2), NULL, NULL, NULL, N'a6243281-262c-46be-b7d2-835e076e63a5', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'd654fc54-18f7-432d-bfa8-ad398dd7b99f', N'e', N'e', CAST(N'2024-12-04T15:41:58.0000000' AS DateTime2), CAST(N'15:41:58' AS Time), CAST(N'15:42:01' AS Time), N'A', NULL, 2, 1, CAST(N'2024-12-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T08:42:12.8345547' AS DateTime2), NULL, NULL, NULL, N'3586aec6-0c70-45e1-b847-96191924f09a', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'7f67b465-45db-423e-a0f8-b77ff82ddadc', N'e', N'e', CAST(N'2024-12-01T15:41:58.0000000' AS DateTime2), CAST(N'15:41:58' AS Time), CAST(N'15:42:01' AS Time), N'A', NULL, 2, 1, CAST(N'2024-12-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T08:42:12.8345547' AS DateTime2), NULL, NULL, NULL, N'3586aec6-0c70-45e1-b847-96191924f09a', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'154179d4-81f1-4447-8492-b79d4424bafc', N'Giảm cân', N'Ăn ít', CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'10:00:20' AS Time), N'Hoàng Mai, Hà Nội', NULL, 2, 1, CAST(N'2024-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T11:51:23.0799036' AS DateTime2), NULL, NULL, NULL, N'82e6d997-e7e4-4382-8345-d20b168abdad', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'b9931822-846b-4ea9-87da-c1af004be5e6', N'e', N'e', CAST(N'2024-12-02T15:41:58.0000000' AS DateTime2), CAST(N'15:41:58' AS Time), CAST(N'15:42:01' AS Time), N'A', NULL, 2, 1, CAST(N'2024-12-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T08:42:12.8345547' AS DateTime2), NULL, NULL, NULL, N'3586aec6-0c70-45e1-b847-96191924f09a', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'4436e5b0-2fb8-46fe-a049-cb6370b5fda4', N'Giảm cân', N'Ăn ít', CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'08:00:20' AS Time), N'Hoàng Mai, Hà Nội', NULL, 2, 1, CAST(N'2024-11-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-01T11:54:20.9712895' AS DateTime2), NULL, NULL, NULL, N'a6243281-262c-46be-b7d2-835e076e63a5', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'ca5f527f-a5bf-438e-85c2-e6b14abea95e', N'Giảm cân', N'Ăn quá nhiều chất béo', CAST(N'2024-11-17T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Ha Noi', NULL, 2, 1, CAST(N'2024-11-17T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T05:51:08.4052868' AS DateTime2), NULL, NULL, NULL, N'6f732770-2dc9-4a17-8e08-b78f4a5a41ab', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'a8e23150-5ed1-4688-bd16-e84ef4361f71', N'Uống nước', N'Đến giờ uống nước rồi', CAST(N'2024-12-02T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), N'', NULL, 2, 1, CAST(N'2024-12-03T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T18:09:40.1661071' AS DateTime2), NULL, NULL, NULL, N'cd5bc055-f4e3-43b8-9d63-040ef1871a46', 1)
INSERT [dbo].[Events] ([EventId], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [RepeatFrequency], [RepeatInterval], [RepeatEndDate], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [OriginalEventId], [Type]) VALUES (N'54e33da3-cc86-42dc-b22d-f26cf131377e', N'Giảm cân', N'Ăn quá nhiều chất béo', CAST(N'2024-11-16T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Ha Noi', NULL, 2, 1, CAST(N'2024-11-17T00:00:00.0000000' AS DateTime2), CAST(N'2024-11-30T05:51:08.4052868' AS DateTime2), NULL, NULL, NULL, N'6f732770-2dc9-4a17-8e08-b78f4a5a41ab', 1)
GO
INSERT [dbo].[HealthProfiles] ([Id], [UserId], [ProfileCode], [FullName], [DateOfBirth], [Gender], [Residence], [Note], [SharedStatus], [CreateDate], [LastModifyDate]) VALUES (N'1d8485b1-c59f-49b5-8347-105753b62d21', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'abc', N'Vũ Văn Mạnh', CAST(N'2024-12-18' AS Date), 2, N'Hà Nội', N'TEst', 0, CAST(N'2024-12-01T01:02:19.5810331' AS DateTime2), CAST(N'2024-12-01T01:02:19.5810400' AS DateTime2))
INSERT [dbo].[HealthProfiles] ([Id], [UserId], [ProfileCode], [FullName], [DateOfBirth], [Gender], [Residence], [Note], [SharedStatus], [CreateDate], [LastModifyDate]) VALUES (N'bc3c80ec-7a99-4e4d-852f-3e74fb47b613', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'abc', N'Hoàng', CAST(N'2024-12-09' AS Date), 1, N'Thụy', N'', 3, CAST(N'2024-12-01T12:42:01.7518984' AS DateTime2), CAST(N'2024-12-01T12:42:01.7519624' AS DateTime2))
INSERT [dbo].[HealthProfiles] ([Id], [UserId], [ProfileCode], [FullName], [DateOfBirth], [Gender], [Residence], [Note], [SharedStatus], [CreateDate], [LastModifyDate]) VALUES (N'3250015b-cd3a-4a4c-a4e4-4aeb08f2caef', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'abc', N'New User', CAST(N'2024-12-17' AS Date), 2, N'here', N'me', 0, CAST(N'2024-12-01T00:03:58.1428986' AS DateTime2), CAST(N'2024-12-01T00:03:58.1429433' AS DateTime2))
INSERT [dbo].[HealthProfiles] ([Id], [UserId], [ProfileCode], [FullName], [DateOfBirth], [Gender], [Residence], [Note], [SharedStatus], [CreateDate], [LastModifyDate]) VALUES (N'f26ed9e1-dfc0-4089-8c4b-f2299047f89c', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'abc', N'Dương Nguyễn', CAST(N'2003-12-23' AS Date), 1, N'hà nội', N'Hồ sơ da dày', 0, CAST(N'2024-12-01T12:24:28.9800075' AS DateTime2), CAST(N'2024-12-01T12:24:28.9803332' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (1, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1732983948/jsa954b2c5hbdu4d34em.jpg', N'jsa954b2c5hbdu4d34em')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (2, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733033982/gixtoyhure3r3xvdp9cs.png', N'gixtoyhure3r3xvdp9cs')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (3, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733034051/uxfy9rgvpwgmfxwjxn3r.png', N'uxfy9rgvpwgmfxwjxn3r')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (4, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733061772/simlhhinzjzb3o4idr85.jpg', N'simlhhinzjzb3o4idr85')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (5, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733062237/ciznz9hy0b5puvdo6hrm.jpg', N'ciznz9hy0b5puvdo6hrm')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (6, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733062304/doqy9se77fplqtjxidj6.jpg', N'doqy9se77fplqtjxidj6')
INSERT [dbo].[Images] ([Id], [ImageUrl], [PublicId]) VALUES (7, N'http://res.cloudinary.com/diwqq5hc6/image/upload/v1733062461/tm9gsarmsg7uylqyafxw.jpg', N'tm9gsarmsg7uylqyafxw')
SET IDENTITY_INSERT [dbo].[Images] OFF
GO
INSERT [dbo].[News] ([Id], [Title], [Author], [Content], [Category], [CreatedAt], [UpdatedAt], [ImageUrl]) VALUES (N'782e9fd0-e3c8-48b5-188a-08dd115baec5', N'New #1', N'Author', N'### ABC', N'News', CAST(N'2024-11-30T16:26:03.0366667' AS DateTime2), NULL, N'http://localhost:5217/api/Images/img/1')
GO
INSERT [dbo].[Notices] ([NoticeId], [UserId], [RecipientId], [Message], [CreatedAt], [HasViewed]) VALUES (N'4bb0c065-ec76-4fb7-b6a3-1ad25acc3938', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'hello2 ', CAST(N'2024-12-01T13:21:07.5378245' AS DateTime2), 1)
INSERT [dbo].[Notices] ([NoticeId], [UserId], [RecipientId], [Message], [CreatedAt], [HasViewed]) VALUES (N'c7e25e3d-bfcc-4d9d-b517-8dde579c243b', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', N'hello2 ', CAST(N'2024-12-01T13:19:21.2532430' AS DateTime2), 0)
GO
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'94a2d901-c238-406b-aa22-04a661a98174', N'54e33da3-cc86-42dc-b22d-f26cf131377e', 2, 3, CAST(N'2024-11-14T09:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'17ad754a-f957-4ad8-9f6a-0da1707af528', N'ca5f527f-a5bf-438e-85c2-e6b14abea95e', 1, 2, CAST(N'2024-11-17T08:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'e5da3ce4-7dc3-4958-b55a-319917c0b0a4', N'b57c702f-a9ca-474f-bad8-3fae7682e98e', 1, 5, CAST(N'2024-12-01T09:00:00.0000000' AS DateTime2), N'Reminder for event: Uống nước', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'84e72f5b-62f2-4672-8bb8-321af7f6ce60', N'3af7eb66-cc6e-4b15-a511-14dc7592bfc8', 1, 2, CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'd9949297-8bc3-4ede-a44c-5a3b3cfb6645', N'ca5f527f-a5bf-438e-85c2-e6b14abea95e', 2, 3, CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'34e6ee76-a699-4333-8a7f-67d65d2df323', N'7b7098c5-f889-4b4b-b755-ad378ef35d16', 14, 1, CAST(N'2024-11-17T08:46:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'af306f0b-e139-4e82-bf0e-700447a2e7d0', N'bf65dfc1-e37d-477e-981a-08dd11d3dd33', 0, 0, CAST(N'2024-12-03T12:30:00.0000000' AS DateTime2), N'Bạn có lịch hẹn với bác sĩ Thanh Pham vào lúc 12:30 ngày 04/12/2024. Địa điểm: https://meet.example.com/cd6bd018-5c5a-4351-899a-1c26f648d730.', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'e4e89256-e375-4718-8076-77cd9ab23df9', N'54e33da3-cc86-42dc-b22d-f26cf131377e', 1, 2, CAST(N'2024-11-16T08:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'7590b072-929a-4d6d-b4ef-8a6595c6cf69', N'd654fc54-18f7-432d-bfa8-ad398dd7b99f', 1, 30, CAST(N'2024-12-04T15:41:58.0000000' AS DateTime2), N'Reminder for event: e', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'41f9d85b-5b0c-49a2-ac91-90fb4d1a6cae', N'598cc23b-6e39-44e6-8a95-4d848a9b2f9c', 1, 5, CAST(N'2024-12-09T09:00:00.0000000' AS DateTime2), N'Reminder for event: Đi chạy', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'22a30a76-e7ff-4e2a-9cf9-93442bb2b7a4', N'7f67b465-45db-423e-a0f8-b77ff82ddadc', 1, 30, CAST(N'2024-12-01T15:41:58.0000000' AS DateTime2), N'Reminder for event: e', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'3282d0c6-2d59-4b73-ba73-abf107c73fe9', N'ac617d71-f831-4fe7-2ea1-08dd1210d10c', 0, 0, CAST(N'2024-12-01T15:30:00.0000000' AS DateTime2), N'Bạn có lịch hẹn với bác sĩ Dương Nguyễn vào lúc 15:30 ngày 02-12-2024. Địa điểm: https://meet.example.com/85e22acf-e619-4602-9c23-51b5155a32aa.', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'ec9b9388-3a35-45f7-9853-ac362c6666e2', N'154179d4-81f1-4447-8492-b79d4424bafc', 10, 2, CAST(N'2024-11-14T23:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'133c28af-2be8-445b-a1a4-b34b0989a8ab', N'4436e5b0-2fb8-46fe-a049-cb6370b5fda4', 10, 2, CAST(N'2024-11-14T23:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'1f477de4-0396-436e-9136-c7bca0885e9a', N'05dc2c99-0016-4eb4-a5e1-0c389c4d1f2d', 10, 2, CAST(N'2024-11-15T23:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'ff8f2845-0149-4f89-8eb8-c84bbec0c506', N'7b7098c5-f889-4b4b-b755-ad378ef35d16', 10, 2, CAST(N'2024-11-16T23:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'5fcd80b9-8c63-49b5-946c-c877d6ae8240', N'05dc2c99-0016-4eb4-a5e1-0c389c4d1f2d', 14, 1, CAST(N'2024-11-16T08:46:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'c079bcb3-7428-44ca-9c6b-cb3aa121d1d1', N'3af7eb66-cc6e-4b15-a511-14dc7592bfc8', 2, 3, CAST(N'2024-11-13T09:00:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'6bbc613f-7c16-4424-b252-ccca5b68dedb', N'4436e5b0-2fb8-46fe-a049-cb6370b5fda4', 14, 1, CAST(N'2024-11-15T08:46:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'd57c8f45-2d93-4b45-8de6-d98582b1584d', N'b9931822-846b-4ea9-87da-c1af004be5e6', 1, 30, CAST(N'2024-12-02T15:41:58.0000000' AS DateTime2), N'Reminder for event: e', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'a5f67d5d-2e6c-44cc-bf7d-dfe1f2537457', N'86a6ce51-9684-47c1-994e-0d517840b321', 1, 5, CAST(N'2024-12-08T09:00:00.0000000' AS DateTime2), N'Reminder for event: Đi chạy', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'76d3b616-0432-4089-a9d8-e24d8fea7d5d', N'a8e23150-5ed1-4688-bd16-e84ef4361f71', 1, 5, CAST(N'2024-12-02T09:00:00.0000000' AS DateTime2), N'Reminder for event: Uống nước', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'967a63ba-251f-44ee-9a8d-e360207f11cd', N'154179d4-81f1-4447-8492-b79d4424bafc', 14, 1, CAST(N'2024-11-15T08:46:00.0000000' AS DateTime2), N'Reminder for event: Giảm cân', 0)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderOffset], [OffsetUnit], [ReminderTime], [Message], [IsSent]) VALUES (N'43e4e048-7fbb-4b4f-a55b-faa55f0bc826', N'2288c2ee-3574-496a-a9c3-70d57b923d7c', 1, 30, CAST(N'2024-12-03T15:41:58.0000000' AS DateTime2), N'Reminder for event: e', 0)
GO
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'bd5ed2b7-522f-4c90-942d-05303ad1e446', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'10:00:00' AS Time), CAST(N'10:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'9a9d69b7-b846-4e01-ad28-09bc09f0c51b', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'08:30:00' AS Time), CAST(N'09:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'68a2afe6-addd-4ebb-942a-12cf5749d007', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'20:00:00' AS Time), CAST(N'20:15:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'5dfd2f8d-7b3d-44f6-9289-1b261dcd40de', N'c7546842-90bd-4f81-9700-6207bb853513', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:45:00' AS Time), CAST(N'16:00:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'2fe1df7c-52d5-4647-82b2-2242d1990bc4', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'20:15:00' AS Time), CAST(N'20:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'16cd6b78-b859-4976-a8de-2f0479ead751', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'14:30:00' AS Time), CAST(N'15:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'01c3ee35-334d-4f76-827b-3016123d267f', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'14:15:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'3fbf5091-9669-49fb-9a3e-38c9497b6705', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:00:00' AS Time), CAST(N'13:15:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'71dc17f4-f465-422d-a19c-4b34c43a817b', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:15:00' AS Time), CAST(N'14:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'0fe94454-6d6c-465b-9496-51d2b98bae4c', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'20:45:00' AS Time), CAST(N'21:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'15d20ee9-fe7a-4628-a056-58a64dd818dd', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'10:30:00' AS Time), CAST(N'11:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'31c50eda-7933-41a3-b322-60ce913fc72b', N'c7546842-90bd-4f81-9700-6207bb853513', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:30:00' AS Time), CAST(N'15:45:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'e0d61ed3-e05d-43da-b7cc-64a7f5801818', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'09:30:00' AS Time), CAST(N'10:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'0e3a7e32-d8bc-492a-9e64-681bfb688a60', N'c7546842-90bd-4f81-9700-6207bb853513', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:00:00' AS Time), CAST(N'15:15:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'e9710891-db2f-440a-89cb-68ad161dee4c', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'12:00:00' AS Time), CAST(N'12:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'8f2fb879-97f8-4de2-87d7-6af6eedbe879', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'11:00:00' AS Time), CAST(N'11:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'547d36d2-e5ce-45b2-8fc4-76583ea431c4', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'20:30:00' AS Time), CAST(N'20:45:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'cab57cd3-93f6-4c37-8eae-7a60d3cf6b20', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:30:00' AS Time), CAST(N'13:45:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'944bc8bf-b397-4499-8c72-7a6d1b08a8ac', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'12:30:00' AS Time), CAST(N'12:45:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'80bf93a0-6f97-40af-b398-80e592bcc3ef', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'09:30:00' AS Time), CAST(N'10:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'cf86b468-30e6-430e-8af9-80f96fcbb572', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'08:00:00' AS Time), CAST(N'08:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'bd0b850c-6253-4acd-8b56-8453462f85d0', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:45:00' AS Time), CAST(N'14:00:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'1f5b8cde-62f0-44cd-9eef-894b048c2472', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'19:45:00' AS Time), CAST(N'20:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'95b01c89-8bf6-41d7-9ec5-94e2e94630b7', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'11:30:00' AS Time), CAST(N'12:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'e3b64105-e57a-471e-9a07-a42cf4d7246b', N'c7546842-90bd-4f81-9700-6207bb853513', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'15:15:00' AS Time), CAST(N'15:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'5a005482-0b32-4884-b391-a7f9d49322c3', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:45:00' AS Time), CAST(N'15:00:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'94c66cb8-0285-49e9-a300-a938043812a2', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'13:00:00' AS Time), CAST(N'13:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'c2cb8f4b-56ed-45a9-be3c-ae0b36f51992', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'14:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'287d9298-a8dc-4156-b58c-af0e69ae5ec0', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'13:15:00' AS Time), CAST(N'13:30:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'bd0dab89-25f9-4418-9b2a-b6433d26da11', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'12:45:00' AS Time), CAST(N'13:00:00' AS Time), N'Unavailable')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'7e780f83-9498-4886-934b-bd0f6b97f86e', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'19:30:00' AS Time), CAST(N'19:45:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'7f6639c3-3170-4292-ae57-dcf9910346d1', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-10T00:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'0ed298eb-8004-4f4c-bef5-e84b8edb4cb6', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'12:30:00' AS Time), CAST(N'13:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'42aa3b67-cec7-4ef7-88b9-edd64b4c4e2f', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-16T00:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'51c963da-3341-4a86-925b-f6dd48414f0b', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-02T00:00:00.0000000' AS DateTime2), CAST(N'13:30:00' AS Time), CAST(N'14:00:00' AS Time), N'Available')
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status]) VALUES (N'7f6776b1-1bd1-4e21-957b-ffc09be4ba01', N'a83010e0-6450-483a-bf40-55de35daf183', CAST(N'2024-12-04T00:00:00.0000000' AS DateTime2), CAST(N'14:30:00' AS Time), CAST(N'14:45:00' AS Time), N'Available')
GO
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'e8b3de98-df21-4ec6-b2b1-102383c90de1', NULL, N'test123@gmail.com', N'Manh', NULL, N'Vu', N'$2a$10$du4go3RbPHCcjoIvNNLO0eB5P1c22JTOa5XlBfIk6r4IrWlG3bc02', N'0353271947', N'CUSTOMER', N'INACTIVE', N'manhvv12zz', NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', NULL, N'hoangthuy.forjob@gmail.com', N'Thụy', NULL, N'Hoàng', N'$2a$10$wXKu2RER0Lwuc2LthhKr0eZOH6zluSfjyhOfhFMYZOWUWQwckrg.C', N'0385319701', N'CUSTOMER', N'ACTIVE', N'username', NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'2b9713c8-4860-4b09-ba00-1dcf1e7672b3', NULL, N'test@gmail.com', N'Manh', NULL, N'Vu', N'$2a$10$c5ApMUKgXfNoGThI/m.iBuo6auaogBHLLOKoKK8hWjTZ/qtjp1c.e', N'0353271947', N'CUSTOMER', N'INACTIVE', N'manhvv12', N'', N'', NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'624b03db-fe5b-4a62-bf85-738eb2516143', NULL, N'admin@gmail.com', NULL, NULL, NULL, N'$2a$10$G.i5OQWtivYO5ZKFFleERejaepS4jcBsrNLVfMDnjVzUdmi338OXi', NULL, N'ADMIN', N'ACTIVE', N'admin', N'', N'', NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'bed9177f-c08e-4113-87ff-7546f4cef7be', NULL, N'duongnthn2312@gmail.com', N'Thanh', NULL, N'Pham', N'$2a$10$jWkbv8kzdVIO4pCqX08VgeUSonU1wQyJBmMLRbQgE.gGe3igOwQga', N'0398896461', N'DOCTOR', N'ACTIVE', N'manhvv14', N'user1', N'eyJjdHkiOiJzdHJpbmdlZS1hcGk7dj0xIiwidHlwIjoiSldUIiwiYWxnIjoiSFMyNTYifQ.eyJqdGkiOiJTSy4wLmlUQjgyU1JYbVBiUlcxem16R0tlUG5Xd1ZzVXV4bzItMTczMjY5NjQyMiIsImlzcyI6IlNLLjAuaVRCODJTUlhtUGJSVzF6bXpHS2VQbld3VnNVdXhvMiIsImV4cCI6MTczNTI4ODQyMiwidXNlcklkIjoidXNlcjEifQ.JiTQwCDxU0Cf1NluHpIgmfeEhvp6LWbKF2j0S9yLSbE', NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'f56f167f-af25-4433-ba65-990d64dd0d61', NULL, N'manhvv15@gmail.com', N'Manh', NULL, N'Vu', N'$2a$10$hkpdJVQkfsLNlI4XUecWQOCJKi9nm2MQuX2ya6Qrf39nkPiXcXaNa', N'0353271947', N'CUSTOMER', N'ACTIVE', N'manhvv15', N'user4', N'eyJjdHkiOiJzdHJpbmdlZS1hcGk7dj0xIiwidHlwIjoiSldUIiwiYWxnIjoiSFMyNTYifQ.eyJqdGkiOiJTSy4wLmlUQjgyU1JYbVBiUlcxem16R0tlUG5Xd1ZzVXV4bzItMTczMjcyODE0OCIsImlzcyI6IlNLLjAuaVRCODJTUlhtUGJSVzF6bXpHS2VQbld3VnNVdXhvMiIsImV4cCI6MTczNTMyMDE0OCwidXNlcklkIjoidXNlcjQifQ.6ZqML7keGhEQ3J68Nmvnz6FbZ4mfI_1urL-4Qvl6rTU', NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'ba5c828d-fc25-4ee3-a9d3-e668d706a866', NULL, N'manhvvv15@gmail.com', N'Duong', NULL, N'Nguyen', N'$2a$10$uvwSV6fSjqhuQwrpfbwmzeaUN2IhBKtc9P7qbrVHidhY9NoEFVkSO', N'0398896461', N'DOCTOR', N'ACTIVE', N'manhvv13', N'user2', N'eyJjdHkiOiJzdHJpbmdlZS1hcGk7dj0xIiwidHlwIjoiSldUIiwiYWxnIjoiSFMyNTYifQ.eyJqdGkiOiJTSy4wLmlUQjgyU1JYbVBiUlcxem16R0tlUG5Xd1ZzVXV4bzItMTczMjY5NjQzNyIsImlzcyI6IlNLLjAuaVRCODJTUlhtUGJSVzF6bXpHS2VQbld3VnNVdXhvMiIsImV4cCI6MTczNTI4ODQzNywidXNlcklkIjoidXNlcjIifQ.LueU1onrDgn8N5QLKRVDrT8YzdOcTjb9rz4xyOw_I-0', NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'8c2f1bc1-c364-431d-8deb-e7a2b955cb4c', NULL, N'duongn1thn2312@gmail.com', N'Thụy khoản', NULL, N'Để tét', N'$2a$10$LDHqdHvriW9fwetuXtAsM.emHkX9R/FXuk.KHCWSWx.j4Yr8nFxWO', N'0398896461', N'CUSTOMER', N'INACTIVE', N'manhvv1233', NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username], [callerid], [tokencall], [avatar]) VALUES (N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', NULL, N'thanhlong2704031@gmail.com', N'b', NULL, N'a', N'$2a$10$GAABJbbQTlF7LEuNMJGHluKaoQ.3445oWC79krjSv4KExv8jYJ3ra', N'0333333333', N'CUSTOMER', N'ACTIVE', N'newUser1', NULL, NULL, NULL)
GO
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'5ff72c31-4e9c-45e7-b6a8-079a0538586f', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'7b7098c5-f889-4b4b-b755-ad378ef35d16', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'50a9c6da-2bc6-4f6c-8033-138621b9e808', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'ac617d71-f831-4fe7-2ea1-08dd1210d10c', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'6142ff24-3914-4372-8af5-17c8f768d3c1', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'154179d4-81f1-4447-8492-b79d4424bafc', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'26ece8f6-58b6-4cac-8cc6-22716b769742', N'aea413ca-5a8f-4aec-b86e-1b40c7c84a16', N'c78996f5-3ebe-4a3c-b6c2-057a1fe15f05', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'1977d92f-a663-43ed-9f81-2e480266fc77', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'3af7eb66-cc6e-4b15-a511-14dc7592bfc8', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'87ed1d53-884c-4714-9fff-311d029c382e', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'bf65dfc1-e37d-477e-981a-08dd11d3dd33', 0, 0)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'b5912304-2c5f-40f8-9270-38bb64bbf1a6', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'7f67b465-45db-423e-a0f8-b77ff82ddadc', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'6cfb547c-cdfc-45d9-9b50-443942765d5b', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'd654fc54-18f7-432d-bfa8-ad398dd7b99f', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'c4fcc527-0e39-4b59-9bb6-4f59afe0e094', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'e3336e8f-408a-43be-8718-006a90ffaa8c', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'46f5c3ab-cd74-4c5e-ace6-5afcda924953', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'54e33da3-cc86-42dc-b22d-f26cf131377e', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'95bb21d5-9241-495e-a62b-5ff2027ffbbe', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'b57c702f-a9ca-474f-bad8-3fae7682e98e', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'0916f556-1831-48aa-a3fd-792c1ec16cf8', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'd5561b8d-a95e-4cbe-9c30-5cd0cd429bed', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'780a327f-5dac-43c1-aab7-7dcd4a605681', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'2288c2ee-3574-496a-a9c3-70d57b923d7c', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'6a6640d9-660e-4295-a676-a6112186cfaa', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'86a6ce51-9684-47c1-994e-0d517840b321', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'4dfabb85-c57b-41a4-a707-a9b4d8ae212d', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'4436e5b0-2fb8-46fe-a049-cb6370b5fda4', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'51d0c385-d9e0-4eef-ab55-b1c80eec27e3', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'ca5f527f-a5bf-438e-85c2-e6b14abea95e', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'84b65f9a-e8d4-4746-9232-b497d081027b', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'05dc2c99-0016-4eb4-a5e1-0c389c4d1f2d', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'bc5625b4-2150-4bee-8ecc-c7a76333f8d3', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'a8e23150-5ed1-4688-bd16-e84ef4361f71', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'a9b324cc-caa9-4535-8f34-d332c43d6d9e', N'd95d1ce5-4de2-4498-beda-ebdcbbde6aec', N'b9931822-846b-4ea9-87da-c1af004be5e6', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'feb74f1d-f022-44ae-9f32-d6a272aa6839', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'6e7af539-ebc5-494e-87c1-89f6d56165d9', 1, 1)
INSERT [dbo].[UserEvents] ([UserEventId], [UserId], [EventId], [IsAccepted], [IsOrganizer]) VALUES (N'b8338cf3-95d5-4631-b04e-f973d7471a6e', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'598cc23b-6e39-44e6-8a95-4d848a9b2f9c', 1, 1)
GO
INSERT [dbo].[UserReports] ([Id], [ReporterId], [ReportType], [ReportDescription], [Status], [CreatedAt], [ResolvedAt], [ReportedId]) VALUES (N'e55963bd-038e-46df-9520-34fa7241802e', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'doctor', N'Stinky', 0, CAST(N'2024-11-30T16:20:56.2935464' AS DateTime2), NULL, N'c7546842-90bd-4f81-9700-6207bb853513')
INSERT [dbo].[UserReports] ([Id], [ReporterId], [ReportType], [ReportDescription], [Status], [CreatedAt], [ResolvedAt], [ReportedId]) VALUES (N'a8630774-0030-4afc-a36d-7f77c0229190', N'624b03db-fe5b-4a62-bf85-738eb2516143', N'news', N'EEEEEEEEEEEEEEEE', 0, CAST(N'2024-11-30T16:28:10.5554652' AS DateTime2), NULL, N'782e9fd0-e3c8-48b5-188a-08dd115baec5')
INSERT [dbo].[UserReports] ([Id], [ReporterId], [ReportType], [ReportDescription], [Status], [CreatedAt], [ResolvedAt], [ReportedId]) VALUES (N'963cb138-59f1-4c2e-b906-bf649be4f59c', N'f56f167f-af25-4433-ba65-990d64dd0d61', N'doctor', N'+1 report', 0, CAST(N'2024-12-01T14:40:46.1182474' AS DateTime2), NULL, N'a83010e0-6450-483a-bf40-55de35daf183')
GO
/****** Object:  Index [IX_Appointments_DoctorId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_DoctorId] ON [dbo].[Appointments]
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Appointments_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_Appointments_UserId] ON [dbo].[Appointments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DailyMetrics_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_DailyMetrics_UserId] ON [dbo].[DailyMetrics]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Doctors_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Doctors_UserId] ON [dbo].[Doctors]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DocumentProfiles_PantientId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_DocumentProfiles_PantientId] ON [dbo].[DocumentProfiles]
(
	[PantientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HealthProfiles_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_HealthProfiles_UserId] ON [dbo].[HealthProfiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notices_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_Notices_UserId] ON [dbo].[Notices]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reminders_EventId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_Reminders_EventId] ON [dbo].[Reminders]
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_DoctorId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_DoctorId] ON [dbo].[Schedules]
(
	[DoctorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK5c856itaihtmi69ni04cmpc4m]    Script Date: 01/12/2024 21:44:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK5c856itaihtmi69ni04cmpc4m] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UKhl4ga9r00rh51mdaf20hmnslt]    Script Date: 01/12/2024 21:44:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UKhl4ga9r00rh51mdaf20hmnslt] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserEvents_EventId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserEvents_EventId] ON [dbo].[UserEvents]
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserEvents_UserId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserEvents_UserId] ON [dbo].[UserEvents]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserReports_ReporterId]    Script Date: 01/12/2024 21:44:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserReports_ReporterId] ON [dbo].[UserReports]
(
	[ReporterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT (N'') FOR [ClinicCity]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT (N'') FOR [DisplayName]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT (N'') FOR [HistoryOfWork]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((0)) FOR [NumberOfAppointments]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((0)) FOR [NumberOfVideoCalls]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT (N'') FOR [Specialization]
GO
ALTER TABLE [dbo].[Events] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [OriginalEventId]
GO
ALTER TABLE [dbo].[Events] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[News] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Notices] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Notices] ADD  DEFAULT ((0)) FOR [HasViewed]
GO
ALTER TABLE [dbo].[UserReports] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ReportedId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Doctors_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Doctors_DoctorId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_user_UserId]
GO
ALTER TABLE [dbo].[DailyMetrics]  WITH CHECK ADD  CONSTRAINT [FK_DailyMetrics_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DailyMetrics] CHECK CONSTRAINT [FK_DailyMetrics_user_UserId]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD  CONSTRAINT [FK_Doctors_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Doctors] CHECK CONSTRAINT [FK_Doctors_user_UserId]
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
ALTER TABLE [dbo].[Notices]  WITH CHECK ADD  CONSTRAINT [FK_Notices_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notices] CHECK CONSTRAINT [FK_Notices_user_UserId]
GO
ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [FK_Reminders_Events_EventId]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Doctors_DoctorId] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Doctors_DoctorId]
GO
ALTER TABLE [dbo].[UserEvents]  WITH CHECK ADD  CONSTRAINT [FK_UserEvents_Events_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserEvents] CHECK CONSTRAINT [FK_UserEvents_Events_EventId]
GO
ALTER TABLE [dbo].[UserEvents]  WITH CHECK ADD  CONSTRAINT [FK_UserEvents_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserEvents] CHECK CONSTRAINT [FK_UserEvents_user_UserId]
GO
ALTER TABLE [dbo].[UserReports]  WITH CHECK ADD  CONSTRAINT [FK_UserReports_user_ReporterId] FOREIGN KEY([ReporterId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[UserReports] CHECK CONSTRAINT [FK_UserReports_user_ReporterId]
GO
ALTER TABLE [dbo].[chatMessage]  WITH CHECK ADD CHECK  (([senderType]='USER' OR [senderType]='AI'))
GO
USE [master]
GO
ALTER DATABASE [DOAN] SET  READ_WRITE 
GO
