using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using CrystalBallSystem.DAL.Entities;

namespace CrystalBallSystem.DAL
{
    
    public partial class CrystalBallContext : DbContext
    {
        public CrystalBallContext()
            : base("name=CrystalBall")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<EntranceRequirement> EntranceRequirements { get; set; }
        public virtual DbSet<HighSchoolCourse> HighSchoolCourses { get; set; }
        public virtual DbSet<NaitCourse> NaitCourses { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<SubjectRequirement> SubjectRequirements { get; set; }
        public virtual DbSet<PreferenceQuestion> PreferenceQuestions { get; set; }
        public virtual DbSet<ProgramPreference> ProgramPreferences { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Programs)
                .WithMany(e => e.Categories)
                .Map(m => m.ToTable("ProgramCategory").MapLeftKey("CategoryID").MapRightKey("ProgramID"));

            modelBuilder.Entity<HighSchoolCourse>()
                .Property(e => e.HighSchoolCourseName)
                .IsUnicode(false);

            modelBuilder.Entity<HighSchoolCourse>()
                .HasMany(e => e.EntranceRequirements)
                .WithRequired(e => e.HighSchoolCourse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NaitCourse>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<NaitCourse>()
                .HasMany(e => e.Students)
                .WithMany(e => e.NaitCourses)
                .Map(m => m.ToTable("CompletedCourses").MapLeftKey("CourseID").MapRightKey("StudentID"));

            modelBuilder.Entity<NaitCourse>()
                .HasMany(e => e.Programs)
                .WithMany(e => e.NaitCourses)
                .Map(m => m.ToTable("ProgramCourses").MapLeftKey("CourseID").MapRightKey("ProgramID"));

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramName)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramLink)
                .IsUnicode(false);

      

            modelBuilder.Entity<Program>()
                .HasMany(e => e.SubjectRequirements)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);
        
        } */
    }
}
