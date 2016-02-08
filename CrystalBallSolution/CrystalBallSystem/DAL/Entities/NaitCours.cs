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
        public NaitCours()
        {
            Programs = new HashSet<Program>();
        }

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
<<<<<<< HEAD:CrystalBallSolution/CrystalBallSystem/DAL/Entities/NaitCours.cs

=======
     
>>>>>>> origin/master:CrystalBallSolution/CrystalBallSystem/DAL/Entities/NaitCourse.cs
        public virtual ICollection<Program> Programs { get; set; }
    }
}
