using CSharpFunctionalExtensions;
using DirectoryService.Contracts;

namespace DirectoryService.Domain.Department;

public class Department
{
    ////////////////////Private
    private readonly List<Department> _childDepartments = [];
    private readonly List<DepartmentLocation.DepartmentLocation> _departmentLocations = [];
    private readonly List<DepartmentPosition.DepartmentPosition> _departmentPositions = [];
    ////////////////////Public
    public DepartmentId Id { get; private set; }
    public Name DepartmentName { get; private set; } //ValueObject
    public Identifier Identifier { get; private set; } //ValueObject
    public Guid? ParentDepartmentId { get; private set; } //ParentId/Foreing key/ null = корень
    public Path Path { get; private set; } //ValueObject
    public short Depth { get; private set; } // Глубина подразделения
    public bool IsActive { get; private set; } // для soft delete
    public IReadOnlyList<Department> ChildDepartments => _childDepartments;  //Дочерние Department
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public IReadOnlyList<DepartmentLocation.DepartmentLocation> DepartmentLocations => _departmentLocations;
    public IReadOnlyList<DepartmentPosition.DepartmentPosition> DepartmentPositions => _departmentPositions;


    //public List<Guid> IdBotDepartments { get; set; } = [];

    //EF CORE ошибка. Оставляем конструктор для избежания проблем
    private Department()
    {
    }

    private Department(Name name, Identifier identifier, Guid parentDepartment, Path path, short depth)
    {
        Id = new DepartmentId(Guid.NewGuid());
        DepartmentName = name;
        Identifier = identifier;
        ParentDepartmentId = parentDepartment;
        Path = path;
        Depth = depth;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
        
    //Создане модели
    public static Result<Department> Create(Name name, Identifier identifier, Guid parentDepartmentId, Path path, short depth)
    {
        //return new Result<Department>(); /// МОЖНО ТАК!!! 
        //Result.Success<Department>(new Department(...))  ///ИЛИ МОЖНО ТАК!!! 
        //return new Department(,,,); /// ИЛИ ТАК!!! 
        
        //Валидация уже имеется в Name Identifier Path
        return new Department(name, identifier, parentDepartmentId, path, depth);
    }

    //Изменение модели
    //Убрал так как много парамметров
    /*public Result Change(Name name, Identifier identifier, Department parentDepartment, Path path, short depth)
    {

        //Если назначил сам себя родителем или глубина уровня другая
        if (this == parentDepartment && this.Depth != depth)
        {
            return  Result.Failure<Department>($"The department can`t be change due to parentDepartment or depth");
        }
        
        this.DepartmentName = name;
        this.Identifier = identifier;
        this.ParentDepartment = parentDepartment;
        this.Path = path;
        this.Depth = depth;
        UpdatedAt = DateTime.UtcNow;

        return Result.Success();
    }*/
    
    //Изменение DepartmentName/Identifier
    public Result RenameDepartment(Name valueName, Identifier identifier)
    {
        //Валидация имеется в Name()
        DepartmentName = valueName;
        Identifier = identifier;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    //Изменение Parent
    public Result ChangeParentDepartment(Guid parentDepartmentId)
    {
        if (parentDepartmentId == null ||  //Если корень
            parentDepartmentId == ParentDepartmentId || //Если тот же parent
            ChildDepartments.Count != 0)   // Если есть дети
        {
            return Result.Failure<Department>($"The parent department can`t be changed");
        }

        ParentDepartmentId = parentDepartmentId;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    //Изменение пути
    public Result ChangePath(Path path)
    {
        Path = path;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    //Изменение глубины
    public Result ChangeDepth(short depth)
    {
        //Если максимальная глубина 3, то как-то надо ограничинь изменение глубины на 5
        //Надо знать макссимальную глубину 

        this.Depth = depth;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }

    //Изменение parent
    //убрал по совету Кирилла
    /*public Result ChangeParent(Department parentDepartment,  Path newPath)
    {
        if (this.ParentDepartment == null ||  //Если корень
            this.ParentDepartment.Id == parentDepartment.Id || //Если тот же parent
            this.ChildDepartments.Count != 0)   // Если есть дети
        {
            return Result.Failure<Department>($"The parent department can`t be changed");
        }
        
        this.ParentDepartment = parentDepartment;
        this.Path = newPath;
        return Result.Success();
    }*/

    //Удаление модели 
    public Result Delete()
    {
        //Нельзя удалить корень 
        if (ParentDepartmentId == null)
        {
            return Result.Failure<Department>($"The department can`t be delete due to parentDepartment is null");
        }
        this.IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        return Result.Success();
    }
}

