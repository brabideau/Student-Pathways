using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    public class ReportController
    {
        #region Metrics
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void InsertNewStudentMetrics(List<StudentPreference> prefs)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                NewStudentData data = null;
                foreach (var item in prefs)
                {
                    data = context.NewStudentDatas.Add(new NewStudentData()
                        {
                            QuestionID = item.QuestionID,
                            StudentAnswer = item.Answer,
                            SearchMonth = DateTime.Now.Month,
                            SearchYear = DateTime.Now.Year
                        });
                }
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void InsertCurrentStudentMetrics(List<StudentPreference> prefs, int programID, int semester, bool changeProgram)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                CurrentStudentData data = null;
                foreach (var item in prefs)
                {
                    data = context.CurrentStudentDatas.Add(new CurrentStudentData()
                    {
                        ProgramID = programID,
                        Semester = semester,
                        ChangeProgram = changeProgram,
                        QuestionID = item.QuestionID,
                        StudentAnswer = item.Answer,
                        SearchMonth = DateTime.Now.Month,
                        SearchYear = DateTime.Now.Year
                    });
                }
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void InsertProgramResults(List<GetCourseCredits> programs)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                ProgramData data = null;
                foreach (var item in programs)
                {
                    data = context.ProgramDatas.Add(new ProgramData()
                        {
                            ProgramID = item.ProgramID,
                            SearchMonth = DateTime.Now.Month,
                            SearchYear = DateTime.Now.Year
                        });
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region student

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable Get_NewStudent_Data()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var objects = from x in context.NewStudentDatas
                              select x;

                DataTable results = new DataTable();
                results.Columns.Add("QuestionID", typeof(int));
                results.Columns.Add("StudentAnswer", typeof(int));
                results.Columns.Add("SearchMonth", typeof(int));
                results.Columns.Add("SearchYear", typeof(int));

                foreach (var r in objects)
                {
                    results.Rows.Add(r.QuestionID, r.StudentAnswer, r.SearchMonth, r.SearchYear);
                }

                return results;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable Get_CurrentStudent_Data()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                
                    var objects = from x in context.CurrentStudentDatas
                                  select x;

                    DataTable results = new DataTable();
                    results.Columns.Add("ProgramID", typeof(int));
                    results.Columns.Add("Semester", typeof(int));
                    results.Columns.Add("ChangeProgram", typeof(bool));
                    results.Columns.Add("QuestionID", typeof(int));
                    results.Columns.Add("StudentAnswer", typeof(int));
                    results.Columns.Add("SearchMonth", typeof(int));
                    results.Columns.Add("SearchYear", typeof(int));

                    foreach (var r in objects)
                    {
                        results.Rows.Add(r.ProgramID, r.Semester, r.ChangeProgram, r.QuestionID, r.StudentAnswer, r.SearchMonth, r.SearchYear);
                    }

                    return results;

            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentPreferenceSummary> Get_Summary_Data(DataTable myData)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
               
                var questions = from q in context.PreferenceQuestions 
                                select q;

                List<StudentPreferenceSummary> summaries = new List<StudentPreferenceSummary> { };
                string quest;
                int qid;
                int theCount;
                int theTotal;
                int defYes;
                int yes;
                int noPref;
                int no;
                int defNo;

                foreach (var x in questions)
                {

                    defNo= 0;
                    no = 0;
                    noPref = 0;
                    yes = 0;
                    defYes = 0;

                    quest = x.Description;
                    qid = x.QuestionID;

                    defNo = myData.Select("StudentAnswer = 1 AND QuestionID = " + qid).Count();
                    no = myData.Select("StudentAnswer = 2 AND QuestionID = " + qid).Count();
                    noPref = myData.Select("StudentAnswer = 3 AND QuestionID = " + qid).Count();
                    yes = myData.Select("StudentAnswer = 4 AND QuestionID = " + qid).Count();
                    defYes = myData.Select("StudentAnswer = 5 AND QuestionID = " + qid).Count();

                    theTotal = myData.Select("QuestionID =" + qid).Count();

                    if (theTotal != 0)
                    {
                        defNo = 100 * defNo / theTotal;
                        no = 100 * no / theTotal;
                        noPref = 100 * noPref / theTotal;
                        yes = 100 * yes / theTotal;
                        defYes = 100 * defYes / theTotal;
                    }
                    

                    summaries.Add(new StudentPreferenceSummary
                    {
                        Question = quest,
                        DefinitelyNot =defNo,
                        No = no,
                        DontKnow = noPref,
                        Yes = yes,
                        Definitely = defYes

                    });
                }

                //yes = 0;

                //if (myData.Columns.Contains("ChangePrograms")) {
                //    yes = 100 * myData.Select("ChangePrograms = true").Count() / myData.AsEnumerable().Count();
                //}

                //summaries.Add(new StudentPreferenceSummary
                //{
                //    Question = "Do you want to change programs?",
                //    PercentYes = yes,
                //});

                return summaries;

                
            }

        }
        
    #endregion

        #region program

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentsDroppingSummary> StudentsDropping_by_Program(int? year, int? month)
        {
            // Which programs have the most students switching?
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var alldata = from c in context.CurrentStudentDatas
                              select c;


                if (year != null)
                {
                    alldata = from x in alldata
                              where x.SearchYear == year
                              select x;
                }
                if (month != null)
                {
                    alldata = from x in alldata
                              where x.SearchMonth == month
                              select x;
                }

                var results = (from c in alldata
                              group c by c.Program into swaps
                              select new StudentsDroppingSummary
                              {
                                  Program = swaps.Key.ProgramName,
                                  PercentDropping = 100 * (from s in swaps
                                                           where s.ChangeProgram
                                                           select swaps).Count() / swaps.Count()
                              }).OrderByDescending(x => x.PercentDropping);

                return results.ToList();
            }
        }

        
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramFrequency> Get_Program_Frequency(int? year, int? month)
        {
            // How often do programs show up in search results?

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var alldata = from pd in context.ProgramDatas
                              select pd;

                if (year != null)
                {
                    alldata = from x in alldata
                              where x.SearchYear == year
                              select x;
                }
                if (month != null)
                {
                    alldata = from x in alldata
                              where x.SearchMonth == month
                              select x;
                }

                var results = (from pd in alldata
                                group pd by pd.Program into p
                                  select new ProgramFrequency
                                  {
                                      Program = p.Key.ProgramName,
                                      Frequency = p.Count()
                                  }).OrderByDescending(x => x.Frequency);


                return results.ToList();

            }
        }



        #endregion
    }
}