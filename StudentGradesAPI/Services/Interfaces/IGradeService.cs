using StudentGradesAPI.Models;

namespace StudentGradesAPI.Services.Interfaces
{
    public interface IGradeService
    {
        Task<List<GradeModel>> GetAll();
        Task<GradeModel> GetById(int id);
        Task<GradeModel> Post(GradeModel grade);
        Task<double> CalculateAverage(int studentId);
        Task<GradeModel> Put(GradeModel grade, int id);
        Task<bool> Delete(int id);
    }
}
