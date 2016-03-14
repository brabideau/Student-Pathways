<Query Kind="Statements">
  <Connection>
    <ID>8ed7549f-3fca-4078-8e01-2fe63f08d8ab</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>NAIT_PATHWAYS</Database>
  </Connection>
</Query>

List<int> myValues = new List<int>() { 1, 2, 3 , 40 , 5 , 6, 7 ,8 , 99 ,10, 200, 300, 400, 401, 402,403,525,546,547,548,9,};

var aa = from x in ProgramCourses where myValues.Contains(x.CourseID) select x;
aa.Dump();

//var bb = from x in aa select x.NaitCourses;
//bb.Dump();

//var ee = from x in bb select x.ProgramCourses;
//ee.Dump();.GroupBy(a=> a.programid)

var c = (from x in aa 
 
select new
{
	programid =  x.Program.ProgramID,
	programname = x.Program.ProgramName,
	Match = from y in aa
	where y.Program.ProgramID == x.Program.ProgramID
	select new 
	{
		y.NaitCourses.CourseID,
		y.NaitCourses.CourseCredits
	}
	
}).GroupBy(a=> a.programid);
	c.Dump();


//double CreditTatol = (from y in aa  select y.NaitCourses.CourseCredits).Sum();
var CreditTatol = (from x in aa  select new {
										programid =  x.Program.ProgramID,
										total = (from y in aa
										where y.Program.ProgramID == x.Program.ProgramID
										select 
										y.NaitCourses.CourseCredits).Sum()}).GroupBy(a=> a.programid);
CreditTatol.Dump();
//FirstOrDefault   x.Program && x.NaitCourses;

CreditTatol = (from y in result where y.Program.ProgramID==x.Program.ProgramID
                                                        select y.NaitCourse.CourseCredits).Sum(),