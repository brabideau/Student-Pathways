using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace
using System.Data;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.POCOs;

#endregion


namespace CrystalBallSystem.BLL
{
    public class SelectNaitCourseController
    {
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<NAITCourse> SearchNaitCourses(string SearchInfo,int programID)
            {
                using (var context = new CrystalBallContext())
                {
                    //searchinfo upper lower case
                    if (programID == 0)
                    {
                        if (SearchInfo == null)
                        {
                            var result = from Ncourse in context.NaitCourses
                                         select new NAITCourse
                                         {
                                             CourseID = Ncourse.CourseID,
                                             CourseCode = Ncourse.CourseCode,
                                             CourseName = Ncourse.CourseName,
                                             CourseCredits = Ncourse.CourseCredits,

                                         };
                            return result.ToList();
                        }
                        else
                        {
                            var result = from Ncourse in context.NaitCourses
                                         where (Ncourse.CourseName.Contains(SearchInfo))
                                         || (Ncourse.CourseCode.Contains(SearchInfo))
                                         select new NAITCourse
                                         {
                                             CourseID = Ncourse.CourseID,
                                             CourseCode = Ncourse.CourseCode,
                                             CourseName = Ncourse.CourseName,
                                             CourseCredits = Ncourse.CourseCredits,

                                         };
                            return result.ToList();
                        }
                    }
                    else
                    {


                        if (SearchInfo == null)
                        {
                            List<NAITCourse> CourseLIst = new List<NAITCourse>();
                            
                            var results = from pc in context.ProgramCourses
                                      //  from n in pc.NaitCourses
                                        where pc.ProgramID == programID
                                        select new NAITCourse
                                        {
                                            CourseID = pc.CourseID,
                                            CourseCode = pc.NaitCourse.CourseCode,
                                            CourseName = pc.NaitCourse.CourseName,
                                            CourseCredits = pc.NaitCourse.CourseCredits
                                        };
                            return results.ToList();

                            
                        }
                        else
                        {
                            List<NAITCourse> CourseLIst = new List<NAITCourse>();

                            var result = from pc in context.ProgramCourses
                                         where pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains(SearchInfo)
                                          || ( pc.NaitCourse.CourseCode.Contains(SearchInfo)))
                                          select new NAITCourse
                                        {
                                            CourseID = pc.CourseID,
                                            CourseCode = pc.NaitCourse.CourseCode,
                                            CourseName = pc.NaitCourse.CourseName,
                                            CourseCredits = pc.NaitCourse.CourseCredits
                                        };

                            return result.ToList();

                          
                        }

                    }
                }
            }

            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<ProgramNameID> GetProgram()
            {
                using (var context = new CrystalBallContext())
                {
                    var results = from x in context.Programs
                                  orderby x.ProgramName
                                  select new ProgramNameID
                                  {
                                      ProgramID = x.ProgramID,
                                      ProgramName = x.ProgramName
                                  };
                    return results.ToList();
                }
            }

            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<NAITCourse> SelectedNaitCourses(int courseID)
            {
                using (var context = new CrystalBallContext())
                {
                    var result = from x in context.NaitCourses
                                 where x.CourseID == courseID
                                 select new NAITCourse
                                 {
                                     CourseID = x.CourseID,
                                     CourseCode = x.CourseCode,
                                     CourseName = x.CourseName,
                                     CourseCredits = x.CourseCredits
                                 };
                    return result.ToList();
                }
            }
            public List<ProgramCourseMatch> PCMatch(List<int> courseids)
            {
                using (var context = new CrystalBallContext())
                {
                    List<Program> programs = new List<Program>();
                    
                        var result = from x in context.ProgramCourses
                                     where courseids.Contains(x.CourseID)
                                     select x;
                        var result2 = from x in result select new ProgramCourseMatch
                                            {
                                                ProgramID = x.Program.ProgramID,
                                                ProgramName = x.Program.ProgramName,
                                                CourseID = x.NaitCourse.CourseID,
                                                CourseCode = x.NaitCourse.CourseCode,
                                                CourseName = x.NaitCourse.CourseName,
                                                CourseCredits = x.NaitCourse.CourseCredits
                                            };

                        return result2.ToList();
                }
            }
        }
    }
