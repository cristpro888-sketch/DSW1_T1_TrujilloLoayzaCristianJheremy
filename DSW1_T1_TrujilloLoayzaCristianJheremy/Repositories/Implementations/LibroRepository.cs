using System.Data;
using Microsoft.Data.SqlClient;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Models;
using DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Interfaces;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Repositories.Implementations
{
    public class LibroRepository : ILibroRepository
    {
        private readonly string _connectionString;

        public LibroRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionBiblioteca")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'ConexionBiblioteca' en appsettings.json.");
        }

        public async Task<IEnumerable<Libro>> ListarLibrosAsync()
        {
            var lista = new List<Libro>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_ListarLibros", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearLibro(reader));
            }

            return lista;
        }

        public async Task<Libro?> ObtenerLibroPorIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_ObtenerLibroPorId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return MapearLibro(reader);
            }

            return null;
        }

        public async Task<bool> RegistrarLibroAsync(Libro libro)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_RegistrarLibro", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@NombreLibro", SqlDbType.VarChar, 120).Value = libro.NombreLibro;
            command.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = libro.Autor;
            command.Parameters.Add("@NombreEditorial", SqlDbType.VarChar, 100).Value = libro.NombreEditorial;
            command.Parameters.Add("@ISDN", SqlDbType.VarChar, 20).Value = libro.ISDN;

            await connection.OpenAsync();
            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> ActualizarLibroAsync(Libro libro)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_ActualizarLibro", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = libro.Id;
            command.Parameters.Add("@NombreLibro", SqlDbType.VarChar, 120).Value = libro.NombreLibro;
            command.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = libro.Autor;
            command.Parameters.Add("@NombreEditorial", SqlDbType.VarChar, 100).Value = libro.NombreEditorial;
            command.Parameters.Add("@ISDN", SqlDbType.VarChar, 20).Value = libro.ISDN;

            await connection.OpenAsync();
            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<bool> EliminarLibroAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_EliminarLibro", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();
            int filasAfectadas = await command.ExecuteNonQueryAsync();
            return filasAfectadas > 0;
        }

        public async Task<IEnumerable<Libro>> ListarLibrosPorAutor(string autor)
        {
            var lista = new List<Libro>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("usp_ListarLibrosPorAutor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = autor ?? string.Empty;

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(MapearLibro(reader));
            }

            return lista;
        }

        private static Libro MapearLibro(SqlDataReader reader)
        {
            return new Libro
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                NombreLibro = reader.GetString(reader.GetOrdinal("NombreLibro")),
                Autor = reader.GetString(reader.GetOrdinal("Autor")),
                NombreEditorial = reader.GetString(reader.GetOrdinal("NombreEditorial")),
                ISDN = reader.GetString(reader.GetOrdinal("ISDN"))
            };
        }
    }
}
