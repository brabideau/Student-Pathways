using System;
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
                var programList = ((from q in context.ProgramPreferences.AsEnumerable()
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


                return result.ToList();
            }
        }



        #endregion


    }
}