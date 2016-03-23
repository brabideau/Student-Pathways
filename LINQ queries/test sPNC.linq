<Query Kind="Expression">
  <Connection>
    <ID>b7e93e6e-ce0b-4c7c-93b8-c044b676e636</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Nait_Pathways</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

 var result = (from x in context.ProgramCourses
                                 where courseids.Contains(x.CourseID)
                                 select new ProgramAndCourses
                                 {
                                      ProgramID = x.Program.ProgramID,
                                      ProgramName = x.Program.ProgramName,
                                      ProgramCreditTotal = x.Program.TotalCredits == null ? 0 : x.Program.TotalCredits,
                                      CreditTatol = (from y in context.ProgramCourses
                                                     where y.Program.ProgramID == x.Program.ProgramID
                                                     select y.NaitCourse.CourseCredits).Sum(),
                                      ProgramCourseMatch = from y in context.ProgramCourses
                                                           where y.Program.ProgramID == x.Program.ProgramID
                                                           select new ProgramCourseMatch
                                                           {
                                                               CourseID = y.NaitCourse.CourseID,
                                                               CourseCode = y.NaitCourse.CourseCode,
                                                               CourseName = y.NaitCourse.CourseName,
                                                               CourseCredits = y.NaitCourse.CourseCredits
                                                           }


                                  }).GroupBy(a => a.ProgramID);