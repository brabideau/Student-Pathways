using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class ProgramResult
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string ProgramDescription { get; set; }
        public string ProgramLink { get; set; }
        public string CredType { get; set; }
        public double? Credits { get; set; }

        public int MatchPercent { get; set; }
        
    }
}
