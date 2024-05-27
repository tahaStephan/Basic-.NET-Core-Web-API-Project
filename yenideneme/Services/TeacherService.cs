using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yenideneme.Models;

namespace yenideneme.Services
{
    public class TeacherService : ITeacherService
    {
        #region Fields
        private readonly AppDbContext _context;

        #endregion
        #region Ctor
        public TeacherService(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetTeachersAsync Çalışıyor
        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await _context.Teachers.ToListAsync();
        }
        #endregion

        #region GetTeachersByIdAsync Çalışıyor
        public async Task<Teacher>  GetTeachersByIdAsync(int id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(s => s.TeacherId == id);
        }
        #endregion

        #region PostCreateTeachersAsync
        public async Task<Teacher> PostCreateTeachersAsync([FromBody] Teacher newTeacher)
        {
            if (newTeacher == null)
            {
                return null;
            }

            await _context.Teachers.AddAsync(newTeacher);
            _context.SaveChanges();

            return newTeacher;
        }
        #endregion

        #region DeleteTeachersByIdAsync 
        public async Task<Teacher> DeleteTeachersByIdAsync(int id)
        {
            var teacherDelete = await _context.Teachers.FindAsync(id);

            if (teacherDelete != null)
            {
                _context.Teachers.Remove(teacherDelete);
                await _context.SaveChangesAsync();
            }

            return teacherDelete;
        }
        #endregion

        #region UpdateTeachersByIdAsync 
        public async Task<Teacher> UpdateTeachersByIdAsync(int id, Teacher updatedTeacher)
        {
            var existingTeacher = await _context.Teachers.FindAsync(id);

            if (existingTeacher != null)
            {
                existingTeacher.TeacherName = updatedTeacher.TeacherName;
                existingTeacher.TeacherLastName = updatedTeacher.TeacherLastName;

                await _context.SaveChangesAsync();
            }

            return existingTeacher;
        }
        #endregion
    }
}
