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
    /// Lógica de interacción para RegistroContacto.xaml
    /// </summary>
    public partial class RegistroContacto : Window
    {
        Cliente cliente;
        public RegistroContacto(Cliente nuevo)
        {
            InitializeComponent();
            cliente = nuevo;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txtCorreo.Text.Equals("") == null)
                    {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {
                        Correo nuevo = new Correo();
                        nuevo.correo1 = txtCorreo.Text;
                        nuevo.rfcCliente = cliente.rfc;
                        nuevo.estatus = "activo";

                        db.Correos.Add(nuevo);
                        db.SaveChanges();
                        MessageBox.Show(Settings.Default.MensajeExito);
                        RegistrarEmpleo registrarempleo = new RegistrarEmpleo(cliente);
                        registrarempleo.WindowStartupLocation = this.WindowStartupLocation;
                        registrarempleo.Show();
                        closeWindow();
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }
    }
}
