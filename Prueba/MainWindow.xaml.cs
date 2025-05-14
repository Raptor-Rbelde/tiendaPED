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

        private int _idUsuario;
        public MainWindow(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            CargarRecomendados();
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

            // Crear la ventana de resultados y pasarle el texto
            var productosVentana = new Producto(textoBusqueda);
            this.Close();
            productosVentana.Show();

        }

        private void Busqueda_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }

    

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Close();
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
            Compra compra = new Compra();
            this.Close();
            compra.Show();
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
                        Detalle1 detalle = new Detalle1(productoOriginal);
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


    }
}