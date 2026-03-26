using DirectoryService.Domain.Location;
using DirectoryService.Domain.Position;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DirectoryService.Domain.Location.TimeZone;

namespace DirectoryService.Infrastructure.Postgres.Configuration;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Position");

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .IsRequired()
            .HasConversion(
                v => v.value,
                v => new PositionId(v)
            );

        builder.Property(p => p.PositionName)
            .HasConversion(
                v => v.ToString(), // в БД (string)
                v => new Domain.Position.Name(v) // из БД
            )
            .IsRequired()
            .HasMaxLength(PositionConstants.MaxLenght100)
            .HasColumnName("position_name");

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(PositionConstants.MaxLenghtDescription);

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasColumnName("is_active");
        
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");
    }
}