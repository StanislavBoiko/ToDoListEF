using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListEF.Data;
using ToDoListEF.Models;
using ToDoListEF.Services;

namespace ToDoListEF.Pages
{
    public class EditModel : PageModel
    {
        private readonly ITodoRepo _todoRepo;

        public EditModel(ITodoRepo todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [BindProperty]
        public Todo Todo { get; set; } = default!;

        public  IActionResult OnGet(Guid? id)
        {
            if (id == null || _todoRepo.GetAll() == null)
            {
                return NotFound();
            }

            var todo = _todoRepo.GetAll().FirstOrDefault(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            Todo = todo;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _todoRepo.Update(Todo);

            _todoRepo.Save();
            return RedirectToPage("./Index");
        }
    }
}
