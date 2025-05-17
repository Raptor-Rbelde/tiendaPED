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
using Tienda_Virtual.Estructuras;
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
                .Where(p => SesionActual.CarritoUsuario.Contains(p.IdProducto))
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
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual, SesionActual.HistorialBusquedas);

            this.Close();
            mainWindow.Show();
        }

        private void lstCarrito_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnFinalizar(object sender, RoutedEventArgs e)
        {

        }


        //Eliminar un elemento de la lista del carrito

        private void BtnEliminarProd(object sender, RoutedEventArgs e)
        {
            if (lstCarrito.SelectedItem != null)
            {
                string itemSeleccionado = lstCarrito.SelectedItem.ToString();

                // Buscar el ID del producto basándose en el nombre
                var producto = _context.Productos
                    .FirstOrDefault(p => itemSeleccionado.Contains(p.NombreProducto) && SesionActual.CarritoUsuario.Contains(p.IdProducto));

                if (producto != null)
                {
                    // Mostrar mensaje de confirmación
                    MessageBoxResult resultado = MessageBox.Show(
                        $"¿Está seguro que desea eliminar el producto \"{producto.NombreProducto}\" del carrito?",
                        "Confirmar eliminación",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (resultado == MessageBoxResult.Yes)
                    {
                        // Eliminar del carrito
                        SesionActual.CarritoUsuario.Remove(producto.IdProducto);

                        // Actualizar vista
                        MostrarProductosEnCarrito();
                    }
                    // Si elige No, no se hace nada
                }
            }
            else
            {
                MessageBox.Show("Selecciona un producto para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
