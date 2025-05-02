
CREATE DATABASE TiendaPED;
GO

USE TiendaPED;
GO

-- Tablas
CREATE TABLE Usuario (
    IdUsuario INT NOT NULL, -- PK
    Contrasenia VARCHAR(50) NOT NULL
);

CREATE TABLE CorreosUsuario (
    IdUsuario INT NOT NULL, -- FK
    CorreoUsuario VARCHAR(50)
);

CREATE TABLE Producto (
    IdProducto INT NOT NULL, -- PK
    IdUsuario INT NOT NULL,	-- FK
    NombreProducto VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2),
    Descripcion VARCHAR(200) NOT NULL
);

CREATE TABLE Factura (
    IdFactura INT NOT NULL, -- PK
    FechaCompra DATETIME,
    IdProducto INT, -- FK
    NombreProducto VARCHAR(100) -- FK
);

-- Claves Primarias
ALTER TABLE Usuario ADD CONSTRAINT
PK_IdUsuario PRIMARY KEY (IdUsuario);

ALTER TABLE Producto ADD CONSTRAINT
PK_IdProducto PRIMARY KEY (IdProducto);

ALTER TABLE Factura ADD CONSTRAINT
PK_IdFactura PRIMARY KEY (IdFactura);

-- Claves Únicas necesarias para clave foránea compuesta
ALTER TABLE Producto ADD CONSTRAINT
UQ_IdProducto_NombreProducto UNIQUE (IdProducto, NombreProducto);

-- Claves Foráneas
ALTER TABLE CorreosUsuario ADD CONSTRAINT
FK_IdUsuarioCorreo FOREIGN KEY (IdUsuario)
REFERENCES Usuario(IdUsuario)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE Producto ADD CONSTRAINT
FK_IdUsuarioProducto FOREIGN KEY (IdUsuario)
REFERENCES Usuario(IdUsuario)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE Factura ADD CONSTRAINT
FK_DatosFactura FOREIGN KEY (IdProducto, NombreProducto)
REFERENCES Producto(IdProducto, NombreProducto)
ON UPDATE CASCADE
ON DELETE CASCADE;

-- Restricciones adicionales
ALTER TABLE CorreosUsuario
ADD CONSTRAINT UC_Correo UNIQUE (CorreoUsuario);

ALTER TABLE Producto
ADD CONSTRAINT CK_PrecioProducto CHECK (Precio >= 0 AND Precio <= 9999);

ALTER TABLE Factura
ADD CONSTRAINT DF_FechaCompra DEFAULT GETDATE() FOR FechaCompra;


-- Registros de Prueba

-- Insertar Usuarios
INSERT INTO Usuario (IdUsuario, Contrasenia) VALUES
(1, 'clave123'),
(2, 'admin2024'),
(3, 'usuario33'),
(4, 'pass456'),
(5, 'qwerty'),
(6, 'segura1'),
(7, 'clave321'),
(8, 'test2025'),
(9, 'pass999'),
(10, 'abc123');

-- Insertar Correos de Usuario
INSERT INTO CorreosUsuario (IdUsuario, CorreoUsuario) VALUES
(1, 'juan@example.com'),
(2, 'ana@example.com'),
(3, 'luis@example.com'),
(4, 'carla@example.com'),
(5, 'pedro@example.com'),
(6, 'sofia@example.com'),
(7, 'mario@example.com'),
(8, 'laura@example.com'),
(9, 'diego@example.com'),
(10, 'eva@example.com');

-- Insertar Productos
INSERT INTO Producto (IdProducto, IdUsuario, NombreProducto, Precio, Descripcion) VALUES
(101, 1, 'Mouse Logitech', 150.00, 'Mouse inalámbrico ergonómico'),
(102, 2, 'Teclado Mecánico', 350.00, 'Teclado RGB para gaming'),
(103, 3, 'Monitor 24"', 1250.00, 'Monitor Full HD de 24 pulgadas'),
(104, 4, 'Laptop Dell', 8500.00, 'Laptop Dell i7 16GB RAM'),
(105, 5, 'USB 64GB', 100.00, 'Memoria USB de alta velocidad'),
(106, 6, 'Router WiFi 6', 699.99, 'Router de última generación'),
(107, 7, 'Audífonos Bluetooth', 250.50, 'Audífonos con cancelación de ruido'),
(108, 8, 'Webcam HD', 330.00, 'Cámara para videollamadas'),
(109, 9, 'Disco SSD 1TB', 999.00, 'Unidad sólida rápida y confiable'),
(110, 10, 'Silla Gamer', 1999.00, 'Silla ergonómica con soporte lumbar');

-- Insertar Facturas
INSERT INTO Factura (IdFactura, FechaCompra, IdProducto, NombreProducto) VALUES
(1001, DEFAULT, 101, 'Mouse Logitech'),
(1002, DEFAULT, 102, 'Teclado Mecánico'),
(1003, DEFAULT, 103, 'Monitor 24"'),
(1004, DEFAULT, 104, 'Laptop Dell'),
(1005, DEFAULT, 105, 'USB 64GB'),
(1006, DEFAULT, 106, 'Router WiFi 6'),
(1007, DEFAULT, 107, 'Audífonos Bluetooth'),
(1008, DEFAULT, 108, 'Webcam HD'),
(1009, DEFAULT, 109, 'Disco SSD 1TB'),
(1010, DEFAULT, 110, 'Silla Gamer');


-- Mostrar Datos de Prueba
SELECT TOP 5 * FROM Usuario;

SELECT TOP 5 * FROM CorreosUsuario;

SELECT TOP 5 * FROM Producto;

SELECT TOP 5 * FROM Factura;


