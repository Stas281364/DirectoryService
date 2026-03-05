namespace DirectoryService.Contracts;

public record CreatePositionDto(
    string NamePosition, 
    string? Description, 
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);