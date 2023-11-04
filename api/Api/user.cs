namespace Api;

public class User
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class UserDto
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<TaskDto> Tasks { get; set; } = default!;
}

public class UserEditDto
{
    public string Name { get; set; } = default!;
    public List<TaskDto> Tasks { get; set; } = default!;
}

public class UserAddDto
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public static class UserManipulations
{
    public static UserDto ToDto(this User full) =>
        new()
        {
            Id = full.Id,
            Name = full.Name
        };
}