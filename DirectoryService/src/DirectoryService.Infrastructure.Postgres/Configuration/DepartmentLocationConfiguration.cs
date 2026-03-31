using DirectoryService.Domain.Department;
using DirectoryService.Domain.DepartmentLocation;
using DirectoryService.Domain.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configuration;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("departments_location");

        builder.HasKey(d => d.Id);

        builder.Property(x => x.DepartmentId)
            .HasColumnName("department_id")
            .IsRequired()
            .HasConversion(
                value => value.Value,
                value => new DepartmentId(value)
            );
        
        builder.Property(x => x.LocationId)
            .HasColumnName("location_id")
            .IsRequired()
            .HasConversion(
                value => value.value,
                value => new LocationId(value)
            );
    }
}