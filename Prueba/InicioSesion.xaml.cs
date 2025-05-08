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
    /// Lógica de interacción para InicioSesion.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        public InicioSesion()
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
           

            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        // Boton para Iniciar sesion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string correoUser = txtCorreo.Text.Trim().ToLower();
            string passwordUser = txtPassword.Password;

            var user = App.context.Usuarios.FirstOrDefault(u => u.CorreoUsuario.ToLower() == correoUser && u.Contrasenia == passwordUser);

            if (user != null)
            {
                MessageBox.Show("Exito");
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        // Boton para Crear cuenta
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CrearCuenta crear = new CrearCuenta();
            this.Close();
            crear.Show();
        }
    }
}
