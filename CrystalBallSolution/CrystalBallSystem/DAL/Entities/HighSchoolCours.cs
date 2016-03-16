using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
   
    [Table("HighSchoolCourses")]
    public partial class HighSchoolCours
    {
     
        public HighSchoolCours()
        {
            EntranceRequirements = new HashSet<EntranceRequirement>();
        }

        [Key]
        public int HighSchoolCourseID { get; set; }

        [Required(ErrorMessage = "A High School Course Name is required (5-30 characters)")]
        [StringLength(30)]
        public string HighSchoolCourseName { get; set; }
        [Required(ErrorMessage = "A Course Group is required")]
        public string CourseGroup { get; set; }
        [Required(ErrorMessage = "A Highest is required")]
        public bool Highest { get; set; }

        public virtual ICollection<EntranceRequirement> EntranceRequirements { get; set; }
    }
}
