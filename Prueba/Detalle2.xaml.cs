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
    /// Lógica de interacción para Detalle2.xaml
    /// </summary>
    public partial class Detalle2 : Window
    {
        public Detalle2()
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

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
        }
    }
}
