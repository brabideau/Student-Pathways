using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    
    [Table("EntranceRequirement")]
    public partial class EntranceRequirement
    {
        [Key]
        public int EntranceRequirementID { get; set; }
<<<<<<< HEAD

        public int SubjectRequirementID { get; set; }

        public int HighSchoolCourseID { get; set; }

        public int ProgramID { get; set; }

=======
        
        [Required]
        public int HighSchoolCourseID { get; set; }

        [Required]
        public int SubjectRequirementID { get; set; }

        [Required]
        public int ProgramID { get; set; }
        
>>>>>>> origin/master
        public int? RequiredMark { get; set; }

        public virtual Program Program { get; set; }

        public virtual HighSchoolCours HighSchoolCours { get; set; }

        public virtual SubjectRequirement SubjectRequirement { get; set; }

        public virtual Program Program { get; set; }
    }
}
