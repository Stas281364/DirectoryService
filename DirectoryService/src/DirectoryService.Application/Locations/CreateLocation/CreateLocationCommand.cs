using DirectoryService.Application.Abstractions;
using DirectoryService.Contracts.Location;

//using ICommand = DirectoryService.Application.Abstractions.ICommand;

namespace DirectoryService.Application.Locations.CreateLocation;

//(команда, токен)
public record CreateLocationCommand(CreateLocationDto locationDto, CancellationToken cancellationToken) : ICommands;