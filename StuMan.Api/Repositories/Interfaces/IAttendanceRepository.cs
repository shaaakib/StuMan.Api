using StuMan.Api.Models.Domain;

namespace StuMan.Api.Repositories.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendancesAsync();
        Task<Attendance> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId);
        Task CreateAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(int id);
    }
}
