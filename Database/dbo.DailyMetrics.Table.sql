USE [DOAN]
GO
/****** Object:  Table [dbo].[DailyMetrics]    Script Date: 10/29/2024 11:09:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailyMetrics](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Weight] [float],
	[Height] [float],
	[SystolicBloodPressure] [int],
	[DiastolicBloodPressure] [int],
	[HeartRate] [int],
	[BloodSugar] [float],
	[BodyTemperature] [float],
	[Date] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DailyMetrics]  WITH CHECK ADD  CONSTRAINT [FK_DailyMetric_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[user] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DailyMetrics] CHECK CONSTRAINT [FK_DailyMetric_User]
GO
