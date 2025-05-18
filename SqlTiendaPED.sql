
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
    IdUsuario INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
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
INSERT INTO Usuario (CorreoUsuario, Contrasenia) VALUES
('juan@example.com','clave123'),
('ana@example.com' ,'admin2024'),
('luis@example.com','usuario33'),
('carla@example.com' ,'pass456'),
('pedro@example.com','qwerty'),
('julian@example.com' ,'segura1'),
('marcos@example.com','clave321'),
('ernesto@example.com','test2025'),
('lucia@example.com','pass999'),
('maria@example.com','abc123');

-- Productos
INSERT INTO Producto (IdProducto, IdUsuario, NombreProducto, Precio, Descripcion, RutaImagen, Categoria) VALUES
(101, 1, 'Mouse Logitech', 150.00, 'Mouse inalámbrico ergonómico', 'ImagenesProductos/mouse_logitech.png', 'Accesorios'),
(102, 2, 'Teclado Mecánico', 250.00, 'Teclado RGB para gaming', 'ImagenesProductos/teclado_mecanico.jpg', 'Accesorios'),
(103, 3, 'Monitor 24"', 179.00, 'Monitor Full HD de 24 pulgadas', 'ImagenesProductos/monitor24.jpg', 'Pantallas'),
(104, 4, 'Laptop Dell', 1599.95, 'Laptop Dell i7 16GB RAM', 'ImagenesProductos/laptop_dell.jpg', 'Laptops'),
(105, 5, 'USB 64GB', 100.00, 'Memoria USB de alta velocidad', 'ImagenesProductos/usb64gb.jpg', 'Almacenamiento'),
(106, 6, 'Router WiFi 6', 699.99, 'Router de última generación', 'ImagenesProductos/routerwifi6.jpg', 'Redes'),
(107, 7, 'Audífonos Bluetooth', 250.50, 'Audífonos con cancelación de ruido', 'ImagenesProductos/audifonos_bluetooth.jpg', 'Audio'),
(108, 8, 'Webcam HD', 330.00, 'Cámara para videollamadas', 'ImagenesProductos/webcamhd.png', 'Periféricos'),
(109, 9, 'Disco SSD 1TB', 230.95, 'Unidad sólida rápida y confiable', 'ImagenesProductos/disco1tb.jpg', 'Almacenamiento'),
(110, 10, 'Silla Gamer', 379.99, 'Silla ergonómica con soporte lumbar', 'ImagenesProductos/sillaredragon.jpg', 'Muebles'),
(111, 1, 'Mouse Redragon', 79.00, 'Mouse ergonómico con botones programables', 'ImagenesProductos/mouse_redragon.jpg', 'Accesorios'),
(112, 2, 'Pad Mouse XL', 20.00, 'Alfombrilla de ratón extendida para gamers', 'ImagenesProductos/pad_mouse.jpg', 'Accesorios'),
(113, 3, 'Cargador Universal', 45.95, 'Cargador compatible con múltiples dispositivos', 'ImagenesProductos/cargador_universal.jpg', 'Accesorios'),
(114, 4, 'Hub USB 4 Puertos', 25.00, 'Hub USB 3.0 de alta velocidad', 'ImagenesProductos/hub_usb.jpg', 'Accesorios'),
(115, 5, 'Soporte para Laptop', 30.00, 'Soporte ergonómico ajustable de aluminio', 'ImagenesProductos/soporte_laptop.jpg', 'Accesorios'),
(116, 1, 'Teclado Mecánico K616', 79.99, 'Teclado Mecánico RGB Redragon K616 ', 'ImagenesProductos/teclado_k616.jpg', 'Accesorios'),
(117, 1, 'Teclado Mecánico G915', 129.99, 'Teclado Mecánico Logitech G915 Pro', 'ImagenesProductos/teclado_g915.jpg', 'Accesorios'),
(118, 2, 'Teclado Mecánico XPG', 130.00, 'Teclado Mecánico XPG Summoner Mini Black 60%', 'ImagenesProductos/teclado_xpg.jpg', 'Accesorios'),
(119, 4, 'Teclado Mecánico K630', 54.95, 'Teclado Mecánico DragonBorn Redragon K630 White', 'ImagenesProductos/teclado_k630.jpg', 'Accesorios'),
(120, 3, 'Teclado Mecánico Black Widow', 149.99, 'Teclado Mecánico Razer Black Widow V03', 'ImagenesProductos/teclado_razerblackwidow.jpg', 'Accesorios'),
(121, 1, 'Laptop Vector MSI', 3799.99, 'Laptop MSI Vector 16HX A14', 'ImagenesProductos/soporte_laptop_vector16.jpg', 'Laptops'),
(122, 1, 'Laptop HP Victus', 1139.99, 'Laptop HP Victus RH 15', 'ImagenesProductos/laptop_hpvictus.jpg', 'Laptops'),
(123, 2, 'Laptop Acer NITRO', 1499.99, 'Laptop Acer NITRO AV15', 'ImagenesProductos/laptop_acernitro.jpg', 'Laptops'),
(124, 5, 'Laptop Asus Zephyrus', 130.00, 'Laptop Gamer (ROG) Asus Zephyrus', 'ImagenesProductos/laptop_zephyrus.jpg', 'Laptops'),
(125, 7, 'Audífonos Razer Barracuda', 139.95, 'Audifonos Multiplataforma Inalabricos Razer Barracuda - PUGB Edition', 'ImagenesProductos/audifonos_razerbarracuda.jpg', 'Audio'),
(126, 7, 'Audífonos SkullCandy Pro', 250.50, 'Audífonos SkullCandy Pro Slyr Q733', 'ImagenesProductos/audifonos_skullcandypro.jpg', 'Audio'),
(127, 7, 'Audífonos Logitech G535', 119.00, 'Audifonos Inalambricos Logitech Lightspeed G535 Blue', 'ImagenesProductos/audifonos_g535.jpg', 'Audio'),
(128, 7, 'Audífonos Logitech G733', 250.50, 'Audifonos Inalambricos Logitech Lightspeed G733 Lila', 'ImagenesProductos/audifonos_g733.jpg', 'Audio'),
(129, 10, 'Silla Gamer Razer Iskur', 449.99, 'Silla Profesional Razer Iskur v2 Light Gray', 'ImagenesProductos/silla_iskur.jpg', 'Muebles'),
(130, 10, 'Silla Ergonomica Thunder X3', 329.00, 'Silla Profesional Ergonomica Thunder X3 Light Blue', 'ImagenesProductos/silla_thunderx3.jpg', 'Muebles'),
(131, 10, 'Silla Gamer GuardianMesh', 299.00, 'Silla Profesional Aerocool Guardian Mesh Red-Black', 'ImagenesProductos/silla_aerocool.jpg', 'Muebles'),
(132, 10, 'Silla Gamer', 299.95, 'Silla ergonómica con soporte lumbar', 'ImagenesProductos/silla_aerocoolthunder.jpg', 'Muebles'),
(133, 3, 'Monitor MSI 27"', 129.00, 'Monitor MSI G274f de 27 Pulgadas 180Hz Full HD+', 'ImagenesProductos/monitor_g274f.jpg', 'Pantallas'),
(134, 3, 'Monitor Xiaomi 27	"', 109.95, 'Monitor Xiaomi G271i de 27 Pulgadas 165Hz HD+ IPS', 'ImagenesProductos/monitor_g271.jpg', 'Pantallas'),
(135, 3, 'Monitor Philips 26"', 299.99, 'Monitor Philips 278B1 de 26 Pulgadas 75Hz Resolucion 4K IPS', 'ImagenesProductos/monitor_278.jpg', 'Pantallas'),
(136, 3, 'Monitor MSI Mag 27"', 249.99, 'Monitor MSI Mag271 de 24 Pulgadas 240Hz Resolucion 2K con Pantalla OLED', 'ImagenesProductos/monitor_mag271.jpg', 'Pantallas');

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
