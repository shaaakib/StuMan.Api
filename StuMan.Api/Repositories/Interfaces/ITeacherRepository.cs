using StuMan.Api.Models.Domain;

namespace StuMan.Api.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll();
        Task<Teacher> GetById(int id);
        Task Create(Teacher teacher);
        Task Update(Teacher teacher);
        Task DeleteById(int id);
    }
}
