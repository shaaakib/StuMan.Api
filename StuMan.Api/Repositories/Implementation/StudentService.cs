using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Repositories.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _db;
        public StudentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _db.Students.ToListAsync();

            var result = students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Phone = s.Phone,
                DateOfBirth = s.DateOfBirth,
            }).ToList();

            return result;
        }

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            var student = await _db.Students.SingleOrDefaultAsync(s => s.Id == id);

            if (student == null) return null;

            var result = new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Phone = student.Phone,
                DateOfBirth = student.DateOfBirth,
            };

            return result;
        }

        public async Task<CreateStudentDto> CreateStudentAsync(CreateStudentDto createStudentDto)
        {
            var student = new Student() 
            {
                Name = createStudentDto.Name,
                Email = createStudentDto.Email,
                Phone = createStudentDto.Phone,
                DateOfBirth= createStudentDto.DateOfBirth,
                ClassId = createStudentDto.ClassId,
            };

            _db.Students.Add(student);
            await _db.SaveChangesAsync();

            var result = new CreateStudentDto
            {
                Name = createStudentDto.Name,
                Email = createStudentDto.Email,
                Phone = createStudentDto.Phone,
                DateOfBirth = createStudentDto.DateOfBirth,
                ClassId= createStudentDto.ClassId,
            };

            return result;
        }

        public async Task<bool> UpdateStudentAsyn(int id, UpdateStudentDto updateStudentDto)
        {
            var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return false;

            student.Name = updateStudentDto.Name;
            student.Email = updateStudentDto.Email;
            student.Phone = updateStudentDto.Phone;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudentAsynAsync(int id)
        {
            var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null) return false;

            _db.Students.Remove(student);
            await _db.SaveChangesAsync(true);

            return true;
        }
    }
}
