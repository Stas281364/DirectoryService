namespace DirectoryService.Contracts.Location;

public record CreateLocationDto(
    string Name,
    AddressDto Address,
    string TimeZone,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt);