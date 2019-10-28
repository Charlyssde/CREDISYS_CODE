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
    /// Lógica de interacción para VisualizarSolicitudes.xaml
    /// </summary>
    public partial class VisualizarSolicitudes : Window
    {
        public VisualizarSolicitudes(Usuario user)
        {
            InitializeComponent();
            cargarDatos(user);
            cargarComboBox();
        }

        private void cargarComboBox()
        {
            String[] listaFiltros = {"Fecha","Folio","Rango de monto","RFC de cliente" };
            String[] listaEstadosSol = {"Aceptada","Rechazada","En espera","En modificación", "Cancelada" };

            cbEstatus.ItemsSource = listaEstadosSol;
            cbFiltro.ItemsSource = listaFiltros;

        }

        private void cargarDatos(Usuario user)
        {
            lblNombre.Content = user.nombre;
            lblRol.Content = user.Rol.rol1;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (cbFiltro.SelectedItem == null || cbEstatus.SelectedItem == null
                || txtBusqueda.Text.Equals(null))
            {
                MessageBox.Show("No puede quedar ningún apartado vacío");
            }
            else
            {

            }
        }

        private void cbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltro.SelectedItem)
            {
                case "Fecha":
                     
                    break;
            }
        }
    }
}
