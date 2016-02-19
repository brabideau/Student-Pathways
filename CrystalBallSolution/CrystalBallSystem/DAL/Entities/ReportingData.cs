using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("ReportingData")]
    public class ReportingData
    {
        [Key]
        public int ReportingID { get; set; }

        public int? ProgramID { get; set; }

        public int? Semester { get; set; }

        public bool? ChangeProgram { get; set; }

        //[Required]
        public int? QuestionID { get; set; }

        //[Required]
        public bool? StudentAnswer { get; set; }

        //[Required]
        //public int quantity { get; set; }


        public virtual Program Program { get; set; }
        public virtual PreferenceQuestion PreferenceQuestion { get; set; }

    }
}
