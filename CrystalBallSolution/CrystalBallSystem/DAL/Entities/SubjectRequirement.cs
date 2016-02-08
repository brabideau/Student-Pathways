using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{

    [Table("SubjectRequirement")]
    public partial class SubjectRequirement
    {
        public SubjectRequirement()
        {
            EntranceRequirements = new HashSet<EntranceRequirement>();
        }

        public int SubjectRequirementID { get; set; }
<<<<<<< HEAD

=======
>>>>>>> origin/master
        [Required]
        [StringLength(30)]
        public string SubjectDescription { get; set; }

        public virtual ICollection<EntranceRequirement> EntranceRequirements { get; set; }
<<<<<<< HEAD
=======

>>>>>>> origin/master
    }
}
