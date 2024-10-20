using Microsoft.EntityFrameworkCore;
using StudentGradesAPI.Models;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPI.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _dbContext;

        public GradeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GradeModel>> GetAll()
        {
            return await _dbContext.Grades.ToListAsync();
        }

        public async Task<GradeModel> GetById(int id)
        {
            return await _dbContext.Grades
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GradeModel> Post(GradeModel grade)
        {
            await _dbContext.Grades.AddAsync(grade);
            await _dbContext.SaveChangesAsync();

            return grade;
        }

        public async Task<GradeModel> Put(GradeModel grade, int id)
        {
            GradeModel gradeById = await GetById(id);
            if (gradeById == null)
            {
                throw new Exception($"Grade Id's {id} wasn't found in databank.");
            }

            gradeById.Subject = grade.Subject;
            gradeById.Score = grade.Score;
            gradeById.StudentId = grade.StudentId;

            _dbContext.Update(gradeById);
            await _dbContext.SaveChangesAsync();

            return grade;
        }

        public async Task<bool> Delete(int id)
        {
            GradeModel gradeById = await GetById(id);
            if (gradeById == null)
            {
                throw new Exception($"Grade Id's {id} wasn't found in databank.");
            }

            _dbContext.Grades.Remove(gradeById);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<double> CalculateAverage(int studentId)
        {
            var grades = _dbContext.Grades
                .Where(g => g.StudentId == studentId)
                .ToList();

            if (grades.Count == 0) return 0;

            return grades.Average(g => g.Score);
        }

    }
}