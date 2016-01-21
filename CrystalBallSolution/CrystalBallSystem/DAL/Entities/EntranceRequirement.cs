using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    
    [Table("EntranceRequirement")]
    public partial class EntranceRequirement
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProgramID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HighSchoolCourseID { get; set; }

        public int? RequiredMark { get; set; }

        public virtual HighSchoolCours HighSchoolCours { get; set; }

        public virtual Program Program { get; set; }
    }
}
