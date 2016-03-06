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
    class ReportController
    {

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
    }
}