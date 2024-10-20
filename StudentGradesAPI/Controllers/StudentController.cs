using StudentGradesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentGradesAPI.Models;
using StudentGradesAPI.Services;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentModel>>> GetAll()
        {
            List<StudentModel> student = await _studentService.GetAll();
            return Ok(student);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentModel>> GetById(int id)
        {
            StudentModel student = await _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentModel>> Post([FromBody] StudentModel student)
        {
            StudentModel studentModel = await _studentService.Post(student);
            return Ok(studentModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentModel>> Put(StudentModel student, int id)
        {
            student.Id = id;
            StudentModel studentModel = await _studentService.Put(student, id);
            return Ok(studentModel);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var user = await _studentService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            bool deleted = await _studentService.Delete(id);

            if (deleted)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}