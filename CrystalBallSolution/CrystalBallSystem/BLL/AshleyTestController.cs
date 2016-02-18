using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.Entities;
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

        #region Tally Preference Question Matches
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<int> QuestionTally(List<int>questions, List<bool>answers)
        {
            using (var context = new CrystalBallContext())
            {
                List<bool> programAnswers = new List<bool>();
                //PROGRAMID START AND END POINT
                var startPoint = (from program in context.Programs
                                  orderby program.ProgramID
                                  select program.ProgramID).FirstOrDefault();

                var endPoint = (from program in context.ProgramPreferences
                               where program.ProgramID == (from max in context.ProgramPreferences select max.ProgramID).Max()
                               select program.ProgramID).FirstOrDefault();

                var distinctIDs = (from program in context.ProgramPreferences
                                   orderby program.ProgramID
                                   select program.ProgramID).Distinct().ToList();

                //QUESTIONID START AND END POINT
                var questionStartPoint = (from question in context.PreferenceQuestions
                                          orderby question.QuestionID
                                          select question.QuestionID).FirstOrDefault();

                var questionEndPoint = (from question in context.PreferenceQuestions
                                        where question.QuestionID == (from max in context.PreferenceQuestions select max.QuestionID).Max()
                                        select question.QuestionID).FirstOrDefault();

                var questionDistinctIDs = (from question in context.ProgramPreferences.OrderBy(question => question.QuestionID)
                                           select question.QuestionID).Distinct().ToList();

                int programID = startPoint;
                int questionID = questionStartPoint;
                List<int> questionMatches = new List<int>();

                //run through all programs and tally question matches
                while (programID <= endPoint)   
                {
                    int count = 0;
                    int matches = 0;
                    while (questionID <= questionEndPoint)
                    {
                        var results = (from preference in context.ProgramPreferences
                                      where preference.ProgramID == programID && preference.QuestionID == questionID
                                      select preference.Answer).FirstOrDefault();

                        if (results == answers[count])
                        {
                            matches++;
                        }
                        questionID++;
                        while (questionID <= questionEndPoint && questionID != questionDistinctIDs.ElementAt(count + 1))
                        {
                            questionID++;
                        }
                        count++;
                    }
                    questionMatches.Add(matches);
                    programID++;
                    count = 0;
                    while (programID <= endPoint && programID != distinctIDs.ElementAt(count + 1))
                    {
                        programID++;
                    }
                }
                return questionMatches;
            }
        }
        #endregion
    }
}
