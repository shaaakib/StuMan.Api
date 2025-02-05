using Microsoft.EntityFrameworkCore;
using StuMan.Api.Models.Domain;
using System;

public class ApplicationDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Attendance> Attendances { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // **Class Table Seed Data**
        modelBuilder.Entity<Class>().HasData(
            new Class { Id = 1, ClassName = "Class 8" },
            new Class { Id = 2, ClassName = "Class 9" }
        );

        // **Student Table Seed Data**
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "Shakib", Email = "shakib@example.com", Phone = "1234567890", DateOfBirth = new DateTime(2010, 5, 1), ClassId = 1 },
            new Student { Id = 2, Name = "Rahim", Email = "rahim@example.com", Phone = "0987654321", DateOfBirth = new DateTime(2009, 8, 15), ClassId = 2 }
        );

        // **Course Table Seed Data**
        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, CourseName = "Mathematics", CourseCode = "MATH101" },
            new Course { Id = 2, CourseName = "Physics", CourseCode = "PHYS102" }
        );

        // **StudentCourse Table Seed Data (Many-to-Many Relationship)**
        modelBuilder.Entity<StudentCourse>().HasData(
            new StudentCourse { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2013, 3, 23) },
            new StudentCourse { Id = 2, StudentId = 2, CourseId = 2, EnrollmentDate = new DateTime(2015, 8, 1) }
        );

        // **Teacher Table Seed Data**
        modelBuilder.Entity<Teacher>().HasData(
            new Teacher { Id = 1, Name = "Mr. Karim", Email = "karim@example.com", Phone = "1112223333" },
            new Teacher { Id = 2, Name = "Ms. Ayesha", Email = "ayesha@example.com", Phone = "4445556666" }
        );

        // **Attendance Table Seed Data**
        modelBuilder.Entity<Attendance>().HasData(
            new Attendance { Id = 1, StudentId = 1, Date = new DateTime(2012, 5, 1), IsPresent = true },
            new Attendance { Id = 2, StudentId = 2, Date = new DateTime(2012, 5, 2), IsPresent = false }
        );
    }
}
