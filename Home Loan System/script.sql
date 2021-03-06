USE [master]
GO
/****** Object:  Database [homee]    Script Date: 05-07-2021 18:17:13 ******/
CREATE DATABASE [homee]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'homee', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\homee.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'homee_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\homee_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [homee] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [homee].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [homee] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [homee] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [homee] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [homee] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [homee] SET ARITHABORT OFF 
GO
ALTER DATABASE [homee] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [homee] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [homee] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [homee] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [homee] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [homee] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [homee] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [homee] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [homee] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [homee] SET  DISABLE_BROKER 
GO
ALTER DATABASE [homee] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [homee] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [homee] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [homee] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [homee] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [homee] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [homee] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [homee] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [homee] SET  MULTI_USER 
GO
ALTER DATABASE [homee] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [homee] SET DB_CHAINING OFF 
GO
ALTER DATABASE [homee] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [homee] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [homee] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [homee] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [homee] SET QUERY_STORE = OFF
GO
USE [homee]
GO
/****** Object:  Table [dbo].[Admin_login]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin_login](
	[Admin_Id] [int] IDENTITY(1,1) NOT NULL,
	[Admin_Name] [varchar](50) NULL,
	[Admin_Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Confirm_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Admin_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MVCregUser]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MVCregUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[F_name] [varchar](20) NULL,
	[L_name] [varchar](20) NULL,
	[MobileNo] [numeric](18, 0) NULL,
	[Email] [varchar](100) NULL,
	[Dob] [datetime] NULL,
	[Address1] [varchar](80) NULL,
	[Address2] [varchar](80) NULL,
	[City] [varchar](25) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](25) NULL,
	[Pincode] [numeric](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[occupation_table]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[occupation_table](
	[Occ_ID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Occ_type] [varchar](50) NULL,
	[Designation] [varchar](80) NULL,
	[Gross_income] [bigint] NULL,
	[Adharcard_No] [bigint] NULL,
	[Pancard_No] [nvarchar](20) NULL,
	[Loan_amount] [bigint] NULL,
	[Tensure] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Occ_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_login]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar](50) NULL,
	[User_Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Confirm_Password] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[occupation_table]  WITH CHECK ADD FOREIGN KEY([ID])
REFERENCES [dbo].[MVCregUser] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[loantracker]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[loantracker](@ID int)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as Age, Gross_income from occupation_table
where ID = @ID
end

GO
/****** Object:  StoredProcedure [dbo].[loantracker1]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[loantracker1](@ID int)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as Age, Gross_income,Loan_amount,Tensure from occupation_table
where ID = @ID
end

GO
/****** Object:  StoredProcedure [dbo].[loantrackeredit]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[loantrackeredit](@ID int,@MobileNo numeric)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as Age, Gross_income,Loan_amount,Tensure from occupation_table
where ID = @ID
end

GO
/****** Object:  StoredProcedure [dbo].[LoginCheck]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[LoginCheck](@User_Name varchar(50), @Password varchar(50)) as
begin
    if(@User_Name = 'kavya' and @Password = 'kav123')
	begin
	 select 1 -- ADMIN
	End
	else if Exists (SELECT User_Name Exist FROM User_login WHERE User_Name = @User_Name AND Password = @Password)
    begin
     select 2 -- USER EXIST
    end
    else 
    begin
     select 0 -- INCORRECT USERID OR PWD
    end
End

GO
/****** Object:  StoredProcedure [dbo].[tracker]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[tracker](@ID int)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as Age, Gross_income,Loan_amount,Tensure from occupation_table
where ID = @ID
end

GO
/****** Object:  StoredProcedure [dbo].[tracker3]    Script Date: 05-07-2021 18:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[tracker3](@ID int)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as [Age],[Gross_income],[Loan_amount],[Tensure] from occupation_table
where ID = @ID
end

GO
USE [master]
GO
ALTER DATABASE [homee] SET  READ_WRITE 
GO
