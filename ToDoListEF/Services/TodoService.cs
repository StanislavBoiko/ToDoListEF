using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoListEF.Models;
using ToDoListEF.Repositories;

namespace ToDoListEF.Services;

public class TodoService : ITodoService
{
    
    private ITodoRepo _todoRepo;

    public TodoService(ITodoRepo todoRepo)
    {
        _todoRepo = todoRepo;
    }
    
    
    public List<Todo> GetAll()
    {
        return _todoRepo.GetAll();
    }

    public void Add(Todo todo)
    {
        todo.TimeCreated = DateTime.UtcNow;
        todo.TimeUpdated = DateTime.UtcNow;
        _todoRepo.Add(todo);
    }

    public void Remove(Guid id)
    {
        _todoRepo.Remove(id);
    }

    public Todo Find(Guid id)
    {
        return _todoRepo.Find(id);
    }

    public void Update(Todo todo)
    {
        todo.TimeUpdated = DateTime.UtcNow;
        _todoRepo.Update(todo);
    }

    public List<Todo> GetByDate(DateTime date)
    {
        return _todoRepo.GetAll().FindAll(t => t.TimeCreated.Date == date);
    }
}