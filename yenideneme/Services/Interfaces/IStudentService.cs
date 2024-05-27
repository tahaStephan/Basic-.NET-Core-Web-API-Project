using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using yenideneme;
using yenideneme.Models;

public interface IStudentService
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentsByIdAsync(int id);
    Task<Student> PostCreateStudentsAsync([FromBody] Student newStudent);
    Task<Student> DeleteStudentsByIdAsync(int id);
    Task<Student> UpdateStudentsByIdAsync(int id, Student updatedStudent);
}   
