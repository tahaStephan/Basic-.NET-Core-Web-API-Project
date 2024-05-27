using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using yenideneme.Models;
using yenideneme.Services.Interfaces;
using ClosedXML.Excel;
using System.Net.Http;

namespace yenideneme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {

        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        #region GetClassDetailsAsync Çalışıyor
        [HttpGet("{classId}")]
        public async Task<ActionResult<Class>> GetClassDetailsAsync(int classId)
        {
            var classDetails = await _classService.GetClassDetailsAsync(classId);

            if (classDetails == null)
            {
                return NotFound();
            }

            var studentDetails = classDetails.StudentsAndTeachers
                .Select(cs => new
                {
                    cs.Student.StudentName,
                    cs.Student.StudentLastname
                })
                .ToList();

            var teacherDetails = classDetails.StudentsAndTeachers
                .Select(ct => new
                {
                    ct.Teacher.TeacherName,
                    ct.Teacher.TeacherLastName
                })
                .ToList();

            var result = new
            {
                classDetails.ClassId,
                Students = studentDetails,
                Teachers = teacherDetails
            };

            return Ok(result);
        }
        #endregion

        #region ExportToExcelTheClassDatas Çalışıyor
        [HttpGet("{classId}")]
        public async Task<IActionResult> ExportToExcelTheClassDatas(int classId)
        {
            var excelFilePath = await _classService.ExportToExcelTheClassDatas(classId);
            if (excelFilePath == null)
            {
                return NotFound();
            }
            return File(excelFilePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        #endregion

        #region GetClassDetailsThenPdf
        [HttpGet("{classId}")]
        public async Task<IActionResult> GetClassDetailsThenPdf(int classId)
        {
            var classInfo = await _classService.GetClassDetailsAsync(classId);

            if (classInfo == null)
            {
                return NotFound();
            }

            var pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ClassInfo.pdf");
            await _classService.GenerateAndSavePdfAsync(classInfo, pdfPath);

            return Ok($"PDF created and saved at: {pdfPath}");
        }
        #endregion

    }
}
