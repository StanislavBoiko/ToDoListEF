using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoListEF.Data;
using ToDoListEF.Models;
using ToDoListEF.Repositories;
using ToDoListEF.Services;

namespace ToDoListEF.Pages
{
    public class IndexModel : PageModel
    {
        private ITodoService _todoService;
        
        public IndexModel(ITodoService todoService)
        {
            _todoService = todoService;
        }
        
        [TempData]
        public string Message { get; set; }
        
        public IList<Todo> Todos { get;set; } = default!;
        
        
        [BindProperty(SupportsGet = true), DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public void OnGet()
        {
            if (Date.HasValue)
            {
                Todos = _todoService.GetByDate((DateTime)Date);
            }
            else
            {
                Todos = _todoService.GetAll();
            }
        }

        public IActionResult OnPost(Todo todo)
        {
            OnGet();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _todoService.Add(todo);
            
            Message = $"Added: {todo.Description}";
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(Guid id)
        {
            if (ModelState.IsValid)
            {
                _todoService.Remove(id);                       
                
                return RedirectToPage();
            }

            return Page();
        }

        public IActionResult OnPostEdit(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_todoService.Find(id) != null)
            {
                return RedirectToPage("Edit", new { id = id });
            }

            return Page();
        }


    }
}
