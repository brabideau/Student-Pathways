using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace

using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;

#endregion

namespace CrystalBallSystem.BLL
{
    public class AdminController
    {
        #region program

        public void AddProgram(Program item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.Programs.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteProgram(Program item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var existing = context.Programs.Find(item.ProgramID);
                context.Programs.Remove(existing);
                context.SaveChanges();
            }
        }

        #endregion

        #region requirement
        
        #endregion
        #region report
        
        #endregion
    }
}
