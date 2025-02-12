using Microsoft.EntityFrameworkCore;
using StuMan.Api.Models.Domain;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Repositories.Implementation
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _db;
        public AttendanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Attendance attendance)
        {
            await _db.Attendances.AddAsync(attendance);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _db.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _db.Attendances.Remove(attendance);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Attendance>> GetAllAttendancesAsync()
        {
            return await _db.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            var result = await _db.Attendances.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId)
        {
            return await _db.Attendances.Where(a => a.StudentId == studentId).ToListAsync();    
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _db.Attendances.Update(attendance);
            await _db.SaveChangesAsync();
        }
    }
}
