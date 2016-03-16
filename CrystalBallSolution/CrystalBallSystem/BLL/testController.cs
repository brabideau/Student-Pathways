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
    [DataObject]
    public class testController
    {
        //List Equivalent Courses
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<NAITCourse> Equivalent_Courses(int courseid)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from x in context.CourseEquivalencies
                             from nc in context.NaitCourses
                             where x.CourseID == courseid && nc.CourseID == x.DestinationCourseID
                             select new NAITCourse
                                    {
                                        CourseID = nc.CourseID,
                                        CourseCode = nc.CourseCode,
                                        CourseName = nc.CourseName,
                                        CourseCredits = nc.CourseCredits
                                    };
                return result.ToList();
            }
        }
    }
}
