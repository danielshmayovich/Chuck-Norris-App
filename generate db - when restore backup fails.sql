
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJokes]
	
AS
BEGIN
	
	select * from Jokes

END

GO
/****** Object:  StoredProcedure [dbo].[spSaveJokeLike]    Script Date: 15/02/2017 20:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSaveJokeLike]
	@Id int
AS
BEGIN
	
	insert into JokesVotes	(JokeId, LikeTimeStamp)
	values					(@Id, GETDATE())

END

GO
/****** Object:  Table [dbo].[Jokes]    Script Date: 15/02/2017 20:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jokes](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Body] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Jokes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JokesVotes]    Script Date: 15/02/2017 20:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JokesVotes](
	[JokeId] [int] NOT NULL,
	[LikeTimeStamp] [datetime] NOT NULL,
 CONSTRAINT [PK_JokesVotes] PRIMARY KEY CLUSTERED 
(
	[JokeId] ASC,
	[LikeTimeStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Jokes] ON 

GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1000, N'Chuck Norris once heard that nothing can kill him, so he tracked down nothing and killed it.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1001, N'While other children were playing in sand, Chuck was playing in concrete.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1002, N'Chuck Norris doesn''t worry about high gas prices. His vehicles run on fear.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1003, N'Chuck Norris has a diary. It''s called the Guinness Book of World Records.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1004, N'The First Law of Thermodynamics states that energy can neither be created nor destroyed... unless it meets Chuck Norris.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1005, N'Chuck Norris threw a grenade and killed 50 people, then it exploded.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1006, N'When a zombie apocalypse starts, Chuck Norris doesn''t try to survive. The zombies do.')
GO
INSERT [dbo].[Jokes] ([Id], [Body]) VALUES (1007, N'Chuck Norris narrates Morgan Freeman''s life.')
GO
SET IDENTITY_INSERT [dbo].[Jokes] OFF
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1002, CAST(0x0000A71B0157E4FC AS DateTime))
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1003, CAST(0x0000A71B0157F087 AS DateTime))
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1005, CAST(0x0000A71B0157F1C2 AS DateTime))
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1006, CAST(0x0000A71B01539C84 AS DateTime))
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1006, CAST(0x0000A71B0153A803 AS DateTime))
GO
INSERT [dbo].[JokesVotes] ([JokeId], [LikeTimeStamp]) VALUES (1006, CAST(0x0000A71B0157EE90 AS DateTime))
GO
ALTER TABLE [dbo].[JokesVotes]  WITH CHECK ADD  CONSTRAINT [FK_JokesVotes_Jokes] FOREIGN KEY([JokeId])
REFERENCES [dbo].[Jokes] ([Id])
GO
ALTER TABLE [dbo].[JokesVotes] CHECK CONSTRAINT [FK_JokesVotes_Jokes]
GO
USE [master]
GO
ALTER DATABASE [CNDB] SET  READ_WRITE 
GO
