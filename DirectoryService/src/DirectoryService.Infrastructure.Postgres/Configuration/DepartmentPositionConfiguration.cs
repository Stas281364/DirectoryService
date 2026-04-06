using DirectoryService.Domain.Departments;
using DirectoryService.Domain.DepartmentPositions;
using DirectoryService.Domain.Positions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configuration;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("departments_position");

        builder.HasKey(d => d.Id);

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired()
            .HasConversion(
                value => value.Value,
                value => new DepartmentId(value)
            );
        
        builder.Property(x => x.PositionId)
            .HasColumnName("position_id")
            .IsRequired()
            .HasConversion(
                value => value.value,
                value => new PositionId(value)
            );
    }
}