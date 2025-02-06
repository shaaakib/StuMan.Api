using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;
using System.Reflection.Metadata;

namespace StuMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        [HttpGet("GetAllClasses")]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classes = await _classRepository.GetAllClassesAsync();
            var classDtos = classes.Select(c => new ClassDto
            {
                Id = c.Id,
                ClassName = c.ClassName,
                Students = c.Students.Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    DateOfBirth = s.DateOfBirth,
                }).ToList()
            }).ToList();

            return Ok(classDtos);
        }

        [HttpGet("GetClass/{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(int id)
        {
            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if (classEntity == null) return NotFound("data not found");

            var classDto = new ClassDto
            {
                Id = classEntity.Id,
                ClassName = classEntity.ClassName,
                Students = classEntity.Students.Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Phone = s.Phone,
                    DateOfBirth = s.DateOfBirth,
                }).ToList()
            };

            return Ok(classDto);
        }

        [HttpPost("CreateClass")]
        public async Task<ActionResult<ClassDto>> CreateClass([FromBody]ClassDto classDto)
        {
            var classEntity = new Class
            {
                ClassName = classDto.ClassName,
            };

            foreach (var studentDto in classDto.Students)
            {
                var studentEntity = new Student
                {
                    Name = studentDto.Name,
                    Email = studentDto.Email,
                    Phone = studentDto.Phone,
                    DateOfBirth = studentDto.DateOfBirth
                };

                classEntity.Students.Add(studentEntity);
            }

            await _classRepository.AddClassAsync(classEntity);

            classDto.Id = classEntity.Id;

            return CreatedAtAction(nameof(GetClass), new {id = classEntity.Id}, classDto);
        }

        [HttpPut("UpdatedClass/{id}")]
        public async Task<ActionResult<ClassDto>> UpdatedClass([FromBody] ClassDto classDto, int id)
        {
           var clas = await _classRepository.GetClassByIdAsync(id);
           if(clas == null) return NotFound($"Record with ID {id} was not found");

            clas.ClassName = classDto.ClassName;

            clas.Students.Clear();

            foreach(var item in classDto.Students)
            {
                clas.Students.Add(new Student
                {
                    Name = item.Name,
                    Email = item.Email,
                    Phone = item.Phone
                });
            }

            await _classRepository.UpdateClassAsync(clas);
            return Ok(new {Message = "Blog updated successfully!" });
        }

        [HttpDelete("DeleteClass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classEntity = await _classRepository.GetClassByIdAsync(id);
            if(classEntity == null) return NotFound($"Record with ID {id} was not found");

            await _classRepository.DeleteClassAsync(id);

            return Ok(new { Message = "Class Deleted successfully!" });
        }
    }
}
