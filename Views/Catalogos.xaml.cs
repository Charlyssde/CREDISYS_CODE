using CREDISYS.Views.Catalogos;
using CREDISYS.Views.CatalogosFolder;
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

namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para Catalogos.xaml
    /// </summary>
    public partial class CatalogosC : Window
    {
        Usuario usuario;
        public CatalogosC(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DashboardAdmin dashboardAdmin = new DashboardAdmin(this.usuario);
            dashboardAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dashboardAdmin.Show();
            this.Close();
        }

        private void btnPaises_Click(object sender, RoutedEventArgs e)
        {
            VisualizarPaises visualizarPaises = new VisualizarPaises(this.usuario);
            visualizarPaises.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarPaises.Show();
            this.Close();
        }

        private void btnEstados_Click(object sender, RoutedEventArgs e)
        {
            VisualizarEstados visualizarEstados = new VisualizarEstados(this.usuario);
            visualizarEstados.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarEstados.Show();
            this.Close();
        }

        private void btnCiudades_Click(object sender, RoutedEventArgs e)
        {
            VisualizarCiudades visualizarCiudades = new VisualizarCiudades(this.usuario);
            visualizarCiudades.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarCiudades.Show();
            this.Close();
        }

        private void btnPoliticas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCondiciones_Click(object sender, RoutedEventArgs e)
        {
            VisualizarCondiciones visualizarCondiciones = new VisualizarCondiciones(this.usuario);
            visualizarCondiciones.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarCondiciones.Show();
            this.Close();
        }

        private void btnCatalogos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
