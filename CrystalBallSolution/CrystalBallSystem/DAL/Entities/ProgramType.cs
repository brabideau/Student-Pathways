using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    

    [Table("ProgramType")]
    public partial class ProgramType
    {
        [Key]
        public int ProgramTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string ProgramTypeDescription { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
    }
}
