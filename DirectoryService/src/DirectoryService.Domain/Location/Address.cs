using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Contracts;

namespace DirectoryService.Domain.Location;

public record Address
{
    public string Country { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
    public string HouseNumber { get; init; }
    public string ApartmentNumber { get; init; }
    

    public Address(string country, 
        string city, 
        string street,  
        string houseNumber, 
        string apartmentNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public static Result<Address> Create(AddressDto addressDto)
    {
        return new Address(addressDto.Country, 
            addressDto.City,
            addressDto.Street,
            addressDto.HouseNumber,
            addressDto.ApartmentNumber);
    }
}