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
    /// Lógica de interacción para RegistrarReferencia2.xaml
    /// </summary>
    public partial class RegistrarReferencia2 : Window
    {
        Cliente clientenuevo;
        public RegistrarReferencia2(Cliente cliente)
        {
            InitializeComponent();
            clientenuevo = cliente;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txt_name1.Text.Equals("") || txtRelacion.Text.Equals("")
                         || txtDir.Text.Equals("") || txtTel.Text.Equals("") ||
                          txtHorario.Text.Equals(""))
                    {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {
                        Referencia nueva = new Referencia();
                        nueva.nombre = txt_name1.Text;
                        nueva.relacion = txtRelacion.Text;
                        nueva.telefono = txtTel.Text;
                        nueva.direccion = txtDir.Text;
                        nueva.horario = txtHorario.Text;
                        nueva.estatus = "activa";
                        nueva.rfcCliente = this.clientenuevo.rfc;


                        clientenuevo.Referencias = new List<Referencia>();

                        db.Referencias.Add(nueva);

                        db.SaveChanges();

                        RegistrarTelefonos regisTel = new RegistrarTelefonos(clientenuevo);
                        regisTel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        this.Hide();
                        regisTel.ShowDialog();

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
