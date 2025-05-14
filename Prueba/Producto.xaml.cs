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
using ProductoModel = Tienda_Virtual.Models.Producto;


namespace Tienda_Virtual
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : Window
    {

        private string _textoBusqueda;
        private TiendaPedContext _context = new TiendaPedContext();

        public Producto(string textoBusqueda)
        {
            InitializeComponent();
            _textoBusqueda = textoBusqueda;
            CargarResultados();
        }

        public Producto()
        {
            InitializeComponent();
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btnregresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
        }

        private void BtnDetalle1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDetalle2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDetalle3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Busqueda_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CargarResultados()
        {
            var resultados = _context.Productos
                .Where(p => p.NombreProducto.Contains(_textoBusqueda))
                .ToList();

            if (resultados.Count == 0)
            {
                MessageBox.Show("No se encontraron productos.");
                return;
            }

            MostrarProductosEnPantalla(resultados);
        }

        private void MostrarProductosEnPantalla(List<ProductoModel> productos)
        {
            // Limpiar cualquier contenido anterior (como resultados estáticos o duplicados)
            GridContent.Children.Clear();

            // Contenedor principal de los productos encontrados
            WrapPanel contenedor = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10)
            };

            foreach (var prod in productos)
            {
                // Contenedor individual de cada producto
                StackPanel item = new StackPanel
                {
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Imagen del producto (por ahora genérica)
                Image imagen = new Image
                {
                    Source = new BitmapImage(new Uri("/SCS/IMG/Compu.jpeg", UriKind.Relative)),
                    Width = 150,
                    Height = 100
                };

                // Botón de ver detalles
                Button boton = new Button
                {
                    Content = "Ver detalles",
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(0, 5, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(58, 109, 140)),
                    Foreground = Brushes.White
                };

                // Asociar el producto al botón para enviarlo al detalle
                boton.Click += (s, e) =>
                {
                    Detalle1 detalle = new Detalle1(prod);
                    this.Close();
                    detalle.Show();
                };

                // Agregar elementos al contenedor individual
                item.Children.Add(imagen);
                item.Children.Add(boton);

                // Agregar item al contenedor global
                contenedor.Children.Add(item);
            }

            // Agregar todo al grid principal de resultados
            GridContent.Children.Add(contenedor);
        }


    }
}
