using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.DAL.Entities
{
<<<<<<< HEAD:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs
    
    [Table("ProgramPreference")]
    public partial class ProgramPreference
=======
    [Table("ProgramPreference")]
    public class ProgramPreference
>>>>>>> origin/master:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuestionID { get; set; }
<<<<<<< HEAD:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs

=======
>>>>>>> origin/master:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProgramID { get; set; }

<<<<<<< HEAD:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs
        public int Quantity { get; set; }

        public virtual PreferenceQuestion PreferenceQuestion { get; set; }

        public virtual Program Program { get; set; }
=======
        [Required]
        public int Answer { get; set; }

        public virtual PreferenceQuestion PreferenceQuestion { get; set; }

>>>>>>> origin/master:CrystalBallSolution/CrystalBallSystem/DAL/Entities/ProgramPreference.cs
    }

}
