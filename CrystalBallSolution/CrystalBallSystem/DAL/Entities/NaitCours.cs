using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    

    [Table("NaitCourses")]
    public partial class NaitCours
    {
        

        [Key]
        public int CourseID { get; set; }

        [Required]
        [StringLength(10)]
        public string CourseCode { get; set; }
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }

        public double CourseCredits { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }

        public virtual ICollection<CourseEquivalency> CourseEquivalencies { get; set; }

    }
}
