using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Windows;
using Tienda_Virtual.Models;

namespace Tienda_Virtual
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        public static TiendaPedContext context {  get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var connectionString = @"Server=localhost;Database=TiendaPED;Trusted_Connection=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<TiendaPedContext>()
                .UseSqlServer(connectionString)
            .Options;

            context = new TiendaPedContext(options);


            // Verificar la conexión
            try
            {
                if (context.Database.CanConnect())
                {
                    MessageBox.Show("Conexión a la base de datos exitosa.");
                }
                else
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }


        }
    }

}
