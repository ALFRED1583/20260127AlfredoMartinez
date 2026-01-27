--- USER: PruebaBSCI
--- PASS: D4MhNqndI4Vb~V

CREATE DATABASE BSCI
GO
USE BSCI
GO
CREATE TABLE Categorias(
IdCategoria BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
);
GO
INSERT INTO Categorias VALUES('Mantenimiento')
INSERT INTO Categorias VALUES('TI')
INSERT INTO Categorias VALUES('Red')
INSERT INTO Categorias VALUES('Seguridad')
GO
CREATE PROCEDURE SP_ObtenerCategorias
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdCategoria, Nombre
    FROM Categorias
    ORDER BY Nombre ASC;
END
GO
CREATE PROCEDURE SP_ExisteCategoria
    @IdCategoria BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Existe
    FROM Categorias
    WHERE IdCategoria = @IdCategoria;
END
GO
CREATE TABLE Incidencias(
IdIncidencia BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
Titulo VARCHAR(100) NOT NULL,
Descripcion VARCHAR(500) NOT NULL CHECK (LEN(Descripcion) >= 10),
IdCategoria BIGINT NOT NULL REFERENCES Categorias(IdCategoria),
Severidad VARCHAR(10) NOT NULL CHECK (severidad IN ('Baja','Media','Alta','Crítica')),
FechaRegistro DATETIME2 DEFAULT GETDATE(),
EstadoActual VARCHAR(20) NOT NULL ,
BitacoraInicial VARCHAR(MAX) NULL,
);
GO
CREATE PROCEDURE SP_CrearIncidencia
    @Titulo VARCHAR(100),
    @Descripcion VARCHAR(MAX),
    @IdCategoria BIGINT,
    @Severidad VARCHAR(10),
    @BitacoraInicial VARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Incidencias (Titulo, Descripcion, IdCategoria, severidad, BitacoraInicial,EstadoActual)
    VALUES (@Titulo, @Descripcion, @IdCategoria, @Severidad, @BitacoraInicial,'Pendiente');
    SELECT SCOPE_IDENTITY() AS IdIncidencia;
END
GO
CREATE TABLE Bitacora(
IdBitacora BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdIncidencia BIGINT REFERENCES Incidencias(IdIncidencia),
FechaHora DATETIME2 DEFAULT GETDATE(),
AccionRealizada VARCHAR(500) NOT NULL,
Estado VARCHAR(20),
Comentario VARCHAR(MAX) NOT NULL,
Usuario VARCHAR(100) NOT NULL,
);


GO
CREATE PROCEDURE SP_ObtenerIncidencia
    @IdIncidencia BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT *
    FROM Incidencias
    WHERE IdIncidencia = @IdIncidencia;
    SELECT * 
    FROM Bitacora 
    WHERE IdIncidencia = @IdIncidencia
    ORDER BY FechaHora ASC;
END
GO
CREATE PROCEDURE SP_ListarIncidencias
    @Estado VARCHAR(20) = NULL,
    @IdCategoria INT = NULL,
    @Severidad VARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT i.*, C.nombre AS Categoria
    FROM Incidencias i
    INNER JOIN Categorias as C ON i.IdCategoria = C.IdCategoria
    WHERE (@estado IS NULL OR i.EstadoActual = @estado)
      AND (@IdCategoria IS NULL OR i.IdCategoria = @IdCategoria)
      AND (@Severidad IS NULL OR i.severidad = @Severidad)
    ORDER BY FechaRegistro DESC;
END
GO
ALTER PROCEDURE SP_ActualizarEstadoIncidencia
    @IdIncidencia BIGINT,
    @Estado VARCHAR(20),
	@AccionRealizada VARCHAR(500),
    @Comentario VARCHAR(MAX),
    @Usuario VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Incidencias WHERE IdIncidencia = @IdIncidencia)
    BEGIN
        RAISERROR('La incidencia no existe.', 16, 1);
        RETURN;
    END
    UPDATE Incidencias
    SET EstadoActual = @Estado
    WHERE IdIncidencia = @IdIncidencia;

    INSERT INTO Bitacora (IdIncidencia,AccionRealizada, Estado, Comentario, Usuario)
    VALUES (@IdIncidencia,@AccionRealizada, @Estado, @Comentario, @Usuario);
END
