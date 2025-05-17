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
    public partial class MainWindow : Window
    {
        private int _idUsuario;


        public MainWindow(int idUsuario, ListaEnlazada historialExistente = null)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            if (historialExistente != null)
                SesionActual.HistorialBusquedas = historialExistente;
            else
                SesionActual.HistorialBusquedas = new ListaEnlazada();

            CargarRecomendados();
            MostrarHistorial();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string textoBusqueda = Busqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                MessageBox.Show("Ingresa un término de búsqueda.");
                return;
            }

            // Aquí se buscara el producto real en la base de datos:
            var context = new TiendaPedContext();
            var productoBuscado = context.Productos.FirstOrDefault(p => p.NombreProducto.Contains(textoBusqueda));

            if (productoBuscado != null)
            {
                // Agregar el producto al historial
                SesionActual.HistorialBusquedas.Agregar(productoBuscado);

                // Mostrar cuántos productos hay en el historial
                int contar = 0;
                NodoLista actual = SesionActual.HistorialBusquedas.inicio;
                while (actual != null)
                {
                    contar++;
                    actual = actual.siguiente;
                }

                // Actualizar el historial en pantalla
                MostrarHistorial();
            }

            var productosVentana = new Producto(textoBusqueda, SesionActual.HistorialBusquedas);
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


        private void BtnCarrito_Click(object sender, RoutedEventArgs e)
        {
            Compra carritoWindow = new Compra(new TiendaPedContext());
            this.Close();
            carritoWindow.Show();
        }

        private void CargarRecomendados()
        {
            var context = new TiendaPedContext();

            // Registrar todos los productos en el grafo global (por si no están)
            foreach (var p in context.Productos.ToList())
            {
                SesionActual.Grafo.AgregarProducto(p.IdProducto, p.NombreProducto);
            }

            // Obtener recomendaciones usando el grafo global
            var servicio = new ServicioDeRecomendacion(context, SesionActual.Grafo);
            var recomendados = servicio.ObtenerRecomendacionesParaUsuario(_idUsuario);

            MostrarRecomendadosEnPantalla(recomendados);
        }

        private void MostrarRecomendadosEnPantalla(List<NodoGrafo> productos)
        {
            // Panel contenedor
            WrapPanel contenedor = new WrapPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10)
            };

            var context = new TiendaPedContext();   // 1 sola instancia para todo el bucle

            foreach (var nodo in productos.Take(3))               // solo 3 recomendados
            {
                // Recuperar el producto completo
                var prod = context.Productos.FirstOrDefault(p => p.IdProducto == nodo.ID);
                if (prod == null) continue;                       // seguridad

                StackPanel item = new StackPanel
                {
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // ----- Imagen -------------------------------------------------------
                Image img = new Image
                {
                    Width = 150,
                    Height = 100
                };

                try
                {
                    if (!string.IsNullOrEmpty(prod.RutaImagen))
                    {
                        string rutaAbsoluta = System.IO.Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory, prod.RutaImagen);

                        if (System.IO.File.Exists(rutaAbsoluta))
                            img.Source = new BitmapImage(new Uri(rutaAbsoluta, UriKind.Absolute));
                        else                                            // fallback si no existe
                            img.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
                    }
                }
                catch
                {
                    img.Source = new BitmapImage(new Uri("pack://application:,,,/SCS/IMG/Compu.jpeg"));
                }
                // --------------------------------------------------------------------

                Button boton = new Button
                {
                    Content = "Ver detalles",
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(0, 5, 0, 0),
                    Background = new SolidColorBrush(Color.FromRgb(58, 109, 140)),
                    Foreground = Brushes.White
                };

                boton.Click += (_, __) =>
                {
                    Detalle1 detalle = new Detalle1(prod, context);
                    this.Hide();
                    detalle.Show();
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

            NodoLista nodoActual = SesionActual.HistorialBusquedas.inicio;
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