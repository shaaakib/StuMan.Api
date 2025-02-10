using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository  _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpPost("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            var courseDtos = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                CourseName = c.CourseName,
                CourseCode = c.CourseCode,
                TeacherId = c.TeacherId,
            }).ToList();

            return Ok(courseDtos);
        }

        [HttpGet("GetCourseById{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            var courseDto = new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                TeacherId = course.TeacherId,
            };

            return Ok(courseDto);
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody]CourseDto courseDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var course = new Course
            {
                CourseName = courseDto.CourseName,
                CourseCode = courseDto.CourseCode,
                TeacherId = courseDto.TeacherId,
            };

            var createdCourse = await _courseRepository.AddAsync(course);

            var cousDto = new CourseDto
            {
                Id = createdCourse.Id,
                CourseName = createdCourse.CourseName,
                CourseCode = createdCourse.CourseCode,
                TeacherId = createdCourse.TeacherId,
            };

            return CreatedAtAction(nameof(GetCourseById), new {id =  course.Id}, cousDto);
        }

        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseDto courseDto, int id)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if(existingCourse == null) return NotFound();

            existingCourse.CourseName = courseDto.CourseName;
            existingCourse.CourseCode = courseDto.CourseCode;
            existingCourse.TeacherId = courseDto.TeacherId;

            await _courseRepository.UpdateAsync(existingCourse);

            return Ok("Updated data successfully");
        }

        [HttpDelete("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var isDelete = await _courseRepository.DeleteAsync(id);
            if(!isDelete) return NotFound();

            return Ok("Deleted data successfully");
        }
    }
}
