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
        public int CourseID { get; set; }
        public string CourseCode { get; set; }
        public IEnumerable GetHSCourseCode { get; set; }
    }
}
