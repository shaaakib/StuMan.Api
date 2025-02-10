using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuMan.Api.Models.Domain
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string CourseName { get; set; }

        [Required, MaxLength(10)]
        public string CourseCode { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
