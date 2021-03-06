USE [master]
GO
/****** Object:  Database [VFDB]    Script Date: 3/24/2020 11:54:33 AM ******/
CREATE DATABASE [VFDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VFDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VFDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VFDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\VFDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VFDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VFDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VFDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VFDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VFDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VFDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VFDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [VFDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VFDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VFDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VFDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VFDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VFDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VFDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VFDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VFDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VFDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VFDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VFDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VFDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VFDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VFDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VFDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VFDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VFDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VFDB] SET  MULTI_USER 
GO
ALTER DATABASE [VFDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VFDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VFDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VFDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VFDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VFDB] SET QUERY_STORE = OFF
GO
USE [VFDB]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 3/24/2020 11:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)
           
            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
 
      RETURN
END
GO
/****** Object:  Table [dbo].[VFRoles]    Script Date: 3/24/2020 11:54:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VFRoles](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_VFRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VFUserRoles]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VFUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VFUsers]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VFUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](128) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NULL,
	[CommunicationAddress] [text] NULL,
	[IsActive] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_VFUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[VFRoles] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[VFRoles] ([RoleId], [RoleName]) VALUES (2, N'Supervisor')
INSERT [dbo].[VFRoles] ([RoleId], [RoleName]) VALUES (3, N'Agent')
INSERT [dbo].[VFUserRoles] ([UserId], [RoleId], [CreateDate]) VALUES (2, 3, CAST(N'2020-03-24T04:09:12.067' AS DateTime))
INSERT [dbo].[VFUserRoles] ([UserId], [RoleId], [CreateDate]) VALUES (1, 1, CAST(N'2020-03-24T06:03:07.950' AS DateTime))
SET IDENTITY_INSERT [dbo].[VFUsers] ON 

INSERT [dbo].[VFUsers] ([UserId], [Email], [Password], [FullName], [Phone], [CommunicationAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (1, N'abhi1@gmail.com', N'@Abhi007', N'Abhinandan', N'888888888', N'730 The Kingsway #6', 1, CAST(N'2020-03-24T02:10:59.563' AS DateTime), CAST(N'2020-03-24T06:03:07.950' AS DateTime))
INSERT [dbo].[VFUsers] ([UserId], [Email], [Password], [FullName], [Phone], [CommunicationAddress], [IsActive], [CreateDate], [UpdateDate]) VALUES (2, N'abhi11@gmail.com', N'@Abhi007', N'sadsd', NULL, NULL, 1, CAST(N'2020-03-24T04:09:11.763' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[VFUsers] OFF
ALTER TABLE [dbo].[VFUserRoles] ADD  CONSTRAINT [DF_UserRoles_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[VFUsers] ADD  CONSTRAINT [DF_VFUser_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Get] 
(
	@Id int =NULL,
	@Email varchar(128) =NULL
)
	
AS
BEGIN
	
	SELECT *, (SELECT STUFF((SELECT  ', ' + convert(varchar(30),RoleId) FROM(SELECT RoleId FROM VFUserRoles where UserId=u.UserId) AS T FOR XML PATH('')
),1,1,'')) AS RolesId FROM VFUsers u WHERE 
	(@Id is NULL or UserId = @Id)
	AND
	(@Email is NULL or Email = @Email)
	

END
GO
/****** Object:  StoredProcedure [dbo].[User_Set]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Set] 
(
	@Id int OUTPUT,	
	@Email varchar(128)=NULL,
	@Password	nvarchar(128)=NULL,
	@FullName	nvarchar(200)=NULL,
	@Phone varchar(20)=NULL,
	@CommunicationAddress text=NULL,
	@IsActive bit ,
	@RolesId	varchar(200)=NULL
)
	
AS
BEGIN
BEGIN TRY
    BEGIN TRANSACTION
		IF(@Id != 0)
				BEGIN

				Update [VFUsers] Set
				Email=@Email,
				FullName = @FullName,
				Phone=@Phone,
				CommunicationAddress=@CommunicationAddress,
				IsActive=@IsActive,
				UpdateDate = GETDATE()
				WHERE UserId=@Id

				END
			ELSE
				BEGIN

					INSERT INTO [dbo].[VFUsers]
				   (
					[Email]
				   ,[Password]
				   ,[FullName]
				   ,[Phone]
				   ,[CommunicationAddress]
				   ,[IsActive]
				   )
			 VALUES
				   (
					@Email
				   ,@Password
				   ,@FullName
				   ,@Phone
				   ,@CommunicationAddress
				   ,@IsActive
				   )
			
				set @Id = SCOPE_IDENTITY();
				END
				IF(@RolesId is not NULL)
				BEGIN
				Delete from VFUserRoles where UserId=@Id

				INSERT INTO VFUserRoles(UserId, RoleId) SELECT @Id, CAST(Item AS INTEGER) FROM SplitString(@RolesId, ',') 
				END
			COMMIT
		END TRY
		BEGIN CATCH
			ROLLBACK
		END CATCH

END

GO
/****** Object:  StoredProcedure [dbo].[UserLogin_Get]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserLogin_Get] 
(
	@Email varchar(128),
	@Password nvarchar(128)
)
	
AS
BEGIN
	
	SELECT *, (SELECT STUFF((SELECT  ', ' + convert(varchar(30),RoleId) FROM(SELECT RoleId FROM VFUserRoles where UserId=u.UserId) AS T FOR XML PATH('')
),1,1,'')) AS RolesId
 FROM VFUsers u WHERE 
	
	(Email = @Email AND Password = @Password)
	

END
GO
/****** Object:  StoredProcedure [dbo].[UserRoles_Get]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserRoles_Get] 
(
	@UserId int=NULL,
	@Email varchar(128)=NULL
)
	
AS
BEGIN
	DECLARE @FoundUserId varchar(128);
	IF(@Email is NULL)
		BEGIN
			SELECT * FROM VFRoles WHERE RoleId in(select DISTINCT RoleId from VFUserRoles where UserId=@UserId)
		END
	ELSE
		BEGIN
			Select @FoundUserId=UserId from VFUsers where Email=@Email
			SELECT * FROM VFRoles WHERE RoleId in(select DISTINCT RoleId from VFUserRoles where UserId=@FoundUserId)
		END

END
GO
/****** Object:  StoredProcedure [dbo].[UserStatus_Update]    Script Date: 3/24/2020 11:54:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserStatus_Update] 
(
	@Id int OUTPUT,	
	
	@IsActive bit 
)
	
AS
BEGIN
BEGIN
		Update [VFUsers] Set
		IsActive=@IsActive,
		UpdateDate = GETDATE()
		WHERE UserId=@Id
			
		set @Id = SCOPE_IDENTITY();
		END
END

GO
USE [master]
GO
ALTER DATABASE [VFDB] SET  READ_WRITE 
GO
