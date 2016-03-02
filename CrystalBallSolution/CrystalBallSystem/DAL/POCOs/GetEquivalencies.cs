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
}
