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
        public List<NAITCourse> SearchNaitCourses(string SearchInfo)
        {
            using (var context = new CrystalBallContext())
            {
                var results = from NaitCourse in context.NaitCourses
                              where (NaitCourse.CourseName.Contains(SearchInfo))
                              || (NaitCourse.CourseCode.Contains(SearchInfo))
                              select new NAITCourse
                              {
                                  CourseID = NaitCourse.CourseID,
                                  CourseCode = NaitCourse.CourseCode,
                                  CourseName = NaitCourse.CourseName,
                                  CourseCredits = NaitCourse.CourseCredits,

                              };
                return results.ToList();
            }
        }
    }
}
