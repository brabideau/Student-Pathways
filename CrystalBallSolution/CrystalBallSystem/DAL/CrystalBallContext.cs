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
<<<<<<< HEAD
        public virtual DbSet<CredentialType> CredentialTypes { get; set; }
=======
>>>>>>> origin/master
        public virtual DbSet<EntranceRequirement> EntranceRequirements { get; set; }
        public virtual DbSet<HighSchoolCours> HighSchoolCourses { get; set; }
        public virtual DbSet<NaitCours> NaitCourses { get; set; }
        public virtual DbSet<PreferenceQuestion> PreferenceQuestions { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
<<<<<<< HEAD
        public virtual DbSet<ProgramPreference> ProgramPreferences { get; set; }
=======
>>>>>>> origin/master
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

            modelBuilder.Entity<CredentialType>()
                .Property(e => e.CredentialTypeName)
                .IsUnicode(false);

<<<<<<< HEAD
            modelBuilder.Entity<CredentialType>()
                .HasMany(e => e.Programs)
                .WithRequired(e => e.CredentialType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HighSchoolCours>()
                .Property(e => e.HighSchoolCourseName)
                .IsUnicode(false);

            modelBuilder.Entity<HighSchoolCours>()
=======
            modelBuilder.Entity<HighSchoolCourse>()
>>>>>>> origin/master
                .HasMany(e => e.EntranceRequirements)
                .WithRequired(e => e.HighSchoolCours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NaitCours>()
                .Property(e => e.CourseName)
                .IsUnicode(false);

            modelBuilder.Entity<NaitCours>()
                .HasMany(e => e.Programs)
                .WithMany(e => e.NaitCourses)
                .Map(m => m.ToTable("ProgramCourses").MapLeftKey("CourseID").MapRightKey("ProgramID"));

            modelBuilder.Entity<PreferenceQuestion>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PreferenceQuestion>()
                .HasMany(e => e.ProgramPreferences)
                .WithRequired(e => e.PreferenceQuestion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramName)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramDescription)
                .IsUnicode(false);

<<<<<<< HEAD
            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramLength)
                .IsUnicode(false);
=======
      
>>>>>>> origin/master

            modelBuilder.Entity<Program>()
                .Property(e => e.ProgramLink)
                .IsUnicode(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.EntranceRequirements)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);
<<<<<<< HEAD

            modelBuilder.Entity<Program>()
                .HasMany(e => e.ProgramPreferences)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubjectRequirement>()
                .Property(e => e.SubjectDescription)
                .IsUnicode(false);

            modelBuilder.Entity<SubjectRequirement>()
                .HasMany(e => e.EntranceRequirements)
                .WithRequired(e => e.SubjectRequirement)
                .WillCascadeOnDelete(false);
        }
=======
        
        } */
>>>>>>> origin/master
    }
}
