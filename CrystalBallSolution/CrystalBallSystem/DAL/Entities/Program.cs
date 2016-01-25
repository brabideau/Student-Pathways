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

        [Key]
        public int ProgramID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProgramName { get; set; }

        public double TotalCredits { get; set; }

        public int ProgramLength { get; set; }

        public bool Active { get; set; }

        public bool WorkOutdoors { get; set; }

        public bool ShiftWork { get; set; }

        public bool WorkTravel { get; set; }

        [StringLength(150)]
        public string ProgramLink { get; set; }

        public int ProgramTypeID { get; set; }

        public virtual ICollection<CompletedProgram> CompletedPrograms { get; set; }

        public virtual ICollection<SubjectRequirement> SubjectRequirements { get; set; }

        public virtual ProgramType ProgramType { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<NaitCours> NaitCourses { get; set; }

        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
