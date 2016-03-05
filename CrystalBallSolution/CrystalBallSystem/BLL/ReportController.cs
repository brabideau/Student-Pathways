using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    class ReportController
    {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReportingData> Get_Reporting_Data()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from r in context.ReportingData
                             select r;
                return result.ToList();
            }
        }
    }
}