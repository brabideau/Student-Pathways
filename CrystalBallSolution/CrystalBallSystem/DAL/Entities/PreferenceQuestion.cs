<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
   
    [Table("PreferenceQuestion")]
    public partial class PreferenceQuestion
    {
     
        public PreferenceQuestion()
        {
            ProgramPreferences = new HashSet<ProgramPreference>();
        }

=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
    [Table("PreferenceQuestion")]
    public class PreferenceQuestion
    {
>>>>>>> origin/master
        [Key]
        public int QuestionID { get; set; }

        [Required]
<<<<<<< HEAD
        [StringLength(30)]
        public string Description { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }
=======
        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<ProgramPreference> ProgramPreferences { get; set; }
        
>>>>>>> origin/master
    }
}
