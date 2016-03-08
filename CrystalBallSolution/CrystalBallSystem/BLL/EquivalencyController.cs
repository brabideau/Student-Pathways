using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    public class EquivalencyController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetEquivalencyNames> GetEquivalencies(int programID)
        {
            using (var context = new CrystalBallContext())
            {
                var results = from ce in context.CourseEquivalencies
                              where ce.ProgramID == programID
                              select new GetEquivalencyNames
                                {
                                    CourseEquivalencyID = ce.CourseEquivalencyID,
                                    CourseCode = ce.NaitCourse.CourseCode,
                                    CourseName = ce.NaitCourse.CourseName,
                                    DestinationCourseCode = (from nc in context.NaitCourses
                                                             where nc.CourseID == ce.DestinationCourseID
                                                             select nc.CourseCode).FirstOrDefault(),
                                    DestinationCourseName = (from nc in context.NaitCourses
                                                             where nc.CourseID == ce.DestinationCourseID
                                                             select nc.CourseName).FirstOrDefault(),
                                };
                return results.ToList();
            }
        }
    }
}
