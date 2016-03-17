using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalBallSystem.DAL.Entities.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CrystalBallSystem.BLL.Security
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
