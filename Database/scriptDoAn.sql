USE [master]
GO
CREATE DATABASE [DoAn]
 USE [DoAn]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 11/14/2024 10:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](1000) NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[Location] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Date] [date] NULL,
	[Type] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DailyMetrics]    Script Date: 11/14/2024 10:56:44 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceTokens]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[Doctors]    Script Date: 11/14/2024 10:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DisplayName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ClinicAddress] [nvarchar](255) NOT NULL,
	[ClinicCity] [nvarchar](100) NOT NULL,
	[Specialization] [nvarchar](100) NOT NULL,
	[NumberOfAppointments] [int] NULL,
	[NumberOfVideoCalls] [int] NULL,
	[HistoryOfWork] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentProfiles]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 11/14/2024 10:56:44 PM ******/
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
	[Type] [int] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthProfiles]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[Images]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[invalidated_token]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[Notices]    Script Date: 11/14/2024 10:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notices](
	[NoticeId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NULL,
	[RecipientId] [uniqueidentifier] NULL,
	[HasViewed] bit default 0 not null,
PRIMARY KEY CLUSTERED 
(
	[NoticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reminders]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[Schedules]    Script Date: 11/14/2024 10:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [uniqueidentifier] NOT NULL,
	[DoctorId] [uniqueidentifier] NOT NULL,
	[Date] [date] NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[UserEvents]    Script Date: 11/14/2024 10:56:44 PM ******/
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
/****** Object:  Table [dbo].[UserReports]    Script Date: 11/14/2024 10:56:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserReports](
	[Id] [uniqueidentifier] NOT NULL,
	[ReporterId] [uniqueidentifier] NOT NULL,
	[ReportType] [nvarchar](100) NOT NULL,
	[ReportDescription] [nvarchar](max) NOT NULL,
	[ReportedId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ResolvedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'd8782c3f-c54b-40d5-a4f2-07e9f19886c2', N'2bd6638b-d5bd-4ebe-b4e6-73b0df3592ae', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'Room 101, Main Clinic', N'Rejected', CAST(N'2024-11-11T17:44:22.883' AS DateTime), CAST(N'2024-11-12T05:13:11.863' AS DateTime), CAST(N'2024-11-19' AS Date), N'Offline')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'1b8bbfdc-b0d8-454c-b3ad-32c2d63b3081', N'dce5fe46-5383-4574-a897-d16a46714c61', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'test', N'Pending', CAST(N'2024-11-12T04:16:42.440' AS DateTime), CAST(N'2024-11-12T11:16:42.440' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'a3bef293-38a6-4509-8531-454a6187d724', N'67d86d69-d13f-4a22-b2d8-78f533ecfa91', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/8b3bc005-7164-414a-a551-6167629811b9', N'Pending', CAST(N'2024-11-11T19:00:53.607' AS DateTime), CAST(N'2024-11-12T02:00:53.607' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'a986e2f8-4b83-422c-8ba1-4965975e3008', N'ad56d07c-6957-4ccd-8b6e-fc4cf86f7309', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'21:30:00' AS Time), CAST(N'22:00:00' AS Time), NULL, N'Pending', CAST(N'2024-11-13T12:23:59.070' AS DateTime), CAST(N'2024-11-13T19:23:59.070' AS DateTime), CAST(N'2024-11-15' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'b451f56a-29b5-489a-8c7b-4a950fc77d7c', N'77a51d7a-f0e7-4324-ae04-f42e0b5f43ad', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/772c88eb-8a6b-4701-b8c2-7c819f9b5e95', N'Approved', CAST(N'2024-11-11T18:57:57.697' AS DateTime), CAST(N'2024-11-13T17:45:03.257' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'1723ca0b-7730-42dd-9e72-55ea8d68d7b5', N'b57ef8db-7d79-43c5-8c3a-d0b687e762a6', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/a35afb4d-d3fc-4342-a4b3-22a76a7e108c', N'Approved', CAST(N'2024-11-12T04:02:55.100' AS DateTime), CAST(N'2024-11-12T05:33:45.550' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'935b3207-ebe2-4803-b011-6819c245134e', N'46ee3bf0-0c9b-4fc0-9899-32f8bf3e1ec4', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'21:00:00' AS Time), CAST(N'21:30:00' AS Time), NULL, N'Pending', CAST(N'2024-11-13T12:14:52.083' AS DateTime), CAST(N'2024-11-13T19:14:52.083' AS DateTime), CAST(N'2024-11-15' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'0fe0b507-008b-4323-8bf9-7769cc851b36', N'f899e992-5336-4fdc-aead-15e2c4dbba70', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Consultation for General Check-up', N'A general health check-up and consultation', CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time), N'Room 101, Main Clinic', N'Pending', CAST(N'2024-11-11T14:58:17.663' AS DateTime), CAST(N'2024-11-11T21:58:17.663' AS DateTime), CAST(N'2024-11-11' AS Date), N'Offline')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'1a0a6ffa-9130-45ed-b4c0-8908d326a057', N'00000000-0000-0000-0000-000000000000', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Consultation for General Check-up', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'789 Third Road', N'Approved', CAST(N'2024-11-11T17:39:32.443' AS DateTime), CAST(N'2024-11-13T17:47:05.827' AS DateTime), CAST(N'2024-11-19' AS Date), N'Offline')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'2f462a59-3d8f-43b1-bc76-8af0f760e5da', N'915d8591-b82b-49ce-b3a2-03b46d5c711f', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/aa1519b9-24ce-4b66-abbe-3b3399e59c9e', N'Rejected', CAST(N'2024-11-11T17:54:34.150' AS DateTime), CAST(N'2024-11-13T17:47:21.780' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'508dd9f4-79e4-4bdf-8751-9f056b31ed37', N'3c8d3acb-a00d-4f50-9055-b80d3c0a7a77', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'19:00:00' AS Time), CAST(N'19:30:00' AS Time), N'https://meet.example.com/900aeffc-bfbb-4064-baad-eeadce06dfaa', N'Approved', CAST(N'2024-11-13T12:16:46.570' AS DateTime), CAST(N'2024-11-13T12:17:07.637' AS DateTime), CAST(N'2024-11-15' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'ae4504fc-4890-43a9-8f86-a1896264e915', N'159e6299-194a-4f5c-ac2e-adb3065ca9b7', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/7fbfd589-1daf-4d60-8e96-bbd92304d8f3', N'Approved', CAST(N'2024-11-12T04:19:00.173' AS DateTime), CAST(N'2024-11-12T04:37:23.040' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'6a26d359-691b-45e2-becd-b1de450d307c', N'3b0ba26a-6b87-4b4e-bb4e-9e3d1edf2de1', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Consultation for General Check-up', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'Room 101, Main Clinic', N'Pending', CAST(N'2024-11-11T17:38:16.493' AS DateTime), CAST(N'2024-11-12T00:38:16.493' AS DateTime), CAST(N'2024-11-19' AS Date), N'Offline')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'a76a9869-ef7d-4437-a105-bb901214afc1', N'24768df8-68b5-43a3-8e28-3f4b8709af33', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/b1b4e9b1-a8d7-45f5-8a09-8e50314a0171', N'Pending', CAST(N'2024-11-11T18:59:04.020' AS DateTime), CAST(N'2024-11-12T01:59:04.020' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'3d73fae9-804a-42b4-b834-bd380c6a9485', N'dbe4e94c-5ec4-4f2d-bf6a-da02ed0f3b85', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/0c104c50-bd4f-4478-99df-509ae1ec5cbc', N'Pending', CAST(N'2024-11-12T04:06:46.443' AS DateTime), CAST(N'2024-11-12T11:06:46.443' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'e358a2f6-ed04-43d9-a9f7-cb9822c79c82', N'bd24d1c2-de97-47ef-8e92-6e5b2567cb6b', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/f2639b4c-926f-42cf-b4fd-0786d4d549d0', N'Pending', CAST(N'2024-11-12T04:01:07.727' AS DateTime), CAST(N'2024-11-12T11:01:07.727' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'8d96baf5-0403-41cb-94ca-d39355863439', N'66136e67-3548-4393-8728-475075d3171e', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Tư vấn với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'789 Third Road', N'Pending', CAST(N'2024-11-11T17:54:23.840' AS DateTime), CAST(N'2024-11-12T00:54:23.840' AS DateTime), CAST(N'2024-11-19' AS Date), N'Offline')
INSERT [dbo].[Appointments] ([AppointmentId], [EventId], [DoctorId], [UserId], [Title], [Description], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [UpdatedAt], [Date], [Type]) VALUES (N'3d059793-582f-4763-ab7c-e4df28058090', N'adc0753b-8bb6-4615-84ef-ccac7884a21d', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'333e7891-9949-48d7-97e3-6c68cf88f8f1', N'Tư vấn về Consultation for General Check-up với Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/618ad35f-a80e-4cae-b2fd-68a3930e3150', N'Rejected', CAST(N'2024-11-12T04:11:59.127' AS DateTime), CAST(N'2024-11-12T05:14:25.780' AS DateTime), CAST(N'2024-11-19' AS Date), N'Online')
GO
INSERT [dbo].[DailyMetrics] ([Id], [UserId], [Weight], [Height], [SystolicBloodPressure], [DiastolicBloodPressure], [HeartRate], [BloodSugar], [BodyTemperature], [Date], [OxygenSaturation]) VALUES (N'88ea1ab1-92b1-4b5c-b4fd-8224151acdd7', N'ddd99a47-c20e-49e9-8e4f-cb990202bdd8', NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2024-11-14' AS Date), 15)
INSERT [dbo].[DailyMetrics] ([Id], [UserId], [Weight], [Height], [SystolicBloodPressure], [DiastolicBloodPressure], [HeartRate], [BloodSugar], [BodyTemperature], [Date], [OxygenSaturation]) VALUES (N'b7fed1af-1bd0-4815-9acc-f7bf122cc8e5', N'00000000-0000-0000-0000-000000000000', 43, 33, 120, 12, 43, 20, 36.5, CAST(N'2024-11-14' AS Date), 23)
GO
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'e45f1173-038d-43af-b801-08dcfd5bd6a2', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', N'string', N'string', CAST(N'2024-11-05T12:36:46.657' AS DateTime))
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'c9b7d1c0-e179-4dfa-93d5-08dcfdbe105b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'123', N'string', CAST(N'2024-11-06T00:19:54.033' AS DateTime))
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'2f31cb85-e62c-49d5-93d8-08dcfdbe105b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'ee25qzlTnOJGR5A3rGUfX3:APA91bHYw0aY69neaJqaGyJKUMc5O2R0RD_8-fyk9HpIrj42UWG7zqqs62kgxh_eL-NI1BFtW1EGNCA7SjMsuSJscqGNh5YI_koez0Q7WsV6_r3ph9PrcE8', N'web', CAST(N'2024-11-06T00:32:41.457' AS DateTime))
INSERT [dbo].[DeviceTokens] ([Id], [UserId], [DeviceToken], [DeviceType], [LastUpdated]) VALUES (N'cf61acf9-53bc-4f18-7b84-08dcff3f409b', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'dQU0n_tLRUzWpCtL1-VcRO:APA91bEGF5tx0Pb7AqTLK_SkEgi5cqPYYjNmad0expWSI1ul66kSc6N3-pQ0X-LwlXVPHSDwzPn4RAKVF5D-cV_NDoXao7Kt54oqf9_2rCwZ3yB6Vfr3fqU', N'web', CAST(N'2024-11-07T22:17:12.510' AS DateTime))
GO
INSERT [dbo].[Doctors] ([Id], [UserId], [DisplayName], [Description], [ClinicAddress], [ClinicCity], [Specialization], [NumberOfAppointments], [NumberOfVideoCalls], [HistoryOfWork]) VALUES (N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', N'Dr. Jane Smith', N'Bác si chuyên khoa da li?u, tu v?n da chuyên sâu', N'456 Second Avenue', N'TP. H? Chí Minh', N'Da li?u', 80, 25, N'B?nh vi?n Da li?u Trung Uong')
INSERT [dbo].[Doctors] ([Id], [UserId], [DisplayName], [Description], [ClinicAddress], [ClinicCity], [Specialization], [NumberOfAppointments], [NumberOfVideoCalls], [HistoryOfWork]) VALUES (N'545b3fb7-4721-4b01-9617-3f7fcb4a076b', N'4a5b22dd-cee4-498e-8469-d6b9a6914854', N'Dr. Emily Le', N'Chuyên gia y h?c th? thao và ph?c h?i ch?c nang', N'654 Fifth Boulevard', N'Hu?', N'Y h?c th? thao', 70, 20, N'Trung tâm Y h?c Hu?')
INSERT [dbo].[Doctors] ([Id], [UserId], [DisplayName], [Description], [ClinicAddress], [ClinicCity], [Specialization], [NumberOfAppointments], [NumberOfVideoCalls], [HistoryOfWork]) VALUES (N'5735c7e2-0877-4f6b-95bd-e264a12c9315', N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', N'Dr. David Tran', N'Bác si chuyên khoa tim m?ch', N'321 Fourth Lane', N'C?n Tho', N'Tim m?ch', 100, 55, N'B?nh vi?n Ða khoa C?n Tho')
INSERT [dbo].[Doctors] ([Id], [UserId], [DisplayName], [Description], [ClinicAddress], [ClinicCity], [Specialization], [NumberOfAppointments], [NumberOfVideoCalls], [HistoryOfWork]) VALUES (N'a6d00169-d73a-4b86-8621-e89771ccbce2', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'Dr. John Doe', N'Chuyên gia n?i khoa v?i hon 10 nam kinh nghi?m', N'123 Main Street', N'Hà N?i', N'N?i khoa', 50, 30, N'B?nh vi?n B?ch Mai')
INSERT [dbo].[Doctors] ([Id], [UserId], [DisplayName], [Description], [ClinicAddress], [ClinicCity], [Specialization], [NumberOfAppointments], [NumberOfVideoCalls], [HistoryOfWork]) VALUES (N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', N'Dr. Alice Nguyen', N'Chuyên gia s?c kh?e ph? n?', N'789 Third Road', N'Ðà N?ng', N'Ph? khoa', 65, 40, N'B?nh vi?n Ph? s?n Nhi Ðà N?ng')
GO
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'2875b9d9-c317-4da6-9827-01ebf10c7c92', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2025-01-04T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'9e53a47d-986e-43c4-aa50-0559d3fb6bea', NULL, N'Update this nay thoi event', N'Update initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Updated Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-01T09:00:00.0000000' AS DateTime2), 1, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'737db418-7630-40e1-83d1-07b94cc52d50', NULL, N'Thụy', N'Test', CAST(N'2024-11-13T04:04:26.0000000' AS DateTime2), CAST(N'04:04:26' AS Time), CAST(N'04:04:26' AS Time), N'', NULL, CAST(N'2024-11-14T04:36:15.3254913' AS DateTime2), NULL, NULL, NULL, CAST(N'2024-11-14T00:00:00.0000000' AS DateTime2), 2, 1, N'c11e3162-ac50-489f-af16-d91670b6b3fc', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'8fcb12c9-fb88-4f27-9050-08dd040b32c8', N'Jane Smith', N'Cuộc hẹn với bác sĩ Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'2024-11-19T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'789 Third Road', NULL, CAST(N'2024-11-13T17:47:09.9453649' AS DateTime2), NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 1, N'4ac1f292-2cf7-4cb4-89ac-1e55926fb8a3', 2)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'78ece143-d3a6-4ddc-96db-1e9ab3e5a840', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-14T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'32fbe979-08cc-49ab-a7ab-251d38f70ded', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-21T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'919a856d-aa8d-4b2d-8125-2b35577c1afb', NULL, N'Thụy', N'', CAST(N'2024-11-06T04:04:26.0000000' AS DateTime2), CAST(N'04:04:26' AS Time), CAST(N'04:04:26' AS Time), N'', NULL, CAST(N'2024-11-14T04:32:35.0646105' AS DateTime2), NULL, NULL, NULL, CAST(N'2024-11-08T00:00:00.0000000' AS DateTime2), 1, 1, N'f935fadb-0e25-46ba-8964-cb8d5db64472', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'de222f29-b736-43fd-a3ac-34d3b160f141', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-28T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'c0b719e3-a1fe-40db-b741-3db72e46512a', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-30T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'18c777e5-a90c-4cf1-9428-491cc6352be2', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-28T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'4ed40c90-5d45-4578-8365-49ea4efe2b69', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-07T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'3c11aa01-e9eb-4ad8-a476-49f06ba28e54', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-07T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'1ca9e0c5-8229-454d-a311-4f06ce73599a', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-30T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'8aa5755b-8404-431f-9d79-54332359198c', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2025-01-04T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'cbd63b44-96d5-4fff-b7d1-559a61324170', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-21T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'018ca542-a2e2-4cec-8d2e-605436ba6d74', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-30T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'9622cab0-62d3-4b51-991d-62670fbadf09', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-21T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'ff433b15-4d70-467c-b9aa-67dacde89fe2', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2025-01-04T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'f8c52c49-b782-4bfc-bd48-7a4007baa27a', NULL, N'Thụy', N'TEst', CAST(N'2024-11-06T04:04:26.0000000' AS DateTime2), CAST(N'04:04:26' AS Time), CAST(N'04:04:26' AS Time), N'HN ', NULL, CAST(N'2024-11-14T04:33:03.5491305' AS DateTime2), NULL, NULL, NULL, CAST(N'2024-11-14T00:00:00.0000000' AS DateTime2), 1, 1, N'b1e30d14-7353-468a-87a5-93bdbb21c253', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'a3fbc466-13fd-4441-b018-7c09ff72073e', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-14T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'35128faf-83fb-4b44-a732-93f397202d3d', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-07T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'41352f60-b363-432a-8ff0-a63dd36d8bbf', NULL, N'Update this event', N'Update initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-15T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Updated Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-01T09:00:00.0000000' AS DateTime2), 1, 1, N'8f4f8a68-9fcb-40cb-bf3b-67808382899b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'd55fd491-419b-4387-98cc-ac9230e83713', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-11-30T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-13T14:34:16.2066917' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'f8f1e060-8506-41ba-b307-3343168c4d9e', 1)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'3c8d3acb-a00d-4f50-9055-b80d3c0a7a77', N'John Doe', N'Cuộc hẹn với bác sĩ Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'2024-11-15T00:00:00.0000000' AS DateTime2), CAST(N'19:00:00' AS Time), CAST(N'19:30:00' AS Time), N'https://meet.example.com/900aeffc-bfbb-4064-baad-eeadce06dfaa', NULL, CAST(N'2024-11-13T12:17:11.7084185' AS DateTime2), NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 1, N'391bc10c-8512-4cfc-82b3-d6c167085fe9', 2)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'b57ef8db-7d79-43c5-8c3a-d0b687e762a6', N'John Doe', N'Cuộc hẹn với bác sĩ Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'2024-11-19T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/a35afb4d-d3fc-4342-a4b3-22a76a7e108c', NULL, CAST(N'2024-11-12T05:33:53.2014822' AS DateTime2), NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 1, N'00f193bb-2a2c-4d93-91f2-411dc8bb1937', 2)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'75602733-e64e-4de8-b9c3-d5e3cf3d7ef8', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-14T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'cea31c71-e193-4ebb-a5e7-d6880a47348d', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-21T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'8fb11cb6-0fd2-4782-9b08-de56707a0865', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-28T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-28T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-06T15:13:37.1599224' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'6fa8e4bf-1173-4a44-a585-4d987f0c576b', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'47947fee-d9b0-4b89-93ef-f3684890a04f', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2024-12-07T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-10T07:58:36.4110850' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'4fcab2e5-3647-4147-993f-384c3f8dcabe', NULL)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'77a51d7a-f0e7-4324-ae04-f42e0b5f43ad', N'Jane Smith', N'Cuộc hẹn với bác sĩ Dr. Alice Nguyen', N'A general health check-up and consultation', CAST(N'2024-11-19T00:00:00.0000000' AS DateTime2), CAST(N'14:00:00' AS Time), CAST(N'17:00:00' AS Time), N'https://meet.example.com/772c88eb-8a6b-4701-b8c2-7c819f9b5e95', NULL, CAST(N'2024-11-13T17:45:08.2280332' AS DateTime2), NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, 1, N'e4aed97b-9225-4025-8510-0347d4c8aa85', 2)
INSERT [dbo].[Events] ([EventId], [UserName], [Title], [Description], [EventDateTime], [StartTime], [EndTime], [Location], [Status], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [RepeatEndDate], [RepeatFrequency], [RepeatInterval], [OriginalEventId], [Type]) VALUES (N'5d776672-b0ca-4e38-b8b7-fad0515bf58f', NULL, N'Project Kickoff Meeting', N'initial meeting to discuss project scope and deliverables.', CAST(N'2025-01-04T09:00:00.0000000' AS DateTime2), CAST(N'09:00:00' AS Time), CAST(N'11:00:00' AS Time), N'Conference Room B', NULL, CAST(N'2024-11-11T01:55:01.2998413' AS DateTime2), NULL, NULL, NULL, CAST(N'2025-01-10T09:00:00.0000000' AS DateTime2), 3, 1, N'15fd6e14-ee39-49b1-8b2d-979a7c235b84', NULL)
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
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'e9e6c505-42eb-4855-94c0-68c481f6a6db', N'test manh', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'a0425726-3eae-4033-9444-6e57cf70a196', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'12b1ec5c-47ea-4b89-adf0-e08d665d911d')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'fd2d343d-f429-48ca-a650-6f0088bc0642', N'manh vu', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'd3d91e9b-7ade-4a94-a0df-9ece44147344', N'test manh', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'b3b163c6-a8e1-4e60-8da9-a5c7985c23ae', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'dc57f0de-7b37-42e4-b1bd-b2a228c8cc66', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'd73a942b-8907-4ebf-a631-cbb9f0ac6480', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'5014b9db-95b5-4346-9dd3-da16649e5856', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'a2b55273-cb90-4522-ba6d-ef421c818463', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
INSERT [dbo].[Notices] ([NoticeId], [Message], [UserId], [RecipientId]) VALUES (N'c4e3198d-70ea-4ad8-bd3e-f06aa825e66e', N'string', N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', N'4a5b22dd-cee4-498e-8469-d6b9a6914854')
GO
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c63d1eb7-3917-4ea8-aca9-047723255bca', N'75602733-e64e-4de8-b9c3-d5e3cf3d7ef8', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'80567b55-4223-4bee-a31c-056095faba6d', N'ff433b15-4d70-467c-b9aa-67dacde89fe2', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'700309e8-17c7-4cb0-9e10-074b0ad594c3', N'75602733-e64e-4de8-b9c3-d5e3cf3d7ef8', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'f25e8ecf-ab3a-4823-9897-09e8cbcdf4aa', N'32fbe979-08cc-49ab-a7ab-251d38f70ded', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c2ea6b0b-2fa0-4b0e-9486-0e661c4852aa', N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'619368b9-e537-4f0d-ba44-102e2feebf16', N'018ca542-a2e2-4cec-8d2e-605436ba6d74', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c26d30a1-ef38-4119-a875-1051f60d5e1a', N'4014c0b5-ddd0-489f-b400-e91f4f99b53b', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'52e0287e-c724-42ae-a278-1056ed18d6e8', N'4ed40c90-5d45-4578-8365-49ea4efe2b69', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'33c244c9-d4ff-4af4-8dea-178970ab9ff4', N'ff433b15-4d70-467c-b9aa-67dacde89fe2', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1c582395-a639-4fe4-a401-18e7b3b1152f', N'3c11aa01-e9eb-4ad8-a476-49f06ba28e54', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0fd903fc-f0bc-4a44-8b94-1aef0153e385', N'a3fbc466-13fd-4441-b018-7c09ff72073e', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'0a7bfb21-4a7c-4bcd-a141-1b3d954061a1', N'cea31c71-e193-4ebb-a5e7-d6880a47348d', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4a56b662-5ee1-474f-96f7-2840b7300481', N'47947fee-d9b0-4b89-93ef-f3684890a04f', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'835697ce-71f0-44c1-b4d1-2a145ed0a8fa', N'18c777e5-a90c-4cf1-9428-491cc6352be2', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'978fc0be-1911-484f-ae50-2ba72c7dbd4f', N'1ca9e0c5-8229-454d-a311-4f06ce73599a', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'48cba921-bd4b-46a6-8871-39a3ae84808d', N'47947fee-d9b0-4b89-93ef-f3684890a04f', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'773ccf42-669c-4dc7-82ca-3b8550801971', N'de222f29-b736-43fd-a3ac-34d3b160f141', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'9dc9261c-f650-478d-9ac4-40ce8d80e7fe', N'cea31c71-e193-4ebb-a5e7-d6880a47348d', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ae8fceb6-9db2-4db9-81e4-4c8dbbc233ea', N'd55fd491-419b-4387-98cc-ac9230e83713', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'26671785-643f-4ea5-b238-4e635fd409cf', N'32fbe979-08cc-49ab-a7ab-251d38f70ded', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'210a7d84-2caa-4330-a3c1-5656095e93ca', N'1ca9e0c5-8229-454d-a311-4f06ce73599a', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'bbd64dee-0fe3-4292-b4b5-5c1ef0941fc0', N'78ece143-d3a6-4ddc-96db-1e9ab3e5a840', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'aad57336-658f-4cad-abb4-5d4086586936', N'8aa5755b-8404-431f-9d79-54332359198c', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c0dfd9af-d51d-4b97-ac8b-609e368d2cc5', N'c0b719e3-a1fe-40db-b741-3db72e46512a', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ae10dcd5-fc9b-4373-81e7-69406cea31dd', N'737db418-7630-40e1-83d1-07b94cc52d50', CAST(N'2024-11-13T04:04:26.0000000' AS DateTime2), N'Reminder for event: Thụy', 0, 10, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'5485426c-5205-4303-8538-7146574035d2', N'35128faf-83fb-4b44-a732-93f397202d3d', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8138518b-cf9a-4e6f-9648-755fd137ff21', N'f8c52c49-b782-4bfc-bd48-7a4007baa27a', CAST(N'2024-11-06T04:03:26.0000000' AS DateTime2), N'Reminder for event: Thụy', 0, 1, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'623bcc61-f714-4dea-9133-79a9128b2488', N'41352f60-b363-432a-8ff0-a63dd36d8bbf', CAST(N'2024-11-13T09:00:00.0000000' AS DateTime2), N'Reminder for event: Update this event', 0, 3, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'1c7c6132-6502-42c3-8870-835a7b156e09', N'cbd63b44-96d5-4fff-b7d1-559a61324170', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'4d3dca77-cfd6-42cd-85f4-91af49db2ca3', N'd55fd491-419b-4387-98cc-ac9230e83713', CAST(N'2024-11-30T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'760826be-63f6-472b-8712-91f60e019fee', N'9622cab0-62d3-4b51-991d-62670fbadf09', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'cdaf9d90-d2f6-4362-8167-9aae27a70b43', N'3c11aa01-e9eb-4ad8-a476-49f06ba28e54', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'fb27300f-62ac-4ffd-9a8a-aaad0992d9a3', N'8fb11cb6-0fd2-4782-9b08-de56707a0865', CAST(N'2024-12-28T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'd143ae3d-d54e-4223-82d2-b04a2bda3916', N'5d776672-b0ca-4e38-b8b7-fad0515bf58f', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8952fc66-fb4c-4f60-bad2-b6560145041c', N'4ed40c90-5d45-4578-8365-49ea4efe2b69', CAST(N'2024-12-07T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'3ef67ec8-8c25-4bbf-b09a-c122e9a90c2e', N'cbd63b44-96d5-4fff-b7d1-559a61324170', CAST(N'2024-12-21T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'858c7d70-82ab-49af-ba65-c36913953805', N'18c777e5-a90c-4cf1-9428-491cc6352be2', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'8d3cd2ad-0e86-4481-97f2-ce954713f55b', N'78ece143-d3a6-4ddc-96db-1e9ab3e5a840', CAST(N'2024-12-14T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'892ad2ce-f216-4faa-933a-d0bb81b4c1ee', N'41352f60-b363-432a-8ff0-a63dd36d8bbf', CAST(N'2024-11-15T08:00:00.0000000' AS DateTime2), N'Reminder for event: Update this event', 0, 2, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ea6b9bf0-d45a-45aa-82bb-d448f1e528d5', N'9622cab0-62d3-4b51-991d-62670fbadf09', CAST(N'2024-12-21T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e558c304-fbb1-4fc0-8cbd-d6dbd31a0c41', N'c0b719e3-a1fe-40db-b741-3db72e46512a', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'b752d1d0-2cc6-4cf2-9f62-e137e77e81b3', N'2875b9d9-c317-4da6-9827-01ebf10c7c92', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'78b402a0-e50e-41f3-8c3f-e23c95e50e95', N'8fb11cb6-0fd2-4782-9b08-de56707a0865', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'ac6cb2f5-589e-4999-8bb6-e7499cdb325a', N'018ca542-a2e2-4cec-8d2e-605436ba6d74', CAST(N'2024-11-30T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'23f8cc2f-fa4c-4d9b-98a4-e75acb77a34e', N'5d776672-b0ca-4e38-b8b7-fad0515bf58f', CAST(N'2025-01-04T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'e55d988a-446b-4323-bfa2-eaa163d32072', N'a3fbc466-13fd-4441-b018-7c09ff72073e', CAST(N'2024-12-14T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c03c63ca-1215-457d-be35-ecfb45571782', N'8aa5755b-8404-431f-9d79-54332359198c', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'157192ca-70fb-40a6-8519-ed6c096e0b15', N'919a856d-aa8d-4b2d-8125-2b35577c1afb', CAST(N'2024-11-06T04:03:26.0000000' AS DateTime2), N'Reminder for event: Thụy', 0, 1, 1)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'706470a9-0f66-478f-a545-eeb99fed926c', N'35128faf-83fb-4b44-a732-93f397202d3d', CAST(N'2024-12-07T07:00:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 2, 2)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'92572752-891e-45ad-83d8-f64a9da6cb0b', N'de222f29-b736-43fd-a3ac-34d3b160f141', CAST(N'2024-12-28T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
INSERT [dbo].[Reminders] ([ReminderId], [EventId], [ReminderTime], [Message], [IsSent], [OffsetUnit], [ReminderOffset]) VALUES (N'c0a270da-6071-4389-9297-fffa2add7d50', N'2875b9d9-c317-4da6-9827-01ebf10c7c92', CAST(N'2025-01-04T08:45:00.0000000' AS DateTime2), N'Reminder for event: Project Kickoff Meeting', 0, 1, 15)
GO
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'd3040d43-c1f2-4364-8f92-0d0775de90ed', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'19:00:00' AS Time), CAST(N'19:30:00' AS Time), N'Unavailable', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'4256d7e4-75cd-43aa-9ce1-17bc1427b1dc', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'20:30:00' AS Time), CAST(N'20:45:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'a015c8b4-78e0-4d69-a367-62a1bee2d9c6', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'21:00:00' AS Time), CAST(N'21:30:00' AS Time), N'Available', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'540c1bc7-76dd-42e6-8b20-6cddf836ca44', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'19:15:00' AS Time), CAST(N'19:30:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'dcf3a72d-86ad-4b93-9cd9-89c88d9b3889', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'20:00:00' AS Time), CAST(N'20:30:00' AS Time), N'Available', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'57092c0c-3b5f-4640-9dd5-8ab8abefade2', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'21:30:00' AS Time), CAST(N'21:45:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'69ce29f6-1c86-4810-891c-91769aad2d51', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'20:45:00' AS Time), CAST(N'21:00:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'0e2f2bec-783e-4429-b538-9ac69c25ca7d', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'20:30:00' AS Time), CAST(N'21:00:00' AS Time), N'Available', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'0cc33870-290e-4cd3-993e-a017a7ec59e2', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'19:00:00' AS Time), CAST(N'19:15:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'65a72097-68dc-4572-b3fc-bb01b63bafc9', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'21:00:00' AS Time), CAST(N'21:15:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'2aa83dfb-f5cf-4caa-a736-d9619925ec85', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'19:30:00' AS Time), CAST(N'19:45:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'd6b0a900-81aa-4c39-a7f9-da25abdb2d95', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'20:15:00' AS Time), CAST(N'20:30:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'fb806b5b-8aec-48d3-b00b-e54fb7ef6075', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'19:30:00' AS Time), CAST(N'20:00:00' AS Time), N'Available', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'42f1d9d0-fbc8-4184-be76-e7e65eab4356', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'21:15:00' AS Time), CAST(N'21:30:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'c96a3cee-13b0-49c7-b668-eaa6f7d66826', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'20:00:00' AS Time), CAST(N'20:15:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'fb53e451-6b8b-424e-bb7e-eb06fec748c0', N'd97712c4-adb0-40c3-9bac-1c3f5f631ba0', CAST(N'2024-11-15' AS Date), CAST(N'19:45:00' AS Time), CAST(N'20:00:00' AS Time), N'Available', CAST(N'2024-11-13T19:28:08.640' AS DateTime), CAST(N'2024-11-13T19:28:08.640' AS DateTime))
INSERT [dbo].[Schedules] ([ScheduleId], [DoctorId], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [UpdatedAt]) VALUES (N'faa49c4b-d595-4ef8-9af0-ed6a87ab200a', N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'2024-11-15' AS Date), CAST(N'21:30:00' AS Time), CAST(N'22:00:00' AS Time), N'Unavailable', CAST(N'2024-11-13T18:58:57.090' AS DateTime), CAST(N'2024-11-13T18:58:57.090' AS DateTime))
GO
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'47863e73-e00c-4ebf-8f2a-1e8753359c4d', CAST(N'1988-07-21' AS Date), N'manhvv15test@gmail.com', N'Jane', 0, N'Smith', N'Password123!', N'0987654321', N'User', N'Inactive', N'janesmith')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'd97e18cf-b6b6-40b8-a202-3da2e6202e17', NULL, N'manhvv15@gmail.com', NULL, NULL, NULL, N'$2a$10$vyBxUi1EuGbSrPXADGBAEOxc8kotw/hJfG9OWY/SL5rMiwT8Arbhq', NULL, N'ADMIN', N'ACTIVE', N'admin')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'333e7891-9949-48d7-97e3-6c68cf88f8f1', CAST(N'1990-05-15' AS Date), N'test@gmail.com', N'John', 1, N'Doe', N'Password123!', N'1234567890', N'Admin', N'Active', N'johndoe')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'ef4f6d64-e24d-4230-96d1-8b2b3186c104', NULL, N'duong@gmail.com', N'test4', NULL, N'Nguyen', N'$2a$10$FZc5a5fdlf1lVV/RCpJLWuDNMB96RgYuQTOghog357Is5NZiQQDFq', N'0398896461', N'CUSTOMER', N'INACTIVE', N'Nguyen')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'ddd99a47-c20e-49e9-8e4f-cb990202bdd8', NULL, N'manhvvv15@gmail.com', N'Vu', NULL, NULL, N'$2a$10$gK3L8K6NF6GB4g1iQQctrOjZxY7YYJD13VgzDJJ2IOfS3wXTr8yp.', N'0353271947', N'CUSTOMER', N'ACTIVE', N'manhvv15')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'4a5b22dd-cee4-498e-8469-d6b9a6914854', CAST(N'1992-12-12' AS Date), N'manhvu152k2@gmail.com', N'Sara', 0, N'Jones', N'Password123!', N'5566778899', N'Manager', N'Active', N'sarajones')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'bb70ec35-ba2a-4251-a660-daeb3fd580d4', NULL, N'manh@gmail.com', N'Vu', NULL, NULL, N'$2a$10$tULJAuVXrL9PM7gnr40oI./eVjEC9PA8vThg3uhe3HGsmwcEazsaq', N'0353271931', N'CUSTOMER', N'INACTIVE', N'manh14')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'12b1ec5c-47ea-4b89-adf0-e08d665d911d', CAST(N'1995-09-05' AS Date), N'michaelbrown@example.com', N'Michael', 1, N'Brown', N'Password123!', N'1122334455', N'User', N'Active', N'michaelbrown')
INSERT [dbo].[user] ([user_id], [dob], [email], [first_name], [gender], [last_name], [password], [phone], [role], [status], [username]) VALUES (N'bb9a65f6-67f7-4025-861b-efeaa2957e9f', CAST(N'1985-01-30' AS Date), N'manhvvhe161603@fpt.edu.vn', N'Chris', 1, N'Lee', N'Password123!', N'3344556677', N'User', N'Pending', N'chrislee')
GO
/****** Object:  Index [IX_DocumentProfiles_PantientId]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_DocumentProfiles_PantientId] ON [dbo].[DocumentProfiles]
(
	[PantientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_HealthProfiles_UserId]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE NONCLUSTERED INDEX [IX_HealthProfiles_UserId] ON [dbo].[HealthProfiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_user_email]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK_user_email] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_user_username]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK_user_username] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK5c856itaihtmi69ni04cmpc4m]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK5c856itaihtmi69ni04cmpc4m] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UKhl4ga9r00rh51mdaf20hmnslt]    Script Date: 11/14/2024 10:56:44 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UKhl4ga9r00rh51mdaf20hmnslt] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (newid()) FOR [AppointmentId]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ('Offline') FOR [Type]
GO
ALTER TABLE [dbo].[DeviceTokens] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[DeviceTokens] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((0)) FOR [NumberOfAppointments]
GO
ALTER TABLE [dbo].[Doctors] ADD  DEFAULT ((0)) FOR [NumberOfVideoCalls]
GO
ALTER TABLE [dbo].[Notices] ADD  DEFAULT (newid()) FOR [NoticeId]
GO
ALTER TABLE [dbo].[Schedules] ADD  DEFAULT (newid()) FOR [ScheduleId]
GO
ALTER TABLE [dbo].[Schedules] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Schedules] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Doctors]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Users]
GO
ALTER TABLE [dbo].[Doctors]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
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
ALTER TABLE [dbo].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
ALTER TABLE [dbo].[Reminders] CHECK CONSTRAINT [FK_Reminders_Events]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Doctor] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Doctor]
GO
ALTER TABLE [dbo].[UserEvents]  WITH CHECK ADD  CONSTRAINT [FK_UserEvents_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([EventId])
GO
ALTER TABLE [dbo].[UserEvents] CHECK CONSTRAINT [FK_UserEvents_Events]
GO
ALTER TABLE [dbo].[UserEvents]  WITH CHECK ADD  CONSTRAINT [FK_UserEvents_user_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[UserEvents] CHECK CONSTRAINT [FK_UserEvents_user_UserId]
GO
ALTER TABLE [dbo].[UserReports]  WITH CHECK ADD FOREIGN KEY([ReporterId])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD CHECK  (([Status]='Cancelled' OR [Status]='Rejected' OR [Status]='Approved' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD CHECK  (([Status]='Unavailable' OR [Status]='Available'))
GO
USE [master]
GO
ALTER DATABASE [DoAn] SET  READ_WRITE 
GO
