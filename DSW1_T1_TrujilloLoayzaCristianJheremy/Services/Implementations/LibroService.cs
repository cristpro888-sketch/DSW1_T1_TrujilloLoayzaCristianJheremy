using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Interfaces;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Interfaces;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Implementations
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public async Task<IEnumerable<Libro>> ListarLibrosAsync()
        {
            return await _libroRepository.ListarLibrosAsync();
        }

        public async Task<Libro?> ObtenerLibroPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _libroRepository.ObtenerLibroPorIdAsync(id);
        }

        public async Task<(bool Exito, string Mensaje)> RegistrarLibroAsync(Libro libro)
        {
            NormalizarCampos(libro);

            bool registrado = await _libroRepository.RegistrarLibroAsync(libro);
            return registrado
                ? (true, $"El libro '{libro.NombreLibro}' fue registrado correctamente.")
                : (false, "No se pudo completar el registro del libro.");
        }

        public async Task<(bool Exito, string Mensaje)> ActualizarLibroAsync(Libro libro)
        {
            if (libro.Id <= 0)
                return (false, "El identificador del libro no es válido.");

            NormalizarCampos(libro);

            bool actualizado = await _libroRepository.ActualizarLibroAsync(libro);
            return actualizado
                ? (true, $"El libro '{libro.NombreLibro}' fue actualizado correctamente.")
                : (false, "No se encontró el libro o no se realizaron cambios.");
        }

        public async Task<(bool Exito, string Mensaje)> EliminarLibroAsync(int id)
        {
            if (id <= 0)
                return (false, "El identificador del libro no es válido.");

            bool eliminado = await _libroRepository.EliminarLibroAsync(id);
            return eliminado
                ? (true, "El libro fue eliminado correctamente.")
                : (false, "No se pudo eliminar el libro porque no existe o ya fue eliminado.");
        }

        public async Task<IEnumerable<Libro>> ListarLibrosPorAutor(string autor)
        {
            string filtroLimpio = string.IsNullOrWhiteSpace(autor) ? string.Empty : autor.Trim();
            return await _libroRepository.ListarLibrosPorAutor(filtroLimpio);
        }

        private static void NormalizarCampos(Libro libro)
        {
            libro.NombreLibro = libro.NombreLibro.Trim();
            libro.Autor = libro.Autor.Trim();
            libro.NombreEditorial = libro.NombreEditorial.Trim();
            libro.ISDN = libro.ISDN.Trim();
        }
    }
}
