using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yenideneme.Models;
using yenideneme.Services;

namespace yenideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        #region fields
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        #endregion

        #region GetStudents Service Çalışıyor

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            if (!students.Any())
            {
                return NotFound("No students found.");
            }

            return Ok(students);
        }

        #endregion

        #region GetStudentsByIdAsync Service Çalışıyor
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentsByIdAsync(int id)
        {
            var student = await _studentService.GetStudentsByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        #endregion
        
        #region PostCreateStudentsAsync Service Çalışıyor
        [HttpPost]
        public async Task<ActionResult<Student>> PostCreateStudentsAsync([FromBody] Student newStudent)
        {
            var createdStudent = await _studentService.PostCreateStudentsAsync(newStudent);

            if (createdStudent == null)
            {
                return BadRequest();
            }

            return newStudent;
        }

        #endregion

        #region DeleteStudentsByIdAsync Service Çalışıyor

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudentByIdAsync(int id)
        {
            var deletedStudent = await _studentService.DeleteStudentsByIdAsync(id);

            if (deletedStudent == null)
            {
                return NotFound();
            }

            return Ok(deletedStudent);
        }

        #endregion

        #region UpdateStudentsByIdAsync Service Çalışıyor
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateStudentsByIdAsync(int id, [FromBody] Student updatedStudent)
        {
            var updatedStudentResult = await _studentService.UpdateStudentsByIdAsync(id, updatedStudent);

            if (updatedStudentResult == null)
            {
                return NotFound();
            }

            return Ok(updatedStudentResult);
        }
        #endregion
    }
}
