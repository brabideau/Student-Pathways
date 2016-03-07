using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    public class ReportController
    {

        #region general

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<NewStudentData> Get_NewStudent_Data()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from n in context.NewStudentDatas
                              select n;

                return results.ToList();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CurrentStudentData> Get_CurrentStudent_Data()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from c in context.CurrentStudentDatas
                              select c;

                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentPreferenceSummary> Get_Summary_Data(List<NewStudentData> myData)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
               
                var questions = from q in context.PreferenceQuestions 
                                select q;

                //var results = (from p in myMatches
                //               from s in myPrefs
                //               where s.ProgramID == p.ProgramID
                //               select p).Distinct();

                //return results.ToList();

                List<StudentPreferenceSummary> summaries = new List<StudentPreferenceSummary> { };
                string quest;
                int yes;
                int no;

                foreach (var x in questions)
                {
                    quest = x.Description;
                    yes = (from n in myData
                           where n.StudentAnswer && n.QuestionID == x.QuestionID
                           select n).Count();
                    no = (from n in myData
                           where !n.StudentAnswer && n.QuestionID == x.QuestionID
                           select n).Count();

                    summaries.Add(new StudentPreferenceSummary
                    {
                        Question = quest,
                        Yes = yes,
                        No = no
                    });
                }

                return summaries;

                //foreach (var i in a)
                //{
                //    var results = (from Ncourse in i
                //                  select new NAITCourse
                //                  {
                //                      CourseID = Ncourse.CourseID,
                //                      CourseCode = Ncourse.CourseCode,
                //                      CourseName = Ncourse.CourseName,
                //                      CourseCredits = Ncourse.CourseCredits,

                //                  });
                //    foreach (var result in results)
                //    {
                //        CourseLIst.Add(result);
                //    }

                //}
                //return CourseLIst;
                //var results = from x in questions
                //              from p in myData
                //                  select new StudentPreferenceSummary
                //                  {
                //                      Question = x.Description,
                //                      Yes = (from n in myData
                //                             where n.StudentAnswer && n.QuestionID == x.QuestionID
                //                             select n).Count(),
                //                      No = (from n in myData
                //                             where !n.StudentAnswer && n.QuestionID == x.QuestionID
                //                             select n).Count(),
                //                  };
                              

                //return results.ToList();
            }

        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentPreferenceSummary> Get_Summary_Data(List<CurrentStudentData> myData)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var questions = from q in context.PreferenceQuestions
                                select q;

                //var results = (from p in myMatches
                //               from s in myPrefs
                //               where s.ProgramID == p.ProgramID
                //               select p).Distinct();

                //return results.ToList();

                List<StudentPreferenceSummary> summaries = new List<StudentPreferenceSummary> { };
                string quest;
                int yes;
                int no;

                foreach (var x in questions)
                {
                    quest = x.Description;
                    yes = (from n in myData
                           where n.StudentAnswer && n.QuestionID == x.QuestionID
                           select n).Count();
                    no = (from n in myData
                          where !n.StudentAnswer && n.QuestionID == x.QuestionID
                          select n).Count();

                    summaries.Add(new StudentPreferenceSummary
                    {
                        Question = quest,
                        Yes = yes,
                        No = no
                    });
                }

                return summaries;
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentsDroppingSummary> StudentsDropping_by_Program()
        {
            // Which programs have the most students switching?

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var swapList = from c in context.CurrentStudentDatas
                               group c by c.Program into swaps
                               select new StudentsDroppingSummary
                               {
                                   Program = swaps.Key.ProgramName,
                                   PercentDropping = 100 * (from s in swaps
                                                            where s.ChangeProgram
                                                            select swaps).Count() / swaps.Count()
                               };

                var results = swapList.OrderByDescending(x => x.PercentDropping);

                return results.ToList();

            }
        }
    #endregion


        #region time filters
        public List<NewStudentData> Filter_by_Month(int month, List<NewStudentData> myData)
        {

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from n in myData
                              where n.SearchMonth == month
                              select n;

                return results.ToList();
            }
        }

        public List<CurrentStudentData> Filter_by_Month(int month, List<CurrentStudentData> myData)
        {

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from n in myData
                              where n.SearchMonth == month
                              select n;

                return results.ToList();
            }
        }

        public List<NewStudentData> Filter_by_Year(int year, List<NewStudentData> myData)
        {

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from n in myData
                              where n.SearchYear == year
                              select n;

                return results.ToList();
            }
        }

        public List<CurrentStudentData> Filter_by_Year(int year, List<CurrentStudentData> myData)
        {

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from n in myData
                              where n.SearchYear == year
                              select n;

                return results.ToList();
            }
        }
        #endregion

        #region current student filtering

        public List<CurrentStudentData> Filter_by_Program(int programID, List<CurrentStudentData> myData)
        {
            var results = from x in myData
                              where x.ProgramID == programID
                              select x;

            return results.ToList();

        }

        public List<CurrentStudentData> Filter_by_Semester(int semester, List<CurrentStudentData> myData)
        {
            var results = from x in myData
                          where x.Semester == semester
                          select x;

            return results.ToList();

        }

        public List<CurrentStudentData> Filter_by_ChangeProgram(bool change, List<CurrentStudentData> myData)
        {
            var results = from x in myData
                          where x.ChangeProgram == change
                          select x;

            return results.ToList();

        }


        #endregion

        /*
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentPreferenceSummary> Get_NewStudent_Data(int? month, int? year)
        {

            if (month == null || year == null)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from q in context.PreferenceQuestions
                                 from n in q.NewStudentDatas
                                 where n.SearchMonth == month && n.SearchYear == year
                                 group n by n.PreferenceQuestion into pref
                                 select new StudentPreferenceSummary
                                 {
                                     Question = pref.Key.Description,
                                     Yes = (from p in pref
                                            where p.StudentAnswer
                                            select pref).Count(),
                                     No = (from p in pref
                                           where !p.StudentAnswer
                                           select pref).Count()
                                 };


                return results.ToList();
            }
        }



        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentPreferenceSummary> Prefs_By_Program(int programID, int? month, int? year)
        {

            if (month == null || year == null)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from q in context.PreferenceQuestions
                              from c in q.CurrentStudentDatas
                              where c.SearchMonth == month && c.SearchYear == year && c.ProgramID == programID
                              group c by c.PreferenceQuestion into pref
                              select new StudentPreferenceSummary
                              {
                                  Question = pref.Key.Description,
                                  Yes = (from p in pref
                                         where p.StudentAnswer
                                         select pref).Count(),
                                  No = (from p in pref
                                        where !p.StudentAnswer
                                        select pref).Count()
                              };
                return results.ToList();

            }
        }




        */
    }
}