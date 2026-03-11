namespace DirectoryService.Contracts;

public record CreateLocationDto(
    string Name, 
    string Adress, 
    string TimeZone, 
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);