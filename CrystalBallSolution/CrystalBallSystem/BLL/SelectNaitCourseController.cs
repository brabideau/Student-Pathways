
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


#endregion


namespace CrystalBallSystem.BLL
{
    public class SelectNaitCourseController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramNameID> Program_List()
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
        public List<NAITCourse> SearchNaitCourses(string SearchInfo,int programID)
        {
            using (var context = new CrystalBallContext())
            {
                var result1 = (from x in context.Programs
                              where x.ProgramID == programID
                              select x.NaitCourses).FirstOrDefault();
                //where(y.CourseName.Contains(SearchInfo))
                //              || (y.CourseCode.Contains(SearchInfo))
                var results = from y in result1
                              where(y.CourseName.Contains(SearchInfo))
                              || (y.CourseCode.Contains(SearchInfo))
                              select new NAITCourse
                              {
                                  CourseID = y.CourseID,
                                  CourseCode = y.CourseCode,
                                  CourseName = y.CourseName,
                                  CourseCredits = y.CourseCredits,

                              };
                return results.ToList();
            }
        }
    }
}
