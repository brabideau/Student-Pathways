using CrystalBallSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class SubjectRequirementsDetail
    {
        public int SubjectRequirementID { get; set; }
        public string SubjectDescription { get; set; }

        public List<EntranceRequirementDetail> EntranceReqs { get; set; }
    }
}
