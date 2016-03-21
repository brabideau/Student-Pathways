using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class AddEntranceRequirements
    {
        public int programID { get; set; }
        public int subReqID { get; set; }
        public int hsID { get; set; }
        public int? mark { get; set; }
    }
}
