using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("ProgramData")]
    public partial class ProgramData
    {
        [Key]
        public int ProgramDataID { get; set; }

        [Required]
        public int ProgramID { get; set; }

        [Required]
        public int SearchMonth { get; set; }

        [Required]
        public int SearchYear { get; set; }

        public virtual Program Program { get; set; }
    }
}
