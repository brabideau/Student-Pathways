using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class StudentPreference
    {
        public int QuestionID { get; set; }
        public int Answer { get; set; }

        public StudentPreference(Int32 q, int ans)
        {
            QuestionID = q;
            Answer = ans;
        }
    }
}
