
alter trigger TR_IU_ReportingData 
on ReportingData 
instead of insert
as

declare @progID int = null, @questionID int, @semester int = null, @change bit = null, @answer bit, @reportingID int = null;

set @progID = (select ProgramID from inserted);
set @questionID = (select QuestionID from inserted);
set @semester = (select Semester from inserted);
set @change = (select ChangePrograms from inserted);
set @answer = (select Answer from inserted);

set @reportingID = (select ReportingDataID from ReportingData where exists(select ReportingData.ProgramID intersect select ProgramID from inserted) and 
												exists(select ReportingData.QuestionID intersect select QuestionID from inserted) and 
												exists(select ReportingData.Semester intersect select Semester from inserted) and 
												exists(select ReportingData.ChangePrograms intersect select ChangePrograms from inserted) and 
												exists(select ReportingData.Answer intersect select Answer from inserted));

if @reportingID is null
	begin
		insert into ReportingData (ProgramID, Semester, ChangePrograms, QuestionID, Answer, Quantity) values (@progID, @semester, @change, @questionID, @answer, 1);


	end

else
	begin
		update ReportingData
		set Quantity = Quantity + 1
		where ReportingDataID = @reportingID;
	end

return
go
