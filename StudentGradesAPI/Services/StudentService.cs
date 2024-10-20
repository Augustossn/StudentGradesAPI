using Microsoft.EntityFrameworkCore;
using StudentGradesAPI.Models;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StudentModel>> GetAll()
        {
            return await _dbContext.Students
                .ToListAsync();
        }

        public async Task<StudentModel> GetById(int id)
        {
            return await _dbContext.Students
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<StudentModel> Post(StudentModel student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<StudentModel> Put(StudentModel student, int id)
        {
            StudentModel studentById = await GetById(id);
            if (studentById == null)
            {
                throw new Exception($"Student Id's {id} wasn't found in databank.");
            }

            studentById.Name = student.Name;
            studentById.Average = student.Average;

            _dbContext.Update(studentById);
            await _dbContext.SaveChangesAsync();

            return student;
        }

        public async Task<bool> Delete(int id)
        {
            StudentModel studentById = await GetById(id);
            if (studentById == null)
            {
                throw new Exception($"Student Id's {id} wasn't found in databank.");
            }

            _dbContext.Students.Remove(studentById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task Update(StudentModel student)
        {
            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}