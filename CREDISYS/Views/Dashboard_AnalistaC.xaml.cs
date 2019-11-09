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
            cargarDatos(user);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {
            VisualizarSolicitudes visualizarSolicitudes = new VisualizarSolicitudes(usuario);
            visualizarSolicitudes.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarSolicitudes.Show();
            closeWindow();
        }

        private void cargarDatos(Usuario user)
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
