using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToDoListEF.Models;
using ToDoListEF.Repositories;

namespace ToDoListEF.Services;

public interface ITodoService
{
    List<Todo> GetAll();
    void Add(Todo todo);
    void Remove(Guid id);
    Todo Find(Guid id);
    void Update(Todo todo);
    List<Todo> GetByDate(DateTime date);
}