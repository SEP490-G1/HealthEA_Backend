USE [DOAN]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/29/2024 11:09:51 PM ******/
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
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK5c856itaihtmi69ni04cmpc4m]    Script Date: 10/29/2024 11:09:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UK5c856itaihtmi69ni04cmpc4m] ON [dbo].[user]
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UKhl4ga9r00rh51mdaf20hmnslt]    Script Date: 10/29/2024 11:09:51 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UKhl4ga9r00rh51mdaf20hmnslt] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
