
-- Crear la base de datos
CREATE DATABASE HGC_SIGEM;
GO

USE HGC_SIGEM;
GO

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NOT NULL UNIQUE,
    ContraseñaHash VARCHAR(255) NOT NULL,
    Rol VARCHAR(50) NOT NULL, -- Ej: Admin, Diseñador, Cliente
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Activo BIT DEFAULT 1
);
GO

-- Tabla: Widgets
CREATE TABLE Widgets (
    IdWidget INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50) NOT NULL, -- Ej: Clima, Cambio, Galería
    UrlApi VARCHAR(255),
    ApiKey VARCHAR(255),
    FrecuenciaRefresco INT, -- En segundos
    RutaImagen VARCHAR(255),
    Activo BIT DEFAULT 1
);
GO

-- Tabla: Configuración de Widgets por Usuario
CREATE TABLE ConfiguracionUsuario (
    IdConfiguracion INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    IdWidget INT NOT NULL,
    Posicion INT DEFAULT 0,
    Favorito BIT DEFAULT 0,
    Visible BIT DEFAULT 1,
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdWidget) REFERENCES Widgets(IdWidget)
);
GO

-- Tabla: Productos (Inventario)
CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Cantidad INT NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Activo BIT DEFAULT 1
);
GO

-- Tabla: Órdenes
CREATE TABLE Ordenes (
    IdOrden INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Estado VARCHAR(50) DEFAULT 'Pendiente', -- Ej: Pendiente, Finalizada
    Fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
);
GO
