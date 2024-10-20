using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentGradesAPI.Models;

namespace StudentGradesAPI.Data.Map
{
    public class GradesMap : IEntityTypeConfiguration<GradeModel>
    {
        public void Configure(EntityTypeBuilder<GradeModel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Subject).IsRequired();
            builder.Property(e => e.Score).IsRequired();

            builder.Property(e => e.StudentId).IsRequired();
        }
    }
}