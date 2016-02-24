
create trigger TR_IU_ReportingData 
on ReportingData 
instead of insert
as

declare @progID int = null, @questionID int, @semester int = null, @change bit = null, @answer bit, @reportingID int = null;

set @progID = (select ProgramID from inserted);
set @questionID = (select QuestionID from inserted);
set @semester = (select Semester from inserted);
set @change = (select ChangeProgram from inserted);
set @answer = (select StudentAnswer from inserted);

set @reportingID = (select ReportingID from ReportingData where exists(select ReportingData.ProgramID intersect select @progID) and 
												exists(select ReportingData.QuestionID intersect select @questionID) and 
												exists(select ReportingData.Semester intersect select @semester) and 
												exists(select ReportingData.ChangeProgram intersect select @change) and 
												exists(select ReportingData.StudentAnswer intersect select @answer));

if @reportingID is null
	begin
		insert into ReportingData (ProgramID, Semester, ChangeProgram, QuestionID, StudentAnswer, Quantity) values (@progID, @semester, @change, @questionID, @answer, 1);


	end

else
	begin
		update ReportingData
		set Quantity = Quantity + 1
		where ReportingID = @reportingID;
	end

return
go
