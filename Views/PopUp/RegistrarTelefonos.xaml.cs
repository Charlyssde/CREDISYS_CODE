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
        string selUno;
        string selDos;
        public RegistrarTelefonos(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
        }

        private void rbMovil_Checked(object sender, RoutedEventArgs e)
        {
            selUno = rbMovil.Content.ToString();
        }

        private void rbCasa_Checked(object sender, RoutedEventArgs e)
        {
            selUno = rbCasa.Content.ToString();
        }
        private void rbMovilDos_Checked(object sender, RoutedEventArgs e)
        {
            selDos = rbMovilDos.Content.ToString();
        }

        private void rbCasaDos_Checked(object sender, RoutedEventArgs e)
        {
            selDos = rbCasaDos.Content.ToString();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
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
                    telefono.tipoTelefono = selUno;

                    

                    Telefono telefono2 = new Telefono();
                    telefono2.estatus = "Activo";
                    telefono2.numero = txtNumeroDos.Text;
                    telefono2.rfcCliente = this.cliente.rfc;
                    telefono2.tipoTelefono = selDos;
                    db.Telefonoes.Add(telefono2);
                    db.Telefonoes.Add(telefono);
                    db.SaveChanges();

                    RegistrarTarjetas registrarTarjetas = new RegistrarTarjetas(cliente);
                    registrarTarjetas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    this.Hide();
                    registrarTarjetas.ShowDialog();
                    closeWindow();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private bool camposVacios()
        {
            return txtNumeroUno.Text.Equals("") || txtNumeroDos.Text.Equals("") || selUno == "" || selDos == "";
        }

        private void closeWindow()
        {
            this.Close();
        }

       
    }
}
