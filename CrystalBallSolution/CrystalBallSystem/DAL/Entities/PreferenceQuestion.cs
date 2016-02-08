using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
   
    [Table("PreferenceQuestion")]
    public partial class PreferenceQuestion
    {
     
        public PreferenceQuestion()
        {
            ProgramPreferences = new HashSet<ProgramPreference>();
        }

        [Key]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }
    }
}
