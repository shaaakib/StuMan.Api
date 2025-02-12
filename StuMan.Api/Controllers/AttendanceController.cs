using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StuMan.Api.Models.Domain;
using StuMan.Api.Models.DTO;
using StuMan.Api.Repositories.Interfaces;

namespace StuMan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        [HttpGet("GetAttendances")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAll()
        {
            var attendances = await _attendanceRepository.GetAllAttendancesAsync();

            var result = attendances.Select(a => new AttendanceDto
            {
                Id = a.Id,
                StudentId = a.StudentId,
                Date = a.Date,
                IsPresent = a.IsPresent
            });

            return Ok(result);
        }

        [HttpGet("GetAttendance/{id}")]
        public async Task<ActionResult<AttendanceDto>> GetById(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null) return NotFound();

            var result = new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                Date = attendance.Date,
                IsPresent = attendance.IsPresent
            };

            return Ok(result);
        }

        [HttpPost("CreateAttendance")]
        public async Task<ActionResult> Create([FromBody] AttendanceDto attendanceDto)
        {
            var attendance = new Attendance
            {
                StudentId = attendanceDto.StudentId,
                Date = attendanceDto.Date,
                IsPresent = attendanceDto.IsPresent
            };

            await _attendanceRepository.CreateAsync(attendance);
            return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendanceDto);
        }

        [HttpPut("UpdateAttendance/{id}")]
        public async Task<IActionResult> Update([FromBody] AttendanceDto attendanceDto, int id)
        {
            if (attendanceDto == null || id != attendanceDto.Id) return BadRequest();

            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null) return NotFound();

            existingAttendance.StudentId = attendanceDto.StudentId;
            existingAttendance.Date = attendanceDto.Date;
            existingAttendance.IsPresent = attendanceDto.IsPresent;

            await _attendanceRepository.UpdateAsync(existingAttendance);
            return Ok("Data Successfully Updated");
        }

        [HttpDelete("DeleteAttendance/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAttendance = await _attendanceRepository.GetByIdAsync(id);
            if (existingAttendance == null) return NotFound();

            await _attendanceRepository.DeleteAsync(id);
            return Ok("Data Successfully Deleted");
        }
    }
}
