using System.Diagnostics;

namespace StudentGradesAPI.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Average { get; set; }
    }
}