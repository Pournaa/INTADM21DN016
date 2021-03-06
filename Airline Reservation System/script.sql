/*DB generated based on Code First */
USE [master]
GO
/****** Object:  Database [FlightReservationProjectContext]    Script Date: 05-07-2021 19:06:19 ******/
CREATE DATABASE [FlightReservationProjectContext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlightReservationProjectContext', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FlightReservationProjectContext.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlightReservationProjectContext_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FlightReservationProjectContext_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FlightReservationProjectContext] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlightReservationProjectContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlightReservationProjectContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FlightReservationProjectContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlightReservationProjectContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FlightReservationProjectContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FlightReservationProjectContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlightReservationProjectContext] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FlightReservationProjectContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FlightReservationProjectContext] SET  MULTI_USER 
GO
ALTER DATABASE [FlightReservationProjectContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FlightReservationProjectContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlightReservationProjectContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FlightReservationProjectContext] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FlightReservationProjectContext] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FlightReservationProjectContext] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FlightReservationProjectContext] SET QUERY_STORE = OFF
GO
USE [FlightReservationProjectContext]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 05-07-2021 19:06:19 ******/
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
/****** Object:  Table [dbo].[Feedback]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[name] [nvarchar](max) NOT NULL,
	[feed] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_dbo.Feedback] PRIMARY KEY CLUSTERED 
(
	[feedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[flightId] [int] IDENTITY(1,1) NOT NULL,
	[flightName] [nvarchar](max) NOT NULL,
	[seatFirstclass] [int] NOT NULL,
	[seatBusinessclass] [int] NOT NULL,
	[seatEconomyclass] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Flight] PRIMARY KEY CLUSTERED 
(
	[flightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[paymentId] [int] IDENTITY(1,1) NOT NULL,
	[paymentMode] [nvarchar](max) NULL,
	[totalAmount] [float] NOT NULL,
	[bankDetails] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Payment] PRIMARY KEY CLUSTERED 
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[scheduleId] [int] IDENTITY(1,1) NOT NULL,
	[flightId] [int] NOT NULL,
	[source] [nvarchar](max) NOT NULL,
	[sourceId] [int] NOT NULL,
	[dest] [nvarchar](max) NOT NULL,
	[destId] [int] NOT NULL,
	[scheduleDate] [datetime] NOT NULL,
	[depatureTime] [time](7) NOT NULL,
	[arrivalDate] [datetime] NOT NULL,
	[arrivalTime] [time](7) NOT NULL,
	[status] [nvarchar](max) NOT NULL,
	[FCseats] [int] NOT NULL,
	[BCseats] [int] NOT NULL,
	[ECseats] [int] NOT NULL,
	[costFirst] [float] NOT NULL,
	[costBusiness] [float] NOT NULL,
	[costEconomy] [float] NOT NULL,
	[traveltype] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Schedule] PRIMARY KEY CLUSTERED 
(
	[scheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblsecond]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblsecond](
	[myNumbers] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 05-07-2021 19:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[ticketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[flightId] [int] NOT NULL,
	[scheduleId] [int] NOT NULL,
	[paymentId] [int] NOT NULL,
	[dateOfJourney] [datetime] NOT NULL,
	[seatNo] [int] NOT NULL,
	[passengerName] [nvarchar](max) NULL,
	[phoneNumber] [nvarchar](max) NULL,
	[address] [nvarchar](max) NOT NULL,
	[emergencyContact] [nvarchar](max) NULL,
	[travelclass] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Ticket] PRIMARY KEY CLUSTERED 
(
	[ticketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_flightId]    Script Date: 05-07-2021 19:06:20 ******/
CREATE NONCLUSTERED INDEX [IX_flightId] ON [dbo].[Schedule]
(
	[flightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_flightId]    Script Date: 05-07-2021 19:06:20 ******/
CREATE NONCLUSTERED INDEX [IX_flightId] ON [dbo].[Ticket]
(
	[flightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_paymentId]    Script Date: 05-07-2021 19:06:20 ******/
CREATE NONCLUSTERED INDEX [IX_paymentId] ON [dbo].[Ticket]
(
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_scheduleId]    Script Date: 05-07-2021 19:06:20 ******/
CREATE NONCLUSTERED INDEX [IX_scheduleId] ON [dbo].[Ticket]
(
	[scheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Schedule_dbo.Flight_flightId] FOREIGN KEY([flightId])
REFERENCES [dbo].[Flight] ([flightId])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_dbo.Schedule_dbo.Flight_flightId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Ticket_dbo.Flight_flightId] FOREIGN KEY([flightId])
REFERENCES [dbo].[Flight] ([flightId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_dbo.Ticket_dbo.Flight_flightId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Ticket_dbo.Payment_paymentId] FOREIGN KEY([paymentId])
REFERENCES [dbo].[Payment] ([paymentId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_dbo.Ticket_dbo.Payment_paymentId]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Ticket_dbo.Schedule_scheduleId] FOREIGN KEY([scheduleId])
REFERENCES [dbo].[Schedule] ([scheduleId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_dbo.Ticket_dbo.Schedule_scheduleId]
GO
USE [master]
GO
ALTER DATABASE [FlightReservationProjectContext] SET  READ_WRITE 
GO
