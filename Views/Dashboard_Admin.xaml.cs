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
using CREDISYS.Properties;


namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class DashboardAdmin: Window
    {
        Usuario usuario;
        public DashboardAdmin(Usuario user)
        {
            InitializeComponent();
            cargarInfo(user);
        }

        private void btnAdminUsuarios_Click(object sender, RoutedEventArgs e)
        {
            AdministrarUsuarios admin = new AdministrarUsuarios(this.usuario, this);
            admin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            admin.Show();
            closeWindow();
        }

        private void btnAdminCatalogos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCobranza_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSolicitud_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCliente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void cargarInfo(Usuario user)
        {
            this.usuario = user;
            lblNombre.Content = this.usuario.nombre;
            lblRol.Content = this.usuario.Rol.rol1;
        }
        private void closeWindow()
        {
            this.Close();
        }
    }
}
