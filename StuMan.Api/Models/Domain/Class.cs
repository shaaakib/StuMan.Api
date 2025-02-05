using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuMan.Api.Models.Domain
{
    public class Class
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string ClassName { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
