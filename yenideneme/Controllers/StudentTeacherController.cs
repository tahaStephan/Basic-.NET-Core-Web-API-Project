using Microsoft.AspNetCore.Mvc;
using yenideneme.Services.Interfaces;

namespace yenideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentTeacherController : ControllerBase
    {
        private readonly IStudentTeacherService _studentTeacherService;

        public StudentTeacherController(IStudentTeacherService studentTeacherService)
        {
            _studentTeacherService = studentTeacherService;
        }

        #region GetTeachersForStudentWithMultipleTeacherIds Çalışıyor
        [HttpGet("{studentId}")]
        public IActionResult GetTeachersForStudentWithMultipleTeacherIds(int studentId)
        {
            var teachers = _studentTeacherService.GetTeachersForStudentWithMultipleTeacherIds(studentId);

            if (teachers == null || teachers.Count == 0)
            {
                return NotFound($"No teachers found for student with ID: {studentId}");
            }

            return Ok(teachers);
        }
        #endregion
    }
}
