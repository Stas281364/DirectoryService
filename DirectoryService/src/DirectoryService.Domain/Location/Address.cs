using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DirectoryService.Domain.Location;

public record Country
{
    private string Name { get; init; }

    private Country(string name)
    {
        Name = name;
    }

    public static Result<Country> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            return Result.Failure<Country>($"The Country {value} is invalid: is empty or too long.");
        }

        return new Country(value);
    }
}

public record City
{
    private string Name { get; init; }

    private City(string name)
    {
        Name = name;
    }

    public static Result<City> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30)
        {
            return Result.Failure<City>($"The City {value} is invalid: is empty or too long. ");
        }

        return new City(value);
    }
}

public record Street
{
    private string Name { get; init; }

    private Street(string name)
    {
        Name = name;
    }

    public static Result<Street> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            return Result.Failure<Street>($"The Street {value} is invalid: is empty or too long. ");
        }

        return new Street(value);
    }
}

public record HouseNumber
{
    private string number { get; init; }

    private HouseNumber(string value)
    {
        number = value;
    }

    public static Result<HouseNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) 
            || value.Length > 50 
            && (!Regex.IsMatch(value, "^\\d$") || !Regex.IsMatch(value, "^\\d+\\/\\d+$")))
        {
            return Result.Failure<HouseNumber>($"The HouseNumber {value} is invalid");
        }
        return new HouseNumber(value);
    }
}

public record ApartmentNumber
{
    private string number { get; init; }

    private ApartmentNumber(string value)
    {
        number = value;
    }

    public static Result<ApartmentNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) 
            || value.Length > 50 
            && !Regex.IsMatch(value, "^\\d$"))
        {
            return Result.Failure<ApartmentNumber>($"The ApartmentNumber {value} is invalid");
        }
        return new ApartmentNumber(value);
    }
}

public record Address
{
    private Country Country { get; init; }
    private City City { get; init; }
    private Street Street { get; init; }
    private HouseNumber HouseNumber { get; init; }
    private ApartmentNumber ApartmentNumber { get; init; }
    

    private Address(Country country, 
        City city, 
        Street street,  
        HouseNumber houseNumber, 
        ApartmentNumber apartmentNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
    }

    public static Result<Address> Create(Country country, City city, Street street, HouseNumber houseNumber, ApartmentNumber apartmentNumber)
    {
        return new Address(country, city, street, houseNumber, apartmentNumber);
    }
}