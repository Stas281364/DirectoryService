using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using DirectoryService.Contracts;

namespace DirectoryService.Domain.Location;

public record Address
{
    private string Country { get; init; }
    private string City { get; init; }
    private string Street { get; init; }
    private string HouseNumber { get; init; }
    private string ApartmentNumber { get; init; }
    

    private Address(string country, 
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