using CREDISYS.Views.PopUp;
using System;
using CREDISYS.Properties;
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            consultar();
        }

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente2 registrarCliente2 = new RegistrarCliente2();
            registrarCliente2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registrarCliente2.ShowDialog();
        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {
            //Para ver el listado de las solicitudes que tiene un cliente seleccionado
        }

        private void btnAgregarSolicitud_Click(object sender, RoutedEventArgs e)
        {
            RealizarSolicitud realizarSolicitud = new RealizarSolicitud(this.usuario, this.cliente);
            realizarSolicitud.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            realizarSolicitud.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_Capturista cap= new Dashboard_Capturista(this.usuario);
            cap.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cap.Show();
            closeWindow();
        }
        private void txt_Busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
        private void consultar()
        {
            if (txtBusqueda.Text == "")
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {

                        cliente = db.Clientes.Where(b => b.rfc == txtBusqueda.Text).FirstOrDefault();
                        if (cliente != null)
                        {
                            txtnombre.Text = cliente.nombre.ToString();
                            txtapellidopaterno.Text = cliente.apellidoPaterno.ToString();
                            txtapellidomaterno.Text = cliente.apellidoMaterno.ToString();
                            btnVisualiCliente.IsEnabled = true;
                        } else
                        {
                            MessageBox.Show(Settings.Default.MensajeElementoNoEcontrado);
                            btnAgregarCliente.IsEnabled = true;
                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                }
            }

        }
        private void closeWindow()
        {
            this.Close();
        }

        private void dg_Cliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cliente!=null)
            {
                btnVisualiCliente.IsEnabled = true;
                btnAgregarSolicitud.IsEnabled = false;
            }
            else
            {
                this.cliente = null;
                btnAgregarSolicitud.IsEnabled = true;
               
               
            }
        }

        private void btnVisualiCliente_Click(object sender, RoutedEventArgs e)
        {
           
            VisualizarCliente vzcliente = new VisualizarCliente(cliente);
            vzcliente.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            vzcliente.Show();
            
            
        }

            
    }
}
