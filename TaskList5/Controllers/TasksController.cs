namespace TaskList5.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppContext _context;

    public TasksController(AppContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IResult AddTask(int userId, [FromBody] AddTaskDto p)
    {
        var find = _context.Users.Find(userId);
        if (find is null)
            return Results.NotFound(userId);

        _context.Tasks.Add(new Task { Title = p.Title, Description = p.Description, IsActive = true, UserId = userId });
        _context.SaveChanges();
        return Results.Ok(p);
    }

    [HttpDelete("{id:int}")]
    public IResult DeleteTask(int id)
    {
        var task = _context.Tasks.Find(id);
        if (task is null)
            return Results.NotFound(id);

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return Results.Ok(id);
    }

    [HttpGet("{id:int}")]
    public IResult GetTask(int id)
    {
        var task = _context.Tasks.Find(id);
        return task is null ? Results.NotFound(id) : Results.Ok(task);
    }

    [HttpGet]
    public IResult GetTasks(int userId)
    {
        var find = _context.Users.Find(userId);

        if (find is null)
            return Results.BadRequest(userId);

        var tasks = _context.Tasks.Where(x => x.UserId == userId);
        var taskDtos = tasks.Select(x => new TaskDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Finished = x.Finished,
            IsActive = x.IsActive
        });
        return Results.Ok(taskDtos.ToList());
    }

    [HttpPut]
    public IResult UpdateTask(int taskId, [FromBody] EditTaskDto dto)
    {
        var task = _context.Tasks.FirstOrDefault(x => x.Id == taskId);
        if (task == null)
            return Results.NotFound(taskId);

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.IsActive = dto.IsActive;
        _context.Tasks.Update(task);

        _context.SaveChanges();
        return Results.Ok(taskId);
    }
}