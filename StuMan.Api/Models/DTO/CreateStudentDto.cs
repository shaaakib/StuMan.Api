namespace StuMan.Api.Models.DTO
{
    public class CreateStudentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }

    }
}
