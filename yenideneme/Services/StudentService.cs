using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yenideneme.Models;

namespace yenideneme.Services
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly AppDbContext _context;

        #endregion
        #region Ctor
        public StudentService(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetStudentsAsync Çalışıyor
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        #endregion

        #region GetStudentsByIdAsync Çalışıyor
        public async Task<Student> GetStudentsByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
        }
        #endregion

        #region PostCreateStudentsAsync Çalışıyor
        public async Task<Student> PostCreateStudentsAsync([FromBody] Student newStudent)
        {
            if (newStudent == null)
            {
                return null;
            }

            await _context.Students.AddAsync(newStudent);
            _context.SaveChanges();

            return newStudent;
        }
        #endregion

        #region DeleteStudentsByIdAsync Çalışıyor
        public async Task<Student> DeleteStudentsByIdAsync(int id)
        {
            var studentDelete = await _context.Students.FindAsync(id);

            if (studentDelete != null)
            {
                _context.Students.Remove(studentDelete);
                await _context.SaveChangesAsync();
            }

            return studentDelete;
        }
        #endregion

        #region UpdateStudentsByIdAsync Çalışıyor
        public async Task<Student> UpdateStudentsByIdAsync(int id, Student updatedStudent)
        {
            var existingStudent = await _context.Students.FindAsync(id);

            if (existingStudent != null)
            {
                existingStudent.StudentName = updatedStudent.StudentName;
                existingStudent.StudentLastname = updatedStudent.StudentLastname;

                await _context.SaveChangesAsync();
            }

            return existingStudent;
        }
        #endregion
    }
}
