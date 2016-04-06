using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("CourseGroup")]
    public class CourseGroup
    {                   

        [Key]
        public int CourseGroupID { get; set; }
        public string CourseGroupDescription { get; set; }

        public virtual ICollection<HighSchoolCours> HighSchoolCourses { get; set; }
    }
}
