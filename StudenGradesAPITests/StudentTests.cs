using StudentGradesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentGradesAPI.Controllers;
using StudentGradesAPI.Models;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPITests
{
    public class StudentsTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly StudentController _studentController;

        public StudentsTests()
        {
            _mockStudentService = new Mock<IStudentService>();
            _studentController = new StudentController(_mockStudentService.Object);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            //Arrange
            var studentList = new List<StudentModel>
            {
                new StudentModel { Id = 1, Name = "Augusto", Average = 8},
                new StudentModel { Id = 2, Name = "Otsugua", Average = 6}
            };

            _mockStudentService.Setup(repo => repo.GetAll()).ReturnsAsync(studentList);

            //Act
            var result = await _studentController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<StudentModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetStudentById_ReturnOkResult_WithStudent()
        {
            //Arrange
            var student = new StudentModel { Id = 1, Name = "Augusto", Average = 8 };
            _mockStudentService.Setup(repo => repo.GetById(student.Id)).ReturnsAsync(student);

            //Act
            var result = await _studentController.GetById(student.Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<StudentModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Augusto", returnValue.Name);

        }

        [Fact]
        public async Task GetStudentById_ReturnNotFound_WhenStudentDoesNotExist()
        {
            //Arrange
            _mockStudentService.Setup(repo => repo.GetById(1)).ReturnsAsync((StudentModel)null);

            //Act
            var result = await _studentController.GetById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task PostStudent_ReturnOkResult_WithCreatedStudent()
        {
            //Arrange
            var newStudent = new StudentModel { Id = 1, Name = "Augusto", Average = 8 };
            _mockStudentService.Setup(repo => repo.Post(newStudent)).ReturnsAsync(newStudent);

            //Act
            var result = await _studentController.Post(newStudent);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<StudentModel>(okResult.Value);
            Assert.Equal("Augusto", returnValue.Name);

        }

        [Fact]
        public async Task PutStudent_ReturnOkResult_WithUptadedStudent()
        {
            //Arrange
            var uptadedStudent = new StudentModel { Id = 1, Name = "Augusto", Average = 4 };
            _mockStudentService.Setup(repo => repo.Put(uptadedStudent, 1)).ReturnsAsync(uptadedStudent);

            //Act
            var result = await _studentController.Put(uptadedStudent, 1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<StudentModel>(okResult.Value);
            Assert.Equal(4, returnValue.Average);

        }

        [Fact]
        public async Task DeleteStudent_ReturnOKResult_WhenStudentDeleted()
        {
            //Arrange
            var studentId = 1;

            _mockStudentService.Setup(repo => repo.GetById(studentId)).ReturnsAsync(new StudentModel { Id = studentId });
            _mockStudentService.Setup(repo => repo.Delete(studentId)).ReturnsAsync(true);

            //Act
            var result = await _studentController.Delete(studentId);

            //Assert
            Assert.IsType<OkResult>(result.Result);

        }

        [Fact]
        public async Task DeleteStudent_ReturnNotFound_WhenStudentNotFound()
        {
            //Arrange
            _mockStudentService.Setup(repo => repo.Delete(1)).ReturnsAsync(false);

            //Act
            var result = await _studentController.Delete(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}