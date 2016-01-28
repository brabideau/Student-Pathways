<Query Kind="Statements">
  <Connection>
    <ID>ddf7f3b1-88f4-4d91-a442-3a277fd3c76e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
  </Connection>
</Query>

var a = HighSchoolCourses;
a.Dump();
//high school courses ->completedhighschoolscourse
var b = (from x in a where x.HighSchoolCourseID == 3 select x.CompletedHighSchoolCourses).FirstOrDefault();
b.Dump();
//high school courses-> entrance requiremnt
var c = (from x in a where x.HighSchoolCourseID == 3 select x.EntranceRequirements).FirstOrDefault();
c.Dump();

var d = from x in c  select x.SubjectRequirement;
d.Dump();

var e = from x in d select x.Program;
e.Dump();