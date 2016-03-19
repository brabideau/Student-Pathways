using CrystalBallSystem.DAL;
using CrystalBallSystem.DAL.DTOs;
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
    public class testController
    {
        #region EquivalentCourses
        //List Equivalent Courses
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<NAITCourse> Equivalent_Courses(List<int> courseids)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from x in context.CourseEquivalencies
                             from nc in context.NaitCourses
                             where courseids.Contains(x.CourseID) && nc.CourseID == x.DestinationCourseID
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

        #region Entrance Requirements
        //for already existing entrance requirements
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Equivalency_Delete(int entReqID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                //lookup the instance and record if found (set pointer to instance)
                EntranceRequirement existing = context.EntranceRequirements.Find(entReqID);

                //setup the command to execute the delete
                context.EntranceRequirements.Remove(existing);
                //command is not executed until it is actually saved.
                context.SaveChanges();
            }
        }
        
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SubjectRequirementAndCourses> Get_SubjectReq_ByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = (from x in context.EntranceRequirements
                              where x.ProgramID == programID
                              select new SubjectRequirementAndCourses
                              {
                                  EntranceReqID = x.EntranceRequirementID,
                                  SubjectReqID = x.SubjectRequirementID,
                                  SubjectDesc = x.SubjectRequirement.SubjectDescription,
                                  GetHSCourseIDName = (from hs in context.HighSchoolCourses
                                                     where x.HighSchoolCourseID == hs.HighSchoolCourseID
                                                     select hs.HighSchoolCourseName).FirstOrDefault()
                              }).GroupBy(a => a.EntranceReqID);

                
                List<SubjectRequirementAndCourses> SRC = new List<SubjectRequirementAndCourses>();
                foreach (var item in result)
                {
                    SRC.Add(item.FirstOrDefault());
                }
                return SRC.ToList();
            }
        }

        //for new entrance requirements
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        // Adds the supplied category to the database
        public void AddEntranceRequirement(EntranceRequirement item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                EntranceRequirement added = null;
                added = context.EntranceRequirements.Add(item);
                context.SaveChanges();
            }
        }
        #endregion

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
                                 Mark = null
                             };

                return result.Distinct().ToList();
            }
        }
        #endregion

        #region Delete New Requirement From List

        #endregion
    }
}
