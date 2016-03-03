using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetEquivalencies
    {
        public int CourseEquivalencyID {get; set; }
        public int ProgramID { get; set; }
        public int CourseID { get; set; }
        public int DestinationCourseID { get; set; }
    }

    public class GetEquivalencyNames
    {
        public int CourseEquivalencyID { get; set; }
        public int ProgramID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string DestinationCourseCode { get; set; }
        public string DestinationCourseName { get; set; }
    }
}
