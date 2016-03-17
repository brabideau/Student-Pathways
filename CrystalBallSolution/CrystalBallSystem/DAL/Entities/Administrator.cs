using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace CrystalBallSystem.DAL.Entities
{
    public partial class Administrator
    {
        [Key]
        public int AdministratorID { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "The Email Address is required.")]
        public string Email { get; set; }
    }
}
