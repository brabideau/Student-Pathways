using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    

    [Table("Student")]
    public partial class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    
        public virtual ICollection<CompletedHighSchoolCourse> CompletedHighSchoolCourses { get; set; }
       
        public virtual ICollection<CompletedProgram> CompletedPrograms { get; set; }

        public virtual ICollection<NaitCourse> NaitCourses { get; set; }
    }
}
