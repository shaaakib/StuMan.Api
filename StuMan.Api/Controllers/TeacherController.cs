using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Implementation;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            var teacher = await _teacherRepository.GetAll();
            var teacherDtos = teacher.Select(x => new TeacherDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Courses = x.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    TeacherId = c.TeacherId,
                }).ToList()
            }).ToList();

            return Ok(teacherDtos);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetById(int id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher == null) return NotFound("Data Not Found");

            var teacherDto = new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                Phone = teacher.Phone,
                Courses = teacher.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    CourseName = c.CourseName,
                    CourseCode = c.CourseCode,
                    TeacherId = c.TeacherId,
                }).ToList()
            };

            return Ok(teacherDto);
        }

        [HttpPost("CreateTeacher")]
        public async Task<ActionResult<TeacherDto>> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            var teacher = new Teacher
            {
                Name = teacherDto.Name,
                Email = teacherDto.Email,
                Phone = teacherDto.Phone,
            };

            foreach (var item in teacherDto.Courses)
            {
                var course = new Course
                {
                    CourseName = item.CourseName,
                    CourseCode = item.CourseCode,
                    TeacherId = item.TeacherId,
                };

                teacher.Courses.Add(course);
            }

            await _teacherRepository.Create(teacher);

            teacherDto.Id = teacher.Id;

            return CreatedAtAction(nameof(GetById), new {id = teacher.Id}, teacherDto);
        }

        [HttpPut("UpdateTeacher/{id}")]
        public async Task<ActionResult> UpdateTeacher([FromBody]TeacherDto teacherDto, int id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher == null) return NotFound($"Record with ID {id} was not found");

            teacher.Name = teacherDto.Name;
            teacher.Email = teacherDto.Email;
            teacher.Phone = teacherDto.Phone;

            teacher.Courses.Clear();
            foreach(var item in teacherDto.Courses)
            {
                teacher.Courses.Add(new Course
                {
                    CourseName = item.CourseName,
                    CourseCode = item.CourseCode,
                    TeacherId = item.TeacherId,
                });
            }

            await _teacherRepository.Update(teacher);
            return Ok(new { Message = "Teacher updated successfully!" });
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher == null) return NotFound($"Record with ID {id} was not found");

            await _teacherRepository.DeleteById(id);
            return Ok(new { Message = "Teacher Deleted Succesfully!" });
        }
    }
}
