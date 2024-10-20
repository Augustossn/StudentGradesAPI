using Microsoft.EntityFrameworkCore;
using StudentGradesAPI.Data.Map;
using StudentGradesAPI.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<StudentModel> Students { get; set; }
    public DbSet<GradeModel> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentsMap());
        modelBuilder.ApplyConfiguration(new GradesMap());
        base.OnModelCreating(modelBuilder);
    }
}