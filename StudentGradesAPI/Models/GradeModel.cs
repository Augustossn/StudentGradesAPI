using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentGradesAPI.Models
{
    public class GradeModel
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public double Score { get; set; }
        public int StudentId { get; set; }
    }
}