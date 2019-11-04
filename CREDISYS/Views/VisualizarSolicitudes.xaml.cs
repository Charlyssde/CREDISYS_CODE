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
            emptyFields = txtBusqueda.Text.Equals("");
            consultar(emptyFields, "basic");
        }

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
            consultar(emptyFields, "Fecha");
        }

        private void btnBuscarRango_Click(object sender, RoutedEventArgs e)
        {
            emptyFields = txtRangeMax.Text.Equals("") || txtRangeMin.Text.Equals("");
            consultar(emptyFields, "Rango");
        }


        private void consultar(Boolean emptyFields, String filtro)
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
                    using (DBEntities db = new DBEntities())
                    {
                        switch (filtro)
                        {
                            case "Fecha":
                                var items = db.Solicituds.Where(b => b.fecha == txtDate.SelectedDate && b.estatus == cbEstatus.Text).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                                }
                                else
                                {
                                    dg_Solicitudes.ItemsSource = items;
                                    
                                }
                                txtDate.Text = "";
                                break;
                            case "Rango":
                                int min = int.Parse(txtRangeMin.Text);
                                int max = int.Parse(txtRangeMax.Text);
                                items = db.Solicituds.Where(b => b.montoNumero <= max && b.montoNumero >= min && b.estatus == cbEstatus.Text).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                                }
                                else
                                {
                                    dg_Solicitudes.ItemsSource = items;
                                }
                                txtRangeMax.Text = "";
                                txtRangeMin.Text = "";
                                break;
                            default:
                                int folio = int.Parse(txtBusqueda.Text);
                                items = db.Solicituds.Where(b => b.rfcCliente == txtBusqueda.Text || b.folio == folio && b.estatus == cbEstatus.Text).ToList<Solicitud>();
                                if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                                }
                                else
                                {
                                    dg_Solicitudes.ItemsSource = items;
                                    
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
            RealizarDictamen realizarDictamen = new RealizarDictamen(selected);
            realizarDictamen.WindowStartupLocation = this.WindowStartupLocation;
            realizarDictamen.Show();
            this.Close();
        }

        private void dg_Solicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (Solicitud) dg_Solicitudes.SelectedItem;
        }
    }
}
