using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("getAllStudents")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();

            return Ok(students);
        }

        [HttpGet("getStudent/{id:int}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudentAsync(id);
            if(student == null) return NotFound();

            return Ok(student);
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto createStudentDto)
        {
            var result = await _studentService.CreateStudentAsync(createStudentDto);

            return CreatedAtAction(nameof(GetStudent), new {result.Id}, result);
        }

        [HttpPut("UpdateStudent/{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            var success = await _studentService.UpdateStudentAsyn(id, updateStudentDto);
            if(!success) return NotFound("Data not found in the database");

            return Ok("Updated Successfull");
        }   

        [HttpDelete("DeleteStudent/{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var success = await _studentService.DeleteStudentAsynAsync(id);
            if (!success) return NotFound("Data not found in the database");

            return Ok("Deleted Successfull");
        }

    }
}
