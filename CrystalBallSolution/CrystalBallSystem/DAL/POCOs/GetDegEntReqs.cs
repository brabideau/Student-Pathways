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
        public int CredentialTypeID { get; set; }
        public string CredentialTypeName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public decimal GPA { get; set; }
    }
}
