using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace yenideneme.Models
{
    [Table("student")]
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentLastname { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public List<StudentTeacher> StudentTeachers { get; set; }
    }
}
