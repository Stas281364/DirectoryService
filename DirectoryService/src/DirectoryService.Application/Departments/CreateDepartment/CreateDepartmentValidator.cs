using DirectoryService.Contracts;
using FluentValidation;

namespace DirectoryService.Application;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator()
    {
        RuleFor(departmentDto => departmentDto.Name).NotNull().WithMessage("Name must not null")
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name not more 150");

        RuleFor(departmentDto => departmentDto.Identifier).NotNull().WithMessage("Identifier must not null")
            .NotEmpty().WithMessage("Identifier is required.")
            .Matches("^[a-zA-Z0-9 ]+$").WithMessage("Identifier must not contain letters or digits");
        
        // RuleFor(departmentDto => departmentDto.Path).NotNull().WithMessage("Path must not null")
        //     .NotEmpty().WithMessage("Path is required.")
        //     .Matches("^[a-zA-Z0-9 ]+$").WithMessage("Path must not contain letters or digits");
        
        
    }
}