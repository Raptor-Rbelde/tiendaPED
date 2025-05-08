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

namespace Tienda_Virtual
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TB(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Producto producto = new Producto();
            this.Close();
            producto.Show();
           
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

        private void BtnDetalle1_Click(object sender, RoutedEventArgs e)
        {
            Detalle1 detalle1 = new Detalle1();
            this.Close();
            detalle1.Show();
        }

        private void BtnDetalle2_Click(object sender, RoutedEventArgs e)
        {
            Detalle2 detalle2 = new Detalle2();
            this.Close();
            detalle2.Show();
        }

        private void BtnCarrito_Click(object sender, RoutedEventArgs e)
        {
            Compra compra = new Compra();
            this.Close();
            compra.Show();
        }

        private void BtnDetalle3_Click(object sender, RoutedEventArgs e)
        {
            Detalle3 detalle3 = new Detalle3();
            this.Close();
            detalle3.Show();
        }
    }
}