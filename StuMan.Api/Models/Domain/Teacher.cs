namespace StuMan.Api.Models.Domain
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
