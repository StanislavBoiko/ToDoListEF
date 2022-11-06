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
using ToDoListEF.Services;

namespace ToDoListEF.Pages
{
    public class IndexModel : PageModel
    {
        private ITodoRepo _todoRepo;
        
        public IndexModel(ITodoRepo todoRepo)
        {
            _todoRepo = todoRepo;
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
                Todos = _todoRepo.GetByDate((DateTime)Date);
            }
            else
            {
                Todos = _todoRepo.GetAll();
            }
        }

        public IActionResult OnPost(Todo todo)
        {
            OnGet();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _todoRepo.Add(todo);
            _todoRepo.Save();
            Message = $"Added: {todo.Description}";
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(Guid id)
        {
            if (ModelState.IsValid)
            {
                _todoRepo.Remove(id);                       
                _todoRepo.Save();
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

            if (_todoRepo.Find(id) != null)
            {
                return RedirectToPage("Edit", new { id = id });
            }

            return Page();
        }


    }
}
