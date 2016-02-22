use Nait_Pathways
go




create type CourseIDs as table  --to use as TVP
(	HSCID int );

go


create procedure FindProgramsB @CourseList CourseIDs readonly
as
begin
	--declare the output table
		declare @myPrograms table (	ProgramID int,
									ProgramName varchar(50),
									Description varchar(500),
									URL varchar(200)
									)

		insert into @myPrograms (ProgramID, ProgramName, Description, URL) (
		select P.ProgramID, P.ProgramName, P.ProgramDescription, P.ProgramLink from Program P where not exists (
		--select the programs 
			select 'x' from SubjectRequirement SR where exists ( 
		--for which there is not a SubjectRequirement
				select 'x' from EntranceRequirement ER where ER.SubjectRequirementID = SR.SubjectRequirementID and ER.ProgramID = P.ProgramID) and 
		--where the SubjectRequirement DOES exist for that program
			not exists (
				select 'y' from EntranceRequirement ER where ER.SubjectRequirementID = SR.SubjectRequirementID and ER.HighSchoolCourseID in (
					select HSCID from @CourseList))));
		--but the student has no natching courses

		select ProgramID, ProgramName, Description, URL from @myPrograms;

end
return
go
