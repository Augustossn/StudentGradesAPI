using StudentGradesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using StudentGradesAPI.Models;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly IStudentService _studentService;

        public GradeController(IGradeService gradeService, IStudentService studentService)
        {
            _gradeService = gradeService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GradeModel>>> GetAll()
        {
            List<GradeModel> grade = await _gradeService.GetAll();
            return Ok(grade);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradeModel>> GetById(int id)
        {
            GradeModel grade = await _gradeService.GetById(id);
            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }
        [HttpPost]
        public async Task<ActionResult<GradeModel>> Post([FromBody] GradeModel grade)
        {
            var student = await _studentService.GetById(grade.StudentId);

            if (student == null)
            {
                return NotFound($"Student's Id {student.Id} wasn't found in the databank.");
            }

            grade.StudentId = student.Id;

            GradeModel gradeModel = await _gradeService.Post(grade);

            double average = await _gradeService.CalculateAverage(grade.StudentId);

            student.Average = average;
            await _studentService.Update(student);

            return Ok(gradeModel);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GradeModel>> Put(GradeModel grade, int id)
        {
            grade.Id = id;
            GradeModel gradeModel = await _gradeService.Put(grade, id);
            return Ok(gradeModel);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var user = await _gradeService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            bool deleted = await _gradeService.Delete(id);

            if (deleted)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}