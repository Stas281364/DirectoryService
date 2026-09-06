namespace DirectoryService.Contracts;

public record CreateDepartmentDto(
    string Name, //name
    string Identifier, //slug
    //string Path, 
    Guid? IdTopDepartment, //parentId
    List<Guid> LocationIds,
    short Depth, 
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);