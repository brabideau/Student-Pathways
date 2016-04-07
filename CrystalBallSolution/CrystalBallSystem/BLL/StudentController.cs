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
using CrystalBallSystem.DAL.DTOs;
using System.Data.SqlClient;

#endregion


namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class StudentController
    {
        #region select nait course
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NAITCourse> SearchNaitCourses(string SearchInfo, int programID, bool active)
        {
            using (var context = new CrystalBallContext())
            {
                //if active == true show active only
                //if active == false show all
                if (programID == 0)
                {

                    var result1 = from Ncourse in context.NaitCourses
                                  where (
                                            (SearchInfo == null) ?
                                      //condition 1
                                                  ((active == true) ?
                                      //sub-condition1
                                                          ((Ncourse.CourseName.Contains("") || (Ncourse.CourseCode.Contains(""))) && Ncourse.Active == active) :
                                      //sub-condition2
                                                          (Ncourse.CourseName.Contains("") || (Ncourse.CourseCode.Contains(""))))
                                      //condition 2 
                                  :
                                                  ((active == true) ?
                                      //sub-condtion3
                                                          (((Ncourse.CourseName.Contains(SearchInfo))
                                                          || (Ncourse.CourseCode.Contains(SearchInfo))) && Ncourse.Active == active) :
                                      //sub-condition4
                                                          (((Ncourse.CourseName.Contains(SearchInfo))
                                                          || (Ncourse.CourseCode.Contains(SearchInfo)))))
                                          )
                                  select new NAITCourse
                                  {
                                      CourseID = Ncourse.CourseID,
                                      CourseCode = Ncourse.CourseCode,
                                      CourseName = Ncourse.CourseName,
                                      CourseCredits = Ncourse.CourseCredits,

                                  };
                    return result1.ToList();


                }
                else
                {
                    var result2 = from pc in context.ProgramCourses
                                  where ((SearchInfo == null) ?
                                      //condition 1
                                  ((active == true) ?
                                      //sub-condition 1
                                  (pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains("")
                                      || (pc.NaitCourse.CourseCode.Contains("")))) && pc.NaitCourse.Active == active
                                      :
                                      //sub-condition 2 
                                   (pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains("")
                                      || (pc.NaitCourse.CourseCode.Contains(""))))
                                      )
                                      //condition 2
                                      :
                                      (
                                      (active == true) ?
                                      //sub-condtion 3
                                      (pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains(SearchInfo)
                                      || ((pc.NaitCourse.CourseCode.Contains(SearchInfo)) && pc.NaitCourse.Active == active)))
                                      :
                                      //sub-condtion 4
                                        (pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains(SearchInfo)
                                      || (pc.NaitCourse.CourseCode.Contains(SearchInfo))))
                                      ))
                                  select new NAITCourse
                                  {
                                      CourseID = pc.CourseID,
                                      CourseCode = pc.NaitCourse.CourseCode,
                                      CourseName = pc.NaitCourse.CourseName,
                                      CourseCredits = pc.NaitCourse.CourseCredits
                                  };
                    return result2.ToList();
                }
            }

        }
        #endregion


        #region do we need these?
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramNameID> GetProgram()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from x in context.Programs
                              orderby x.ProgramName
                              select new ProgramNameID
                              {
                                  ProgramID = x.ProgramID,
                                  ProgramName = x.ProgramName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NAITCourse> SelectedNaitCourses(int courseID)
        {
            using (var context = new CrystalBallContext())
            {
                var result = from x in context.NaitCourses
                             where x.CourseID == courseID
                             select new NAITCourse
                             {
                                 CourseID = x.CourseID,
                                 CourseCode = x.CourseCode,
                                 CourseName = x.CourseName,
                                 CourseCredits = x.CourseCredits
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NaitCours> NaitCourse_List(int programid)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                if (programid == 0)
                {
                    var step1 = from x in context.NaitCourses select x;
                    return step1.ToList();
                }
                else if (programid == -1)
                {
                    var step1 = from x in context.NaitCourses
                                where
                                    (x.ProgramCourses).Count() == 0
                                select x;
                    return step1.ToList();
                }
                else
                {
                    var step1 = from x in context.ProgramCourses
                                where x.ProgramID == programid
                                select x.NaitCourse;
                    return step1.ToList();
                }



            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Program> Program_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.Programs.ToList();
            }
        }

        //public void AddCourse(NaitCours item)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {
        //        NaitCours added = null;
        //        added = context.NaitCourses.Add(item);
        //        context.SaveChanges();
        //    }
        //}
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
                                  CourseGroupID = course.CourseGroupID,
                                  CourseLevel = course.CourseLevel
                              };
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> FindHSCourses(List<int> courseids)
        {
            using (var context = new CrystalBallContext())
            {
                var initResults = from h in context.HighSchoolCourses
                                  where courseids.Contains(h.HighSchoolCourseID)
                                  select h;

                var result = (from x in initResults
                              from h in context.HighSchoolCourses
                              where h.CourseGroupID == x.CourseGroupID && x.CourseLevel >= h.CourseLevel
                              select h.HighSchoolCourseID).Distinct();

                return result.ToList();

                
            }
				
        }

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<GetHSCourses> FindHSCourses(List<int> courseIDs)
        //{
        //    List<GetHSCourses> hsCourses = new List<GetHSCourses>();
        //    using (var context = new CrystalBallContext())
        //    {
        //        foreach (var item in courseIDs)
        //        {
        //            var results = (from course in context.HighSchoolCourses
        //                           where course.HighSchoolCourseID == item
        //                           orderby course.HighSchoolCourseName
        //                           select new GetHSCourses
        //                           {
        //                               HighSchoolCourseID = course.HighSchoolCourseID,
        //                               HighSchoolCourseDescription = course.HighSchoolCourseName,
        //                               CourseGroupID = course.CourseGroupID,
        //                               CourseLevel = course.CourseLevel
        //                           }).First();
        //            hsCourses.Add(results);
        //        }
        //        return hsCourses;
        //    }
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<int> GetHighestCourseLevel(List<GetHSCourses> courses)
        //{
        //    List<int> items = new List<int>();
        //    using (var context = new CrystalBallContext())
        //    {
        //        var results = (from x in context.HighSchoolCourses.AsEnumerable()
        //                       from y in courses
        //                       where x.CourseGroupID == y.CourseGroupID && x.CourseLevel == y.CourseLevel
        //                       select x.HighSchoolCourseID).Distinct();
        //        return results.ToList();
        //    }
        //}

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

                programids = (from p in context.Programs.AsEnumerable()
                              from mp in myPrefs
                              where programids.Contains(p.ProgramID) && 
                              !p.ProgramPreferences.Any(y => y.QuestionID == mp.QuestionID && Math.Abs(y.Answer - mp.Answer) ==4)
                              select p.ProgramID).ToList();


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

                                          MatchPercent = (int)(10 * ((from q in p.ProgramPreferences
                                                                       from mp in myPrefs
                                                                       where q.QuestionID == mp.QuestionID
                                                                       select 10 - Math.Pow(Math.Abs(q.Answer - mp.Answer), 2)).Sum()) / qCount) 

                                      }).ToList();

                var finalProgramResults = (from x in initialresults.AsEnumerable()
                                           where x.MatchPercent > 60
                                           orderby x.MatchPercent descending
                                           select x).ToList();
                foreach (var item in finalProgramResults)
                {
                    ProgramData data = context.ProgramDatas.Add(new ProgramData()
                    {
                        ProgramID = item.ProgramID,
                        SearchMonth = DateTime.Now.Month,
                        SearchYear = DateTime.Now.Year
                    });
                    context.SaveChanges();
                }

                return finalProgramResults;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<NAITCourse> Prefill_Courses(int programID, int semester)
        {
            using (var context = new CrystalBallContext())
            {

                var results = from c in context.ProgramCourses
                              where c.ProgramID == programID && c.Semester <= semester
                              select new NAITCourse
                              {
                                  CourseID = c.NaitCourse.CourseID,
                                  CourseCode = c.NaitCourse.CourseCode,
                                  CourseName = c.NaitCourse.CourseName,
                                  CourseCredits = c.NaitCourse.CourseCredits
                              };


                return results.ToList();
            }
        }

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

        public void AddCourse(NaitCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.NaitCourses.Add(item);
                context.SaveChanges();
            }
        }
        #endregion

    }
}

