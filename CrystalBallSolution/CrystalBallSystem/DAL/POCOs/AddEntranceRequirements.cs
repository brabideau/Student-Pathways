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
        public int highSchoolID { get; set; }
        public int? reqMark { get; set; }

        public AddEntranceRequirements(Int32 hsID, Int32 srID, Int32 pID, Int32 mark)
        {
            highSchoolID = hsID;
            subReqID = srID;
            programID = pID;
            reqMark = mark;
        }
    }

   
}
