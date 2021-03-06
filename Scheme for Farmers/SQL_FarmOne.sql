
/****** Object:  Database [Farmers]    ******/

CREATE TABLE [dbo].[Admin](
         [Admin_ID] [int] NOT NULL Primary Key,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL
) ON [PRIMARY]


/****** Object:  Table [dbo].[Adminapproval]     ******/

CREATE TABLE [dbo].[Adminapproval](
	[Farmer_ID] [int] NOT NULL,
	[Crop_ID] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Bidder_ID] [int] NULL,
	[Adminapp_ID] [int] IDENTITY(300,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Adminapp_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[BidderReg]     ******/

CREATE TABLE [dbo].[BidderReg](
	[Bidder_ID] [int] IDENTITY(900,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Contact_no] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Pincode] [varchar](50) NOT NULL,
	[AccountNo] [varchar](50) NOT NULL,
	[IFSC_code] [varchar](50) NOT NULL,
	[Aadhaar] [nvarchar](max) NULL,
	[PAN] [nvarchar](max) NULL,
	[Trader_license] [nvarchar](max) NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BidderReg] PRIMARY KEY CLUSTERED 
(
	[Bidder_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [dbo].[Bidding]    ******/

CREATE TABLE [dbo].[Bidding](
	[BiddingID] [int] IDENTITY(400,1) NOT NULL,
	[Crop_ID] [int] NOT NULL,
	[Crop_type] [varchar](50) NOT NULL,
	[Baseprice] [float] NOT NULL,
	[Current_Bid] [float] NOT NULL,
	[Bidder_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BiddingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[FarmerReg]     ******/

CREATE TABLE [dbo].[FarmerReg](
	[Farmer_ID] [int] IDENTITY(100,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Contact_no] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Pincode] [varchar](50) NOT NULL,
	[Area] [float] NOT NULL,
	[Land_Address] [varchar](100) NOT NULL,
	[Land_Pincode] [varchar](50) NOT NULL,
	[AccountNo] [varchar](50) NOT NULL,
	[IFSC_code] [varchar](50) NOT NULL,
	[Aadhaar] [nvarchar](max) NULL,
	[PAN] [nvarchar](max) NULL,
	[Certificate] [nvarchar](max) NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Farmer_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SellRequest]    ******/
CREATE TABLE [dbo].[SellRequest](
	[Crop_ID] [int] IDENTITY(200,1) NOT NULL,
	[Crop_type] [varchar](50) NOT NULL,
	[Crop_name] [varchar](50) NOT NULL,
	[Fertilizer] [varchar](100) NOT NULL,
	[Quantity] [float] NOT NULL,
	[Soil_PH] [varchar](250) NOT NULL,
	[Baseprice] [float] NULL,
	[Farmer_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Crop_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Soldhistory]    ******/

CREATE TABLE [dbo].[Soldhistory](
	[Sold_ID] [int] IDENTITY(500,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Crop_ID] [int] NOT NULL,
	[Crop_name] [varchar](50) NOT NULL,
	[Quantity] [float] NOT NULL,
	[Baseprice] [float] NOT NULL,
	[Soldprice] [float] NOT NULL,
	[Totalprice] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Sold_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Adminapproval]  WITH CHECK ADD FOREIGN KEY([Bidder_ID])
REFERENCES [dbo].[BidderReg] ([Bidder_ID])
GO
ALTER TABLE [dbo].[Adminapproval]  WITH CHECK ADD FOREIGN KEY([Crop_ID])
REFERENCES [dbo].[SellRequest] ([Crop_ID])
GO
ALTER TABLE [dbo].[Adminapproval]  WITH CHECK ADD FOREIGN KEY([Farmer_ID])
REFERENCES [dbo].[FarmerReg] ([Farmer_ID])
GO
ALTER TABLE [dbo].[Bidding]  WITH CHECK ADD FOREIGN KEY([Bidder_ID])
REFERENCES [dbo].[BidderReg] ([Bidder_ID])
GO
ALTER TABLE [dbo].[Bidding]  WITH CHECK ADD FOREIGN KEY([Crop_ID])
REFERENCES [dbo].[SellRequest] ([Crop_ID])
GO
ALTER TABLE [dbo].[SellRequest]  WITH CHECK ADD FOREIGN KEY([Farmer_ID])
REFERENCES [dbo].[FarmerReg] ([Farmer_ID])
GO
ALTER TABLE [dbo].[Soldhistory]  WITH CHECK ADD FOREIGN KEY([Crop_ID])
REFERENCES [dbo].[SellRequest] ([Crop_ID])
GO
USE [master]
GO
ALTER DATABASE [Farmers] SET  READ_WRITE 
GO
