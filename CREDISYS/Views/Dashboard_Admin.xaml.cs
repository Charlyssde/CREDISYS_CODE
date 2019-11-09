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
        public DashboardAdmin(Usuario user)
        {
            InitializeComponent();
            lblNombre.Content = user.nombre;
            lblRol.Content = user.Rol.rol1;
        }

        private void btnAdminUsuarios_Click(object sender, RoutedEventArgs e)
        {
            AdministrarUsuarios admin = new AdministrarUsuarios();
            admin.Show();
            this.Close();
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
            MainWindow window = new MainWindow();
            window.WindowStartupLocation = this.WindowStartupLocation;
            window.Show();
            this.Close();
        }
    }
}
