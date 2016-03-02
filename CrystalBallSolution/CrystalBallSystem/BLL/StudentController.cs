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
using System.Data;
using System.Data.SqlClient;

#endregion


namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class StudentController
    {
        #region add nait course
        public void AddCourse(NaitCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                // TODO: Validation rules...
                var added = context.NaitCourses.Add(item);
                context.SaveChanges();
            }
        }
        #endregion

        //select method that will populate the drop down list allowing a user to select courses.
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetHSCourses> GetCourseList()
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
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public int GetEntranceList(int courseID, int mark)
        {
            using (var context = new CrystalBallContext())
            {
                var results = (from entrance in context.EntranceRequirements
                               orderby entrance.EntranceRequirementID
                               where entrance.HighSchoolCourseID == courseID && mark >= entrance.RequiredMark
                               select entrance.EntranceRequirementID).FirstOrDefault();
                return results;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public int[] GetPrograms(int[] entranceID)
        {
            List<int> returnArray = new List<int>();
            using (var context = new CrystalBallContext())
            {
                //create a list inside a list that can be cast as an array down the line and will contain
                //the program id and relevant return variables including program name etc etc
                for (int i = 0; i < entranceID.Length; i++)
                {
                    var results = (from program in context.EntranceRequirements
                                  where program.EntranceRequirementID == entranceID[i] && program.Program.Active == true
                                  select program.ProgramID).First();
                    returnArray.Add(results);
                }
            }
            int[] newArray = returnArray.ToArray();
            return newArray;
        }

        #region preference questions
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PreferenceQuestion> GetQuestions()
        {
            using (var context = new CrystalBallContext())
            {
                var results = from row in context.PreferenceQuestions
                              orderby row.QuestionID
                              select row;
                return results.ToList();
            }
        }




        #endregion


        #region briand playspace


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<ProgramResult> FindProgramMatches(List<int> myCourses)
        {
            using (var context = new CrystalBallContext())
            {
                
                var results = from p in context.Programs
			    where p.Active == true && p.EntranceRequirements.All(e => e.SubjectRequirement.EntranceRequirements.Any(er => myCourses.Contains(er.HighSchoolCourseID)))
			    select new ProgramResult 
                {
                    ProgramID = p.ProgramID,
                    ProgramName = p.ProgramName,
                    ProgramDescription = p.ProgramDescription,
                    ProgramLink = p.ProgramLink
                };


                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<ProgramResult> FindPreferenceMatches(List<StudentPreference> myPrefs)
            //  returns programs where the program and student have answered at least one question the same way
            // and have not given conflicting answers to any questions
            // unanswered questions are ignored
        {
            using (var context = new CrystalBallContext())
            {
               /*  
                * ----------  Strict Preference Matching
                * 
                * var programList = ((from q in context.ProgramPreferences.AsEnumerable()
                                from sq in myPrefs

                                where (q.QuestionID == sq.QuestionID && Convert.ToInt32(q.Answer) == sq.Answer)
                                select q.Program).Distinct()).Except((from q in context.ProgramPreferences.AsEnumerable()
                                                                      from sq in myPrefs
                                                                      where (q.QuestionID == sq.QuestionID && Convert.ToInt32(q.Answer) != sq.Answer)
                                                                      select q.Program).Distinct());

                var result = from p in programList
                             select new ProgramResult
                                {
                                    ProgramID = p.ProgramID,
                                    ProgramName = p.ProgramName,
                                    ProgramDescription = p.ProgramDescription,
                                    ProgramLink = p.ProgramLink
                                };
                */


                // -------------Less strict preference matching


                var result = ((from q in context.ProgramPreferences.AsEnumerable()
                                from sq in myPrefs
                               where q.Program.ProgramPreferences.Any(pq => sq.QuestionID == q.QuestionID && sq.Answer == Convert.ToInt32(q.Answer))
                                select new ProgramResult {
                                    ProgramID = q.Program.ProgramID,
                                    ProgramName = q.Program.ProgramName,
                                    ProgramDescription = q.Program.ProgramDescription,
                                    ProgramLink = q.Program.ProgramLink
                                }).Distinct());

                return result.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<ProgramResult> EntranceReq_Pref_Match(List<ProgramResult> myPrefs, List<ProgramResult> myMatches)
        {
            // Compares the results from two lists of programs and returns the programs common to each
            
            var results = (from p in myMatches
				from s in myPrefs
				where s.ProgramID == p.ProgramID
				select p).Distinct();

                return results.ToList();
            
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<NaitCours> Prefill_Courses(int programID, int semester)
        {
            using (var context = new CrystalBallContext())
            {

                var results = from c in context.ProgramCourses
                              where c.ProgramID == programID && c.Semester <= semester
                              select new NaitCours {
                                  CourseID = c.NaitCourse.CourseID,
                                  CourseCode = c.NaitCourse.CourseCode,
                                  CourseName = c.NaitCourse.CourseName,
                                  CourseCredits = c.NaitCourse.CourseCredits,
                                  Active = c.NaitCourse.Active
                              };


                return results.ToList();
            }
        }
        #endregion


    }
}