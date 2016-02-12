use Nait_Pathways
go




create type CourseIDs as table  --to use as TVP
(	HSCID int );

go





create procedure FindPrograms @CourseList CourseIDs readonly
as
begin
	--variable declaration
	declare @programID int, @subjectID int, @entranceID int, @p_bool bit, @s_bool bit, @e_bool bit;

	--output table?
	declare @myPrograms table (ProgramID int);

	set @p_bool = 1;
	set @s_bool = 1;
	set @e_bool = 1;


	--cursor declaration
	declare programLoop cursor
		for select ProgramID from Program where Active = 1;

		--set up program cursor
	open programLoop
	fetch next from programLoop into @programID

	while @@FETCH_STATUS = 0 --the program loop
		begin
			set @p_bool = 1;

			--inner cursor declaration
			declare subjectLoop cursor
			for select SubjectRequirementID from SubjectRequirement;
			

			--set up subject cursor
			open subjectLoop
			fetch next from subjectLoop into @subjectID
			

			while @@FETCH_STATUS = 0 --the subject loop 
				begin
					set @e_bool = 1;
					if exists (select 'x' from EntranceRequirement ER where @programID = ER.ProgramID and @subjectID = ER.SubjectRequirementID) --does the requirement exist?
						begin
							if not exists (select 'x' from EntranceRequirement ER where ER.HighSchoolCourseID in (select HSCID from @CourseList) --does the student match the requirement?
									and @programID = ER.ProgramID and @subjectID = ER.SubjectRequirementID)

								begin --student doesn't match a requirement
									set @e_bool = 0;
							
								end
						end


						if @e_bool = 0
						begin
							set @p_bool = 0;
						end

					fetch next from subjectLoop into @subjectID
				end --end subject loop
					--if e_bool = 1, the entrance requirement is OK. go to next subject

			--close subject cursor
			close subjectLoop
			deallocate subjectLoop



			if @p_bool = 1
				begin
					--add valid programs to a list
					insert into @myPrograms (ProgramID) values (@programID)
				end

			fetch next from programLoop into @programID
		end --end program loop


	--close program cursor
	close programLoop
	deallocate programLoop

	select ProgramID from @myPrograms

end
return
go

