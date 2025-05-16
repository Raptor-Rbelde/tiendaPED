using Microsoft.EntityFrameworkCore;
using System.DirectoryServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tienda_Virtual.Estructuras;
using Tienda_Virtual.Models;
using ProductoModel = Tienda_Virtual.Models.Producto;


namespace Tienda_Virtual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<int> carritoUsuario = new List<int>();
        private int _idUsuario;
        private ListaEnlazada historialBusquedas = new ListaEnlazada();

        public MainWindow(int idUsuario, ListaEnlazada historialExistente = null)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            if (historialExistente != null)
                historialBusquedas = historialExistente;
            else
                historialBusquedas = new ListaEnlazada();

            CargarRecomendados();
            MostrarHistorial();
        }


        private void TB(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = Busqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                MessageBox.Show("Ingresa un término de búsqueda.");
                return;
            }

            // Aquí deberías buscar el producto real en la base de datos:
            var context = new TiendaPedContext();
            var productoBuscado = context.Productos.FirstOrDefault(p => p.NombreProducto.Contains(textoBusqueda));

            if (productoBuscado != null)
            {
                // Agregar el producto al historial
                historialBusquedas.Agregar(productoBuscado);

                // Mostrar cuántos productos hay en el historial
                int contar = 0;
                NodoLista actual = historialBusquedas.inicio;
                while (actual != null)
                {
                    contar++;
                    actual = actual.siguiente;
                }

                // Actualizar el historial en pantalla
                MostrarHistorial();
            }

            var productosVentana = new Producto(textoBusqueda, historialBusquedas);
            this.Hide();
            productosVentana.Show();



        }

        private void Busqueda_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }



        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {
            InicioSesion inicioSesion = new InicioSesion();

            this.Close();
            inicioSesion.Show();
        }

        //private void BtnDetalle1_Click(object sender, RoutedEventArgs e)
        //{
        //    var productoEjemplo = new ProductoModel
        //    {
        //        //IdProducto = 101,
        //        //IdUsuario = 1,
        //        //NombreProducto = "Mouse Logitech",
        //        //Precio = 150,
        //        //Descripcion = "Mouse ergonómico con sensor óptico avanzado"
        //    };

        //    Detalle1 detalle1 = new Detalle1(productoEjemplo);
        //    this.Close();
        //    detalle1.Show();
        //}


        //private void BtnDetalle2_Click(object sender, RoutedEventArgs e)
        //{
        //    var productoEjemplo = new ProductoModel
        //    {
        //        //IdProducto = 101,
        //        //IdUsuario = 1,
        //        //NombreProducto = "Mouse Logitech",
        //        //Precio = 150,
        //        //Descripcion = "Mouse ergonómico con sensor óptico avanzado"
        //    };

        //    Detalle1 detalle1 = new Detalle1(productoEjemplo);
        //    this.Close();
        //    detalle1.Show();
        //}

        private void BtnCarrito_Click(object sender, RoutedEventArgs e)
        {
            Compra carritoWindow = new Compra(new TiendaPedContext());
            this.Close();
            carritoWindow.Show();
            //carritoWindow.ShowDialog();
        }

        //private void BtnDetalle3_Click(object sender, RoutedEventArgs e)
        //{
        //    var productoEjemplo = new ProductoModel
        //    {
        //        //IdProducto = 101,
        //        //IdUsuario = 1,
        //        //NombreProducto = "Mouse Logitech",
        //        //Precio = 150,
        //        //Descripcion = "Mouse ergonómico con sensor óptico avanzado"
        //    };

        //    Detalle1 detalle1 = new Detalle1(productoEjemplo);
        //    this.Close();
        //    detalle1.Show();
        //}


        // Metodo que usa el grafo con recomendaciones reales
        private void CargarRecomendados()
        {
            var context = new TiendaPedContext();

            // Construir el grafo
            var grafo = new GrafoProducto();

            // Agregar todos los productos al grafo
            var productos = context.Productos.ToList();
            foreach (var p in productos)
            {
                grafo.AgregarProducto(p.IdProducto, p.NombreProducto);
            }


            //var p1 = context.Productos.FirstOrDefault(p => p.IdProducto == 101); // Mouse Logitech
            //var p2 = context.Productos.FirstOrDefault(p => p.IdProducto == 102); // Teclado Mecánico

            //if (p1 != null && p2 != null)
            //{
            //    // Agregar ambos productos al grafo por si aún no estaban
            //    grafo.AgregarProducto(p1.IdProducto, p1.NombreProducto);
            //    grafo.AgregarProducto(p2.IdProducto, p2.NombreProducto);

            //    // Simular una relación directa entre ellos
            //    grafo.RelacionarProductos(p1.IdProducto, p2.IdProducto);
            //}


            // Obtener recomendaciones
            var servicio = new ServicioDeRecomendacion(context, grafo);
            var recomendados = servicio.ObtenerRecomendacionesParaUsuario(_idUsuario);

            // Mostrar en la interfaz
            MostrarRecomendadosEnPantalla(recomendados);
        }


        private void MostrarRecomendadosEnPantalla(List<NodoGrafo> productos)
        {
            WrapPanel contenedor = new WrapPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };



            foreach (var prod in productos.Take(3)) // solo mostrar los primeros 3
            {
                StackPanel item = new StackPanel
                {
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var img = new Image
                {
                    Source = new BitmapImage(new Uri("/SCS/IMG/Compu.jpeg", UriKind.Relative)),
                    Width = 150,
                    Height = 100
                };

                var boton = new Button
                {
                    Content = "Ver detalles",
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(0, 5, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(58, 109, 140)),
                    Foreground = Brushes.White
                };

                boton.Click += (s, e) =>
                {
                    // Para abrir el detalle, necesitas recuperar el objeto completo desde el ID
                    var context = new TiendaPedContext();
                    var productoOriginal = context.Productos.FirstOrDefault(p => p.IdProducto == prod.ID);

                    if (productoOriginal != null)
                    {
                        Detalle1 detalle = new Detalle1(productoOriginal, context, historialBusquedas);
                        this.Close();
                        detalle.Show();
                    }
                };

                item.Children.Add(img);
                item.Children.Add(boton);
                contenedor.Children.Add(item);
            }

            PanelRecomendados.Children.Clear();
            PanelRecomendados.Children.Add(contenedor);


        }

        private void MostrarHistorial()
        {
            // Obtener los productos más buscados (con contador) ordenados y limitar a 3
            List<(ProductoModel producto, int contador)> productosContados = new List<(ProductoModel, int)>();

            NodoLista nodoActual = historialBusquedas.inicio;
            while (nodoActual != null)
            {
                productosContados.Add((nodoActual.producto, nodoActual.vistas));// Asumo que tienes contador
                nodoActual = nodoActual.siguiente;
            }

            var top3 = productosContados.OrderByDescending(pc => pc.contador).Take(3).ToList();

            WrapPanel contenedorHistorial = new WrapPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10)
            };

            foreach (var item in top3)
            {
                var prod = item.producto;

                StackPanel itemPanel = new StackPanel
                {
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Image imagen = new Image
                {
                    Width = 150,
                    Height = 100
                };

                try
                {
                    if (!string.IsNullOrEmpty(prod.RutaImagen))
                    {
                        string rutaAbsoluta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, prod.RutaImagen);

                        if (System.IO.File.Exists(rutaAbsoluta))
                        {
                            imagen.Source = new BitmapImage(new Uri(rutaAbsoluta, UriKind.Absolute));
                        }
                        else
                        {
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

                //// Botón para ver detalle si quieres
                //Button boton = new Button
                //{
                //    Content = "Ver detalles",
                //    Width = 100,
                //    Height = 30,
                //    Margin = new Thickness(0, 5, 0, 0),
                //    Background = new SolidColorBrush(Color.FromRgb(58, 109, 140)),
                //    Foreground = Brushes.White
                //};

                //boton.Click += (s, e) =>
                //{
                //    var context = new TiendaPedContext();
                //    var productoOriginal = context.Productos.FirstOrDefault(p => p.IdProducto == prod.IdProducto);
                //    if (productoOriginal != null)
                //    {
                //        Detalle1 detalle = new Detalle1(productoOriginal, context);
                //        this.Close();
                //        detalle.Show();
                //    }
                //};
                //itemPanel.Children.Add(boton);

                itemPanel.Children.Add(imagen);

                TextBlock nombreProducto = new TextBlock
                {
                    Text = prod.NombreProducto,
                    FontSize = 14,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 0),
                    TextWrapping = TextWrapping.Wrap,
                    Width = 100,
                    Height=40
                };

                itemPanel.Children.Add(nombreProducto);



                contenedorHistorial.Children.Add(itemPanel);
            }

            PanelHistorial.Children.Clear();
            PanelHistorial.Children.Add(contenedorHistorial);
        }

    }
}