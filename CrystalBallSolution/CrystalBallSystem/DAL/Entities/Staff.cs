using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{

    [Table("Staff")]
    public partial class Staff
    {
        [Key]
        public int StaffID { get; set; }

        [Required]
        [StringLength(50)]
        public string StaffEmail { get; set; }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
    }
}
