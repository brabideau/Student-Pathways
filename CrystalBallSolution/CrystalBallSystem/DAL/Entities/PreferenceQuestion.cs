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

        [Required(ErrorMessage = "A Question Description is required.")]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }

        public virtual ICollection<CurrentStudentData> CurrentStudentDatas { get; set; }
        public virtual ICollection<NewStudentData> NewStudentDatas { get; set; }
        
    }
}
