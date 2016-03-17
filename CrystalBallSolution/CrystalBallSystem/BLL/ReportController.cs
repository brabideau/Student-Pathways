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
                results.Columns.Add("StudentAnswer", typeof(bool));
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
                    results.Columns.Add("StudentAnswer", typeof(bool));
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
                int? yes;

                foreach (var x in questions)
                {
                    yes = null;
                    quest = x.Description;
                    qid = x.QuestionID;
                    theCount = myData.Select("StudentAnswer = true AND QuestionID = " + qid).Count();
                    theTotal = myData.Select("QuestionID =" + qid).Count();

                    if (theTotal != 0)
                    {
                        yes = 100 * theCount / theTotal;
                    }
                    

                    summaries.Add(new StudentPreferenceSummary
                    {
                        Question = quest,
                        PercentYes = yes,
                    });
                }

                yes = null;

                if (myData.Columns.Contains("ChangePrograms")) {
                    yes = 100 * myData.Select("ChangePrograms = true").Count() / myData.AsEnumerable().Count();
                }

                summaries.Add(new StudentPreferenceSummary
                {
                    Question = "Do you want to change programs?",
                    PercentYes = yes,
                });

                return summaries;

                
            }

        }


        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //public List<StudentPreferenceSummary> Get_Summary_Data(List<CurrentStudentData> myData)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {
        //        var questions = from q in context.PreferenceQuestions
        //                        select q;

        //        //var results = (from p in myMatches
        //        //               from s in myPrefs
        //        //               where s.ProgramID == p.ProgramID
        //        //               select p).Distinct();

        //        //return results.ToList();

        //        List<StudentPreferenceSummary> summaries = new List<StudentPreferenceSummary> { };
        //        string quest;
        //        int yes;
        //        int no;

        //        foreach (var x in questions)
        //        {
        //            quest = x.Description;
        //            yes = (from n in myData
        //                   where n.StudentAnswer && n.QuestionID == x.QuestionID
        //                   select n).Count();
        //            no = (from n in myData
        //                  where !n.StudentAnswer && n.QuestionID == x.QuestionID
        //                  select n).Count();

        //            summaries.Add(new StudentPreferenceSummary
        //            {
        //                Question = quest,
        //                Yes = yes,
        //                No = no
        //            });
        //        }

        //        return summaries;
        //    }

        //}

        
    #endregion

        #region program

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramData> Get_ProgramData()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from x in context.ProgramDatas
                              select x;

                return results.ToList();
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


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramFrequency> Get_Program_Frequency()
        {
            // Which programs have the most students switching?

            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from pd in context.ProgramDatas
                              group pd by pd.Program into p
                              select new ProgramFrequency
                              {
                                  Program = p.Key.ProgramName,
                                  Frequency = p.Count()
                              };

                return results.ToList();
            }
        }



        #endregion
    }
}