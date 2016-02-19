use Nait_Pathways
go


create type StudentPrefs as table  --to use as TVP
(	QuestionID int,
	Answer int );

go


create procedure PreferenceMatching @S_Prefs StudentPrefs readonly
as
begin
	--select a program
	select P.ProgramID from Program P where exists 
	--if the student has at least one matching answer
		(select 'x' from ProgramPreference PPref where PPref.ProgramID = P.ProgramID 
		and PPref.Quantity = (select Answer from @S_Prefs where QuestionID = PPref.QuestionID))
	--and no questions that both have answered
		and not exists (select 'x' from ProgramPreference PPref where PPref.ProgramID = P.ProgramID 
			and PPref.QuestionID in (select QuestionID from @S_Prefs) 
	--for which the answers do not match
			and PPref.Quantity != (select Answer from @S_Prefs where QuestionID = PPref.QuestionID));
		
end
return
go

