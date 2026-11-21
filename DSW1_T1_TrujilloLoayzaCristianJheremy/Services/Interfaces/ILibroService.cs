using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Services.Interfaces
{
    public interface ILibroService
    {
        Task<IEnumerable<Libro>> ListarLibrosAsync();
        Task<Libro?> ObtenerLibroPorIdAsync(int id);
        Task<(bool Exito, string Mensaje)> RegistrarLibroAsync(Libro libro);
        Task<(bool Exito, string Mensaje)> ActualizarLibroAsync(Libro libro);
        Task<(bool Exito, string Mensaje)> EliminarLibroAsync(int id);
        Task<IEnumerable<Libro>> ListarLibrosPorAutor(string autor);
    }
}
