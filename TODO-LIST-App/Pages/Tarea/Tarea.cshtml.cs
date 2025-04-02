using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TODO_LIST_App.Pages.Tarea
{
    public class TareaModel : PageModel
    {
        private static List<Models.Tarea> tareas = new();

        [BindProperty]
        public Models.Tarea NuevaTarea { get; set; }

        public List<Models.Tarea> Tareas => tareas;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NuevaTarea.Nombre))
                {
                    TempData["TipoAlerta"] = "error";
                    TempData["MensajeAlerta"] = "El nombre de la tarea no puede estar vacío";
                    return RedirectToPage();
                }

                if (NuevaTarea.Nombre.Length > 100)
                {
                    TempData["TipoAlerta"] = "error";
                    TempData["MensajeAlerta"] = "El nombre de la tarea no puede exceder los 100 caracteres";
                    return RedirectToPage();
                }

                if (tareas.Any(t => t.Nombre == NuevaTarea.Nombre))
                {
                    TempData["TipoAlerta"] = "error";
                    TempData["MensajeAlerta"] = "Ya existe una tarea con ese nombre";
                    return RedirectToPage();
                }
                if (tareas.Count > 0)
                {
                    NuevaTarea.Id = tareas.Max(t => t.Id) + 1; 
                }else if (tareas == null || tareas.Count ==0)
                {
                    NuevaTarea.Id = 1;
                }
                NuevaTarea.Completado = false;
                tareas.Add(NuevaTarea);

                TempData["TipoAlerta"] = "success";
                TempData["MensajeAlerta"] = "Tarea agregada correctamente";
            }
            catch (Exception)
            {
                TempData["TipoAlerta"] = "error";
                TempData["MensajeAlerta"] = "Ocurrió un error al agregar la tarea";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostToggle(int id, bool completado)
        {
            try
            {
                var tarea = tareas.FirstOrDefault(t => t.Id == id);
                if (tarea == null)
                {
                    TempData["TipoAlerta"] = "error";
                    TempData["MensajeAlerta"] = "No se encontró la tarea solicitada";
                    return RedirectToPage();
                }

                tarea.Completado = completado;
                TempData["TipoAlerta"] = "success";
                TempData["MensajeAlerta"] = $"Tarea marcada como {(completado ? "completada" : "pendiente")}";
            }
            catch (Exception)
            {
                TempData["TipoAlerta"] = "error";
                TempData["MensajeAlerta"] = "Ocurrió un error al actualizar la tarea";
            }

            return RedirectToPage();
        }

        public IActionResult OnPostEliminar(int id)
        {
            try
            {
                var tareaEliminada = tareas.FirstOrDefault(t => t.Id == id);
                if (tareaEliminada == null)
                {
                    TempData["TipoAlerta"] = "error";
                    TempData["MensajeAlerta"] = "No se encontró la tarea a eliminar";
                    return RedirectToPage();
                }

                tareas.RemoveAll(t => t.Id == id);
                TempData["TipoAlerta"] = "success";
                TempData["MensajeAlerta"] = $"Tarea '{tareaEliminada.Nombre}' eliminada correctamente";
            }
            catch (Exception)
            {
                TempData["TipoAlerta"] = "error";
                TempData["MensajeAlerta"] = "Ocurrió un error al eliminar la tarea";
            }

            return RedirectToPage();
        }
    }
}