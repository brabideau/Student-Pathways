using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetDegEntReqs
    {
        public int DegreeEntranceRequirementID { get; set; }
        public string CredentialName { get; set; }
        public string Category { get; set; }
        public decimal GPA { get; set; }
    }
}
