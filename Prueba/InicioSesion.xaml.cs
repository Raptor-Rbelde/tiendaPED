using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using Tienda_Virtual.Models;

namespace Tienda_Virtual
{
    public static class UsuarioSesion
    {
        public static int IdUsuarioActual { get; set; }
    }

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
            Application.Current.Shutdown();
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
        }

        // Botón de iniciar sesión
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string correoUser = txtCorreo.Text.Trim().ToLower();
            string passwordUser = txtPassword.Password;

            var user = App.context.Usuarios
                .FirstOrDefault(u => u.CorreoUsuario.ToLower() == correoUser && u.Contrasenia == passwordUser);

            if (user != null)
            {
                MessageBox.Show("Inicio de sesión exitoso");

                // Guardar el ID del usuario para toda la aplicación
                UsuarioSesion.IdUsuarioActual = user.IdUsuario;

                // Abrir la ventana principal pasando el ID
                MainWindow mw = new MainWindow(user.IdUsuario);
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos");
            }
        }

        // Botón para crear cuenta
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CrearCuenta crear = new CrearCuenta();
            this.Close();
            crear.Show();
        }
    }
}
