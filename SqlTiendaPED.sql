use master;


-- Crear la base de datos
CREATE DATABASE TiendaPED;
GO

USE TiendaPED;
GO

-- =======================
-- TABLAS
-- =======================

-- Tabla Usuario
CREATE TABLE Usuario (
    IdUsuario INT NOT NULL PRIMARY KEY,
    CorreoUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasenia VARCHAR(50) NOT NULL
);

-- Tabla Producto con columna Categoria
CREATE TABLE Producto (
    IdProducto INT NOT NULL PRIMARY KEY,
    IdUsuario INT NOT NULL,
    NombreProducto VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) CHECK (Precio >= 0 AND Precio <= 9999),
    Descripcion VARCHAR(200) NOT NULL,
    RutaImagen VARCHAR(255),
    Categoria VARCHAR(50), -- NUEVO CAMPO
    CONSTRAINT UQ_IdProducto_NombreProducto UNIQUE (IdProducto, NombreProducto),
    CONSTRAINT FK_IdUsuarioProducto FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
        ON UPDATE CASCADE ON DELETE CASCADE
);

-- Tabla Factura
CREATE TABLE Factura (
    IdFactura INT NOT NULL PRIMARY KEY,
    FechaCompra DATETIME DEFAULT GETDATE(),
    IdProducto INT,
    NombreProducto VARCHAR(100),
    CONSTRAINT FK_DatosFactura FOREIGN KEY (IdProducto, NombreProducto)
        REFERENCES Producto(IdProducto, NombreProducto)
        ON UPDATE CASCADE ON DELETE CASCADE
);

-- =======================
-- DATOS DE PRUEBA
-- =======================

-- Usuarios
INSERT INTO Usuario (IdUsuario, CorreoUsuario, Contrasenia) VALUES
(1, 'juan@example.com','clave123'),
(2, 'ana@example.com' ,'admin2024'),
(3, 'luis@example.com','usuario33'),
(4, 'carla@example.com' ,'pass456'),
(5, 'pedro@example.com','qwerty'),
(6, 'julian@example.com' ,'segura1'),
(7, 'marcos@example.com','clave321'),
(8, 'ernesto@example.com','test2025'),
(9, 'lucia@example.com','pass999'),
(10, 'maria@example.com','abc123');

-- Productos
INSERT INTO Producto (IdProducto, IdUsuario, NombreProducto, Precio, Descripcion, RutaImagen, Categoria) VALUES
(101, 1, 'Mouse Logitech', 150.00, 'Mouse inalámbrico ergonómico', 'ImagenesProductos/mouse_logitech.png', 'Accesorios'),
(102, 2, 'Teclado Mecánico', 350.00, 'Teclado RGB para gaming', 'ImagenesProductos/teclado_mecanico.jpg', 'Accesorios'),
(103, 3, 'Monitor 24"', 1250.00, 'Monitor Full HD de 24 pulgadas', 'ImagenesProductos/monitor24.jpg', 'Pantallas'),
(104, 4, 'Laptop Dell', 8500.00, 'Laptop Dell i7 16GB RAM', 'ImagenesProductos/laptop_dell.jpg', 'Laptops'),
(105, 5, 'USB 64GB', 100.00, 'Memoria USB de alta velocidad', 'ImagenesProductos/usb64gb.jpg', 'Almacenamiento'),
(106, 6, 'Router WiFi 6', 699.99, 'Router de última generación', 'ImagenesProductos/routerwifi6.jpg', 'Redes'),
(107, 7, 'Audífonos Bluetooth', 250.50, 'Audífonos con cancelación de ruido', 'ImagenesProductos/audifonos_bluetooth.jpg', 'Audio'),
(108, 8, 'Webcam HD', 330.00, 'Cámara para videollamadas', 'ImagenesProductos/webcamhd.png', 'Periféricos'),
(109, 9, 'Disco SSD 1TB', 999.00, 'Unidad sólida rápida y confiable', 'ImagenesProductos/disco1tb.jpg', 'Almacenamiento'),
(110, 10, 'Silla Gamer', 1999.00, 'Silla ergonómica con soporte lumbar', 'ImagenesProductos/sillaredragon.jpg', 'Muebles'),
(111, 1, 'Mouse Redragon', 180.00, 'Mouse ergonómico con botones programables', 'ImagenesProductos/mouse_redragon.jpg', 'Accesorios'),
(112, 2, 'Pad Mouse XL', 90.00, 'Alfombrilla de ratón extendida para gamers', 'ImagenesProductos/pad_mouse.jpg', 'Accesorios'),
(113, 3, 'Cargador Universal', 120.00, 'Cargador compatible con múltiples dispositivos', 'ImagenesProductos/cargador_universal.jpg', 'Accesorios'),
(114, 4, 'Hub USB 4 Puertos', 75.00, 'Hub USB 3.0 de alta velocidad', 'ImagenesProductos/hub_usb.jpg', 'Accesorios'),
(115, 5, 'Soporte para Laptop', 130.00, 'Soporte ergonómico ajustable de aluminio', 'ImagenesProductos/soporte_laptop.jpg', 'Accesorios');

-- Facturas
INSERT INTO Factura (IdFactura, IdProducto, NombreProducto) VALUES
(1001, 101, 'Mouse Logitech'),
(1002, 102, 'Teclado Mecánico'),
(1003, 103, 'Monitor 24"'),
(1004, 104, 'Laptop Dell'),
(1005, 105, 'USB 64GB'),
(1006, 106, 'Router WiFi 6'),
(1007, 107, 'Audífonos Bluetooth'),
(1008, 108, 'Webcam HD'),
(1009, 109, 'Disco SSD 1TB'),
(1010, 110, 'Silla Gamer');


-- =======================
-- CONSULTAS DE PRUEBA
-- =======================
use TiendaPED
SELECT * FROM Producto;
SELECT * FROM Usuario;
SELECT * FROM Factura;
