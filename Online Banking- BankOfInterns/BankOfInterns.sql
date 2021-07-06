CREATE TABLE [dbo].[NewAcc](
	[CustId] [numeric] primary key identity(101,1),
	[Title] [varchar](10) NOT NULL,
	[Fname] [varchar](50) NOT NULL,
	[Lname] [varchar](50) NOT NULL,
	[Phone] [numeric](18, 0) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PAN] [varchar](20) NOT NULL,
	[Aadhar] [numeric](18, 0) NOT NULL,
	[DOB] [date] NOT NULL,
	[ResAdd] [varchar](50) NOT NULL,
	[BranchName] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Pincode] [numeric](18, 0) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[PerAdd] [varchar](50) NOT NULL,
	[PerAdd1] [varchar](50) NOT NULL,
	[City01] [varchar](50) NOT NULL,
	[pincode01] [numeric](18, 0) NOT NULL,
	[state01] [varchar](50) NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[Occupation] [varchar](50) NOT NULL,
	[Income] [numeric](18, 0) NOT NULL
)
go

CREATE TABLE [dbo].[AdminLogin](
	[UserId] [numeric] primary key,
	[Pwd] [nvarchar](max) NOT NULL
)
go

CREATE TABLE [dbo].[Login](
	[UserId] [numeric] primary key,
	[Pwd] [nvarchar](max) NOT NULL,
	[CustId] [numeric] NOT NULL,
	foreign key (CustId) references NewAcc(CustId)
)
go


CREATE TABLE [dbo].[BranchTable](
	[IFSCCode] [varchar](50) primary key,
	[BranchName] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[BranchCode] [numeric] NOT NULL
	)
go

CREATE TABLE [dbo].[AccountMaster](
	[AccNo] [numeric] primary key,
	[AccHolder] [varchar](30) NOT NULL,
	[IFSCCode] [varchar](50) NOT NULL,
	[AccType] [varchar](10) NULL,
	[AccOpenDate] [date] NULL,
	[AccBalance] [int] NOT NULL,
	[AccStatus] [varchar](10) NULL,
	[CustId] [numeric] NOT NULL,
	foreign key (IFSCCode) references BranchTable(IFSCCode),
	foreign key (CustId) references NewAcc(CustId)
	)
go

CREATE TABLE [dbo].[Beneficiary](
	[AccNo] [numeric] NOT NULL,
	[BenAccNo] [numeric] NOT NULL,
	[BenName] [varchar](50) NOT NULL,
	[NickName] [varchar](50) NULL,
	[BenBankName] [varchar](10) NOT NULL,
	[IFSCCode] [varchar](50) NOT NULL,
	[BeneficiaryID] [numeric] primary key identity(500001, 1),
	[BeneficiaryType] [char](4) NULL,
	[Remarks] [varchar](50) NULL,
	foreign key (AccNo) references AccountMaster(AccNo)
	)
go

CREATE TABLE [dbo].[TransTable](
	[AccNo] [numeric] NULL,
	[FromAccNo] [numeric] NULL,
	[ToAccNo] [numeric] NULL,
	[BeneficiaryID] [numeric] NULL,
	[Amount] [smallmoney] NULL,
	[TransDate] [date] NULL,
	[TransTime] [time] NULL,
	[TransType] [varchar](50) NULL,
	[TransactionID] [numeric] primary key identity(120000000001, 1),
	[Remarks] [varchar](50) NULL
	foreign key (AccNo) references AccountMaster(AccNo),
	foreign key (BeneficiaryID) references Beneficiary(BeneficiaryID)
	)
go

INSERT INTO BranchTable([IFSCCode],[BranchName],[City],[BranchCode])VALUES('BOIN0251823','Peelamedu','Coimbatore',251823)
INSERT INTO BranchTable([IFSCCode],[BranchName],[City],[BranchCode])VALUES('BOIN0251824','Vadavalli','Coimbatore',251824)
INSERT INTO BranchTable([IFSCCode],[BranchName],[City],[BranchCode])VALUES('BOIN0251825','Pollachi','Coimbatore',251825)
INSERT INTO BranchTable([IFSCCode],[BranchName],[City],[BranchCode])VALUES('BOIN0251826','Sulur','Coimbatore',251826)
INSERT INTO BranchTable([IFSCCode],[BranchName],[City],[BranchCode])VALUES('BOIN0251827','Singanallur','Coimbatore',251827)
go



--Trigger to have login details generated
CREATE TRIGGER [dbo].[GetUserIdPwd]
       ON [dbo].[NewAcc]
AFTER INSERT
AS
BEGIN
       SET NOCOUNT ON;
 
       DECLARE @CustId INT
	   DECLARE @UserId numeric(18,0)
	   DECLARE @Pwd nvarchar(max)
 
       SELECT @CustId = INSERTED.CustId       
       FROM INSERTED
	   if(@CustId=101)
	   begin
	   select @UserId = 100001
	   select @Pwd = (select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + 
       char(ascii('a')+(Abs(Checksum(NewId()))%25)) +
       char(ascii('A')+(Abs(Checksum(NewId()))%25)) +
       left(newid(),5))
	   end
	   else
	   begin
	   SELECT @UserId = (concat(0,(select right(max(UserId), 6) + 1 from Login)))
	   SELECT @Pwd = (select cast((Abs(Checksum(NewId()))%10) as varchar(1)) + 
       char(ascii('a')+(Abs(Checksum(NewId()))%25)) +
       char(ascii('A')+(Abs(Checksum(NewId()))%25)) +
       left(newid(),5))
	   end
 
       INSERT INTO Login
       VALUES(@UserId, @Pwd, @CustId)
END
GO



--Trigger to fill AccountMaster table details
create trigger tr_AcMaster on NewAcc
after insert
as
begin
declare @AccNo numeric(18,0) 
declare @AccHolder varchar(30) 
declare @IFSCCode varchar(50) 
declare @AccType varchar(10) 
declare @AccOpenDate date 
declare @AccBalance int
declare @AccStatus varchar(10) 
declare @CustId numeric(18,0)
declare @BranchCode numeric(18,0)
select @CustId=CustId from inserted 
select @BranchCode=BranchCode from BranchTable b join inserted i on b.BranchName=i.BranchName  
select @AccNo=  concat(@BranchCode,88,@CustId) 
select @AccHolder= concat(Fname,' ',LName) from NewAcc 
select @IFSCCode=(select b.IFSCCode from BranchTable b join NewAcc na on na.BranchName = b.BranchName join inserted i on na.CustId = i.CustId)
set @AccType='Savings' 
set @AccOpenDate=GETDATE()
set @AccBalance=2000
set @AccStatus='Active'

insert into AccountMaster values(@AccNo,@AccHolder,@IFSCCode,@AccType,@AccOpenDate,@AccBalance,@AccStatus,@CustId)
end
go



--Trigger to update TransTable
alter trigger update_balance_for_debit
on TransTable
after insert 
as 
begin
	update AccountMaster
	set AccBalance=am.AccBalance-i.Amount
	from AccountMaster am join inserted i
	on am.AccNo=i.AccNO
	update AccountMaster
	set AccBalance=am.AccBalance+i.Amount
	from AccountMaster am join inserted i
	on am.AccNo=i.ToAccNO  where i.ToAccNO like'25182%'
	update TransTable set TransType='Debit' where TransactionID=(select max(TransactionID) from TransTable)
	update TransTable set TransDate=GETDATE()  where TransactionID=(select max(TransactionID) from TransTable)
	update TransTable set TransTime=convert(varchar(10),getdate(),108 ) where TransactionID=(select max(TransactionID) from TransTable)
	
end
go


INSERT INTO NewAcc 
values
('Ms','Arthi','Kumarasamy',9876543210,'arthi@gmail.com','CJSPG5887K',676523457896,'1993/03/05','20 Vivekanandhar street','Peelamedu','Coimbatore',641051,'Tamilnadu','20 Vivekanandhar street','Peelamedu','Coimbatore',641051,'Tamilnadu','Female','Business','400000')
go
INSERT INTO NewAcc 
values
('Mrs','Banu','Kailash',9876543210,'banu@gmail.com','CBSIP5827K',432123453456,'1999/05/04','15 Mayflower','Vadavalli','Coimbatore',641022,'Tamilnadu','15 Mayflower','Vadavalli','Coimbatore',641022,'Tamilnadu','Female','Software Engineer','500000')
go
Insert into NewAcc
values
('Mr','Samuel','Percy',9812343210,'samuel@gmail.com','CPQK4321J',482167652345,'2000/02/06','25C Anna nagar','Pollachi','Coimbatore',641024,'Tamilnadu','25C Anna nagar','Pollachi','Coimbatore',641024,'Tamilnadu','Male','Chatered Accountant','700000')
go
Insert into NewAcc
values
('Mr','Derek','Shepherd',9812341234,'derek@gmail.com','DUIK4321S',412367455324,'1991/07/10','101C AbdulKalam Nagar','Sulur','Coimbatore',651024,'Tamilnadu','101C AbdulKalam Nagar','Sulur','Coimbatore',651024,'Tamilnadu','Male','Doctor','1000000')
go
Insert into NewAcc
values
('Mrs','Ashwin','Lakshmidas',8123433210,'ashwin@gmail.com','JHSI3121S',442334552346,'1991/05/03','56 Periyar nagar','Singanallur','Coimbatore',600032,'Tamilnadu','34 Gandhimanagar','Sulur','Coimbaore',651034,'Tamilnadu','Male','Automobile Engineer','600000')
go
Insert into NewAcc
values
('Ms','Manasi','Manohar',8123342334,'manasi@gmail.com','HGSO2351I',624367652345,'2000/02/02','31 Newyork Nagaram','Peelamedu','Coimbatore',641051,'Tamilnadu','12 Periyakulam street','Nattapettai','Kancheepuram',610000,'Tamilnadu','Female','Software Engineer','500000')
go
Insert into NewAcc
values
('Mrs','Christina','Grey',9712312215,'christina@gmail.com','HSTR67810A',416765472556,'1995/09/14','67 Tata colony','Vadavalli','Coimbatore',641022,'Tamilnadu','7 Mandela colony','Kottur','Chennai',600025,'Tamilnadu','Female','Business','1500000')
go
Insert into NewAcc
values
('Mrs','Kirthika','Balamurugan',9124343354,'kirthi@gmail.com','JSIP126H',821665234545,'1996/10/07','28 NGK Road','Pollachi','Coimbatore',641024,'Tamilnadu','28 NGK Road','Velampalayam','Tirupur',651029,'Tamilnadu','Female','Lab Assistant','200000')
go
Insert into NewAcc
values
('Ms','Divya','Subramaniyam',8212367252,'divya@gmail.com','PQCK3251Y',216765234545,'2001/02/06','71 V.K.Road','Sulur','Coimbatore',651024,'Tamilnadu','71 V.K.Road','Kotturpuram','Chennai',600001,'Tamilnadu','Female','Professor','500000')
go
Insert into NewAcc
values
('Mr','Harper','Avery',8126353210,'Harper@gmail.com','HSYK7254K',676523453456,'1975/10/25','53B Amman nagar','Singanallur','Coimbatore',600032,'Tamilnadu','53B Amman nagar','Nava India','Coimbatore',641062,'Tamilnadu','Male','Lawyer','700000')
go



INSERT INTO ADMINLOGIN VALUES (999999, 'BOI_99')
go


INSERT INTO Beneficiary(AccNo,BenAccNo,BenName,NickName,BenBankName,IFSCCode, BeneficiaryType, Remarks)
VALUES
(25182388101,25182488102,'Banu','Banu','BOI','BOIN0251824','Sm','Sister'),
(25182588103,88452136796,'Manas','Manas','SBI','SBIN0056725','Df','Aunt'),
(25182788105,31526248897,'Kritish','Krithu','ICICI', 'ICICI31805','Df',Null),
(25182488107, 25182588103,'Samuel','Sam','BOI', 'BOIN0251825','Sm','Uncle'),
(25182788110, 25182388101, 'Arti',' Arti','BOI', 'BOIN0251823' ,'Sm','Sister'),
(25182388101,71532468921,'Jack Johns','Jack','HDFC','HDFC0023456','Df','Brother'),
(25182588103,88452136796,'Somya Dutt','Som','SBI','SBIN0056780','Df','Friend'),
(25182588108, 25182788110, 'Harper', Null,'BOI', 'BOIN0251827','Sm','Friend'),
(25182688109,71532413694,'Piyush',Null,'HDFC','HDFC0023456','Df',Null)
go


INSERT INTO TransTable (AccNo,FromAccNo,ToAccNo,BeneficiaryID,Amount,TransType,Remarks)
VALUES 
(25182388101,NULL,25182388102,500001,200,'Debit','Shopping')
go
INSERT INTO TransTable (AccNo,FromAccNo,ToAccNo,BeneficiaryID,Amount,TransType,Remarks)
VALUES 
(25182488107,NULL,25182588103,500004,500,'Debit','Shopping')
go
INSERT INTO TransTable (AccNo,FromAccNo,ToAccNo,BeneficiaryID,Amount,TransType,Remarks)
VALUES
(25182788105,NULL,31526248897,500003,1000,'Debit',Null)
go



--Procedure for AccDetails mail
alter procedure MailingDet 
as 
begin
select na.[Email], am.[AccNo], ln.[UserId], ln.[Pwd]
from [dbo].[NewAcc] na
left join [dbo].[AccountMaster] am on am.CustId= na.CustId
left join [dbo].[Login] ln on ln.CustId= na.CustId
where na.CustId= (select max(CustId) from NewAcc)
end 
Go



--Procedure for verifyign if the userid entered in CustDetail is valid
create procedure ValidIdVerify @UserId numeric(18, 0)
as
begin
select CustId
from [dbo].[Login]
where @UserId= UserId
end
Go


-- Procedure for admin to have a list of customers using branchname
Create procedure ViewCustomer_List(@BranchName varchar(50))
as
begin
select am.[AccNo] as Account_Number, am.[AccHolder] as Account_Holder_Name
from [dbo].[AccountMaster] am join [dbo].[BranchTable] bt
on am.[IFSCCode] = bt.[IFSCCode]
where bt.[BranchName] = @BranchName
end
Go


-- Procedure for admin to access customer information using user id
create procedure ViewUserDetails @UserId numeric(18, 0)
as
begin
select na.[Title]+'. '+ na.[Fname]+' '+ na.[Lname] as Name, 
       na.[DOB], na.[Gender], na.[Phone], na.[Email],	   
	   na.[PAN], na.[Aadhar], na.[Occupation], na.[Income], 
	   na.[ResAdd] +', '+ na.[BranchName] +', '+ na.[City] +', '+ na.[State] +'- '+  cast(na.[Pincode] as varchar(50)) as Residential_Address, 
	   na.[PerAdd] +', '+ na.[PerAdd1] +', '+ na.[City01] +', '+ na.[state01] +'- '+  cast(na.[pincode01] as varchar(50)) as Permanent_Address
from [dbo].[NewAcc] na join [dbo].[Login] l 
on na.[CustId] = l.[CustId]
where l.[UserId] = @UserId
end
Go


--Procedure to update password
alter procedure UpdatePwd(@UserId numeric, @newpwd nvarchar(max)) as
begin
 update Login
 set Pwd = HASHBYTES('SHA2_512', @newpwd)
 where UserId = @UserId
end
go


--Procedure to retrieve email using UserId
create procedure GetEmail(@UserId numeric) 
as
begin
select Email from NewAcc na
join Login lg 
on lg.CustId = na.CustId
where UserId = @UserId
end
go


-- Procedure for Login(redirecting Admin to admin page and user to users page)
alter procedure [dbo].[LoginCheck](@UserId numeric, @Pwd nvarchar (max)) as
begin
    if(@UserId = 999999 and @Pwd = 'BOI_99')
	begin
	 select 1 -- ADMIN
	End
	else if Exists (SELECT User Exist FROM Login WHERE UserID = @UserID AND Pwd = HASHBYTES('SHA2_512', @Pwd) or Pwd = @Pwd)
    begin
     select 2 -- USER EXIST
    end
    else 
    begin
     select 0 -- INCORRECT USERID OR PWD
    end
End
Go


-- Procedure to get branch details
create procedure [dbo].[GetBranchDetails]
@UserId numeric(18,0)
as
begin
select b.BranchName,b.City,a.IFSCCode from BranchTable b join AccountMaster a 
on b.IFSCCode=a.IFSCCode join Login l on a.CustId=l.CustId
where l.UserId=@UserId
end
GO


-- Procedure to have Account summary
create procedure GetAcSummary(@UserId numeric(18,0))
as
begin
select AccNo,AccType,AccBalance from AccountMaster am join Login l on am.CustId=l.CustId
where l.UserId=@UserId
end
go


-- Procedure to have Account statement
create procedure GetAcStatement(@UserId numeric(18,0) ,@TransDate1 date,@TransDate2 date)
as
begin
select t.AccNo,a.AccHolder,t.TransType,t.TransDate,t.Amount from TransTable t join AccountMaster a 
on t.AccNo=a.AccNo join Login l on a.CustId=l.CustId
where l.UserId=@UserId and t.TransDate between @TransDate1 and @TransDate2
end
go


--Procedure to have email for transaction
create procedure GetEmail2(@AccNo numeric) 
as
begin
select Email from NewAcc na
join TransTable tt 
join AccountMaster am
on
tt.AccNo = am.AccNo 
on
am.CustId = na.CustId
where tt.AccNo = @AccNo
end
go


--Procedure to have transaction details
alter procedure [dbo].[TransDetails]
as
begin
select TransactionID,ToAccNo,Amount,TransDate,TransTime,am.AccBalance from TransTable t join AccountMaster am
on t.AccNO=am.AccNo where TransDate=(select max(TransDate) from TransTable) and TransTime=(select max(TransTime) from TransTable)
end
GO










