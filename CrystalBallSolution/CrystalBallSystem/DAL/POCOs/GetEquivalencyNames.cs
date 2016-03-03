using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetEquivalencyNames
    {
        public int CourseEquivalencyID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string DestinationCourseCode { get; set; }
        public string DestinationCourseName { get; set; }
    }
}
