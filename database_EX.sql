USE [database-webExam]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/29/2023 10:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 6/29/2023 10:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[AnswerTitle] [varchar](2) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[AnswerContent] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chapter]    Script Date: 6/29/2023 10:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chapter](
	[ChapterID] [int] IDENTITY(1,1) NOT NULL,
	[ChapterName] [nvarchar](50) NULL,
	[SubjectID] [int] NULL,
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ChapterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 6/29/2023 10:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[ExamID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[BirthDay] [date] NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[Address] [nvarchar](250) NULL,
	[RoleID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exam]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exam](
	[ExamID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[ExamTime] [int] NULL,
	[NumOfQuestions] [int] NOT NULL,
	[Levels] [nvarchar](50) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Exam] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamHis]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamHis](
	[ExamHisID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
	[ExamID] [int] NOT NULL,
	[Score] [decimal](18, 0) NOT NULL,
	[TakeExamTime] [int] NULL,
	[Questions] [nvarchar](max) NOT NULL,
	[Answers] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_ExamHis] PRIMARY KEY CLUSTERED 
(
	[ExamHisID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[Level_ID] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [nchar](10) NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[Level_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionContent] [nvarchar](max) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Level_ID] [int] NULL,
	[SubjectID] [int] NULL,
	[ChapterID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Question_1] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[BirthDay] [date] NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [varchar](10) NULL,
	[Address] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 6/29/2023 10:01:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](50) NULL,
	[EmployeeID] [int] NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202304181811543_InitialCreate', N'WebExam.DB.Entities.EntityMgr', 0x1F8B0800000000000400CD58DB6EE336107D2FD07F10F4D402592B7650A01BC8BB48ECA408BA4E8228BB7DA6A5B14D94A25492CA5ADFD6877E527F6147574A942DCB897B815FEC2179E6CC903C33F4DF7FFEE57EDC86CC7A012169C4A7F678746E5BC0FD28A07C3DB513B57AF7B3FDF1C3F7DFB93741B8B5BE54F32EB279B892CBA9BD512ABE741CE96F20247214525F44325AA9911F850E092267727EFEDE198F1D40081BB12CCB7D4AB8A221E43FF0E72CE23EC42A216C1105C06469C7112F47B5EE490832263E4CEDDF6079B325E1687E3DBA41104541DAD615A304A978C056B645388F145148F4F2B3044F8988AFBD180D843DA731E0BC156112CA002EF5F4A1B19C4FB2581CBDB082F213A9A2F048C0F145991CC75CFEAA14DB75F2307D7986D22CEA3C8553FB268C599402066F3ABB9C31914DDC99E151B5EECC2A47CFEAC3806726FB9C59B384A944C09443A2046167D663B264D4FF15D2E7E877E0539E30D66487FC70AC6540D3A38862102A7D8295C1F96E6E5B4E7BBD6302D4CB77AC2DA2BBE3EA62625BF748862C19D487A191094F45027E010E8228081E895220702FEF02C8D3D96161F8BC45E8EC5BE5114F20DE26DB5A90ED27E06BB599DA3FE1F5B9A55B082A4349E233A778F734A97E4778BAC5BFE2E89148F93512C13FEEE89A0AB59993B47234C70D78A659849DFDEA07BA0909653D74F1EB4912B389785FFAC727C9CA55100890B2C7CFE434E97F8AD8809BD28F311390DD9A6CE7DEBC8B1E5EC8A48EFB3A5587205C472B5E5707B1CC2842F15A578724B72ED662871AE2DD2A0511F55789040CE52AD67AA03AC2AA19140569A4877651AD49E9A2E71455AFAA8ECE9EF2E82E481CE3496894CBD2627945AD9CBDF38EAF216181E1F8724729A9D9D69E502BC91A8C51748D4C6FA9900A379F2C49B651B320EC4CD35BB027BD951F23CB66D5D049AF1664DF8B457DF5CC04D239BCC5B04214FC3C42A8F9ECE7902FF67CC288E82942B38825213F54D8FAD07479696269EB70245D3F9A48DA3A1C4917882692B60E47D215A089A4ADC391CA12D04E786E3A22AE42DF5B4115A6E118B57A37516AE3709C4A9C9B30956D384A4B9E9B50AD81E178954437A12A5B17C5758CDB65DE63A773918D86CF14863E4535A7D4DE6B653514D42DD5ECF02BA4236FC514DBC2F4BCD02093362F950A507570C2C8FB83CD18C578F58405E174055215BDB18DEF8089F18EF9FFBC291C290336F061F19F37F834CBF2C116FEC84EC4ECE95F88F03744745A600D9BF70C4736F0A74135BBF5D3A09AAD798089556F6ECD4B6E3F8464FBE3D171369BEF5D418E8F0ED2E8B477814E8E4F5DBBADCECFE7DB9BEA57E7BFDD542BCAD3C38C8EEBABBBBDDF097AE7426031EE6584DC0BA6D5A87C756BDD157CD769FE39E5CE41D2B586C8FEAAE2E0674AAA41AB39777C1555C9C6E89A8CAA29C65E2C4011DC487225145D115FE1B08FE72F7FDE7D212CC9635C4270C71F121527EA4A4A0897ACD507B94EBFFFFCFDD0E6EC3EC4D92F798A109026CDCEE203BF4E280B6ADEB73B4ED11E88ECBC945A8DACF0798B70EBB446BAEF345CFB80CAF4CD21069E29FD33E0014130F9C03DF202AFE18612FD09D6C44FABBABD1FE4F046B4D3EECE29590B12CA1243AFCFFE7075B27F5C3F7C03E7B7932AA3150000, N'6.4.4')
GO
SET IDENTITY_INSERT [dbo].[Answer] ON 

INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (100, N'A', 50, N'will come', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (101, N'B', 50, N'would come', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (102, N'A', 51, N'drove', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (103, N'B', 51, N'drive', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (105, N'A', 52, N'writes', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (106, N'B', 52, N'will write', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (107, N'A', 53, N'get', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (108, N'B', 53, N'getting ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (109, N'A', 54, N'are going', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (110, N'B', 54, N'go', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (111, N'C', 54, N'went', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (112, N'D', 54, N'have been', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (113, N'A', 55, N'have you studied', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (114, N'B', 55, N'did you study', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (115, N'A', 73, N'Chiến tranh là một hiện tượng chính trị xã hội có tính lịch sử', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (116, N'B', 73, N'Chiến tranh là những cuộc xung đột tự phát ngẫu nhiên', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (117, N'C', 73, N'Chiến tranh là một hiện tượng xã hội mang tính vĩnh viễn', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (118, N'D', 73, N'Chiến tranh là những xung đột do mâu thuẫn không mang tính xã hội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (119, N'A', 74, N'Bảo vệ nhân dân, bảo vệ chế độ, bảo vệ tổ quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (120, N'B', 74, N'Bảo vệ đất nước và chống ách đô hộ của thực dân, đế quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (121, N'C', 74, N'Bảo vệ tính mạng, tài sản của nhân dân,của chế độ XHCN', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (122, N'D', 74, N'Bảo vệ độc lập, chủ quyền và thống nhất đất nước', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (145, N'A', 93, N'Để lật đổ chế độ cũ, xây dựng chế độ mới XHCN', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (146, N'B', 93, N'Để xây dựng chế độ mới âm no, tự do, hạnh phúc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (147, N'C', 93, N'Để giành lấy chính quyền và bảo vệ chính quyền', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (148, N'D', 93, N'Để lật đổ chế độ cũ, xây dựng chính quyền', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (149, N'A', 94, N'Là sức mạnh của cả dân tộc, sức mạnh quốc phòng, an ninh nhân dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (150, N'B', 94, N'Là sức mạnh tổng hợp của cả dân tộc, cả nước, kết hợp với sức mạnh thời đại', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (151, N'A', 95, N'Tăng cường quân thường trực gắn với phát triển kinh tế - xã hội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (152, N'B', 95, N'Tăng cường thế trận gắn với thực hiện chính sách đãi ngộ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (155, N'C', 50, N'come', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (156, N'D', 50, N'comes', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (157, N'C', 51, N'driven', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (158, N'D', 51, N'was driving', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (159, N'C', 52, N'has written', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (160, N'D', 52, N'was writing', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (161, N'C', 53, N' got', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (162, N'D', 53, N'gets', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (163, N'C', 55, N'do you study', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (164, N'D', 55, N'had you studied', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (165, N'A', 56, N'has taken', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (166, N'B', 56, N'took', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (167, N'C', 56, N'had taken', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (168, N'D', 56, N'was taken', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (169, N'A', 57, N'realized', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (170, N'B', 57, N'had realized', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (171, N'C', 57, N'realize', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (172, N'D', 57, N'have realized', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (173, N'A', 58, N'have seen', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (174, N'B', 58, N'shall have seen', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (175, N'C', 58, N'see', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (176, N'D', 58, N'am going to see', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (177, N'A', 59, N'to', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (178, N'B', 59, N'for', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (179, N'C', 59, N'in', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (180, N'D', 59, N'at', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (181, N'A', 60, N'psychologists', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (182, N'B', 60, N'psychology', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (183, N'C', 60, N'psychologica', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (184, N'D', 60, N'psyche', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (185, N'A', 61, N'some', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (186, N'B', 61, N'any', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (187, N'C', 61, N'little', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (188, N'D', 61, N'a', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (189, N'A', 62, N'are speaking', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (190, N'B', 62, N'were spoken here', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (191, N'C', 62, N'are spoken', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (192, N'D', 62, N' speak', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (193, N'A', 63, N'would get', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (194, N'B', 63, N'get worse', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (195, N'C', 63, N'might get worse', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (196, N'D', 63, N'should get worse', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (197, N'A', 64, N'would stop', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (198, N'B', 64, N'have stopped', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (199, N'C', 64, N'stop', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (200, N'D', 64, N'stopping', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (201, N'A', 65, N'Xây dựng tinh thần trách nhiệm, ý thức tham gia bảo vệ Tổ quốc trong mọi tình huống', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (202, N'B', 65, N'Xây dựng tình yêu quê hương đất nước sẵn sàng tham gia lực lượng vũ trang nhân dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (203, N'C', 65, N'Đào tạo cán bộ có ý thức tổ chức kỷ luật và tình yêu quê hương đất nước', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (204, N'D', 65, N'Đào tạo đội ngũ cán bộ khoa học kỹ thuật có ý thức, năng lực sẵn sàng tham gia bảo vệ Tổ quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (205, N'A', 66, N'Xã hội, nhân văn, khoa học cơ bản và kỹ thuật quân sự', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (206, N'B', 66, N'Xã hội nhân văn, khoa học công nghệ và khoa học quân sự', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (207, N'C', 66, N'Xã hội, nhân văn, khoa học tự nhiên và khoa học kỹ thuật quân sự', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (208, N'D', 66, N'Xã hội nhân văn và kỹ thuật công nghệ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (209, N'A', 67, N'Đường lối quân sự của Đảng; công tác quốc phòng-an ninh; quân sự chung; kĩ thuật chiến đấu bộ binh và chiến thuật', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (210, N'B', 67, N'Quan điểm đường lối quân sự của Đảng về xây dựng nền quốc phòng toàn dân, chiến tranh nhân dân bảo vệ Tổ quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (211, N'C', 67, N'Quan điểm của chủ nghĩa Mác-Lênin, tư tưởng Hồ Chí Minh về công tác quốc phòng, an ninh; kĩ thuật chiến đấu bộ binh và chiến thuật', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (212, N'D', 67, N'Quan điểm của Chủ nghĩa Mác – Lênin, tư tưởng Hồ Chí Minh về chiến tranh, quân đội và bảo vệ Tổ quốc xã hội chủ nghĩa', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (213, N'A', 68, N'Học thuyết Mác-Lênin, tư tưởng HCM về chiến tranh, quân đội và bảo vệ Tổ Quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (214, N'B', 68, N'Xây dựng nền quốc phòng toàn dân, an ninh nhân dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (215, N'C', 68, N'Chiến tranh nhân dân bảo vệ Tổ Quốc, xây dựng lực lượng vũ trang nhân dân...', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (216, N'D', 68, N'Tất cả đều đúng', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (217, N'A', 69, N'Giai cấp lãnh đạo tiến hành chiến tranh', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (218, N'B', 69, N'Chế độ xã hội tiến hành chiến tranh', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (219, N'C', 69, N'Mục đích chính trị của chiến tranh', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (220, N'D', 69, N'Bản chất xã hội của chiến tranh', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (221, N'A', 70, N'Sự lãnh đạo của Đảng cộng sản đối với quân đội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (222, N'B', 70, N'Giữ vững quan điểm giai cấp trong xây dựng quân đội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (223, N'C', 70, N'Tính kỷ luật cao là yếu tố quyết định sức mạnh quân đội', 1)
GO
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (224, N'D', 70, N'Quân đội chính quy, hiện đại, trung thành với giai cấp công nhân và nhân dân lao động', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (225, N'A', 71, N'Sức mạnh của toàn dân, bằng cả lực lượng chính trị và lực lượng vũ trang', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (226, N'B', 71, N'Sức mạnh của toàn dân, bằng cả lực lượng chính trị và kinh tế', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (227, N'C', 71, N'Kết hợp chặt chẽ giữa đấu tranh chính trị với đấu tranh kinh tế', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (228, N'D', 71, N'Tất cả đều đúng', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (229, N'A', 72, N'Các đoàn thể, các tổ chức chính trị xã hội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (230, N'B', 72, N'Quần chúng nhân dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (231, N'C', 72, N'Đảng Cộng sản Việt Nam', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (232, N'D', 72, N'Hệ thống chính trị', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (233, N'A', 92, N'Là nghĩa vụ số một, là trách nhiệm đầu tiên của mọi công dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (234, N'B', 92, N'Là sẵn sàng chiến đấu hy sinh vì Tổ quốc', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (235, N'C', 92, N'Là nghĩa vụ và trách nhiệm của mọi công dân', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (236, N'D', 92, N'Là nghĩa vụ của mọi công dân Việt Nam', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (237, N'C', 94, N'Là sức mạnh của toàn dân của các cấp, các ngành, các tổ chức đoàn thể', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (238, N'D', 94, N'Là sức mạnh nền quốc phòng toàn dân do nhiều yếu tố, nhân tố tạo thành', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (239, N'C', 95, N'Tăng cường tiềm lực quốc phòng gắn với phát triển kinh tế - xã hội', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (240, N'D', 95, N'Tăng cường tiềm lực an ninh gắn với hợp tác quốc tế', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (246, N'A', 100, N'buried', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (247, N'B', 100, N'busy ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (248, N'C', 100, N'absorbed', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (249, N'D', 100, N'helping', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (250, N'A', 101, N'Following', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (251, N'B', 101, N'According to', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (252, N'C', 101, N'Hearing', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (253, N'D', 101, N'meaning', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (254, N'A', 102, N'take', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (255, N'B', 102, N'except', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (256, N'C', 102, N'agree', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (257, N'D', 102, N'accept', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (258, N'A', 103, N'nevertheless ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (259, N'B', 103, N'accordingly ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (260, N'C', 103, N'yet ', 1)
INSERT [dbo].[Answer] ([AnswerID], [AnswerTitle], [QuestionID], [AnswerContent], [Status]) VALUES (261, N'D', 103, N'eventually', 1)
SET IDENTITY_INSERT [dbo].[Answer] OFF
GO
SET IDENTITY_INSERT [dbo].[Chapter] ON 

INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (1, N'Ngữ Pháp', 10)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (3, N'Từ vựng', 10)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (6, N'GDQP1', 11)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (7, N'GDQP2', 11)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (8, N'Chương 1', 12)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (9, N'Chương 2', 12)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (10, N'Chương 1', 19)
INSERT [dbo].[Chapter] ([ChapterID], [ChapterName], [SubjectID]) VALUES (13, N'Chương 2', 19)
SET IDENTITY_INSERT [dbo].[Chapter] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([CommentID], [Message], [ExamID], [CreatedDate], [Status]) VALUES (1, N'hello', 1, CAST(N'2023-06-11T07:09:47.210' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Message], [ExamID], [CreatedDate], [Status]) VALUES (2, N'hello1', 1, CAST(N'2023-06-11T07:10:25.007' AS DateTime), 1)
INSERT [dbo].[Comment] ([CommentID], [Message], [ExamID], [CreatedDate], [Status]) VALUES (13, N'Bài thi còn đơn giản lắm', 1, CAST(N'2023-06-21T23:38:07.317' AS DateTime), 0)
INSERT [dbo].[Comment] ([CommentID], [Message], [ExamID], [CreatedDate], [Status]) VALUES (14, N'kltn', 1, CAST(N'2023-06-24T16:54:09.737' AS DateTime), 0)
INSERT [dbo].[Comment] ([CommentID], [Message], [ExamID], [CreatedDate], [Status]) VALUES (15, N'tiếp tục tạo thêm bài để mọi người ôn tập nhé ad', 6, CAST(N'2023-06-26T04:18:13.030' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [RoleID], [CreatedDate], [Status]) VALUES (5, N'Tạ Cao Ngọc Ngà', N'admin', N'oYnYahRssQFPAgoPBItcFQ==', CAST(N'2000-01-30' AS Date), N'Nga.197ct13513@vanlanguni.vn', N'0964965826', N'69/68 Đặng Thùy Trâm, P. 13, Q. Bình Thạnh, Tp.Hồ Chí Minh', 2, CAST(N'2023-03-26T01:37:54.000' AS DateTime), 1)
INSERT [dbo].[Employee] ([EmployeeID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [RoleID], [CreatedDate], [Status]) VALUES (24, N'Tạ Ngà', N'tanga302', N'oYnYahRssQFPAgoPBItcFQ==', CAST(N'2000-02-03' AS Date), N'tanga0203@email.com', N'0121731962', N'tp.Hồ Chí Minh', 1, CAST(N'2023-06-08T16:09:25.443' AS DateTime), 1)
INSERT [dbo].[Employee] ([EmployeeID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [RoleID], [CreatedDate], [Status]) VALUES (25, N'Nguyễn Văn A', N'nguyenvana152', N'oYnYahRssQFPAgoPBItcFQ==', CAST(N'2002-12-02' AS Date), N'nguyenvana@email.com', N'0967941745', N'Cần Thơ', 1, CAST(N'2023-06-10T19:31:22.783' AS DateTime), 1)
INSERT [dbo].[Employee] ([EmployeeID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [RoleID], [CreatedDate], [Status]) VALUES (28, N'Lê Thị B', N'Blethi', N'oYnYahRssQFPAgoPBItcFQ==', NULL, NULL, NULL, NULL, 1, CAST(N'2023-06-21T23:04:55.527' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Exam] ON 

INSERT [dbo].[Exam] ([ExamID], [Title], [ExamTime], [NumOfQuestions], [Levels], [SubjectID], [CreatedDate], [ModifiedDate], [Status]) VALUES (1, N'Ngữ pháp - Bài 1', 4, 10, N'1-1:5,2:3,3:2,;', 10, CAST(N'2023-04-26T00:41:38.927' AS DateTime), CAST(N'2023-06-27T03:17:05.003' AS DateTime), 0)
INSERT [dbo].[Exam] ([ExamID], [Title], [ExamTime], [NumOfQuestions], [Levels], [SubjectID], [CreatedDate], [ModifiedDate], [Status]) VALUES (3, N'Ngữ pháp - Bài 2', 5, 10, N'1-1:0,2:5,3:5,;', 10, CAST(N'2023-04-26T02:57:16.017' AS DateTime), CAST(N'2023-06-24T16:30:19.097' AS DateTime), 0)
INSERT [dbo].[Exam] ([ExamID], [Title], [ExamTime], [NumOfQuestions], [Levels], [SubjectID], [CreatedDate], [ModifiedDate], [Status]) VALUES (5, N'GDQP-1: Bài 1', 10, 10, N'6-1:5,2:3,3:2,;', 11, CAST(N'2023-04-26T16:42:07.327' AS DateTime), CAST(N'2023-06-23T18:43:04.900' AS DateTime), 0)
INSERT [dbo].[Exam] ([ExamID], [Title], [ExamTime], [NumOfQuestions], [Levels], [SubjectID], [CreatedDate], [ModifiedDate], [Status]) VALUES (6, N'GDQP-1: Bài 2', 5, 10, N'6-1:0,2:5,3:5,;', 11, CAST(N'2023-04-30T22:40:56.050' AS DateTime), CAST(N'2023-06-24T16:30:27.320' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Exam] OFF
GO
INSERT [dbo].[ExamHis] ([ExamHisID], [StudentID], [ExamID], [Score], [TakeExamTime], [Questions], [Answers], [CreatedDate]) VALUES (1, 4, 1, CAST(10 AS Decimal(18, 0)), 54000, N'50;53;52;51;54;55;57;56;61;60', N'A;C;D;A;D;A;A;B;B;A', CAST(N'2023-06-21T09:23:28.877' AS DateTime))
INSERT [dbo].[ExamHis] ([ExamHisID], [StudentID], [ExamID], [Score], [TakeExamTime], [Questions], [Answers], [CreatedDate]) VALUES (2, 4, 3, CAST(6 AS Decimal(18, 0)), 66000, N'56;55;59;58;57;60;63;61;62;64', N'A;;D;D;B;A;D;B;C;A', CAST(N'2023-06-21T09:24:48.877' AS DateTime))
INSERT [dbo].[ExamHis] ([ExamHisID], [StudentID], [ExamID], [Score], [TakeExamTime], [Questions], [Answers], [CreatedDate]) VALUES (3, 4, 1, CAST(0 AS Decimal(18, 0)), 241000, N'51;54;50;52;53;55;56;57;60;61', N';;;;;;;;;', CAST(N'2023-06-23T16:59:06.810' AS DateTime))
INSERT [dbo].[ExamHis] ([ExamHisID], [StudentID], [ExamID], [Score], [TakeExamTime], [Questions], [Answers], [CreatedDate]) VALUES (4, 4, 5, CAST(1 AS Decimal(18, 0)), 33000, N'67;68;69;65;66;70;71;72;92;74', N'B,A,D,C;;;;;;;;;', CAST(N'2023-06-24T16:50:21.680' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([Level_ID], [LevelName]) VALUES (1, N'Dễ        ')
INSERT [dbo].[Level] ([Level_ID], [LevelName]) VALUES (2, N'Trung bình')
INSERT [dbo].[Level] ([Level_ID], [LevelName]) VALUES (3, N'Khó       ')
SET IDENTITY_INSERT [dbo].[Level] OFF
GO
SET IDENTITY_INSERT [dbo].[Question] ON 

INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (50, N'I think John_______tomorrow.', N'A', 1, 10, 1, CAST(N'2023-04-18T15:39:56.000' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (51, N' When she was 21 she_______across the United States.', N'A', 1, 10, 1, CAST(N'2023-04-18T15:42:08.470' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (52, N'When the phone rang she_______a letter.', N'D', 1, 10, 1, CAST(N'2023-04-18T15:43:04.623' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (53, N'Has your teacher ever_______angry with you?', N'C', 1, 10, 1, CAST(N'2023-04-18T15:59:30.443' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (54, N'They_______to the theatre twice so far this month.', N'D', 1, 10, 1, CAST(N'2023-04-18T16:02:30.963' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (55, N'How many pages_______so far?', N'A', 2, 10, 1, CAST(N'2023-04-18T16:15:30.253' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (56, N'We are too late. The plane_______off ten minutes ago.', N'B', 2, 10, 1, CAST(N'2023-04-18T16:18:07.217' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (57, N'When I got to the office, I_______that I had forgot to lock the door.', N'A', 2, 10, 1, CAST(N'2023-04-18T16:19:27.077' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (58, N'I_______your uncle tomorrow, so I’ll give him your note.', N'D', 2, 10, 1, CAST(N'2023-04-18T16:20:30.833' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (59, N'When Joe was at school, he was very good_______running.', N'D', 2, 10, 1, CAST(N'2023-04-18T16:22:39.893' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (60, N'Many_______believe dreams help us to understand our lives.', N'A', 3, 10, 1, CAST(N'2023-04-18T16:27:24.563' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (61, N'We haven’t got_______flour for dinner. ', N'B', 3, 10, 1, CAST(N'2023-04-18T16:36:07.910' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (62, N'English and French_______in Canada.', N'C', 3, 10, 1, CAST(N'2023-04-24T03:19:18.457' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (63, N'“It’s really raining.” “Yes. If the weather_______, we’ll have to give upcamping.”', N'B', 3, 10, 1, CAST(N'2023-04-24T03:19:39.723' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (64, N'This room is smoky. I wish you_______smoking.', N'A', 3, 10, 1, CAST(N'2023-04-24T03:29:28.130' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (65, N'Thực hiện tốt Giáo dục quốc phòng – an ninh cho sinh viên là góp phần:', N'D', 1, 11, 6, CAST(N'2023-04-24T14:56:56.583' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (66, N'Giáo dục quốc phòng – an ninh là môn học bao gồm những kiến thức khoa học:', N'C', 1, 11, 6, CAST(N'2023-04-24T15:51:16.560' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (67, N'Đối tượng nghiên cứu của môn học Giáo dục quốc phòng – an ninh:', N'A', 1, 11, 6, CAST(N'2023-04-24T15:54:16.683' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (68, N'Nghiên cứu những quan điểm cơ bản của Đảng về đường lối quân sự gồm:', N'D', 1, 11, 6, CAST(N'2023-04-24T21:10:42.030' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (69, N'Dựa trên cơ sở nào Hồ Chí Minh đã xác định tính chất xã hội của chiến tranh?', N'C', 1, 11, 6, CAST(N'2023-04-24T21:11:07.477' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (70, N'Một trong những nguyên tắc quan trọng nhất về xây dựng quân đội kiểu mới của Lênin là:', N'A', 2, 11, 6, CAST(N'2023-04-24T21:12:08.533' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (71, N'Bạo lực cách mạng theo tư tưởng Hồ Chí Minh được tạo bởi:', N'A', 2, 11, 6, CAST(N'2023-04-24T21:31:14.583' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (72, N'Vai trò lãnh đạo trong bảo vệ tổ quốc xã hội chủ nghĩa thuộc về:', N'C', 2, 11, 6, CAST(N'2023-04-25T22:31:53.070' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (73, N'Theo quan điểm của chủ nghĩa Mác-Lênin về chiến tranh:', N'A', 2, 11, 6, CAST(N'2023-05-27T01:13:22.990' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (74, N'Hồ Chí Minh đã chỉ rõ cuộc chiến tranh của dân ta chống thực dân Pháp xâm lược là nhằm:', N'D', 3, 11, 6, CAST(N'2023-05-27T01:42:16.593' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (92, N'Chủ tịch Hồ Chí Minh xác định nghĩa vụ, trách nhiệm của công dân về bảo vệ Tổ quốc:', N'C', 3, 11, 6, CAST(N'2023-05-28T21:23:49.867' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (93, N'Theo tư tưởng Hồ Chí Minh nhất thiết phải sử dụng bạo lực cách mạng:', N'C', 3, 11, 6, CAST(N'2023-05-28T21:29:27.560' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (94, N'Sức mạnh bảo vệ Tổ quốc theo tư tưởng Hồ Chí Minh là gì?', N'B', 3, 11, 6, CAST(N'2023-05-28T21:47:42.010' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (95, N'Theo quan điểm CN Mác Lênin để bảo vệ tổ quốc Xã hội chủ nghĩa phải:', N'C', 3, 11, 6, CAST(N'2023-05-29T21:12:39.097' AS DateTime), 1)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (100, N'The house was burgled while the family was____________ in a card game.', N'C', 1, 10, 3, CAST(N'2023-06-21T11:46:03.810' AS DateTime), 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (101, N'_________what he says, he wasn''t even there when the crime was committed.', N'B', 1, 10, 3, CAST(N'2023-06-23T13:28:09.260' AS DateTime), 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (102, N'I am sorry that I can''t________ your invitation.', N'D', 1, 10, 3, CAST(N'2023-06-23T13:51:36.380' AS DateTime), 0)
INSERT [dbo].[Question] ([QuestionID], [QuestionContent], [CorrectAnswer], [Level_ID], [SubjectID], [ChapterID], [CreatedDate], [Status]) VALUES (103, N'He has impressed his employers considerably and_______ he is soon to be promoted. ', N'D', 1, 10, 3, CAST(N'2023-06-24T12:06:16.080' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Cộng tác viên')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Admin')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [CreatedDate], [Status]) VALUES (4, N'Tạ Cao Ngọc Ngà', N'tcnnga301', N'oYnYahRssQFPAgoPBItcFQ==', CAST(N'2000-01-30' AS Date), N'nga301@email.com', N'0901200212', N'69/68 Đặng Thùy Trâm, P. 13, Q. Bình Thạnh, Tp.Hồ Chí Minh', CAST(N'2023-04-20T00:47:34.263' AS DateTime), 1)
INSERT [dbo].[Student] ([StudentID], [FullName], [UserName], [Password], [BirthDay], [Email], [Phone], [Address], [CreatedDate], [Status]) VALUES (27, N'Tạ Cao Ngọc Ngà', N'Nga.13513', N'oYnYahRssQFPAgoPBItcFQ==', NULL, N'nga13513@email.com', NULL, NULL, CAST(N'2023-04-23T16:39:30.163' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [EmployeeID]) VALUES (10, N'Tiếng anh', 24)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [EmployeeID]) VALUES (11, N'GDQP', 5)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [EmployeeID]) VALUES (12, N'Tư tưởng Hồ Chí Minh', 5)
INSERT [dbo].[Subject] ([SubjectID], [SubjectName], [EmployeeID]) VALUES (19, N'Triết học Mác-Lênin', 5)
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
ALTER TABLE [dbo].[Answer] ADD  CONSTRAINT [DF_Answer_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_comment_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_comment_Status]  DEFAULT ((2)) FOR [Status]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Admin_role_id]  DEFAULT ((1)) FOR [RoleID]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Exam] ADD  CONSTRAINT [DF_Exam_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Exam] ADD  CONSTRAINT [DF_Exam_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[ExamHis] ADD  CONSTRAINT [DF_Result_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_CrateDate_1]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Question] ADD  CONSTRAINT [DF_Question_Status_1]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Subject] ADD  CONSTRAINT [DF_Subject_EmployeeID]  DEFAULT ((1)) FOR [EmployeeID]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Question] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Question]
GO
ALTER TABLE [dbo].[Chapter]  WITH CHECK ADD  CONSTRAINT [FK_Chapter_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Chapter] CHECK CONSTRAINT [FK_Chapter_Subject]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ExamID])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_comment_Exam]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Admin_Role]
GO
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK_Exam_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK_Exam_Subject]
GO
ALTER TABLE [dbo].[ExamHis]  WITH CHECK ADD  CONSTRAINT [FK_ExamHis_Exam] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exam] ([ExamID])
GO
ALTER TABLE [dbo].[ExamHis] CHECK CONSTRAINT [FK_ExamHis_Exam]
GO
ALTER TABLE [dbo].[ExamHis]  WITH CHECK ADD  CONSTRAINT [FK_ExamHis_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[ExamHis] CHECK CONSTRAINT [FK_ExamHis_Student]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Chapter] FOREIGN KEY([ChapterID])
REFERENCES [dbo].[Chapter] ([ChapterID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Chapter]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Level] FOREIGN KEY([Level_ID])
REFERENCES [dbo].[Level] ([Level_ID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Level]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_Subject]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Admin] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Admin]
GO
