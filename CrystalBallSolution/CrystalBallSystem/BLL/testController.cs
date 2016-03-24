using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.DTOs;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class testController
    {
        #region Equivalencies
        #region List Equivalent Courses
        //List Equivalent Courses
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<NAITCourse> Equivalent_Courses(int courseids)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from x in context.CourseEquivalencies
                             from nc in context.NaitCourses
                             where nc.CourseID == x.TransferCourseID && x.ProgramCourseID == courseids
                             //where courseids.Contains(x.CourseID) && nc.CourseID == x.DestinationCourseID
                             select new NAITCourse
                             {
                                 CourseID = nc.CourseID,
                                 CourseCode = nc.CourseCode,
                                 CourseName = nc.CourseName,
                                 CourseCredits = nc.CourseCredits
                             };

                return result.ToList();
            }
        }
        #endregion
        #endregion

        #region Entrance Requirements
        #region Delete Entrance Requirements
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void ER_Delete(int entReqID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                EntranceRequirement existing = context.EntranceRequirements.Find(entReqID);

                context.EntranceRequirements.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion        

        #region Add Entrance Requirement Manual
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddEntranceRequirement(List<AddEntranceRequirements> er)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                EntranceRequirement data = null;
                foreach (var item in er)
                {
                    data = context.EntranceRequirements.Add(new EntranceRequirement()
                    {
                        ProgramID = item.programID,
                        HighSchoolCourseID = item.highSchoolID,
                        SubjectRequirementID = item.subReqID,
                        RequiredMark = item.reqMark
                    });
                }
                context.SaveChanges();
            }
        }
        #endregion

        #region Add Entrance Requirement no Program ID
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddEntranceRequirementNPID(int hsID, int srID, int mark)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                EntranceRequirement data = null;
                data = context.EntranceRequirements.Add(new EntranceRequirement()
                {
                    ProgramID = null,
                    HighSchoolCourseID = hsID,
                    SubjectRequirementID = srID,
                    RequiredMark = mark
                });
                context.SaveChanges();
            }
        }
        #endregion

        #region Add Entrance Requirement - No Program ID, No Mark
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddEntranceRequirement_NPIDNM(int hsID, int srID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                EntranceRequirement data = null;
                data = context.EntranceRequirements.Add(new EntranceRequirement()
                {
                    ProgramID = null,
                    HighSchoolCourseID = hsID,
                    SubjectRequirementID = srID,
                    RequiredMark = null
                });
                context.SaveChanges();
            }
        }
        #endregion
        #endregion

        #region Subject Requirements
        #region List Subject Requirements
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Adds the supplied category to the database
        public List<SubjectRequirement> Get_SubjectRequirements()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.SubjectRequirements.OrderBy(x => x.SubjectDescription).ToList();
            }
        }
        #endregion

        #region List Subject Requirements in Program
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SubjectRequirementAndCourses> Get_SubjectReq_ByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = (from x in context.EntranceRequirements
                              where x.ProgramID == programID
                              orderby x.SubjectRequirementID
                              select new SubjectRequirementAndCourses
                              {
                                  EntranceReqID = x.EntranceRequirementID,
                                  SubjectReqID = x.SubjectRequirementID,
                                  SubjectDesc = x.SubjectRequirement.SubjectDescription,
                                  HSCourseCode = (from hs in context.HighSchoolCourses
                                                  where x.HighSchoolCourseID == hs.HighSchoolCourseID
                                                  select hs.HighSchoolCourseName).FirstOrDefault(),
                                  HSCourseMark = (from hs in context.HighSchoolCourses
                                                  where x.HighSchoolCourseID == hs.HighSchoolCourseID
                                                  select x.RequiredMark).FirstOrDefault() == null ? 0 : (from hs in context.HighSchoolCourses
                                                                                                         where x.HighSchoolCourseID == hs.HighSchoolCourseID
                                                                                                         select x.RequiredMark).FirstOrDefault()
                              }).GroupBy(a => a.EntranceReqID);

                List<SubjectRequirementAndCourses> SRC = new List<SubjectRequirementAndCourses>();
                foreach (var item in result)
                {
                    SRC.Add(item.FirstOrDefault());
                }
                return SRC.ToList();
            }
        }
        #endregion

        #region List Courses in SubjectRequirements
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Adds the supplied category to the database
        public List<GetEntranceReq> Get_CoursesBySubjectRequirement(int subjectReqID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from course in context.EntranceRequirements
                             where course.SubjectRequirementID == subjectReqID
                             select new GetEntranceReq
                             {           
                                 HSCourseID = course.HighSchoolCours.HighSchoolCourseID,
                                 HSCourseName = course.HighSchoolCours.HighSchoolCourseName,
                                 //Mark = null
                             };

                return result.Distinct().ToList();
            }
        }
        #endregion

        #region Add Subject Requirement
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        // Adds the supplied category to the database
        public int AddSubjectRequirement(string description)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                SubjectRequirement data = null;
                data = context.SubjectRequirements.Add(new SubjectRequirement()
                {
                    SubjectDescription = description
                });
                context.SaveChanges();
                return data.SubjectRequirementID;
            }
        }
        #endregion

        #region Delete Subject Requirement
        //WON'T WORK IF SUBJECT REQUIREMENT IS BEGIN USED - CHANGE TO ARCHIVE?
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void SR_Delete(int sReqID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                SubjectRequirement existing = context.SubjectRequirements.Find(sReqID);

                context.SubjectRequirements.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
