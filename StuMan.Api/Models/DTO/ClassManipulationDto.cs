using System.ComponentModel.DataAnnotations;

namespace StuMan.Api.Models.DTO
{
    public class ClassManipulationDto
    {
        [Required, MaxLength(50)]
        public string ClassName { get; set; }
    }
}
