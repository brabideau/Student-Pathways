using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("PreferenceQuestion")]
    public class PreferenceQuestion
    {
        [Key]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }
        
    }
}
