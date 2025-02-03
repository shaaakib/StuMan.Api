namespace StuMan.Api.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public Class  Class { get; set; }
    }
}
