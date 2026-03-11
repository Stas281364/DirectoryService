using DirectoryService.Contracts;
using DirectoryService.Domain.Department;

namespace DirectoryService.Application;

public class DepartmentService
{
    //[HttpPost]
    public Task<> Create(CreateDepartmentDto departmentDto, CancellationToken cancellationToken)
    {
        //Создание валидности(Проверка входных данных / проверка данных бд)
        
        //Создание сущности Department

        var Department = new Department(departmentDto);

        //Сохранение сущности Department в БД

        //Логгирование об успехе или отказной ситуации(ошибки сохранения)


    }

    
    //[HttpPut("{departmentId:guid}")]
    public Task<> UpdateById(UpdateDepartmentDto updateDepartmentDto, Guid departmentId, CancellationToken cancellationToken)
    {
        
    }
    
    //[HttpDelete("{departmentId:guid}")]
    public async Task<> DeleteById( Guid departmentId, CancellationToken cancellationToken)
    {
        
    }
}