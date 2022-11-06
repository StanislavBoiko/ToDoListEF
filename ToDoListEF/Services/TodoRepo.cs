using Microsoft.EntityFrameworkCore;
using ToDoListEF.Data;
using ToDoListEF.Models;

namespace ToDoListEF.Services;

public class TodoRepo : ITodoRepo, IDisposable
{
    private readonly TodoContext _context;

    public TodoRepo(TodoContext context)
    {
        _context = context;
    }
    
    public List<Todo> GetAll()
    {
        return _context.Todo.ToList();
    }

    public void Add(Todo todo)
    {
        todo.TimeCreated = DateTime.UtcNow;
        todo.TimeUpdated = DateTime.UtcNow;
        _context.Todo.Add(todo);
    }

    public void Remove(Guid id)
    {
        Todo todo = _context.Todo.Find(id);
        _context.Todo.Remove(todo);

    }

    public Todo Find(Guid id)
    {
        return _context.Todo.Find(id);
    }

    public void Update(Todo todo)
    {
        todo.TimeUpdated = DateTime.UtcNow;
        _context.Entry(todo).State = EntityState.Modified;
    }

    public List<Todo> GetByDate(DateTime date)
    {
         return _context.Todo.ToList().FindAll(t => t.TimeCreated.Date == date);
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}