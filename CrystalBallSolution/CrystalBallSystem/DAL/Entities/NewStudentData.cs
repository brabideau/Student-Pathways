using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{

    [Table("NewStudentData")]
    public partial class NewStudentData
    {
        [Key]
        public int ReportingID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        public int StudentAnswer { get; set; }

        [Required]
        public int SearchMonth { get; set; }

        [Required]
        public int SearchYear { get; set; }

        public virtual PreferenceQuestion PreferenceQuestion { get; set; }
    }
}
