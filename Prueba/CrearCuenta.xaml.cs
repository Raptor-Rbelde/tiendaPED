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
    /// Lógica de interacción para CrearCuenta.xaml
    /// </summary>
    public partial class CrearCuenta : Window
    {

        public CrearCuenta()
        {
            InitializeComponent();
        }

        private void Minimizar(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            InicioSesion inicioSesion = new InicioSesion();
            this.Close();
            inicioSesion.Show();

            /*
            MainWindow mainWindow = new MainWindow(UsuarioSesion.IdUsuarioActual);
            this.Close();
            mainWindow.Show();
            */
        }

        private void Cerrar(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Crear cuenta
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string correo = txtCorreo.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Campos incompletos");
                return;
            }
            
            // Si el metodo booleano de verificar cuenta valida para crear es verdadero (es decir se pudo crear la cuenta)
            if (VerificarCuenta(correo, password))
            {
                MessageBox.Show("Cuenta Creada!");
                txtCorreo.Clear();
                txtPassword.Clear();
            }

        }

        public bool VerificarCuenta(string correo, string contrasenia)
        {
            // Using para no alterar el contexto de otras clases
            try
            {
                using (var context = new TiendaPedContext())
                {
                    // Verificar si el correo ya existe
                    if (context.Usuarios.Any(u => u.CorreoUsuario == correo))
                    {
                        MessageBox.Show("El correo ya está registrado");
                        return false;
                    }

                    var nuevoUsuario = new Usuario
                    {
                        CorreoUsuario = correo,
                        Contrasenia = contrasenia
                    };

                    context.Usuarios.Add(nuevoUsuario);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la cuenta: {ex.Message}");
                return false;
            }
        }




    }
}
