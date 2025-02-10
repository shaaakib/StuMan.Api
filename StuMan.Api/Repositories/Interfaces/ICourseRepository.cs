using StuMan.Api.Models.Domain;

namespace StuMan.Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> AddAsync(Course course);
        Task<Course> UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
    }
}
