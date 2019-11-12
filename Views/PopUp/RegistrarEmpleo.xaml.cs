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
    /// Lógica de interacción para RegistrarEmpleo.xaml
    /// </summary>
    public partial class RegistrarEmpleo : Window
    {
        Cliente clientenuevo;
        public RegistrarEmpleo(Cliente cliente)
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
                    if (txt_numero.Text.Equals("") || txt_antiguedad.Text.Equals("") || txt_empresa.Text.Equals("")
                         || txt_centro.Text.Equals("") || txt_puesto.Text.Equals("") || txt_ocupacion.Text.Equals("")
                         || txt_periodo.Text.Equals(""))
                     {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {


                        Empleo nuevo = new Empleo();
                        nuevo.centroDeTrabajo = txt_centro.Text;
                        nuevo.nombreEmpresa = txt_empresa.Text;
                        int antiguedad = Int32.Parse(txt_antiguedad.Text);
                        nuevo.antiguedadMeses = antiguedad;
                        int numero = Int32.Parse(txt_numero.Text);
                        nuevo.numEmpleado = numero;
                        nuevo.puesto = txt_puesto.Text;
                        nuevo.ocupacion = txt_ocupacion.Text;
                        nuevo.periodoPresentacion = txt_periodo.Text;
                        nuevo.estatus = "activo";
                        nuevo.rfcCliente = clientenuevo.rfc;

                        this.clientenuevo.idEmpleo = nuevo.idEmpleo;
                        this.clientenuevo.Empleo = nuevo;

                        MessageBox.Show(Settings.Default.MensajeExito);
                        RegistrarReferecias registrarReferencias = new RegistrarReferecias(clientenuevo);
                        registrarReferencias.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        registrarReferencias.ShowDialog();
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
