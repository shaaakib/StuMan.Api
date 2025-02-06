using Microsoft.EntityFrameworkCore;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Repositories.Implementation
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _db;
        public ClassRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddClassAsync(Class classEntity)
        {
            await _db.Classes.AddAsync(classEntity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
           var classEntity = await GetClassByIdAsync(id);
            if (classEntity != null)
            {
                _db.Classes.Remove(classEntity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _db.Classes.Include(c => c.Students).ToListAsync();
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            var result = await _db.Classes.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task UpdateClassAsync(Class classEntity)
        {
            _db.Classes.Update(classEntity);
            await _db.SaveChangesAsync();
        }
    }
}
