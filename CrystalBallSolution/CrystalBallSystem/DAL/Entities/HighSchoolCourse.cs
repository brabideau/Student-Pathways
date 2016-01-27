using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    
    [Table("HighSchoolCourses")]
    public partial class HighSchoolCourse
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HighSchoolCourseID { get; set; }

        [Required]
        [StringLength(30)]
        public string HighSchoolCourseName { get; set; }

        public virtual ICollection<CompletedHighSchoolCourse> CompletedHighSchoolCourses { get; set; }

        public virtual ICollection<EntranceRequirement> EntranceRequirements { get; set; }
    }
}
