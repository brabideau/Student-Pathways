using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetEntranceReq
    {
        public int HSCourseID { get; set; }
        public string HSCourseName { get; set; }
        public int? Mark { get; set; }
    }
}
