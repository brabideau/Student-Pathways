using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class ProgramSummary
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string EntranceSummary { get; set; }
        public int? CompetitiveEntrance { get; set; }
        public double? TotalCredits { get; set; }
        public bool WorkOutdoors { get; set; }
        public bool ShiftWork { get; set; }
        public bool WorkTravel { get; set; }
        public string ProgramLink { get; set; }


    }
}
