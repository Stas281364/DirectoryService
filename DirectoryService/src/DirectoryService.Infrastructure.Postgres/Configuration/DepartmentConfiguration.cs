using DirectoryService.Domain.Department;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Path = System.IO.Path;

namespace DirectoryService.Infrastructure.Postgres.Configuration;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .IsRequired()
            .HasConversion(
                v => v.value,
                v => new DepartmentId(v)
            );

        builder.Property(d => d.DepartmentName)
            .HasConversion(
                v => v.Value, // в БД (string)
                v => new Name(v) // из БД
            )
            .IsRequired()
            .HasMaxLength(DepartmentConstants.MaxLenght150)
            .HasColumnName("department_name");

        builder.Property(d => d.Identifier)
            .HasConversion(
                v => v.ToString(),
                v => new Identifier(v)
            )
            .IsRequired()
            .HasMaxLength(DepartmentConstants.MaxLenght150)
            .HasColumnName("identifier");

        builder.Property(d => d.ParentDepartmentId)
            .IsRequired(false)
            .HasColumnName("parent_department_id");
        
        builder.Property(d => d.Path)
            .HasConversion(
                v => v.ToString(),
                v => new Domain.Department.Path(v)
            )
            .IsRequired()
            .HasColumnName("path");


        builder.Property(d => d.Depth)
            .IsRequired()
            .HasColumnName("depth");

        builder.Property(d => d.IsActive)
            .IsRequired()
            .HasColumnName("is_active");

        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(d => d.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");

        builder.HasMany(d => d.ChildDepartments)
            .WithOne()
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(d => d.ParentDepartmentId);

        builder.HasMany(d => d.DepartmentLocations)
            .WithOne()
            .HasForeignKey(d => d.DepartmentId);

        builder.HasMany(d => d.DepartmentPositions)
            .WithOne()
            .HasForeignKey(d => d.DepartmentId);
    }
}