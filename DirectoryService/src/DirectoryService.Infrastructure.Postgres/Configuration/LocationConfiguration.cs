using DirectoryService.Domain.Departments;
using DirectoryService.Domain.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
                v => v.Value, // в БД (string)
                v => new Domain.Locations.Name(v) // из БД
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
                v => v.Value,
                v => new Domain.Locations.TimeZone(v)
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