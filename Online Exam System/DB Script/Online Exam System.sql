create database OES;

CREATE TABLE Student(
[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
UserName varchar(50),
Email varchar(50),
User_Password varchar(50),
City varchar(50),
State varchar(50),
Postal_Code int,
DOB date,
Mobile_no int,
Qualification varchar(50),
Year_of_Completion date,
);

CREATE TABLE QuestionCategory
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED ,
	[Category] [varchar](50) NULL,
)
GO

CREATE TABLE Exhibit
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[ExhibitData] [nvarchar](max) NULL,
)
GO

CREATE TABLE Question
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[QuestionCategoryId] [int] NOT NULL, constraint fk_Question_Category foreign key (QuestionCategoryId) references QuestionCategory(Id),
	[QuestionType] [varchar](50) NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[ExhibitId] [int] NULL, constraint fk_Question_Exhibit foreign key (ExhibitId) references Exhibit(Id),
	[Points] [int] NOT NULL,
	[IsActive] [bit] NOT NULL Default 1,
)
GO


CREATE TABLE Choice
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED ,
	[QuestionId] [int] NOT NULL, constraint fk_ChoiceXQuestion foreign key (QuestionId) references Question(Id),
	[Label] [nvarchar](max) NOT NULL,
	[Points] [decimal](13, 2) NOT NULL default 0.0,
	[IsActive] [bit] NOT NULL default 1,
)
GO



/***** PREPARING THE TEST ENGINE TRANSACTION TABLES ***************/

CREATE TABLE Test
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [varchar](250) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL Default 1,
	[DurationInMinute] [int] NOT NULL Default 60,
)
GO

CREATE TABLE Registration
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[StudentId] [int] NOT NULL, constraint fk_Registration_Student foreign key (StudentId) references Student(Id),
	[TestId] [int] NOT NULL, constraint fk_Registration_Test foreign key (TestId) references Test(Id),
	[RegistrationDate] [datetime] NOT NULL default getutcdate(),
	[Token] [uniqueidentifier] NOT NULL default newid(),
	[TokenExpireTime] [datetime] NOT NULL DEFAULT (dateadd(minute,(60),getutcdate())),
)
GO


CREATE TABLE TestXQuestion
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[TestId] [int] NOT NULL, constraint fk_TestXQuestionXTest foreign key (TestId) references Test(Id),
	[QuestionId] [int] NOT NULL, constraint fk_TestXQuestionXQuestion foreign key (QuestionId) references Question(Id),
	[QuestionNumber] [int] NOT NULL Default 1,
	[IsActive] [bit] NOT NULL Default 1,
)
GO



CREATE TABLE TestXPaper
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[RegistrationId] [int] NOT NULL, constraint fk_TestXPaper_Registration foreign key (RegistrationId) references Registration(Id),
	[TestXQuestionId] [int] NOT NULL, constraint fk_TextXParent_TestQuestion foreign key (TestXQuestionId) references TestXQuestion(Id),
	[ChoiceId] [int] NOT NULL, constraint fk_TextXPaper_Choice foreign key (ChoiceId) references Choice(Id),
	[Answer] [nvarchar](max) NULL,
	[MarkScored] [decimal](13, 2) NULL,
)
GO


CREATE TABLE QuestionXDuration
(
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[RegistrationId] [int] NOT NULL, constraint fk_QuestionDuration_Registration foreign key (RegistrationId) references Registration(Id),
	[TestXQuestionId] [int] NOT NULL, constraint fk_QuestionDuration_TestQuestion foreign key (TestXQuestionId) references TestXQuestion(Id),
	[RequestTime] [datetime] NOT NULL default getutcdate(),
	[LeaveTime] [datetime] NULL,
	[AnsweredTime] [datetime] NULL, 
)
GO
alter table Student
alter column Mobile_no varchar(10);

select * from QuestionCategory
insert into QuestionCategory values ('Development Fundamentals')

select * from Question
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which SQL function is used to count the number of rows in a SQL query?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which SQL keyword is used to retrieve a maximum value?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which of the following SQL clauses is used to DELETE tuples from a database table?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', ' _____removes all rows from a table without logging the individual row deletions.', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'If you don’t specify ASC or DESC after a SQL ORDER BY clause, the following is used by default ____', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which of the following is a legal expression in SQL?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which of the following are TCL commands?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'The COUNT function in SQL returns the number of', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'A SQL query will not work if there are no indexes on the relations - Is it true?', null, 100)
insert into Question(QuestionCategoryId, QuestionType, Question, ExhibitId, Points) values 
(1, 'RADIO', 'Which of the following must be enclosed in double-quotes?', null, 100)



select * from Test
insert into Test (Name, Description, IsActive, DurationInMinute) values ('SQL', 'This test is for SQL', 1, 30)


select * from Choice
insert into Choice(QuestionId, Label, Points,IsActive) values
(1, 'COUNT()', 0, 1),
(1, 'Number()', 0, 1),
(1, 'SUM()', 0, 1),
(1, 'COUNT(*)', 100, 1)

insert into Choice(QuestionId, Label, Points,IsActive) values
(2, 'MOST', 0, 1),
(2, 'TOP', 0, 1),
(2, 'MAX', 100, 1),
(2, 'UPPER', 0, 1)

insert into Choice(QuestionId, Label, Points,IsActive) values
(3, 'DELETE', 0, 1),
(3, 'REMOVE ', 100, 1),
(3, 'DROP', 0, 1),
(3, 'CLEAR', 0, 1)

insert into Choice(QuestionId, Label, Points,IsActive) values
(4, 'DELETE', 0, 1),
(4, 'REMOVE ', 100, 1),
(4, 'DROP', 0, 1),
(4, 'TRUNCATE', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(5, 'ASC', 100, 1),
(5, 'DSC', 0, 1),
(5, 'There is no default value', 0, 1),
(5, 'None of the mentioned', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(6, 'SELECT NULL FROM SALES;', 0, 1),
(6, 'SELECT NAME FROM SALES;', 100, 1),
(6, 'SELECT * FROM SALES WHEN PRICE = NULL;', 0, 1),
(6, 'SELECT # FROM SALES;', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(7, 'UPDATE and TRUNCATE', 0, 1),
(7, 'SELECT and INSERT', 100, 1),
(7, 'GRANT and REVOKE', 0, 1),
(7, 'ROLLBACK and SAVEPOINT', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(8, 'Values', 100, 1),
(8, 'Distict Values', 0, 1),
(8, 'Group By', 0, 1),
(8, 'Columns', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(9, 'No', 0, 1),
(9, 'Yes', 100, 1),
(9, 'Maybe', 0, 1),
(9, 'None of the mentioned', 0, 1)
insert into Choice(QuestionId, Label, Points,IsActive) values
(10, 'Dates', 0, 1),
(10, 'Column Alias', 100, 1),
(10, 'Strings', 0, 1),
(10, 'Allof the above', 0, 1)


select * from TestXQuestion
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 1, 1) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 2, 2) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 3, 3) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 4, 4) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 5, 5) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 6, 6) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 7, 7) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 8, 8) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 9, 9) 
insert into TestXQuestion (TestId, QuestionId, QuestionNumber) values
(1, 10, 10) 

select * from TestXPaper;

