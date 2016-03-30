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
                //var results = from row in context.Programs.Where(s => s.Categories.Any(c => c.CategoryID == categoryID))
                //from s in context.Programs
                //from c in s.Categories
                //where c.CategoryID == categoryID
                return context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == categoryID)).ToList();

                //var results = from row in context.Categories
                //              where row.CategoryID == categoryID
                //              select new
                //              {

                //                  Programs = from info in row.Programs
                //                              where info.Active == true
                //                              select new ProgramSummary()
                //                              {
                //                                  ProgramName = info.ProgramName,
                //                                  EntranceSummary = info.ProgramDescription,
                //                                  CompetitiveEntrance = info.CompetitiveAdvantage,
                //                                  TotalCredits = info.TotalCredits,
                //                                  ProgramLink = info.ProgramLink

                //                              }

                //              };

                //return results.Cast<ProgramSummary>().ToList();
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
        public void AddEquivalency(int programID, int courseID, int destinationCourseID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                CourseEquivalency added = null;
                added = context.CourseEquivalencies.Add(new CourseEquivalency() { ProgramID = programID, ProgramCourseID = courseID, TransferCourseID = destinationCourseID });
                context.SaveChanges();
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

        #region Briand Playspace

        

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all categories
        public List<Program> Program_Search(string searchTerm, int? catID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                List<Program> results = new List<Program> { };

                if (catID == null && searchTerm != null)
                {
                    results = (from p in context.Programs
                                  where p.ProgramName.Contains(searchTerm)
                                  select p).ToList();
                }
                else if (catID != null && searchTerm == null)
                {
                    results = (context.Programs.Where(p => p.Categories.Any(c => c.CategoryID == catID))).ToList();
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
                if ( 0 < sem && sem < 5)
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

                var added = context.ProgramCourses.Add(item);
                context.SaveChanges();
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
        public List<HighSchoolCours> HighSchoolCourse_List()
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.HighSchoolCourses.OrderBy(x => x.HighSchoolCourseID).ToList();
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

        //[DataObjectMethod(DataObjectMethodType.Insert, false)]
        //public void AddProgram(List<Program> program, int categoryid)
        //{
        //    using (CrystalBallContext context = new CrystalBallContext())
        //    {
        //        Program added = null;

        //        var newProgram = new Program();
        //        int newProgramID = newProgram.ProgramID;

        //        foreach (var item in program)
        //        {
        //            added = context.Programs.Add(new Program()
        //            {
        //                ProgramID = newProgramID,
        //                CredentialTypeID = item.CredentialTypeID,
        //                ProgramName = item.ProgramName,
        //                ProgramDescription = item.ProgramDescription,
        //                TotalCredits = item.TotalCredits,
        //                ProgramLength = item.ProgramLength,
        //                CompetitiveAdvantage = item.CompetitiveAdvantage,
        //                Active = item.Active,
        //                ProgramLink = item.ProgramLink
        //            });
        //        }

        //        Category newCategory = new Category { CategoryID = categoryid };
        //        context.Categories.Add(newCategory);
        //        context.Categories.Attach(newCategory);
        //        added.Categories.Add(newCategory);

        //        context.SaveChanges();
        //    }
        //}

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
                //
                // return context.NaitCourses.Where(c => c.Programs.Any(p => p.ProgramID == programID)).ToList();
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
                //Program newProgram = new Program { ProgramID = programid };
                //context.Programs.Add(newProgram);
                //context.Programs.Attach(newProgram);
                //added.Programs.Add(newProgram);

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
                    CourseGroup = item.CourseGroup,
                    Highest = item.Highest
                };

                context.Entry<HighSchoolCours>(context.HighSchoolCourses.Attach(data)).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddHighSchoolCourse(HighSchoolCours item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var added = context.HighSchoolCourses.Add(item);
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
                else if(catID !=0 && searchTerm != null)
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
        public List<Category> GetCategoryByProgram(int programid)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                return context.Categories.Where(c => c.Programs.Any(p => p.ProgramID == programid)).ToList();
            };
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

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void PreferenceQuestion_Delete(PreferenceQuestion item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                //lookup the instance and record if found (set pointer to instance)
                PreferenceQuestion existing = context.PreferenceQuestions.Find(item.QuestionID);

                //setup the command to execute the delete
                context.PreferenceQuestions.Remove(existing);
                //command is not executed until it is actually saved.
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateProgramPreferenceQuestion(GetProgramPreferenceQuestions item)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                ProgramPreference data = new ProgramPreference()
                {
                    QuestionID = item.QuestionID,
                    ProgramID = item.ProgramID,
                    Answer = item.Answer
                };

                context.Entry<ProgramPreference>(context.ProgramPreferences.Attach(data)).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void AddProgramPreferenceQuestion(ProgramPreference questions)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                ProgramPreference added = null;

                added = context.ProgramPreferences.Add(questions);

                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NAITCourse GetCourseName(string courseCode)
        {
            using (var context = new CrystalBallContext())
            {
                var results = (from equivalency in context.NaitCourses
                               where equivalency.CourseCode == courseCode
                               select new NAITCourse
                               {
                                   CourseID = equivalency.CourseID,
                                   CourseCode = equivalency.CourseCode,
                                   CourseName = equivalency.CourseName,
                                   CourseCredits = equivalency.CourseCredits
                               }).FirstOrDefault();
                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        // Returns all Programs
        public ProgramPreference Get_Program_Question(int programID, int questionID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {
                var result = (from p in context.ProgramPreferences
                              where p.ProgramID == programID && p.QuestionID == questionID
                              select p).FirstOrDefault();
                return result;
            }
        }

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



        public List<DegreeEntranceRequirement> Get_DegEntReq_ByProgram(int programID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = from x in context.DegreeEntranceRequirements
                             where x.ProgramID == programID
                             select x;
                return result.ToList();

            }
        }

        public List<GetHSCourseIDName> Get_EntReq_ByProgram_Subject(int programID, int subjectID)
        {
            using (CrystalBallContext context = new CrystalBallContext())
            {

                var result = from x in context.EntranceRequirements
                             from hs in context.HighSchoolCourses
                             where x.ProgramID == programID
                             && x.SubjectRequirementID == subjectID
                             && x.HighSchoolCourseID == hs.HighSchoolCourseID
                             select new GetHSCourseIDName
                             {
                                 CourseID = hs.HighSchoolCourseID,
                                 CourseName = hs.HighSchoolCourseName
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
