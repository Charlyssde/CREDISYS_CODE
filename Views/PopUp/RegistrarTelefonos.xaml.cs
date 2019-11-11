using CREDISYS.Properties;
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

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para VisualizarTelefonos.xaml
    /// </summary>
    public partial class RegistrarTelefonos : Window
    {
        Cliente cliente;
        public RegistrarTelefonos(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
        }

        private void rbMovil_Checked(object sender, RoutedEventArgs e)
        {
            rbCasaDos.IsChecked = true;
        }

        private void rbCasa_Checked(object sender, RoutedEventArgs e)
        {
            rbMovilDos.IsChecked = true;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (camposVacios())
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                Telefono telefono = new Telefono();
                telefono.estatus = "Activo";
                telefono.numero = txtNumeroUno.Text;
                telefono.rfcCliente = this.cliente.rfc;
                if (rbCasa.IsChecked ==  true)
                {
                    telefono.tipoTelefono = "casa";
                }
                else
                {
                    telefono.tipoTelefono = "movil";
                }
                this.cliente.Telefonoes.Add(telefono);

                telefono.numero = txtNumeroDos.Text;
                if (rbCasa.IsChecked == true)
                {
                    telefono.tipoTelefono = "movil";
                }
                else
                {
                    telefono.tipoTelefono = "casa";
                }
                this.cliente.Telefonoes.Add(telefono);

                RegistrarTarjetas registrarTarjetas = new RegistrarTarjetas(this.cliente);
                registrarTarjetas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                registrarTarjetas.Show();
                closeWindow();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private bool camposVacios()
        {
            return txtNumeroUno.Text.Equals("") || txtNumeroDos.Text.Equals("");
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
