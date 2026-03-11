using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;
namespace DirectoryService.Presenter;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        
        return Ok("Create Department");
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        return Ok("Get Department");
    }
    
    [HttpGet("{departmentId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid departmentId, CancellationToken cancellationToken)
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