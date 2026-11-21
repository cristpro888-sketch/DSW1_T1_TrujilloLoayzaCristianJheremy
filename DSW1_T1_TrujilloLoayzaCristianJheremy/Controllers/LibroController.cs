using Microsoft.AspNetCore.Mvc;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Interfaces;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

    
        private async Task<IEnumerable<Libro>> ObtenerListadoLibrosAsync()
        {
            return await _libroService.ListarLibrosAsync();
        }

      

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.TipoMensaje = TempData["TipoMensaje"];

            try
            {
                IEnumerable<Libro> libros = await ObtenerListadoLibrosAsync();
                return View(libros);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al cargar los libros: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                return View(Enumerable.Empty<Libro>());
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new Libro());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mensaje = "Por favor, complete correctamente todos los campos obligatorios.";
                ViewBag.TipoMensaje = "warning";
                return View(libro);
            }

            try
            {
                var resultado = await _libroService.RegistrarLibroAsync(libro);
                if (resultado.Exito)
                {
                    TempData["Mensaje"] = resultado.Mensaje;
                    TempData["TipoMensaje"] = "success";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensaje = resultado.Mensaje;
                ViewBag.TipoMensaje = "danger";
                return View(libro);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Ocurrió un error al registrar el libro: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                return View(libro);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            if (id <= 0)
            {
                TempData["Mensaje"] = "Identificador de libro no válido.";
                TempData["TipoMensaje"] = "warning";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var libro = await _libroService.ObtenerLibroPorIdAsync(id);
                if (libro == null)
                {
                    TempData["Mensaje"] = "El libro solicitado no existe.";
                    TempData["TipoMensaje"] = "warning";
                    return RedirectToAction(nameof(Index));
                }

                return View(libro);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Error al obtener el libro: {ex.Message}";
                TempData["TipoMensaje"] = "danger";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Mensaje = "Por favor, complete correctamente todos los campos obligatorios.";
                ViewBag.TipoMensaje = "warning";
                return View(libro);
            }

            try
            {
                var resultado = await _libroService.ActualizarLibroAsync(libro);
                if (resultado.Exito)
                {
                    TempData["Mensaje"] = resultado.Mensaje;
                    TempData["TipoMensaje"] = "success";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Mensaje = resultado.Mensaje;
                ViewBag.TipoMensaje = "danger";
                return View(libro);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Ocurrió un error al actualizar el libro: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                return View(libro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var resultado = await _libroService.EliminarLibroAsync(id);
                TempData["Mensaje"] = resultado.Mensaje;
                TempData["TipoMensaje"] = resultado.Exito ? "success" : "danger";
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = $"Ocurrió un error al eliminar el libro: {ex.Message}";
                TempData["TipoMensaje"] = "danger";
            }

            return RedirectToAction(nameof(Index));
        }

   

        [HttpGet]
        public async Task<IActionResult> BuscarPorAutor(string? autor)
        {
            ViewBag.AutorBuscado = autor ?? string.Empty;

            try
            {
                IEnumerable<Libro> libros = await _libroService.ListarLibrosPorAutor(autor ?? string.Empty);
                return View(libros);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = $"Error al realizar la búsqueda por autor: {ex.Message}";
                ViewBag.TipoMensaje = "danger";
                return View(Enumerable.Empty<Libro>());
            }
        }
    }
}
