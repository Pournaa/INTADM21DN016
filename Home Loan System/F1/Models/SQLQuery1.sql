create procedure tracker4(@ID int)
as
DECLARE @Dob DATE
DECLARE @age INT
begin
SET @Dob = (select Dob from MVCregUser where @ID = ID)
Set @age = year(GetDate())-year(@Dob)
SELECT @age as [Age],[Gross_income],[Loan_amount],[Tensure] from occupation_table
where ID = @ID
end
go