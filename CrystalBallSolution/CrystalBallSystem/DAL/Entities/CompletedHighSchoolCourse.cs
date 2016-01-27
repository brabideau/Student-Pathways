using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{

    [Table("CompletedHighSchoolCourses")]
    public partial class CompletedHighSchoolCourse
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HighSchoolCourseID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentID { get; set; }

        public double Mark { get; set; }

        public virtual HighSchoolCourse HighSchoolCourse { get; set; }

        public virtual Student Student { get; set; }
    }
}
