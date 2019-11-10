using CREDISYS.Views.PopUp;
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
    /// Lógica de interacción para BuscarCliente.xaml
    /// </summary>
    public partial class BuscarCliente : Window
    {
        Cliente cliente = null;
        Usuario usuario;
        public BuscarCliente(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregarSolicitud_Click(object sender, RoutedEventArgs e)
        {
            RealizarSolicitud realizarSolicitud = new RealizarSolicitud(this.usuario, this.cliente);
            realizarSolicitud.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            realizarSolicitud.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
