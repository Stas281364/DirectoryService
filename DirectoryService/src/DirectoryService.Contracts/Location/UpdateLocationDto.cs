namespace DirectoryService.Contracts;

public record UpdateLocationDto(
    string Name, 
    string Adress, 
    string TimeZone,  
    bool IsActive, 
    DateTime CreatedAt,  
    DateTime UpdatedAt);