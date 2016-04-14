using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional namespace
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using CrystalBallSystem.DAL.Entities;
using CrystalBallSystem.DAL.POCOs;
using CrystalBallSystem.DAL;
using System.ComponentModel;
using CrystalBallSystem.DAL.DTOs;
#endregion

namespace CrystalBallSystem.BLL
{
    [DataObject]
    public class AdminController
    {
        #region Admin management


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all programs for the specified category
        public List<Program> GetProgramByCategory(int categoryID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == categoryID)).ToList();
                return result;

            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Program_Update(Program item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                Program data = new Program()
                {
                    ProgramID = item.ProgramID,
                    ProgramName = item.ProgramName,
                    ProgramDescription = item.ProgramDescription,
                    ProgramLength = item.ProgramLength,
                    ProgramLink = item.ProgramLink,
                    Active = item.Active,
                    CompetitiveAdvantage = item.CompetitiveAdvantage,
                    TotalCredits = item.TotalCredits,
                    CredentialTypeID = item.CredentialTypeID
                };

                context.Entry<Program>(context.Programs.Attach(data)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }


        #endregion


        #region Preference Questions


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all questions and answers for the specified program
        public List<GetProgramPreferenceQuestions> GetQuestionsByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from x in context.ProgramPreferences
                             where x.ProgramID == programID
                             select new GetProgramPreferenceQuestions()
                             {
                                 QuestionID = x.QuestionID,
                                 Question = x.PreferenceQuestion.Description,
                                 Answer = x.Answer
                             };
                return result.ToList();
                //
                // return context.NaitCourses.Where(c => c.Programs.Any(p => p.ProgramID == programID)).ToList();
            }
        }



        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateProgramPreferences(List<ProgramPreference> questions)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                foreach (ProgramPreference item in questions)
                {
                    bool exists = context.ProgramPreferences.Any(x => x.QuestionID == item.QuestionID && x.ProgramID == item.ProgramID);

                    if (exists)
                    {
                        context.Entry<ProgramPreference>(context.ProgramPreferences.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                    }

                    else
                    {
                        context.ProgramPreferences.Add(item);
                    }
                    context.SaveChanges();
                }
            }
        }
        #endregion

        #region Equivalency Page

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GetEquivalencyNames> GetEquivalencies(int programID)
        {
            using (var context = new CrystalBallContext())
            {
                var results = from ce in context.CourseEquivalencies
                              where ce.ProgramID == programID
                              select new GetEquivalencyNames
                              {
                                  CourseEquivalencyID = ce.CourseEquivalencyID,
                                  CourseCode = ce.NaitCourse.CourseCode,
                                  CourseName = ce.NaitCourse.CourseName,
                                  DestinationCourseCode = (from nc in context.NaitCourses
                                                           where nc.CourseID == ce.TransferCourseID
                                                           select nc.CourseCode).FirstOrDefault(),
                                  DestinationCourseName = (from nc in context.NaitCourses
                                                           where nc.CourseID == ce.TransferCourseID
                                                           select nc.CourseName).FirstOrDefault(),
                              };
                return results.ToList();
            }
        }



        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public bool AddEquivalency(int programID, int courseID, int destinationCourseID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                CourseEquivalency added = null;
                int exist = (from x in context.CourseEquivalencies
                            where x.ProgramID == programID && x.ProgramCourseID == courseID && x.TransferCourseID == destinationCourseID
                            select x).Count();
                if (exist == 0)
                {
                    added = context.CourseEquivalencies.Add(new CourseEquivalency() { ProgramID = programID, ProgramCourseID = courseID, TransferCourseID = destinationCourseID });
                    context.SaveChanges();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Equivalency_Delete(int courseEquivalencyID)
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

        #region Degree Entrance Requirements
        #region List Degree Entrance Requirements
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Adds the supplied category to the database
        public List<GetDegEntReqs> Get_DERByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from der in context.DegreeEntranceRequirements
                             where der.ProgramID == programID
                             select new GetDegEntReqs
                             {
                                 DegreeEntranceRequirementID = der.DegreeEntranceReqID,
                                 CredentialTypeID = der.CredentialType.CredentialTypeID,
                                 CredentialTypeName = der.CredentialType.CredentialTypeName,
                                 CategoryID = der.Category.CategoryID,
                                 CategoryDescription = der.Category.CategoryDescription,
                                 GPA = der.GPA
                             };

                return result.ToList();
            }
        }
        #endregion

        #region Delete Entrance Requirements
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DER_Delete(int degReqID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                DegreeEntranceRequirement existing = context.DegreeEntranceRequirements.Find(degReqID);

                context.DegreeEntranceRequirements.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion

        #region Add DER
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public bool AddDER(DegreeEntranceRequirement item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                int pID = item.ProgramID;
                int credentialtID = item.CredentialTypeID;
                int categroyid = item.CategoryID;
                int exists = (from x in context.DegreeEntranceRequirements where
                             x.ProgramID ==pID && x.CredentialTypeID == credentialtID && x.CategoryID == categroyid
                             select x).Count();

                if (exists == 0)
                {
                    var added = context.DegreeEntranceRequirements.Add(item);
                    context.SaveChanges();
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        #endregion
        #endregion


        #region Subject Requirements
      
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


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<SubjectRequirementsDetail> Get_Subject_Requirement_Details(int programid)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from p in context.Programs
                              from x in p.EntranceRequirements
                              where x.ProgramID == programid
                              group x by x.SubjectRequirement into y
                              select new SubjectRequirementsDetail
                              {
                                  SubjectRequirementID = y.Key.SubjectRequirementID,
                                  SubjectDescription = y.Key.SubjectDescription,
                                  EntranceReqs = (from z in y
                                                  select new EntranceRequirementDetail
                                                  {
                                                      EntranceRequirementID = z.EntranceRequirementID,
                                                      HSCourseID = z.HighSchoolCourseID,
                                                      HSCourseName = z.HighSchoolCours.HighSchoolCourseName,
                                                      Mark = z.RequiredMark
                                                  }).ToList()
                              };

                return results.ToList();

            }
        }

        #region Briand Playspace



        //[DataObjectMethod(DataObjectMethodType.Select, false)]
        //// Returns all categories
        //public List<Program> Program_Search(string searchTerm, int? catID)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {

        //        List<Program> results = new List<Program> { };

        //        if (catID == null && searchTerm != null)
        //        {
        //            results = (from p in context.Programs
        //                       where p.ProgramName.Contains(searchTerm)
        //                       select p).ToList();
        //        }
        //        else if (catID != null && searchTerm == null)
        //        {
        //            results = (context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == catID))).ToList();
        //        }
        //        else
        //        {
        //            results = (from p in context.Programs
        //                       select p).ToList();
        //        }

        //        return results;
        //    }
        //}


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Get one program by ID
        public Program Get_Program(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = (from p in context.Programs
                              where p.ProgramID == programID
                              select p).FirstOrDefault();
                return result;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories for a specific program
        public List<Int32> Get_Categories_By_Program(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from p in context.Programs
                              from pc in p.Categories
                              where p.ProgramID == programID
                              select pc.CategoryID;

                return results.ToList();
            }
        }

        public List<NaitCours> GetCoursesByProgramSemester(int programID, int sem)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                if (0 < sem && sem < 5)
                {
                    var result = from x in context.ProgramCourses
                                 where x.ProgramID == programID && x.Semester == sem
                                 select x.NaitCourse;
                    return result.ToList();
                }
                else
                {
                    var result = (from x in context.ProgramCourses
                                  where x.ProgramID == programID
                                  select x.NaitCourse).Except(from y in context.ProgramCourses
                                                              where y.Semester > 0 && y.Semester < 5
                                                              select y.NaitCourse);

                    return result.ToList();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddProgramCourse(ProgramCourse item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                bool exists = context.ProgramCourses.Any(x => x.CourseID == item.CourseID && x.ProgramID == item.ProgramID);

                if (!exists)
                {
                    var added = context.ProgramCourses.Add(item);
                    context.SaveChanges();
                }
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DeleteProgramCourse(ProgramCourse item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                ProgramCourse existing = context.ProgramCourses.Find(item.CourseID, item.ProgramID);

                context.ProgramCourses.Remove(existing);

                context.SaveChanges();
            }
        }

        #endregion


        #region do we need these?

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CourseGroup> CourseGroup_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.CourseGroups.OrderBy(x => x.CourseGroupID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<Category> Category_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.Categories.OrderBy(x => x.CategoryID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all high school courses
        public List<GetHighSchoolCourses> HighSchoolCourse_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from row in context.HighSchoolCourses
                              select new GetHighSchoolCourses()
                              {
                                  HighSchoolCourseID = row.HighSchoolCourseID,
                                  HighSchoolCourseName = row.HighSchoolCourseName,
                                  CourseGroupID = row.CourseGroup.CourseGroupID,
                                  CourseGroup = row.CourseGroup.CourseGroupDescription,
                                  CourseLevel = row.CourseLevel
                              };
                return results.OrderBy(c => c.CourseGroupID).ThenBy(l => l.CourseLevel).ToList();
               // return context.HighSchoolCourses.OrderBy(x => x.HighSchoolCourseID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all credential types
        public List<CredentialType> CredentialType_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                return context.CredentialTypes.OrderBy(x => x.CredentialTypeID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        // Adds the supplied category to the database
        public void AddCategory(Category item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                Category added = null;
                added = context.Categories.Add(item);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCategory(Category item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                Category data = new Category()
                {
                    CategoryID = item.CategoryID,
                    CategoryDescription = item.CategoryDescription
                };

                context.Entry<Category>(context.Categories.Attach(data)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddProgramInCategories(List<int> categoryId, int programId)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                Category add = null;

                Program newProgram = null;
                newProgram = context.Programs.Find(programId);

                if (newProgram.Categories.Count() != 0)
                {
                    var deletedCategory = newProgram.Categories.ToList<Category>();
                    deletedCategory.ForEach(dc => newProgram.Categories.Remove(dc));
                    context.SaveChanges();
                }

                foreach (var item in categoryId)
                {
                    add = context.Categories.Find(item);
                    newProgram.Categories.Add(add);
                }

                context.Programs.Add(newProgram);
                context.Programs.Attach(newProgram);
                add.Programs.Add(newProgram);
                context.SaveChanges();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddProgram(List<Program> program)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                Program added = null;

                var newProgram = new Program();
                int newProgramID = newProgram.ProgramID;

                foreach (var item in program)
                {
                    added = context.Programs.Add(new Program()
                    {
                        ProgramID = newProgramID,
                        CredentialTypeID = item.CredentialTypeID,
                        ProgramName = item.ProgramName,
                        ProgramDescription = item.ProgramDescription,
                        TotalCredits = item.TotalCredits,
                        ProgramLength = item.ProgramLength,
                        CompetitiveAdvantage = item.CompetitiveAdvantage,
                        Active = item.Active,
                        ProgramLink = item.ProgramLink
                    });
                }


                context.SaveChanges();
            }
        }

        public int GetProgramIDByName(string name)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                int programId = (from row in context.Programs
                                 where row.ProgramName == name
                                 select row.ProgramID).FirstOrDefault();
                return programId;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all courses for the specified program
        public List<NaitCours> GetCoursesByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = from x in context.ProgramCourses
                             where x.ProgramID == programID
                             select x.NaitCourse;
                return result.ToList();

            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddNaitCourse(List<NaitCours> courses, int programid)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                NaitCours added = null;
                ProgramCourse addedprogramlink = null;

                var newCourse = new NaitCours();
                int newCourseID = newCourse.CourseID;

                //added = context.NaitCourses.Add(item);
                foreach (var item in courses)
                {
                    added = context.NaitCourses.Add(new NaitCours()
                    {
                        CourseID = newCourseID,
                        CourseCode = item.CourseCode,
                        CourseName = item.CourseName,
                        CourseCredits = item.CourseCredits,
                        Active = item.Active
                    });


                }

                foreach (var item in courses)
                {
                    addedprogramlink = context.ProgramCourses.Add(new ProgramCourse()
                    {
                        ProgramID = programid,
                        CourseID = newCourseID,
                        Semester = 1
                    });
                }

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateNaitCourse(NaitCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                NaitCours data = new NaitCours()
                {
                    CourseID = item.CourseID,
                    CourseCode = item.CourseCode,
                    CourseName = item.CourseName,
                    CourseCredits = item.CourseCredits,
                    Active = item.Active,

                };

                context.Entry<NaitCours>(context.NaitCourses.Attach(data)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void HighSchoolCourse_Update(HighSchoolCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                HighSchoolCours data = new HighSchoolCours()
                {
                    HighSchoolCourseID = item.HighSchoolCourseID,
                    HighSchoolCourseName = item.HighSchoolCourseName,
                    CourseGroupID = item.CourseGroupID,
                    CourseLevel = item.CourseLevel
                };

                context.Entry<HighSchoolCours>(context.HighSchoolCourses.Attach(data)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddHighSchoolCourse(List<HighSchoolCours> item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                HighSchoolCours added = null;

                var newHighShoolCourse = new HighSchoolCours();
                //int newCourseID = newHighShoolCourse.HighSchoolCourseID;

                foreach (var data in item)
                {
                    added = context.HighSchoolCourses.Add(new HighSchoolCours()
                    {
                        //HighSchoolCourseID = newCourseID,
                        HighSchoolCourseName = data.HighSchoolCourseName,
                        CourseGroupID = data.CourseGroupID,
                        CourseLevel = data.CourseLevel
                    });
                }

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<Program> findProgram(string searchTerm, int catID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                List<Program> results = new List<Program> { };
                List<Program> results2 = new List<Program> { };

                if (catID == 0 && searchTerm != null)
                {
                    results = (from p in context.Programs
                               where p.ProgramName.Contains(searchTerm)
                               select p).ToList();
                }
                else if (catID != 0 && string.IsNullOrEmpty(searchTerm))
                {
                    results = (context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == catID))).ToList();
                }
                else if (catID != 0 && searchTerm != null)
                {
                    results2 = (context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == catID))).ToList();
                    results = (from p in results2
                               where p.ProgramName.ToUpper().Contains(searchTerm.ToUpper())
                               select p).ToList();
                }
                else
                {
                    results = (from p in context.Programs
                               select p).ToList();
                }

                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<PreferenceQuestion> Question_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.PreferenceQuestions.OrderBy(x => x.QuestionID).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdatePreferenceQuestion(PreferenceQuestion item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                context.Entry<PreferenceQuestion>(context.PreferenceQuestions.Attach(item)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddPreferenceQuestion(PreferenceQuestion item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                PreferenceQuestion added = null;
                added = context.PreferenceQuestions.Add(item);
                context.SaveChanges();
            }
        }

        //[DataObjectMethod(DataObjectMethodType.Update, false)]
        //public void UpdateProgramPreferenceQuestion(GetProgramPreferenceQuestions item)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {
        //        ProgramPreference data = new ProgramPreference()
        //        {
        //            QuestionID = item.QuestionID,
        //            ProgramID = item.ProgramID,
        //            Answer = item.Answer
        //        };

        //        context.Entry<ProgramPreference>(context.ProgramPreferences.Attach(data)).State = System.Data.Entity.EntityState.Modified;

        //        context.SaveChanges();
        //    }
        //}

        //[DataObjectMethod(DataObjectMethodType.Insert, false)]
        //public void AddProgramPreferenceQuestion(ProgramPreference questions)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {
        //        ProgramPreference added = null;

        //        added = context.ProgramPreferences.Add(questions);

        //        context.SaveChanges();
        //    }
        //}


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all Programs
        public List<Program> Get_All_Programs()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var results = from p in context.Programs
                              select p;
                return results.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        // Adds the supplied category to the database
        public bool AddEntranceRequirement(EntranceRequirement item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                int hsid = item.HighSchoolCourseID;
                int rid = item.SubjectRequirementID;
                int? programid = item.ProgramID;

                bool success = false;

                int existing = (from x in context.EntranceRequirements
                               where x.HighSchoolCourseID==hsid &&
                               x.SubjectRequirementID == rid && x.ProgramID == programid
                               select x).Count();

                EntranceRequirement added = null;
                if (existing == 0)
                {
                    added = context.EntranceRequirements.Add(item);
                    context.SaveChanges();
                    success = true;
                }

                return success;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void EntranceReq_Delete(EntranceRequirement req)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                //lookup the instance and record if found (set pointer to instance)
                EntranceRequirement existing = context.EntranceRequirements.Find(req.EntranceRequirementID);
                 
                //setup the command to execute the delete

                context.EntranceRequirements.Remove(existing);
                //command is not executed until it is actually saved.
                context.SaveChanges();
            }
        }

        public void EntranceRequirement_Update(EntranceRequirement item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                context.Entry<EntranceRequirement>(context.EntranceRequirements.Attach(item)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

        }

        public void Deg_EntranceRequirement_Update(DegreeEntranceRequirement item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                DegreeEntranceRequirement data = new DegreeEntranceRequirement()
                {
                    DegreeEntranceReqID = item.DegreeEntranceReqID,
                    ProgramID = item.ProgramID,
                    CredentialTypeID = item.CredentialTypeID,
                    CategoryID = item.CategoryID,
                    GPA = item.GPA
                };

                context.Entry<DegreeEntranceRequirement>(context.DegreeEntranceRequirements.Attach(data)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }

        #endregion
    }
}
