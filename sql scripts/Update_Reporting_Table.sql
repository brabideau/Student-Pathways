


create procedure U_Reporting_Data @progID int = null, @questionID int, @semester int = null, @change bit = null, @answer bit
as
begin

declare @reportingID int;

set @reportingID = null;


set @reportingID = (select ReportingID from ReportingData where exists(select ProgramID intersect select @progID) and 
																exists(select QuestionID intersect select @questionID) and 
																exists(select Semester intersect select @semester) and 
																exists(select ChangeProgram intersect select @change) and 
																exists(select StudentAnswer intersect select @answer));

if @reportingID is null
	begin
		insert into ReportingData (ProgramID, QuestionID, Semester, ChangeProgram, Quantity, StudentAnswer)
		values (@progID, @questionID, @semester, @change, 1, @answer);
	end

else
	begin
		update ReportingData
			set Quantity = Quantity + 1
			where ReportingID = @reportingID;

	end

end
return
go