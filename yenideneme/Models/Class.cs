using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yenideneme.Models
{
    [Table("class")]
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public List<StudentTeacher> StudentsAndTeachers { get; set; }
    }
}
