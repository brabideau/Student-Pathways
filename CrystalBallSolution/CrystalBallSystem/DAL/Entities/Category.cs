using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    

    [Table("Category")]
    public partial class Category
    {
       
        public Category()
        {
            Programs = new HashSet<Program>();
        }

        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Program> Programs { get; set; }

        public virtual ICollection<DegreeEntranceRequirement> DegreeEntranceRequirements { get; set; }
    }
}
