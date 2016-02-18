use Nait_Pathways
go




create type CourseIDs as table  --to use as TVP
(	HSCID int );

go




create procedure FindProgramsB @CourseList CourseIDs readonly
as
begin
	--variable declaration
	declare @programID int, @subjectID int, @entranceID int, @p_bool bit, @e_bool bit;

	--output table?
	declare @myPrograms table (ProgramID int);

	set @p_bool = 1;  --check if I need these later
	set @e_bool = 1;

	insert into @myPrograms (ProgramID) (
		select P.ProgramID from Program P where not exists ( --select the programs (?)
			select 'x' from SubjectRequirement SR where exists (
				select 'x' from EntranceRequirement ER where ER.SubjectRequirementID = SR.SubjectRequirementID and ER.ProgramID = P.ProgramID) and 
			


			not exists (
				select 'y' from EntranceRequirement ER where ER.SubjectRequirementID = SR.SubjectRequirementID and ER.HighSchoolCourseID in (
					select HSCID from @CourseList))));

	select ProgramID from @myPrograms

end
return
go

