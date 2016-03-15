using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("DegreeEntranceRequirement")]
        public partial class DegreeEntranceRequirement
    {
        
        [Key]
        public int DegreeEntranceReqID { get; set; }

        [Required]
        public int ProgramID { get; set; }

        [Required]
        public int CredentialTypeID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public decimal GPA { get; set; }


        public virtual Program Program { get; set; }

        public virtual CredentialType CredentialType { get; set; }

        public virtual Category Category { get; set; }
        
    }
}
