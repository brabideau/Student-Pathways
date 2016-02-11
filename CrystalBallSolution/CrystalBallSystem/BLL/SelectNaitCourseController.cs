
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


#endregion


namespace CrystalBallSystem.BLL
{
    public class SelectNaitCourseController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProgramNameID> Program_List()
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
        public List<NAITCourse> SearchNaitCourses(string SearchInfo,int programID)
        {
            using (var context = new CrystalBallContext())
            {
                if (programID != 0)
                {
                    var result1 = (from x in context.Programs
                                   where x.ProgramID == programID
                                   select x.NaitCourses).FirstOrDefault();
                    //where(y.CourseName.Contains(SearchInfo))
                    //              || (y.CourseCode.Contains(SearchInfo))
                    var results = SearchInfo != null ? (from y in result1
                                                        where (y.CourseName.Contains(SearchInfo))
                                                        || (y.CourseCode.Contains(SearchInfo))
                                                        select new NAITCourse
                                                        {
                                                            CourseID = y.CourseID,
                                                            CourseCode = y.CourseCode,
                                                            CourseName = y.CourseName,
                                                            CourseCredits = y.CourseCredits

                                                        }) : 
                                                        from y in result1
                                                        select new NAITCourse
                                                             {
                                                                 CourseID = y.CourseID,
                                                                 CourseCode = y.CourseCode,
                                                                 CourseName = y.CourseName,
                                                                 CourseCredits = y.CourseCredits

                                                             };
                    return results.ToList();
                }
                else
                {
                    var results =SearchInfo != null ?
                                  from z in context.NaitCourses
                                  where (z.CourseName.Contains(SearchInfo)) || (z.CourseCode.Contains(SearchInfo))
                                  select new NAITCourse
                                    {
                                        CourseID = z.CourseID,
                                        CourseCode = z.CourseCode,
                                        CourseName = z.CourseName,
                                        CourseCredits = z.CourseCredits
                                    }:
                                    from z in context.NaitCourses
                                    select new NAITCourse
                                    {
                                        CourseID = z.CourseID,
                                        CourseCode = z.CourseCode,
                                        CourseName = z.CourseName,
                                        CourseCredits = z.CourseCredits

                                    }
                                    ;
                    return results.ToList();
                }
                
            }
        }
    }
}
