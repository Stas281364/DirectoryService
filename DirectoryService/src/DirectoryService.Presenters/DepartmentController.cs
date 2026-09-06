using DirectoryService.Application;
using DirectoryService.Application.Abstractions;
using DirectoryService.Application.GetDepartment;
using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;
namespace DirectoryService.Presenter;

[ApiController]
[Route("/api/departments")]
public class DepartmentController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto request, 
        [FromServices] ICommandHandler<Guid, CreateDepartmentCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new CreateDepartmentCommand(request, cancellationToken);
        var department = await handler.Handle(command, cancellationToken);
        return Ok("Create Department");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetDepartmentDto request, 
        [FromServices] ICommandHandler<Guid, GetDepartmentCommand> handler, 
        CancellationToken cancellationToken)
    {
        var command = new GetDepartmentCommand(request, cancellationToken);
        var department = await handler.Handle(command, cancellationToken); 
        return Ok("Get Department");
    }
    
    [HttpGet("{departmentId:guid}")]
    public async Task<ActionResult> GetById([FromRoute] Guid departmentId, CancellationToken cancellationToken)
    {
        return Ok($"Get DepartmentById {departmentId}");
    }
    
    [HttpPut("{departmentId:guid}")]
    public async Task<IActionResult> UpdateById([FromBody]UpdateDepartmentDto updateDepartmentDto,[FromRoute] Guid departmentId, CancellationToken cancellationToken)
    {
        return Ok($"Put UpdateDepartmentById {departmentId}");
    }
    
    [HttpDelete("{departmentId:guid}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid departmentId, CancellationToken cancellationToken)
    {
        return Ok($"Delete DeleteDepartmentById {departmentId}");
    }
}