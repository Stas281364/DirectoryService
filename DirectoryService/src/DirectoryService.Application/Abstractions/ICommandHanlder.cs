using CSharpFunctionalExtensions;

namespace DirectoryService.Application.Abstractions;

public interface ICommandHanlder<TResponse, in TCommand>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHanlder<in TCommand>
{
    //Task<UnitResult<> Handle(TCommand command, CancellationToken cancellationToken);
}