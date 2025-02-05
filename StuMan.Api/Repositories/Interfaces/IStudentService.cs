using StuMan.Api.Models.DTO;

namespace StuMan.Api.Repositories.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto> GetStudentAsync(int id);
        Task<CreateStudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task<bool> UpdateStudentAsyn(int  id, UpdateStudentDto updateStudentDto);
        Task<bool> DeleteStudentAsynAsync(int id);
    }
}
