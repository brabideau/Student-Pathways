using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using CrystalBallSystem.DAL.POCOs;
using System.Collections;
using CrystalBallSystem.DAL.Entities;
#endregion 

namespace CrystalBallSystem.DAL.DTOs
{
    public class CategoryProgram
    {
        public string Category { get; set; }

        public ICollection<Program> Programs { get; set; }
        //public IEnumerable<ProgramSummary> Programs { get; set; }
    }
}
