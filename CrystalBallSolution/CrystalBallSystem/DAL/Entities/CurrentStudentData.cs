using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("CurrentStudentData")]
    public partial class CurrentStudentData
    {
        [Key]
        public int ReportingID { get; set; }
        [Required]
        public int ProgramID { get; set; }
        [Required]
        public int Semester { get; set; }
        [Required]
        public bool ChangeProgram { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public bool StudentAnswer { get; set; }

        [Required]
        public int SearchMonth { get; set; }

        [Required]
        public int SearchYear { get; set; }


        public virtual Program Program { get; set; }
        public virtual PreferenceQuestion PreferenceQuestion { get; set; }
    }
}
