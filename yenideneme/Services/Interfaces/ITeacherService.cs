using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using yenideneme;
using yenideneme.Models;

public interface ITeacherService
{
    Task<IEnumerable<Teacher>> GetTeachersAsync();
    Task<Teacher> GetTeachersByIdAsync(int id);
    Task<Teacher> PostCreateTeachersAsync([FromBody] Teacher newTeacher); 
    Task<Teacher> DeleteTeachersByIdAsync(int id);
    Task<Teacher> UpdateTeachersByIdAsync(int id, Teacher updateTeacher);
    
}
