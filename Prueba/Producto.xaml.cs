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

namespace Tienda_Virtual
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : Window
    {
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
            MainWindow mainWindow = new MainWindow();
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
    }
}
