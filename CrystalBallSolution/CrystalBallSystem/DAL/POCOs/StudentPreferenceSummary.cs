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
        public int DefinitelyNot { get; set; }

        public int No { get; set; }

        public int DontKnow {get; set;}
        public int Yes { get; set; }
        public int Definitely { get; set; }
    }
}
