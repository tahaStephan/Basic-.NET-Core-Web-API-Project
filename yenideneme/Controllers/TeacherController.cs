using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yenideneme.Models;
using yenideneme.Services;

namespace yenideneme.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        #region fields
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        #endregion

        #region GetTeachers Service Çalışıyor
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherService.GetTeachersAsync();
            return Ok(teachers);
        }
        #endregion

        #region GetTeachersByIdAsync Service Çalışıyor
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeachersByIdAsync(int id)
        {
            var teacher = await _teacherService.GetTeachersByIdAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
        #endregion

        #region PostCreateTeachersAsync Service Çalışıyor
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostCreateTeachersAsync([FromBody] Teacher newTeacher)
        {
            var createdTeacher = await _teacherService.PostCreateTeachersAsync(newTeacher);

            if (createdTeacher == null)
            {
                return BadRequest();
            }

            return newTeacher;
        }
        #endregion

        #region DeleteTeachersByIdAsync Service Çalışıyor

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacherByIdAsync(int id)
        {
            var deletedTeacher = await _teacherService.DeleteTeachersByIdAsync(id);

            if (deletedTeacher == null)
            {
                return NotFound();
            }

            return Ok(deletedTeacher);
        }

        #endregion

        #region UpdateTeachersByIdAsync Service Çalışıyor
        [HttpPut("{id}")]
        public async Task<ActionResult<Teacher>> UpdateTeachersByIdAsync(int id, [FromBody] Teacher updatedTeacher)
        {
            var updatedTeacherResult = await _teacherService.UpdateTeachersByIdAsync(id, updatedTeacher);

            if (updatedTeacherResult == null)
            {
                return NotFound();
            }

            return Ok(updatedTeacherResult);
        }
        #endregion
    }
}
