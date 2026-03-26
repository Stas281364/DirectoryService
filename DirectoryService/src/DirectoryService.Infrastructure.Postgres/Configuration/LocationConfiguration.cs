using DirectoryService.Domain.Department;
using DirectoryService.Domain.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Name = DirectoryService.Domain.Department.Name;
using TimeZone = DirectoryService.Domain.Location.TimeZone;
using System.Text.Json;

namespace DirectoryService.Infrastructure.Postgres.Configuration;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");

        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.Id)
            .IsRequired()
            .HasConversion(
                v => v.value,
                v => new LocationId(v)
            );

        builder.Property(l => l.LocationName)
            .HasConversion(
                v => v.ToString(), // в БД (string)
                v => new Domain.Location.Name(v) // из БД
            )
            .IsRequired()
            .HasMaxLength(LocationConstants.MaxLenght120)
            .HasColumnName("location_name");

        builder.OwnsOne(l => l.Address, address =>
        {
            address.ToJson("Addresses");
            
            address.Property(l => l.Country)
                .IsRequired()
                .HasColumnName("country")
                .HasMaxLength(30);

            address.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(30)
                .IsRequired();

            address.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(50)
                .IsRequired();

            address.Property(a => a.HouseNumber)
                .HasColumnName("house_number")
                .HasMaxLength(20)
                .IsRequired();

            address.Property(a => a.ApartmentNumber)
                .HasColumnName("apartment_number")
                .HasMaxLength(20);
        });

        builder.Property(l => l.TimeZone)
            .HasConversion(
                v => v.ToString(),
                v => new TimeZone(v)
            )
            .IsRequired()
            .HasColumnName("timezone");

        builder.Property(l => l.IsActive)
            .IsRequired()
            .HasColumnName("is_active");
        
        builder.Property(l => l.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
        
        builder.Property(l => l.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");
    }
}