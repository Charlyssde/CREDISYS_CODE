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
        Referencia referencia;
        Referencia referencia2;
        public RegistrarReferencia2(Referencia referencia, Referencia referencia2, Cliente cliente)
        {
            InitializeComponent();
           
            clientenuevo = cliente;
            
            this.referencia = referencia;
            this.referencia2 = referencia2;
            txt_name1.Text = referencia.nombre;
            txtRelacion.Text = referencia.relacion;
            txtDir.Text = referencia.direccion;
            txtTel.Text = referencia.telefono;
            txtHorario.Text = referencia.horario;
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
                        Referencia nueva = new Referencia { idReferencia = referencia.idReferencia };
                        db.Referencias.Attach(nueva);
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

                        modificarReferencia modificarReferencia2 = new modificarReferencia(referencia2, referencia,clientenuevo);
                        modificarReferencia2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        this.Hide();
                        modificarReferencia2.ShowDialog();

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
