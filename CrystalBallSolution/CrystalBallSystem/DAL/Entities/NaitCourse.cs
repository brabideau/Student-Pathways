using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    

    [Table("NaitCourses")]
    public partial class NaitCourse
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }

        [Required]
        [StringLength(30)]
        public string CourseName { get; set; }

        public double CourseCredits { get; set; }

        public bool Active { get; set; }
     
        public virtual ICollection<Program> Programs { get; set; }
    }
}
