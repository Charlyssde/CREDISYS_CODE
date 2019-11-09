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

        private void cargarInfo(Solicitud solicitud, Usuario usuario)
        {
            this.solicitud = solicitud;
            this.usuario = usuario;
        }

        private void btnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //Redirige a la pantalla para ver el expediente de la solicitud
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            if (!porcentajeRealizado)
            {
                MessageBox.Show("No se ha realizado el cálculo de las políticas");
            }
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

        public void setResultado(String resultado)
        {
            this.resultado = resultado;
            if (this.resultado.Equals("RECHAZADA"))
            {
                rbRechazarSolicitud.IsChecked = true;
                rbRechazarSolicitud.IsEnabled = false;
                rbAceptarSolicitud.IsEnabled = false;
                MessageBox.Show("La solicitud ha sido rechazada de forma automática por no cumplir las políticas","informacion");
            }
            else
            {
                MessageBox.Show("La solicitud ha pasado el porcentaje mínimo de políticas");
            }
        }
    }
}
