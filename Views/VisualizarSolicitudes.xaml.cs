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

namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para VisualizarSolicitudes.xaml
    /// </summary>
    public partial class VisualizarSolicitudes : Window
    {
        bool emptyFields;
        Solicitud selected;

        Usuario usuario;
        /*
         * En esta clase, se tienen 3 stack panels diferentes para la búsqueda, de acuerdo al filtro, aparece uno u otro
         * Para mejor usabilidad.
         */
        public VisualizarSolicitudes(Usuario user)
        {
            InitializeComponent();
            cargarDatos(user);
            cargarComboBox();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_AnalistaC dashboard_AnalistaC = new Dashboard_AnalistaC(this.usuario);
            dashboard_AnalistaC.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dashboard_AnalistaC.Show();
            closeWindow();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            emptyFields = txtBusqueda.Text.Equals("");
            obtenerSolicitudes(emptyFields, "basic");
        }

        /*
         * Conforme cambia la selección del filtro, cambia el stack panel
         */
        private void cbFiltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFiltro.SelectedItem)
            {
                case "Fecha":
                    stackEasy.Visibility = Visibility.Hidden;
                    stackRange.Visibility = Visibility.Hidden;
                    stackDate.Visibility = Visibility.Visible;
                    break;
                case "Rango de monto":
                    stackDate.Visibility = Visibility.Hidden;
                    stackEasy.Visibility = Visibility.Hidden;
                    stackRange.Visibility = Visibility.Visible;
                    break;
                case "RFC de cliente": //Evento en cascada intencional
                case "Folio":
                    stackRange.Visibility = Visibility.Hidden;
                    stackDate.Visibility = Visibility.Hidden;
                    stackEasy.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnBuscarDate_Click(object sender, RoutedEventArgs e)
        {
            emptyFields = txtDate.Text.Equals("");
            obtenerSolicitudes(emptyFields, "Fecha");
        }

        private void btnBuscarRango_Click(object sender, RoutedEventArgs e)
        {
            emptyFields = txtRangeMax.Text.Equals("") || txtRangeMin.Text.Equals("");
            obtenerSolicitudes(emptyFields, "Rango");
        }


        private void obtenerSolicitudes(Boolean emptyFields, String filtro)
        {
            if (cbEstatus.SelectedItem == null || cbFiltro.SelectedItem == null)
            {
                emptyFields = true;
            }

            if (emptyFields)
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                try
                {
                    /*
                     * Consulta de acuerdo al tipo de filtro que se escogió
                     */
                    using (DBEntities db = new DBEntities())
                    {
                        switch (filtro)
                        {
                            case "Fecha":
                                var items = db.Solicituds.Where(b => b.fecha == txtDate.SelectedDate && b.estatus1 == cbEstatus.Text.ToLower()).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                                }
                                else
                                {
                                    dg_Solicitudes.ItemsSource = items;
                                    dg_Solicitudes.Columns.RemoveAt(12);
                                    dg_Solicitudes.Columns.RemoveAt(11);
                                    dg_Solicitudes.Columns.RemoveAt(10);
                                    dg_Solicitudes.Columns.RemoveAt(8);

                                }
                                txtDate.Text = "";
                                break;
                            case "Rango":
                                int min = int.Parse(txtRangeMin.Text);
                                int max = int.Parse(txtRangeMax.Text);
                                items = db.Solicituds.Where(b => b.montoNumero <= max && b.montoNumero >= min && b.estatus1 == cbEstatus.Text.ToLower()).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                                }
                                else
                                {
                                    dg_Solicitudes.ItemsSource = items;
                                    dg_Solicitudes.Columns.RemoveAt(12);
                                    dg_Solicitudes.Columns.RemoveAt(11);
                                    dg_Solicitudes.Columns.RemoveAt(10);
                                    dg_Solicitudes.Columns.RemoveAt(8);
                                }
                                txtRangeMax.Text = "";
                                txtRangeMin.Text = "";
                                break;
                            default:
                                int folio = int.Parse(txtBusqueda.Text);
                                items = db.Solicituds.Where(b => (b.rfcCliente == txtBusqueda.Text && b.estatus1 == cbEstatus.Text.ToLower()) || 
                                (b.folio == folio && b.estatus1 == cbEstatus.Text.ToLower())).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);

                                }
                                else
                                {
                                    /*
                                     * Se remueven de forma manual las columnas innecesarias
                                     */
                                    dg_Solicitudes.ItemsSource = items;
                                    dg_Solicitudes.Columns.RemoveAt(12);
                                    dg_Solicitudes.Columns.RemoveAt(11);
                                    dg_Solicitudes.Columns.RemoveAt(10);
                                    dg_Solicitudes.Columns.RemoveAt(8);

                                }
                                txtBusqueda.Text = "";
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                }
            }
        }

        private void btnDictamen_Click(object sender, RoutedEventArgs e)
        {
            RealizarDictamen realizarDictamen = new RealizarDictamen(selected, this.usuario);
            realizarDictamen.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            realizarDictamen.Show();
            closeWindow();
        }

        private void dg_Solicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_Solicitudes.SelectedItem != null)
            {
                selected = (Solicitud)dg_Solicitudes.SelectedItem;
                if (cbEstatus.SelectedItem.ToString().Equals("En espera"))
                {
                    btnDictamen.IsEnabled = true;
                }
            }
        }

        private void cargarDatos(Usuario user)
        {
            this.usuario = user;
            lblNombre.Content = this.usuario.nombre;
            lblRol.Content = this.usuario.Rol.rol1;
        }

        private void cargarComboBox()
        {
            String[] listaFiltros = { "Fecha", "Folio", "Rango de monto", "RFC de cliente" };
            String[] listaEstadosSol = {Settings.Default.SolicitudEstatus1, Settings.Default.SolicitudEstatus2, Settings.Default.SolicitudEstatus3,
                    Settings.Default.SolicitudEstatus4, Settings.Default.SolicitudEstatus5, Settings.Default.SolicitudEstatus6, Settings.Default.SolicitudEstatus7,
                    Settings.Default.SolicitudEstatus8};

            cbEstatus.ItemsSource = listaEstadosSol;
            cbFiltro.ItemsSource = listaFiltros;

        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
