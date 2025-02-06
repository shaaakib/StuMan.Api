using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;

namespace StuMan.Api.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<Class> GetClassByIdAsync(int id);
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task AddClassAsync(Class classEntity);
        Task UpdateClassAsync(Class classEntity);
        Task DeleteClassAsync(int id);
    }
}
