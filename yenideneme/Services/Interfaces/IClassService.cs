using yenideneme.Models;

namespace yenideneme.Services.Interfaces
{
    public interface IClassService
    {       
        Task<Class> GetClassDetailsAsync(int classId);

        Task<string> ExportToExcelTheClassDatas(int classesId);
        Task GenerateAndSavePdfAsync(Class classInfo, string filePath);
    }
}
