using StudentGradesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentGradesAPI.Controllers;
using StudentGradesAPI.Services.Interfaces;

namespace StudentGradesAPITests
{
    public class GradesTests
    {
        private readonly Mock<IGradeService> _mockGradeService;
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly GradeController _gradeController;

        public GradesTests()
        {
            _mockGradeService = new Mock<IGradeService>();
            _mockStudentService = new Mock<IStudentService>();
            _gradeController = new GradeController(_mockGradeService.Object, _mockStudentService.Object);
        }

        [Fact]
        public async Task GetAllGrade_ReturnsOkResult_WithListOfGrades()
        {
            //Arrange
            var gradeList = new List<GradeModel>
            {
                new GradeModel { Id = 1, Subject = "Math", Score = 8, StudentId = 1},
                new GradeModel { Id = 2, Subject = "English", Score = 6, StudentId = 1}
            };

            _mockGradeService.Setup(repo => repo.GetAll()).ReturnsAsync(gradeList);

            //Act
            var result = await _gradeController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GradeModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetGradeById_ReturnOkResult_WithGrade()
        {
            //Arrange
            var grade = new GradeModel { Id = 1, Subject = "Math", Score = 8, StudentId = 1 };
            _mockGradeService.Setup(repo => repo.GetById(grade.Id)).ReturnsAsync(grade);

            //Act
            var result = await _gradeController.GetById(grade.Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GradeModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Math", returnValue.Subject);

        }

        [Fact]
        public async Task GetGradeById_ReturnNotFound_WhenGradeDoesNotExist()
        {
            //Arrange
            _mockGradeService.Setup(repo => repo.GetById(1)).ReturnsAsync((GradeModel)null);

            //Act
            var result = await _gradeController.GetById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task PostGrade_ReturnOkResult_WithCreatedGrade()
        {
            //Arrange
            var newGrade = new GradeModel { Id = 1, Subject = "Math", Score = 8, StudentId = 1 };
            var newStudent = new StudentModel { Id = 1, Name = "Augusto", Average = 8 };
            _mockGradeService.Setup(repo => repo.Post(newGrade)).ReturnsAsync(newGrade);
            _mockStudentService.Setup(repo => repo.GetById(newStudent.Id)).ReturnsAsync(newStudent);

            //Act
            var result = await _gradeController.Post(newGrade);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GradeModel>(okResult.Value);
            Assert.Equal("Math", returnValue.Subject);

        }

        [Fact]
        public async Task PutGrade_ReturnOkResult_WithUptadedGrade()
        {
            //Arrange
            var uptadedGrade = new GradeModel { Id = 1, Subject = "Math", Score = 6, StudentId = 1 };
            _mockGradeService.Setup(repo => repo.Put(uptadedGrade, 1)).ReturnsAsync(uptadedGrade);

            //Act
            var result = await _gradeController.Put(uptadedGrade, 1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GradeModel>(okResult.Value);
            Assert.Equal(6, returnValue.Score);

        }

        [Fact]
        public async Task DeleteGrade_ReturnOKResult_WhenGradeDeleted()
        {
            //Arrange
            var gradeId = 1;

            _mockGradeService.Setup(repo => repo.GetById(gradeId)).ReturnsAsync(new GradeModel { Id = gradeId });
            _mockGradeService.Setup(repo => repo.Delete(gradeId)).ReturnsAsync(true);

            //Act
            var result = await _gradeController.Delete(gradeId);

            //Assert
            Assert.IsType<OkResult>(result.Result);

        }

        [Fact]
        public async Task DeleteGrade_ReturnNotFound_WhenGradeNotFound()
        {
            //Arrange
            _mockGradeService.Setup(repo => repo.Delete(1)).ReturnsAsync(false);

            //Act
            var result = await _gradeController.Delete(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }
    }
}               