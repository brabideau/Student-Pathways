using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("CourseEquivalency")]
    public partial class CourseEquivalency
    {
       
        [Key]
        public int CourseEquivalencyID { get; set; }

        [Required]
        public int ProgramID { get; set; }

        [Required]
        public int ProgramCourseID { get; set; }

        [Required]
        public int TransferCourseID { get; set; }

        public virtual Program Program { get; set; }

        public virtual NaitCours NaitCourse { get; set; }

        //public virtual NaitCours DestinationCourse { get; set; }
        
    }
}
