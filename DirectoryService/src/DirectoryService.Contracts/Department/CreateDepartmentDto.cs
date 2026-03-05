namespace DirectoryService.Contracts;

public record CreateDepartmentDto(
    string Name, 
    string Identifier, 
    string Path, 
    Guid? IdTopDepartment, 
    short Depth, 
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);