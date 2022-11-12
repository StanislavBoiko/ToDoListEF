using ToDoListEF.Models;

namespace ToDoListEF.Repositories;

public interface ITodoRepo : IDisposable
{
    List<Todo> GetAll();
    void Add(Todo todo);
    void Remove(Guid id);
    Todo Find(Guid id);
    void Update(Todo todo);
    
}