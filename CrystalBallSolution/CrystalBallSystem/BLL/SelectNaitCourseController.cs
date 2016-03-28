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
using CrystalBallSystem.DAL.DTOs;
#endregion


namespace CrystalBallSystem.BLL
{
    public class SelectNaitCourseController
    {

        #region select nait course
        [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<NAITCourse> SearchNaitCourses(string SearchInfo,int programID)
            {
                using (var context = new CrystalBallContext())
                {
                    //searchinfo upper lower case
                    if (programID == 0)
                    {
                        //if (SearchInfo == null)
                        //{
                        //    var result = from Ncourse in context.NaitCourses
                        //                 select new NAITCourse
                        //                 {
                        //                     CourseID = Ncourse.CourseID,
                        //                     CourseCode = Ncourse.CourseCode,
                        //                     CourseName = Ncourse.CourseName,
                        //                     CourseCredits = Ncourse.CourseCredits,

                        //                 };
                        //    return result.ToList();
                        //}
                        //else
                        //{
                        //    var result = from Ncourse in context.NaitCourses
                        //                 where (Ncourse.CourseName.Contains(SearchInfo))
                        //                 || (Ncourse.CourseCode.Contains(SearchInfo))
                        //                 select new NAITCourse
                        //                 {
                        //                     CourseID = Ncourse.CourseID,
                        //                     CourseCode = Ncourse.CourseCode,
                        //                     CourseName = Ncourse.CourseName,
                        //                     CourseCredits = Ncourse.CourseCredits,

                        //                 };
                        //    return result.ToList();
                        //}
                        var result1 =  from Ncourse in context.NaitCourses  
                                      where ((SearchInfo==null) ? Ncourse.CourseName.Contains("")||(Ncourse.CourseCode.Contains("") ): (Ncourse.CourseName.Contains(SearchInfo))
                                         || (Ncourse.CourseCode.Contains(SearchInfo)))
                                         select new NAITCourse
                                         {
                                             CourseID = Ncourse.CourseID,
                                             CourseCode = Ncourse.CourseCode,
                                             CourseName = Ncourse.CourseName,
                                             CourseCredits = Ncourse.CourseCredits,

                                         };
                        return result1.ToList();
                                     
                                     
                    }
                    else
                    {


                        //if (SearchInfo == null)
                        //{
                        //    List<NAITCourse> CourseLIst = new List<NAITCourse>();
                            
                        //    var results = from pc in context.ProgramCourses
                        //              //  from n in pc.NaitCourses
                        //                where pc.ProgramID == programID
                        //                select new NAITCourse
                        //                {
                        //                    CourseID = pc.CourseID,
                        //                    CourseCode = pc.NaitCourse.CourseCode,
                        //                    CourseName = pc.NaitCourse.CourseName,
                        //                    CourseCredits = pc.NaitCourse.CourseCredits
                        //                };
                        //    return results.ToList();

                            
                        //}
                        //else
                        //{
                        //    List<NAITCourse> CourseLIst = new List<NAITCourse>();

                        //    var result = from pc in context.ProgramCourses
                        //                 where pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains(SearchInfo)
                        //                  || ( pc.NaitCourse.CourseCode.Contains(SearchInfo)))
                        //                  select new NAITCourse
                        //                {
                        //                    CourseID = pc.CourseID,
                        //                    CourseCode = pc.NaitCourse.CourseCode,
                        //                    CourseName = pc.NaitCourse.CourseName,
                        //                    CourseCredits = pc.NaitCourse.CourseCredits
                        //                };

                        //    return result.ToList();

                          
                        //}
                        var result2 = from pc in context.ProgramCourses
                                      where ((SearchInfo==null) ?
                                      pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains("")
                                          || ( pc.NaitCourse.CourseCode.Contains(""))) 
                                          : pc.ProgramID == programID && (pc.NaitCourse.CourseName.Contains(SearchInfo)
                                          || ( pc.NaitCourse.CourseCode.Contains(SearchInfo))))
                                      select new NAITCourse
                                        {
                                            CourseID = pc.CourseID,
                                            CourseCode = pc.NaitCourse.CourseCode,
                                            CourseName = pc.NaitCourse.CourseName,
                                            CourseCredits = pc.NaitCourse.CourseCredits
                                        };
                        return result2.ToList();

                    }
                }
            }


        #endregion

        #region add new nait course
         
        #endregion


        #region do we need these?
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
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<ProgramAndCourses> PCMatch(List<int> courseids)
            {
                using (var context = new CrystalBallContext())
                {
                    List<Program> programs = new List<Program>();

                    //var equivalencies = from x in context.CourseEquivalencies
                    //                    where courseids.Contains(x.CourseID)
                    //                    select x.DestinationCourseID;
                    //foreach (int i in equivalencies)
                    //{
                    //    courseids.Add(i);
                    //}

                    var result = from x in context.ProgramCourses
                                 where courseids.Contains(x.CourseID)
                                 select x;




                    var result2 = (from x in result
                                   orderby x.Program.ProgramName
                                   select new ProgramAndCourses
                                   {
                                       ProgramID = x.Program.ProgramID,
                                       ProgramName = x.Program.ProgramName,
                                       ProgramCreditTotal = x.Program.TotalCredits == null ? 0 : x.Program.TotalCredits,
                                       CreditTotal = (from y in result
                                                      where y.Program.ProgramID == x.Program.ProgramID
                                                      select y.NaitCourse.CourseCredits).Sum(),
                                       ProgramCourseMatch = from y in result
                                                            where y.Program.ProgramID == x.Program.ProgramID
                                                            select new ProgramCourseMatch
                                                            {
                                                                CourseID = y.NaitCourse.CourseID,
                                                                CourseCode = y.NaitCourse.CourseCode,
                                                                CourseName = y.NaitCourse.CourseName,
                                                                CourseCredits = y.NaitCourse.CourseCredits
                                                            }


                                   }).GroupBy(a => a.ProgramID);

                    List<ProgramAndCourses> PAC = new List<ProgramAndCourses>();
                    foreach (var item in result2)
                    {
                        PAC.Add(item.FirstOrDefault());
                    }
                    return PAC;
                }
            }

            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<NaitCours> NaitCourse_List(int programid)
            {
                using (CrystalBallContext context = new CrystalBallContext())
                {

                    if (programid == 0)
                    {
                        var step1 = from x in context.NaitCourses select x;
                        return step1.ToList();
                    }
                    else if (programid == -1)
                    {
                        var step1 = from x in context.NaitCourses
                                    where
                                        (x.ProgramCourses).Count() == 0
                                    select x;
                        return step1.ToList();
                    }
                    else
                    {
                        var step1 = from x in context.ProgramCourses
                                    where x.ProgramID == programid
                                    select x.NaitCourse;
                        return step1.ToList();
                    }



                }
            }


            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<Program> Program_List()
            {
                using (CrystalBallContext context = new CrystalBallContext())
                {
                    return context.Programs.ToList();
                }
            }

            //[DataObjectMethod(DataObjectMethodType.Insert, false)]
            // Adds the supplied category to the database
            public void AddCourse(NaitCours item)
            {
                using (CrystalBallContext context = new CrystalBallContext())
                {
                    NaitCours added = null;
                    added = context.NaitCourses.Add(item);
                    context.SaveChanges();
                }
            }
        #endregion
    }
    }
