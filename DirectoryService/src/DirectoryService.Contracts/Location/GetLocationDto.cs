namespace DirectoryService.Contracts;

public record GetLocationDto(string Search, 
    int Page,  
    int Limit );