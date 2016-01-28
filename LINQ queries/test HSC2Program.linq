<Query Kind="Statements">
  <Connection>
    <ID>ddf7f3b1-88f4-4d91-a442-3a277fd3c76e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
  </Connection>
</Query>

	
var a =  (from x in Programs select x.SubjectRequirements).FirstOrDefault();
a.Dump();

var b = (from x in a
select x.EntranceRequirements).FirstOrDefault();
b.Dump();

var c = from x in b select x.HighSchoolCourse;
c.Dump();

var d = from x in c select  x.CompletedHighSchoolCourses;

d.Dump();