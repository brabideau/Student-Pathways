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

    }
}
