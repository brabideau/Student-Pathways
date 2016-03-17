using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetCourseCredits
    {
        public int CourseID { get; set; }
        public int ProgramID { get; set; }
        public double Credits { get; set; }
    }
}
