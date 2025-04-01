using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODO_LIST_App.Models;
namespace TODO_LIST_App.Pages.Tarea
{
    public class TareaModel : PageModel
    {
        private static List<TODO_LIST_App.Models.Tarea> tareas = new();

        [BindProperty]
        public TODO_LIST_App.Models.Tarea NuevaTarea { get; set; } 

        public List<TODO_LIST_App.Models.Tarea> Tareas => tareas;

        public void OnGet() { }

    }
}
