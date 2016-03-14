using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Collections;
#endregion



namespace CrystalBallSystem.DAL.DTOs
{
    public class ProgramAndCourses
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public double CreditTatol { get; set; }
        public double? ProgramCreditTotal { get; set; }
        public IEnumerable ProgramCourseMatch { get; set; }
    }
}
