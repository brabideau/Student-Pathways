using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.DTOs
{
    public class SubjectRequirementAndCourses
    {
        public int EntranceReqID { get; set; }
        public int SubjectReqID { get; set; }
        public string SubjectDesc { get; set; }
        public IEnumerable GetHSCourseIDName { get; set; }
    }
}
