# DSW1_T1_TrujilloLoayzaCristianJheremy
Solución de Evaluación de Laboratorio T1 - Curso: **Desarrollo de Servicios Web I** (CIBERTEC).

## Estructura del Proyecto (Buenas Prácticas)
- **Models/Libro.cs**: Entidad con Data Annotations y validaciones obligatorias.
- **Repositories/**: Acceso a datos desacoplado con `Microsoft.Data.SqlClient`, bloques `using`, `async/await` y ejecución exclusiva de Procedimientos Almacenados.
- **Services/**: Capa de lógica de negocio y normalización de datos inyectada por interfaz (`ILibroService`).
- **Controllers/LibroController.cs**: Controlador limpio que resuelve Pregunta 1 (CRUD) y Pregunta 2 (`ListarLibrosPorAutor` con `LIKE`).
- **Database/BDBIBLIOTECA2026.sql**: Script completo de creación de base de datos, tabla `dbo.Libros`, datos de prueba y procedimientos almacenados.

## Pasos para ejecutar
1. Abrir SQL Server Management Studio (SSMS) y ejecutar el archivo `Database/BDBIBLIOTECA2026.sql`.
2. Verificar en `DSW1_T1_TrujilloLoayzaCristianJheremy/appsettings.json` que el servidor de `ConexionBiblioteca` coincida con tu instancia local de SQL Server (`Server=localhost` o `Server=.\SQLEXPRESS`).
3. Abrir `DSW1_T1_TrujilloLoayzaCristianJheremy.sln` en Visual Studio y presionar **F5**.
