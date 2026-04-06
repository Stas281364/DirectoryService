using DirectoryService.Contracts.Location;
using FluentValidation;
using DirectoryService.Domain.Locations;
namespace DirectoryService.Application.Locations;

public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(location => location.Name).NotNull().WithMessage("Name must not null")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(LocationConstants.MaxLenght120).WithMessage("Заголовок слишком длинный");

        RuleFor(location => location.Address.Country).NotNull().WithMessage("Address must not null")
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(30).WithMessage("Country more 30");
        
        RuleFor(location => location.Address.City).NotNull().WithMessage("City must not null")
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(30).WithMessage("City more 30");
        
        RuleFor(location => location.Address.Street).NotNull().WithMessage("Street must not null")
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(50).WithMessage("Street more 50");
        
        RuleFor(location => location.Address.HouseNumber).NotNull().WithMessage("HouseNumber must not null")
            .NotEmpty().WithMessage("HouseNumber is required.")
            .MaximumLength(20).WithMessage("HouseNumber more 20");
        
        RuleFor(location => location.Address.ApartmentNumber).NotNull().WithMessage("ApartmentNumber must not null")
            .NotEmpty().WithMessage("ApartmentNumber is required.")
            .MaximumLength(20).WithMessage("ApartmentNumber more 20");

    }
}