using System.ComponentModel.DataAnnotations;

namespace StuMan.Api.Models.DTO
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
