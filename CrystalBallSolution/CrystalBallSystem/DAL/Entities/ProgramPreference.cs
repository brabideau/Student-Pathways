using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("ProgramPreference")]
    public class ProgramPreference
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProgramID { get; set; }

        [Required]
        public int Answer { get; set; }

        public virtual PreferenceQuestion PreferenceQuestion { get; set; }

    }

}
