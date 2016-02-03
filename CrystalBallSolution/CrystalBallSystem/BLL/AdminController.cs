using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
#endregion

namespace CrystalBallSystem.BLL
{
    public class AdminController
    {
        #region Admin management

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Category> Category_List()
        {
            using(CrystalBallContext context = new CrystalBallContext())
            {
                return context.Categories.OrderBy(x => x.CategoryID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<HighSchoolCourse> HighSchoolCourse_List()
        {
            using(CrystalBallContext context = new CrystalBallContext())
            {
                return context.HighSchoolCourses.OrderBy(x => x.HighSchoolCourseID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddCategory(Category item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                Category added = null;
                added = context.Categories.Add(item);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCategory(Category item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                context.Entry<Category>(context.Categories.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

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
