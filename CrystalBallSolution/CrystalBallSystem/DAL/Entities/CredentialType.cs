using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    
    [Table("CredentialType")]
    public partial class CredentialType
    {
        public CredentialType()
        {
            Programs = new HashSet<Program>();
        }

        public int CredentialTypeID { get; set; }

        [Required]
        [StringLength(20)]
        public string CredentialTypeName { get; set; }

        public virtual ICollection<Program> Programs { get; set; }
        public virtual ICollection<DegreeEntranceRequirement> DegreeEntranceRequirements { get; set; }
    }
}
