using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using yenideneme.Models;
using yenideneme.Services.Interfaces;

namespace yenideneme.Services
{
    public class StudentTeacherService  :IStudentTeacherService
    {
        private readonly AppDbContext _context;

        public StudentTeacherService(AppDbContext context)
        {
            _context = context;
        }

        #region GetTeachersForStudentWithMultipleTeacherIds Çalışıyor

        public List<Teacher> GetTeachersForStudentWithMultipleTeacherIds(int studentId)
        {
            var teacherIdsList = _context.StudentTeachers
                .Where(st => st.StudentId == studentId)
            .Select(st => st.TeacherId)
                .ToList();

            var teachers = _context.Teachers
                .Where(t => teacherIdsList.Contains(t.TeacherId))
                .ToList();

            return teachers;
        }

        #endregion



        #region



        #endregion
 
    }
}
