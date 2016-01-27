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
        public virtual DbSet<CompletedHighSchoolCourse> CompletedHighSchoolCourses { get; set; }
        public virtual DbSet<CompletedProgram> CompletedPrograms { get; set; }
        public virtual DbSet<EntranceRequirement> EntranceRequirements { get; set; }
        public virtual DbSet<HighSchoolCourse> HighSchoolCourses { get; set; }
        public virtual DbSet<NaitCourse> NaitCourses { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramType> ProgramTypes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<SubjectRequirement> SubjectRequirements { get; set; }

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
                .HasMany(e => e.CompletedHighSchoolCourses)
                .WithRequired(e => e.HighSchoolCourse)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.CompletedPrograms)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.SubjectRequirements)
                .WithRequired(e => e.Program)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Program>()
                .HasMany(e => e.Staffs)
                .WithMany(e => e.Programs)
                .Map(m => m.ToTable("StaffProgram").MapLeftKey("ProgramID").MapRightKey("StaffID"));

            modelBuilder.Entity<ProgramType>()
                .Property(e => e.ProgramTypeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ProgramType>()
                .HasMany(e => e.Programs)
                .WithRequired(e => e.ProgramType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.CompletedHighSchoolCourses)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.CompletedPrograms)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);
        }
    }
}
