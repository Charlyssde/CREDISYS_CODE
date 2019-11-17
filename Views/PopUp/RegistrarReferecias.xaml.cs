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
    /// Lógica de interacción para RegistrarReferecias.xaml
    /// </summary>
    public partial class RegistrarReferecias : Window
    {
        Cliente clientenuevo;
        public RegistrarReferecias(Cliente clientenuevo)
        {
            InitializeComponent();
            this.clientenuevo = clientenuevo;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using(DBEntities db = new DBEntities())
            {
                try
                {
                    if (txt_name1.Text.Equals("") || txt_name2.Text.Equals("") || txtRelacion.Text.Equals("")
                         || txtRelacion2.Text.Equals("") || txtDir.Text.Equals("") || txtDir2.Text.Equals("") || txtTel.Text.Equals("") ||
                         txtTel2.Text.Equals("") || txtHorario.Text.Equals("") || txtHorario2.Text.Equals(""))
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

                        Referencia nueva2 = new Referencia();
                        nueva.nombre = txt_name2.Text;
                        nueva.relacion = txtRelacion2.Text;
                        nueva.telefono = txtTel2.Text;
                        nueva.direccion = txtDir2.Text;
                        nueva.horario = txtHorario2.Text;
                        clientenuevo.Referencias = new List<Referencia>();
                        clientenuevo.Referencias.Add(nueva);
                        clientenuevo.Referencias.Add(nueva2);
                        db.Referencias.Add(nueva);
                        db.Referencias.Add(nueva2);
                        db.SaveChanges();

                        RegistrarTelefonos regisTel = new RegistrarTelefonos(clientenuevo);
                        regisTel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        regisTel.ShowDialog();
                        
                        closeWindow();
                    }
                } catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                }
             }
        }
        private void closeWindow()
        {
            this.Close();
        }
    }
}
