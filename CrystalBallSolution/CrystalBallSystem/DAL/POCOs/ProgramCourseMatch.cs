using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class ProgramCourseMatch
    {
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public double CourseCredits { get; set; }
    }
}
