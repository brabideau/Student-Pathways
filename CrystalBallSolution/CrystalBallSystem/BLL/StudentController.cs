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
        /*
         // Input datatable should have one column, listing courseIDs
         * 
         * 
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public DataTable FindProgramMatches(DataTable myCourses)
           
        {
            using (var context = new CrystalBallContext())
            {
                var param = new SqlParameter("@CourseList", myCourses);
                param.TypeName = "CourseIDs";
                var result = context.Database
                    .SqlQuery<int>("FindProgramsB @CourseList", param)
                    .ToList();


                DataTable progMatches = new DataTable();
                progMatches.Columns.Add("ProgramID");
                foreach (var item in result)
                {
                    var row = progMatches.NewRow();
                    row["ProgramID"] = item;
                    progMatches.Rows.Add(row);
                }
                return progMatches;
            }
        }

        */

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        static public List<ProgramResult> FindProgramMatches(List<int> myCourses)
            // Input datatable should have one column, listing courseIDs
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
        static public DataTable FindPreferenceMatches(DataTable myPrefs)
            // the input datatable should have 2 columns
            // first column is the questionID, second column is the answer
        {
            using (var context = new CrystalBallContext())
            {
                var param = new SqlParameter("@S_Prefs", myPrefs);
                param.TypeName = "StudentPrefs";
                var result = context.Database
                    .SqlQuery<int>("PreferenceMatching @S_Prefs", param)
                    .ToList();


                DataTable progMatches = new DataTable();
                progMatches.Columns.Add("ProgramID");
                foreach (var item in result)
                {
                    var row = progMatches.NewRow();
                    row["ProgramID"] = item;
                    progMatches.Rows.Add(row);
                }
                return progMatches;
            }
        }



        #endregion


    }
}