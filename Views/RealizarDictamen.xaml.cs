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
    /// Lógica de interacción para RealizarDictamen.xaml
    /// </summary>
    public partial class RealizarDictamen : Window
    {
        public bool porcentajeRealizado = false;
        private string resultado;

        private Solicitud solicitud;
        private Usuario usuario;

        public RealizarDictamen(Solicitud solicitud, Usuario usuario)
        {
            InitializeComponent();
            cargarInfo(solicitud, usuario);
        }

        /*
         * Cargar los campos con la información de la solicitud
         */
        private void cargarInfo(Solicitud solicitud, Usuario usuario)
        {
            this.solicitud = solicitud;
            this.usuario = usuario;
            lblFolio.Content = this.solicitud.folio;
            lblFecha.Content = this.solicitud.fecha;
            txtMontoLetra.Text = this.solicitud.montoLetra;
            txtMontoNumero.Text = this.solicitud.montoNumero.ToString();
            txtPlazo.Text = "12";
            using (DBEntities db = new DBEntities())
            {
                CondicionCredito c = db.CondicionCreditoes.Where(b => b.idCondicionCredito == this.solicitud.idCondicion).SingleOrDefault();
                txtInteres.Text = c.interes.ToString();
                txtIva.Text = c.iva.ToString();
            }
                
        }

        private void btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //Redirige a la pantalla para ver el expediente de la solicitud

            VisualizarExpediente visualizarExpediente = new VisualizarExpediente(this.solicitud);
            visualizarExpediente.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarExpediente.ShowDialog();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (!porcentajeRealizado && !mandarModificacion())
            {
                MessageBox.Show("No se ha realizado el cálculo de las políticas");
            }
            else
            {
                /*
                 * Se actualiza el estado de acuerdo a lo sucedido en el dictamen, y el cálculo de las políticas.
                 * Si algún elemento (informacion, expediente) está mal, se manda a corrección con el capturista.
                 * Sino, se le pone el resultado que viene de las políticas.
                 */
                using (DBEntities db = new DBEntities())
                {
                    Solicitud s = db.Solicituds.Where(b => b.folio == this.solicitud.folio).FirstOrDefault();
                    if (mandarModificacion())
                    {
                        s.estatus1 = "en espera";
                    }
                    else
                    {
                        s.estatus1 = this.resultado;
                    }
                    db.SaveChanges();
                    MessageBox.Show("Se ha actualizado correctamente el dictamen");
                }
            }
        }

        private bool mandarModificacion()
        {
            
            return rbIncorrectoExp.IsChecked == true || rbIncorrectoSol.IsChecked == true;
        }

        private void btnCalcularPoliticas_Click(object sender, RoutedEventArgs e)
        {
            CalcularPoliticas calcularPoliticas = new CalcularPoliticas(this);
            calcularPoliticas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            calcularPoliticas.ShowDialog();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            VisualizarSolicitudes visualizarSolicitudes = new VisualizarSolicitudes(this.usuario);
            visualizarSolicitudes.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarSolicitudes.Show();
            closeWindow();
        }

        private void closeWindow()
        {
            this.Close();
        }
        /*
         * Desde la ventana de cálculo de políticas se verifican dos cosas, que se haya realizado el cálculo
         * Y el estado con el que se determinó el resultado de la solicitud.
         */
        public void setResultado(String resultado)
        {
            this.resultado = resultado;
            if (this.resultado.Equals("rechazada"))
            {
                rbRechazarSolicitud.IsChecked = true;
                rbRechazarSolicitud.IsEnabled = false;
                rbAceptarSolicitud.IsEnabled = false;
                MessageBox.Show("La solicitud ha sido rechazada de forma automática por no cumplir las políticas","informacion");
            }
            else
            {
                MessageBox.Show("La solicitud ha pasado el porcentaje mínimo de políticas, " +
                    "puede aceptarla o rechazarla por otros motivos");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            VisualizarSolicitudes visualizarSolicitudes = new VisualizarSolicitudes(this.usuario);
            visualizarSolicitudes.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            visualizarSolicitudes.Show();
            this.Close();
        }

        private void rbAceptarSolicitud_Checked(object sender, RoutedEventArgs e)
        {
            this.resultado = "aceptada";
        }

        private void rbRechazarSolicitud_Checked(object sender, RoutedEventArgs e)
        {
            this.resultado = "rechazada";
        }
    }
}
