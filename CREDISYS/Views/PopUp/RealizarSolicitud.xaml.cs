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
    /// Lógica de interacción para RealizarSolicitud.xaml
    /// </summary>
    public partial class RealizarSolicitud : Window
    {
        private List<CondicionCredito> condiciones;
        private int plazos = 12;

        public RealizarSolicitud()
        {
            InitializeComponent();
            cargarTasas();
            txtPlazos.Text = plazos.ToString();
        }

        private void cargarTasas()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    condiciones = db.CondicionCreditoes.ToList<CondicionCredito>();      
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (camposVacios() == true)
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                Solicitud solicitud = new Solicitud();
                CloseWindow();
            }

        }

        private void rbTasaCero_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CondicionCredito condicion in condiciones)
            {
                if (condicion.condicion.Equals("tasa cero"))
                {
                    txtIva.Text = condicion.iva.ToString();
                    txtInterés.Text = condicion.interes.ToString();
                }
            }
        }

        private void rbTasaCinco_Checked(object sender, RoutedEventArgs e)
        {

            foreach (CondicionCredito condicion in condiciones)
            {
                if (condicion.condicion.Equals("tasa cinco %"))
                {
                    txtIva.Text = condicion.iva.ToString();
                    txtInterés.Text = condicion.interes.ToString();
                }
            }
        }

        private bool camposVacios()
        {
            if (txtMontoLetra.Text.Equals("") || txtMontoNumero.Text.Equals("") || txtDestino.Text.Equals(""))
            {
                if (rbTasaCero.IsChecked == false && rbTasaCinco.IsChecked == false)
                {
                    return true;
                }
                return true;
            }
            return false;
        }

        private void CloseWindow()
        {
            this.Close();
        }
    }
}
