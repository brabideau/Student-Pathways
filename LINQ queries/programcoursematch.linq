<Query Kind="Statements">
  <Connection>
    <ID>bfa7ecad-18c9-462b-abd4-23b7f9cddaf7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
    <ShowServer>true</ShowServer>
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
		y.NaitCourses.CourseID
	}
	
}).GroupBy(a=> a.programid);
	c.Dump();




//FirstOrDefault   x.Program && x.NaitCourses;