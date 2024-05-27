using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using yenideneme.Models;

namespace yenideneme
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Class tablosu için yapılandırma
            modelBuilder.Entity<Class>()
                .HasKey(c => c.ClassId);

            // Student tablosu için yapılandırma
            modelBuilder.Entity<Student>()
           .HasKey(s => s.StudentId);
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId);

            // Teacher tablosu için yapılandırma
            modelBuilder.Entity<Teacher>()
            .HasKey(t => t.TeacherId);
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Class)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.ClassId);


            // StudentTeacher tablosu için yapılandırma
            modelBuilder.Entity<StudentTeacher>()
           .HasKey(st => new { st.StudentId, st.TeacherId });
            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Student)
                .WithMany(s => s.StudentTeachers)
                .HasForeignKey(st => st.StudentId);
            modelBuilder.Entity<StudentTeacher>()
                .HasOne(st => st.Teacher)
                .WithMany(t => t.StudentTeachers)
                .HasForeignKey(st => st.TeacherId);
        }

    }
}
