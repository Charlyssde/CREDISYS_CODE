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
    /// Lógica de interacción para Dashboard_AnalistaC.xaml
    /// </summary>
    public partial class Dashboard_AnalistaC : Window
    {
        Usuario usuario;

        public Dashboard_AnalistaC(Usuario user)
        {
            InitializeComponent();
            usuario = user;
            cargarDatos(user);
        }

        private void cargarDatos(Usuario user)
        {
            lblNombre.Content = user.nombre;
            lblRol.Content = user.Rol.rol1;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {
            VisualizarSolicitudes visualizarSolicitudes = new VisualizarSolicitudes(usuario);
            visualizarSolicitudes.WindowStartupLocation = this.WindowStartupLocation;
            visualizarSolicitudes.Show();
            this.Close();
        }
    }
}
