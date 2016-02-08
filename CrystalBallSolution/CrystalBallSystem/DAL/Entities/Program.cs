using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    
    [Table("Program")]
    public partial class Program
    {
        public Program()
        {
            EntranceRequirements = new HashSet<EntranceRequirement>();
            ProgramPreferences = new HashSet<ProgramPreference>();
            Categories = new HashSet<Category>();
            NaitCourses = new HashSet<NaitCours>();
        }

        [Key]
        public int ProgramID { get; set; }

        public int CredentialTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProgramName { get; set; }

<<<<<<< HEAD
        [StringLength(500)]
        public string ProgramDescription { get; set; }

        public double? TotalCredits { get; set; }

        [StringLength(30)]
        public string ProgramLength { get; set; }

        public int? CompetitiveAdvantage { get; set; }

        public bool Active { get; set; }

        [StringLength(200)]
        public string ProgramLink { get; set; }

        public virtual CredentialType CredentialType { get; set; }

        public virtual ICollection<EntranceRequirement> EntranceRequirements { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<NaitCours> NaitCourses { get; set; }
=======
        [Required]
        [StringLength(30)]
        public string ProgramType { get; set; }

        [Required]
        public double TotalCredits { get; set; }

        [Required]
        public int ProgramLength { get; set; }

        [Required]
        public bool Active { get; set; }

       [StringLength(150)]
        public string ProgramLink { get; set; }




        public virtual ICollection<SubjectRequirement> SubjectRequirements { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<NaitCourse> NaitCourses { get; set; }

>>>>>>> origin/master
    }
}
