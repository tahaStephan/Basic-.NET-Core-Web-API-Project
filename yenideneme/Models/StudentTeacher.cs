using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace yenideneme.Models
{
    [Table("student_teacher")]
    public class StudentTeacher
    {
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [ForeignKey("Class")]

        public int classId { get; set; }
        public Class Class { get; set; }
    }
}
