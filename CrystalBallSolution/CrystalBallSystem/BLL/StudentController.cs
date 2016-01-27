using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Addtional namespace

using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.POCOs;

#endregion


namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class StudentController
    {
        #region account setup
        public void Registration(Student item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.Students.Add(item);
                context.SaveChanges();
            }
        }

        public void ChangePassword(Student item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation...
                var attached = context.Students.Attach(item);
                var existing = context.Entry<Student>(attached);
                existing.State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public string AccountRecovery(string email)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                string result = (from account in context.Students
                                 where (account.Email == email)
                                 select account.Password).FirstOrDefault();
                return result;
            }
        }
        #endregion

        #region course
        public void AddCourse(NaitCourse item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.NaitCourses.Add(item);
                context.SaveChanges();
            }
        }
        #endregion

        #region course
        public void AddCourse(HighSchoolCourse item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.HighSchoolCourses.Add(item);
                context.SaveChanges();
            }
        }
        #endregion
        //select method that will populate the drop down list allowing a user to select courses.
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses>GetCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }
    }
}

