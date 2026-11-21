using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Interfaces
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> ListarLibrosAsync();
        Task<Libro?> ObtenerLibroPorIdAsync(int id);
        Task<bool> RegistrarLibroAsync(Libro libro);
        Task<bool> ActualizarLibroAsync(Libro libro);
        Task<bool> EliminarLibroAsync(int id);
        Task<IEnumerable<Libro>> ListarLibrosPorAutor(string autor);
    }
}
