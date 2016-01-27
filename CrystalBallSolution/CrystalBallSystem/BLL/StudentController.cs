﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace

using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.POCOs;

#endregion


namespace CrystalBallSystem.BLL
{
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

        #region add nait course
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

        #region add high school course
        public void AddHighSchoolCourse(CompletedHighSchoolCourse item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.CompletedHighSchoolCourses.Add(item);
                context.SaveChanges();
            }
        }
        #endregion

        #region list high school courses
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<StudentHighSchoolCourses> CompletedHighSchoolCourses(int studentID)
        {
            using (var context = new CrystalBallContext())
            {
                var results = from row in context.CompletedHighSchoolCourses
                              orderby row.HighSchoolCourseID
                              where row.StudentID == studentID
                              select new StudentHighSchoolCourses
                              {
                                  HighSchoolCourse = row.HighSchoolCourse.HighSchoolCourseName,
                                  Mark = row.Mark
                              };

                return results.ToList();
            }
        }
        #endregion
    }
}

