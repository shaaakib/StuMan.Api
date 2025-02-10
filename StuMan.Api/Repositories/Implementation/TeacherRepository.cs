using Microsoft.EntityFrameworkCore;
using StuMan.Api.Models.Domain;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Repositories.Implementation
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _db;
        public TeacherRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Teacher teacher)
        {
            await _db.Teachers.AddAsync(teacher);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var teacher = await GetById(id);
            if (teacher != null)
            {
                _db.Teachers.Remove(teacher);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await _db.Teachers.Include(t => t.Courses).ToListAsync();
        }

        public async Task<Teacher> GetById(int id)
        {
            var result = await _db.Teachers.Include(t => t.Courses).FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }

        public async Task Update(Teacher teacher)
        {
            _db.Teachers.Update(teacher);
            await _db.SaveChangesAsync();
        }
    }
}
