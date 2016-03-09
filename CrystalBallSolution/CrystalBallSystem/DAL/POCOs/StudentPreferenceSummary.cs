using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
   [Serializable]
    public class StudentPreferenceSummary
    {
        public string Question { get; set; }
        public int? PercentYes { get; set; }
    }
}
