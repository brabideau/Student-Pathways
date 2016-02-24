<Query Kind="Statements">
  <Connection>
    <ID>b7e93e6e-ce0b-4c7c-93b8-c044b676e636</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>


								
var a = from x in Programs 
		where x.ProgramID == 2000
		select x.ProgramCourses ;
	a.Dump();
foreach( var item in a) 
{
	var c = from x in item
			select x.NaitCourses;
	var d = from x in c 
			where  x.CourseName.Contains("P")
			|| x.CourseCode.Contains("P")
			select x ;
	c.Dump();	
	d.Dump();
}
