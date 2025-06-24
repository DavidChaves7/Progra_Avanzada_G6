-- Crear base de datos
CREATE DATABASE Proyecto_HGC;
GO

-- Usar la base de datos
USE Proyecto_HGC;
GO

-- Tabla de Clientes
CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100),
    Telefono NVARCHAR(20),
    Empresa NVARCHAR(100),
    FechaRegistro DATE DEFAULT GETDATE()
);
GO

-- Tabla de Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100),
    Telefono NVARCHAR(20),
    Empresa NVARCHAR(100),
    Direccion NVARCHAR(200),
    FechaRegistro DATE DEFAULT GETDATE()
);
GO

-- Tabla de Proyectos / Solicitudes
CREATE TABLE Proyectos (
    ProyectoID INT PRIMARY KEY IDENTITY(1,1),
    ClienteID INT NOT NULL,
    NombreProyecto NVARCHAR(150),
    Descripcion NVARCHAR(MAX),
    Estado NVARCHAR(50), -- Ej: En Diseño, En Producción, Finalizado
    FechaInicio DATE,
    FechaEntregaEstimada DATE,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);
GO

-- Tabla de Cotizaciones
CREATE TABLE Cotizaciones (
    CotizacionID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT NOT NULL,
    FechaCotizacion DATE DEFAULT GETDATE(),
    MontoTotal DECIMAL(12,2),
    Estado NVARCHAR(50), -- Ej: Enviada, Aprobada, Rechazada
    FOREIGN KEY (ProyectoID) REFERENCES Proyectos(ProyectoID)
);
GO

-- Tabla de Órdenes de Producción
CREATE TABLE OrdenesProduccion (
    OrdenID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT NOT NULL,
    FechaInicio DATE,
    Estado NVARCHAR(50), -- Ej: En proceso, Detenida, Completada
    Responsable NVARCHAR(100),
    FOREIGN KEY (ProyectoID) REFERENCES Proyectos(ProyectoID)
);
GO
