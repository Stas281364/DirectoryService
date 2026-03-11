namespace DirectoryService.Contracts;

public record GetPositionDto(string Search, 
    int Page,  
    int Limit );