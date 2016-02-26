<Query Kind="Program">
  <Connection>
    <ID>43b9b31b-e809-4ddb-9f85-6c293277163b</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	List<StudentPreference> myPrefs = new List<StudentPreference> {};
	
	
	StudentPreference myPref = new StudentPreference(15, 0);
	myPrefs.Add(myPref);
	myPref = new StudentPreference(19, 1);
	myPrefs.Add(myPref);
	myPref = new StudentPreference(18, 0);
	myPrefs.Add(myPref);
	
	var results = from p in Programs
	from q in p.ProgramPreferences
	where p.Active == true
	select new PPref{
		Question = q.QuestionID,
		Answer = q.Quantity
	};
	
	results.Dump();

}



 public class StudentPreference
 {
        public int Question { get; set; }
        public int Answer { get; set; }
		
		public StudentPreference(int q, int ans){
			Question = q;
			Answer =  ans;
		}
 };
 
 
 
  public class PPref
 {
        public int Question { get; set; }
        public int Answer { get; set; }
		
		public PPref(int q, int ans){
			Question = q;
			Answer =  ans;
		}
 };
 

/*
var results = from p in Programs
			where p.EntranceRequirements.All(e => e.SubjectRequirement.EntranceRequirements.Any(er => CourseList.Contains(er.HighSchoolCourseID)))
			select p;

*/

/*
	select P.ProgramID from Program P where exists 

		(select 'x' from ProgramPreference PPref where PPref.ProgramID = P.ProgramID 
		and PPref.Quantity = (select Answer from @S_Prefs where QuestionID = PPref.QuestionID))

		and not exists (select 'x' from ProgramPreference PPref where PPref.ProgramID = P.ProgramID 
			and PPref.QuestionID in (select QuestionID from @S_Prefs) 

			and PPref.Quantity != (select Answer from @S_Prefs where QuestionID = PPref.QuestionID));
*/