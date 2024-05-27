using Azure;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yenideneme.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.api;
using iTextSharp;
using DocumentFormat.OpenXml.InkML;


namespace yenideneme.Services.Interfaces
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _context;

        public ClassService(AppDbContext context)
        {
            _context = context;
        }

        #region GetClassDetailsAsync Çalışıyor
        public async Task<Class> GetClassDetailsAsync(int classId)
        {
            return await _context.Classes
                .Include(c => c.StudentsAndTeachers)
                    .ThenInclude(cs => cs.Student)
                .Include(c => c.StudentsAndTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(c => c.ClassId == classId);
        }
        #endregion



        #region ExportToExcelTheClassDatas Çalışıyor
        public async Task<string> ExportToExcelTheClassDatas(int classId)
        {
            var classDetails = await _context.Classes
                .Include(c => c.StudentsAndTeachers)
                    .ThenInclude(cs => cs.Student)
                .Include(c => c.StudentsAndTeachers)
                    .ThenInclude(ct => ct.Teacher)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (classDetails == null)
            {
                return null;
            }

            var studentDetails = classDetails.StudentsAndTeachers
                .Select(cs => new { cs.Student.StudentName, cs.Student.StudentLastname })
                .ToList();

            var teacherDetails = classDetails.StudentsAndTeachers
                .Select(ct => new { ct.Teacher.TeacherName, ct.Teacher.TeacherLastName })
                .ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Class Data");

                worksheet.Cell(1, 1).Value = "Class ID";
                worksheet.Cell(1, 2).Value = "Student Name";
                worksheet.Cell(1, 3).Value = "Student Last Name";
                worksheet.Cell(1, 4).Value = "Teacher Name";
                worksheet.Cell(1, 5).Value = "Teacher Last Name";

                for (int i = 0; i < studentDetails.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = classDetails.ClassId;
                    worksheet.Cell(i + 2, 2).Value = studentDetails[i].StudentName;
                    worksheet.Cell(i + 2, 3).Value = studentDetails[i].StudentLastname;
                    worksheet.Cell(i + 2, 4).Value = teacherDetails[i].TeacherName;
                    worksheet.Cell(i + 2, 5).Value = teacherDetails[i].TeacherLastName;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    var filePath = "C:\\Users\\taha.ulgi\\Desktop\\" + "Sınıf" + classId + "_Data.xlsx";
                    File.WriteAllBytes(filePath, content);

                    var fileUrl = $"{filePath}/{Path.GetFileName(filePath)}";

                    return filePath;
                }
            }
        }
        #endregion

        #region GenerateAndSavePdfAsync Çalışıyor
        public async Task GenerateAndSavePdfAsync(Class classInfo, string filePath)
        {
            using (var myIoStream = new FileStream(filePath, FileMode.Create))
            {
                using (var document = new Document())
                {
                    using (var writer = PdfWriter.GetInstance(document, myIoStream))
                    {
                        document.Open();

                        foreach (var student in classInfo.StudentsAndTeachers.Select(cs => cs.Student))
                        {
                            document.Add(new Paragraph($"Student Name: {student.StudentName} {student.StudentLastname}"));
                        }

                        foreach (var teacher in classInfo.StudentsAndTeachers.Select(ct => ct.Teacher))
                        {
                            document.Add(new Paragraph($"Teacher Name: {teacher.TeacherName} {teacher.TeacherLastName}"));
                        }

                        document.Close();
                    }
                }
            }
        }
        #endregion

    }
}