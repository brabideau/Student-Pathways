using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class AshleyTestController
    {
        #region List High School Courses
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetEnglishCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              where course.HighSchoolCourseName.Contains("English")
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetMathCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              where course.HighSchoolCourseName.Contains("Math")
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetSocialCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              where course.HighSchoolCourseName.Contains("Social")
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetScienceCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              where course.HighSchoolCourseName.Contains("Science") || 
                                    course.HighSchoolCourseName.Contains("Physics") ||
                                    course.HighSchoolCourseName.Contains("Chemistry") ||
                                    course.HighSchoolCourseName.Contains("Biology")
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetOtherCourseList()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from course in context.HighSchoolCourses
                              where !course.HighSchoolCourseName.Contains("English") &&
                                    !course.HighSchoolCourseName.Contains("Social") &&
                                    !course.HighSchoolCourseName.Contains("Math") &&
                                    !course.HighSchoolCourseName.Contains("Science") &&
                                    !course.HighSchoolCourseName.Contains("Physics") &&
                                    !course.HighSchoolCourseName.Contains("Chemistry") &&
                                    !course.HighSchoolCourseName.Contains("Biology")
                              orderby course.HighSchoolCourseName
                              select new GetHSCourses
                              {
                                  HighSchoolCourseID = course.HighSchoolCourseID,
                                  HighSchoolCourseDescription = course.HighSchoolCourseName
                              };
                return results.ToList();
            }
        }
        #endregion

        #region List Preference Questions
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetPreferenceQuestions> GetQuestions()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from question in context.PreferenceQuestions
                              orderby question.QuestionID
                              select new GetPreferenceQuestions
                              {
                                  QuestionID = question.QuestionID,
                                  Question = question.Description
                              };
                return results.ToList();
            }
        }
        #endregion
    }
}
