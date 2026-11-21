IF DB_ID('BDBIBLIOTECA2026') IS NULL
BEGIN
    CREATE DATABASE BDBIBLIOTECA2026;
END
GO

USE BDBIBLIOTECA2026;
GO

IF OBJECT_ID('dbo.Libros', 'U') IS NOT NULL
    DROP TABLE dbo.Libros;
GO

CREATE TABLE dbo.Libros
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreLibro VARCHAR(120) NOT NULL,
    Autor VARCHAR(100) NOT NULL,
    NombreEditorial VARCHAR(100) NOT NULL,
    ISDN VARCHAR(20) NOT NULL
);
GO

INSERT INTO dbo.Libros (NombreLibro, Autor, NombreEditorial, ISDN)
VALUES
    ('La ciudad y los perros', 'Mario Vargas Llosa', 'Alfaguara', '978-8420471839'),
    ('Conversación en La Catedral', 'Mario Vargas Llosa', 'Alfaguara', '978-8420412146'),
    ('Los heraldos negros', 'César Vallejo', 'Peisa', '978-9972401503'),
    ('Cien años de soledad', 'Gabriel García Márquez', 'Sudamericana', '978-0307474728'),
    ('La palabra del mudo', 'Julio Ramón Ribeyro', 'Seix Barral', '978-8432219122'),
    ('El mundo es ancho y ajeno', 'Ciro Alegría', 'Losada', '978-9500394673');
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibros
AS
BEGIN
    SELECT Id, NombreLibro, Autor, NombreEditorial, ISDN
    FROM dbo.Libros
    ORDER BY Id ASC;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_ListarLibrosPorAutor
    @Autor VARCHAR(100) = ''
AS
BEGIN
    SELECT Id, NombreLibro, Autor, NombreEditorial, ISDN
    FROM dbo.Libros
    WHERE Autor LIKE '%' + ISNULL(@Autor, '') + '%'
    ORDER BY Id ASC;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_RegistrarLibro
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    INSERT INTO dbo.Libros (NombreLibro, Autor, NombreEditorial, ISDN)
    VALUES (@NombreLibro, @Autor, @NombreEditorial, @ISDN);
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_ActualizarLibro
    @Id INT,
    @NombreLibro VARCHAR(120),
    @Autor VARCHAR(100),
    @NombreEditorial VARCHAR(100),
    @ISDN VARCHAR(20)
AS
BEGIN
    UPDATE dbo.Libros
    SET NombreLibro = @NombreLibro,
        Autor = @Autor,
        NombreEditorial = @NombreEditorial,
        ISDN = @ISDN
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.usp_EliminarLibro
    @Id INT
AS
BEGIN
    DELETE FROM dbo.Libros
    WHERE Id = @Id;
END
GO