using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tienda_Virtual.Estructuras;
using Tienda_Virtual.Models;
using ProductoModel = Tienda_Virtual.Models.Producto;


namespace Tienda_Virtual
{
    /// <summary>
    /// Lógica de interacción para Detalle1.xaml
    /// </summary>
    public partial class Detalle1 : Window
    {
        private ProductoModel _producto;
        public static List<int> carritoUsuario = new List<int>();
        private TiendaPedContext _context;


        public Detalle1(ProductoModel producto, TiendaPedContext context)
        {
            InitializeComponent();
            _producto = producto;
            _context = context;
            MostrarDatos();
        }


        private void MostrarDatos()
        {
            NombreProductoLabel.Text = _producto.NombreProducto;
            lblDescripcion.Text = _producto.Descripcion;
            lblPrecio.Text = $"Precio: {_producto.Precio}";
            try
            {
                if (!string.IsNullOrEmpty(_producto.RutaImagen))
                {
                    // Construir la ruta absoluta
                    string rutaAbsoluta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _producto.RutaImagen);

                    if (System.IO.File.Exists(rutaAbsoluta))
                    {
                        imagen.Source = new BitmapImage(new Uri(rutaAbsoluta, UriKind.Absolute));
                    }
                    else
                    {
                        // Imagen de respaldo
                        imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
                    }
                }
                else
                {
                    imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
                }
            }
            catch
            {
                imagen.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
            }
        }


        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var grafo = new GrafoProducto();
            int idProducto = _producto.IdProducto;
            string nombreProducto = _producto.NombreProducto;

            // Agregar al carrito
            carritoUsuario.Add(idProducto);

            // Actualizar el grafo
            foreach (var idAnterior in carritoUsuario.Where(id => id != idProducto))
            {
                string nombreAnterior = _context.Productos
                    .Where(p => p.IdProducto == idAnterior)
                    .Select(p => p.NombreProducto)
                    .FirstOrDefault();

                grafo.AgregarProducto(idProducto, nombreProducto);
                grafo.AgregarProducto(idAnterior, nombreAnterior);
                grafo.RelacionarProductos(idProducto, idAnterior);
            }

            MessageBox.Show("Producto agregado al carrito y relaciones actualizadas.");

            foreach (var producto in carritoUsuario)
            {
                MessageBox.Show(producto.ToString());
            }
        }
    }

}
