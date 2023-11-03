namespace TaskList5.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly AppContext _context;

    public UserController(AppContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IResult AddUser([FromBody] UserAddDto p)
    {
        _context.Users.Add(new User { Name = p.Name, Password = p.Password });
        _context.SaveChanges();
        return Results.Ok(p);
    }

    [HttpDelete("{id:int}")]
    public IResult DeleteUser(int id)
    {
        var entity = _context.Users.Find(id);
        if (entity is null)
            return Results.NotFound(id);

        _context.Users.Remove(entity);
        _context.SaveChanges();

        return Results.Ok(id);
    }

    [HttpGet("{id:int}")]
    public IResult GetUser(int id)
    {
        var user = _context.Users.Find(id);
        return user is null ? Results.NotFound(id) : Results.Ok(user);
    }

    [HttpGet]
    public IResult GetUsers() => Results.Ok(_context.Users.Select(x => x.ToDto()).ToList());

    [HttpPut]
    public IResult UpdateUser(int id, [FromBody] UserEditDto dto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Id == id);
        if (user == null)
            return Results.NotFound(id);

        user.Name = dto.Name;
        _context.Users.Update(user);

        _context.SaveChanges();
        return Results.Ok(id);
    }
}