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
        }

        [Key]
        public int ProgramID { get; set; }

        public int CredentialTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProgramName { get; set; }

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

        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }

        public virtual ICollection<DegreeEntranceRequirement> DegreeEntranceRequirements { get; set; }

        public virtual ICollection<CourseEquivalency> CourseEquivalencies { get; set; }
        
        public virtual ICollection<CurrentStudentData> CurrentStudentDatas { get; set;}

        public virtual ICollection<ProgramData> ProgramDatas { get; set; }
    }
}
