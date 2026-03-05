namespace DirectoryService.Contracts;

public record GetDepartmentDto(string Search, 
    int Page,  
    int Limit );