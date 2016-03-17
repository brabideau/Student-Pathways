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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SubjectRequirement> Get_SubjectReq_ByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = (from x in context.EntranceRequirements
                              where x.ProgramID == programID
                              select x.SubjectRequirement).Distinct();
                return result.ToList();
            }
        }

        public List<GetHSCourseCode> Get_EntReq_ByProgram_Subject(int programID, int subjectID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = from x in context.EntranceRequirements
                             from hs in context.HighSchoolCourses
                             where x.ProgramID == programID
                             && x.SubjectRequirementID == subjectID
                             && x.HighSchoolCourseID == hs.HighSchoolCourseID
                             select new GetHSCourseCode
                             {
                                 CourseID = hs.HighSchoolCourseID,
                                 CourseCode = hs.HighSchoolCourseName
                             };
                return result.ToList();
            }
        }

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

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void EntranceReq_Delete(int courseEquivalencyID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                //lookup the instance and record if found (set pointer to instance)
                CourseEquivalency existing = context.CourseEquivalencies.Find(courseEquivalencyID);

                //setup the command to execute the delete
                context.CourseEquivalencies.Remove(existing);
                //command is not executed until it is actually saved.
                context.SaveChanges();
            }
        }
        #endregion
    }
}
