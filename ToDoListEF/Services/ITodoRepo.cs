using ToDoListEF.Models;

namespace ToDoListEF.Services;

public interface ITodoRepo : IDisposable
{
    List<Todo> GetAll();
    void Add(Todo todo);
    void Remove(Guid id);
    Todo Find(Guid id);
    void Update(Todo todo);

    List<Todo> GetByDate(DateTime date);

    void Save();
}