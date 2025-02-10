using Microsoft.EntityFrameworkCore;
using StuMan.Api.Models.Domain;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Repositories.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext  _db;
        public CourseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Course> AddAsync(Course course)
        {
            _db.Courses.Add(course);
            await _db.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course =  await _db.Courses.FindAsync(id);
            if (course == null) return false;

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var result = await _db.Courses.Include(c => c.Teacher).ToListAsync();
            return result;
        }

        public Task<Course> GetByIdAsync(int id)
        {
            var result = _db.Courses.Include(c => c.Teacher).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<Course> UpdateAsync(Course course)
        {
            _db.Courses.Update(course);
            await _db.SaveChangesAsync();
            return course;
        }
    }
}
