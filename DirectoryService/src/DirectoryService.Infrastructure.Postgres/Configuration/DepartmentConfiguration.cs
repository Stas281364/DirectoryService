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
                v => v.Value,
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

        /*builder.Property(d => d.ParentDepartment)
            .IsRequired(false)
            .HasColumnName("parent_department_id")
            .HasConversion(d => d.value,
            d => new DepartmentId(d.value));*/
        
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

        /*
        builder.HasOne<Department>()
            .WithMany(d => d.ChildDepartments)
            .HasForeignKey(d => d.ParentDepartmentId)
            .OnDelete(DeleteBehavior.Restrict);*/
        
        builder.HasOne(d => d.ParentDepartment)
            .WithMany(d => d.ChildDepartments)
            .HasForeignKey(d => d.ParentDepartmentId)
            .IsRequired(false);
        
        builder.HasMany(d => d.DepartmentLocations)
            .WithOne()
            .HasForeignKey(d => d.DepartmentId);

        builder.HasMany(d => d.DepartmentPositions)
            .WithOne()
            .HasForeignKey(d => d.DepartmentId);
    }
}