using StudentGradesAPI.Models;

namespace StudentGradesAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetAll();
        Task<StudentModel> GetById(int id);
        Task<StudentModel> Post(StudentModel student);
        Task<StudentModel> Put(StudentModel student, int id);
        Task<bool> Delete(int id);
        Task Update(StudentModel student);
    }
}