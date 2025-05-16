using Microsoft.EntityFrameworkCore;
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
using Tienda_Virtual.Models;

namespace Tienda_Virtual
{
    /// <summary>
    /// Lógica de interacción para Compra.xaml
    /// </summary>
    public partial class Compra : Window
    {
        private TiendaPedContext _context;
        public Compra()
        {
            InitializeComponent();
            MostrarProductosEnCarrito();

        }

        public Compra(TiendaPedContext context)
        {
            InitializeComponent();
            this._context = context;
            MostrarProductosEnCarrito();
        }

        private void MostrarProductosEnCarrito()
        {
            List<string> producto = new List<string>();
            // Cargar los productos en el carrito usando sus IDs
            //var productosEnCarrito = Detalle1.carritoUsuario
            //    .Select(id => _context.Productos.FirstOrDefault(p => p.IdProducto == id))
            //    .Where(p => p != null)
            //    .ToList();

            var productosEnCarrito = _context.Productos
                .Where(p => Detalle1.carritoUsuario.Contains(p.IdProducto))
                .ToList();

            // Mostrar los nombres de los productos en un ListBox (lstCarrito)
            //lstCarrito.ItemsSource = productosEnCarrito.Select(p => $"{p.NombreProducto} - Precio: {p.Precio:C}");

            if (productosEnCarrito.Any())
            {
                lstCarrito.ItemsSource = productosEnCarrito
                    .Select(p => $"{p.NombreProducto} - Precio: {p.Precio:C}")
                    .ToList();
            }
            else
            {
                lstCarrito.ItemsSource = new List<string> { "No hay productos en el carrito." };
            }

        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
        }

        private void lstCarrito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnFinalizar(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEliminarProd(object sender, RoutedEventArgs e)
        {

        }
    }
}
