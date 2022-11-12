using Microsoft.EntityFrameworkCore;
using ToDoListEF.Data;
using ToDoListEF.Models;

namespace ToDoListEF.Repositories;

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
        _context.Todo.Add(todo);
        _context.SaveChanges();
    }

    public void Remove(Guid id)
    {
        Todo todo = _context.Todo.Find(id);
        _context.Todo.Remove(todo);
        _context.SaveChanges();
    }

    public Todo Find(Guid id)
    {
        return _context.Todo.Find(id);
    }

    public void Update(Todo todo)
    {
        _context.Entry(todo).State = EntityState.Modified;
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