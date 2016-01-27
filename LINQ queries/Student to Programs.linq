<Query Kind="Statements">
  <Connection>
    <ID>c086e8f7-7155-4ab3-a058-85b5a7f4f768</ID>
    <Persist>true</Persist>
    <Server>WB304-08</Server>
    <Database>Nait_Pathways</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

// given a student ID (in this case 49), this returns a list of all programs for which the student meets at least one of that program's entrance requirements


var ereqs = from course in CompletedHighSchoolCourses
			join er in EntranceRequirements on course.HighSchoolCourseID equals er.HighSchoolCourseID
			where course.StudentID == 49 && course.Mark >= er.RequiredMark
			select er;


//ereqs.Dump();


var subreqs = from row in ereqs
			join sreq in SubjectRequirements on row.SubjectRequirementID equals sreq.SubjectRequirementID
			select row;

//subreqs.Dump();


var prog = from row in Programs
			join sr in subreqs on row.ProgramID equals sr.SubjectRequirement.ProgramID
			select row;
			
			prog.Dump();
