namespace ToDoListEF.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    
    public DateTime TimeCreated { get; set; }
    public DateTime TimeUpdated { get; set; }
}