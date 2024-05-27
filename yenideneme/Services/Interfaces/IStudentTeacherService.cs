using yenideneme.Models;

namespace yenideneme.Services.Interfaces
{
    public interface IStudentTeacherService
    {
        List<Teacher> GetTeachersForStudentWithMultipleTeacherIds(int studentId);
    }
}
