namespace DirectoryService.Contracts;

public record UpdatePositionDto(
    string NamePosition, 
    string? Description,  
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);