using CSharpFunctionalExtensions;
using DirectoryService.Domain.Departments;

namespace DirectoryService.Application.Abstractions;

public interface ICommands;

public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommands
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    
}

public interface ICommandHanlder<in TCommand>
{
    //Task<UnitResult<> Handle(TCommand command, CancellationToken cancellationToken);
}