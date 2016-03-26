using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace

using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.POCOs;
using System.Data;
using System.Data.SqlClient;

#endregion


namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class StudentController
    {
        #region add nait course

        #endregion

        //Method that will get all of the highschool courses and their relevant details
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName,
                                  HighSchoolCourseGroup = course.CourseGroup,
                                  HighSchoolHighestCourse = course.Highest
                              };
                return results.ToList();
            }
        }
        
        
        
        //Method returns the list of course ids in a given category
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public int[] GetParentCategory(int courseCode)
        {
            List<int> returnArray = new List<int>();
            using (var context = new CrystalBallContext())
            {
                string results = (from program in context.HighSchoolCourses
                              where program.HighSchoolCourseID == courseCode
                              select program.CourseGroup).FirstOrDefault();

                var returnArrayTemp = from x in context.HighSchoolCourses
                                  where x.CourseGroup == results
                                  select x.HighSchoolCourseID;
                foreach(int item in returnArrayTemp)
                {
                    returnArray.Add(item);
                }
                return returnArray.ToArray();
            }
        }
       



        #region preference questions
       


        #endregion


        #region briand playspace


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<int> FindProgramMatches(List<int> myCourses)
        {
            using (var context = new CrystalBallContext())
            {

                var results = from p in context.Programs
                              where p.Active == true && p.EntranceRequirements.All(e => e.SubjectRequirement.EntranceRequirements.Any(er => myCourses.Contains(er.HighSchoolCourseID)))
                              select p.ProgramID;


                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<ProgramResult> EntranceReq_Pref_Match(List<StudentPreference> myPrefs, List<int> programids, List<int> naitcourseids)
        {
            //takes the list of programs with matching entrance requirements, and returns results with preference matches
            using (var context = new CrystalBallContext())
            {

                int qCount = (from x in context.PreferenceQuestions
                              where x.Active
                              select x).Count();

                var initialresults = (from p in context.Programs.AsEnumerable()
                             where programids.Contains(p.ProgramID)
                             select new ProgramResult
                             {
                                 ProgramID = p.ProgramID,
                                 ProgramName = p.ProgramName,
                                 ProgramDescription = p.ProgramDescription,
                                 ProgramLink = p.ProgramLink,
                                 CredType = (from d in context.CredentialTypes
                                                 where p.CredentialTypeID == d.CredentialTypeID
                                                 select d.CredentialTypeName).FirstOrDefault(),
                                 Credits = (from x in
                                                (from ce in context.CourseEquivalencies.AsEnumerable()
                                                 from c in p.ProgramCourses
                                                 where naitcourseids.Contains(c.CourseID) || naitcourseids.Contains(ce.TransferCourseID) && c.CourseID == ce.ProgramCourseID
                                                 select c.NaitCourse).Distinct()
                                            select (double?)x.CourseCredits).Sum(),

                                MatchPercent = (int)(100 - ((from q in p.ProgramPreferences
                                             from mp in myPrefs
                                             where q.QuestionID == mp.QuestionID
                                             select Math.Pow(1 + Math.Abs(q.Answer - mp.Answer), 2) - 1)).Sum() / qCount / .24)

                             }).ToList();

                return initialresults;

                //var results = from x in initialresults
                //              where x.MatchPercent > 50
                //              select x;
			
                //return results.ToList();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<NAITCourse> Prefill_Courses(int programID, int semester)
        {
            using (var context = new CrystalBallContext())
            {

                var results = from c in context.ProgramCourses
                              where c.ProgramID == programID && c.Semester <= semester
                              select new NAITCourse {
                                  CourseID = c.NaitCourse.CourseID,
                                  CourseCode = c.NaitCourse.CourseCode,
                                  CourseName = c.NaitCourse.CourseName,
                                  CourseCredits = c.NaitCourse.CourseCredits
                              };


                return results.ToList();
            }
        }
        #endregion



        #region do we need these?
        public void AddCourse(NaitCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.NaitCourses.Add(item);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> GetCourseIDs(string[] courseCodes)
        {
            List<int> intList = new List<int>();
            using (var context = new CrystalBallContext())
            {
                for (int i = 0; i < courseCodes.Length; i++)
                {
                    var results = (from course in context.NaitCourses
                                   where course.CourseCode == courseCodes[i]
                                   select course.CourseID).First();
                    intList.Add(results);
                }
                return intList;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public int GetEntranceList(int courseID, int mark)
        {
            using (var context = new CrystalBallContext())
            {
                var results = (from entrance in context.EntranceRequirements
                               orderby entrance.EntranceRequirementID
                               where entrance.HighSchoolCourseID == courseID && mark >= entrance.RequiredMark
                               select entrance.EntranceRequirementID).FirstOrDefault();
                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Program> GetPrograms()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from p in context.Programs
                              select p;
                return (results.OrderBy(x => x.ProgramName)).ToList();

            }

        }

        //Method will return a list of program ids and credits for each program returned based on the user course selection
        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<GetCourseCredits> GetCourseCredits(List<int> courseid, List<int> programid)
        //{
        //    using (var context = new CrystalBallContext())
        //    {
        //          var result = from p in context.Programs
        //                     where programid.Contains(p.ProgramID)
        //                     select new GetCourseCredits
        //                     {
        //                         ProgramID = p.ProgramID,
        //                         ProgramName = p.ProgramName,
        //                         ProgramDescription = p.ProgramDescription,
        //                         ProgramLink = p.ProgramLink,
        //                         CredType = (from d in context.CredentialTypes
        //                                         where p.CredentialTypeID == d.CredentialTypeID
        //                                         select d.CredentialTypeName).FirstOrDefault(),
        //                         Credits = (from x in
        //                                        (from ce in context.CourseEquivalencies
        //                                         from c in p.ProgramCourses
        //                                         where courseid.Contains(c.CourseID) || courseid.Contains(ce.TransferCourseID) && c.CourseID == ce.ProgramCourseID
        //                                         select c.NaitCourse).Distinct()
        //                                    select (double?)x.CourseCredits).Sum()

        //                     };


        //        return result.ToList();
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PreferenceQuestion> GetQuestions()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from row in context.PreferenceQuestions
                              orderby row.QuestionID
                              select row;
                return results.ToList();
            }
        }

        #endregion
    }
}

