using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.POCOs
{
    public class GetProgramPreferenceQuestions
    {
        public int ProgramID { get; set; }
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
    }
}
