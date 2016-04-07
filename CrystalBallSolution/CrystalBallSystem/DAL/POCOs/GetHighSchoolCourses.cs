using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetHighSchoolCourses
    {
        public int HighSchoolCourseID { get; set; }
        public string HighSchoolCourseName { get; set; }
        public string CourseGroup { get; set; }
        public int CourseLevel { get; set; }
    }
}
