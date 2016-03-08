<Query Kind="Statements">
  <Connection>
    <ID>8ed7549f-3fca-4078-8e01-2fe63f08d8ab</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>NAIT_PATHWAYS</Database>
  </Connection>
</Query>

List<int> myValues = new List<int>() { 1, 2, 3 , 40 , 5 , 6, 7 ,8 , 99 ,10, 200, 300, 400, 401, 402,403};

var aa = from x in ProgramCourses where myValues.Contains(x.CourseID) select x;
aa.Dump();

var c = from x in aa select new
{
	x.Program.ProgramID,
	x.Program.ProgramName,
	x.NaitCourses.CourseID,
	x.NaitCourses.CourseCode,
	x.NaitCourses.CourseName,
	x.NaitCourses.CourseCredits
};
	c.Dump();


//FirstOrDefault   x.Program && x.NaitCourses;

