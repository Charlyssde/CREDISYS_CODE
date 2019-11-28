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

namespace CREDISYS.Views.CatalogosFolder
{
    /// <summary>
    /// Lógica de interacción para VisualizarPoliticas.xaml
    /// </summary>
    public partial class VisualizarPoliticas : Window
    {
        private Usuario usuario;
        private bool lastClick;
        private PoliticaAprobacion politica;

        public VisualizarPoliticas(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogosC catalogos = new CatalogosC(this.usuario);
            catalogos.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            catalogos.Show();
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            lastClick = true;
            
            btnBuscar.IsEnabled = false;

            txtResultado.Text = "";
            txtResultado.IsEnabled = true;

            txtPolitica.Text = "";
            txtPolitica.IsEnabled = false;

            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            lastClick = false;

            btnBuscar.IsEnabled = false;

            txtResultado.IsEnabled = true;

            txtPolitica.Text = "";
            txtPolitica.IsEnabled = false;

            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea eliminar?", "Confirmación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.politica != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.PoliticaAprobacions.Remove(this.politica);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                }
            }
        }


        private void btnBuscar_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtPolitica.Text == "")
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                using (DBEntities db = new DBEntities())
                {
                    this.politica = db.PoliticaAprobacions.Where(b => b.politica == txtPolitica.Text).FirstOrDefault();
                    if (politica == null)
                    {
                        MessageBox.Show(Settings.Default.MensajeElementoNoEcontrado);
                    }
                    else
                    {
                        txtResultado.Text = politica.politica;

                        btnEditar.IsEnabled = true;
                        btnEliminar.IsEnabled = true;

                        txtPolitica.Text = "";
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (lastClick)
            {
                if (txtResultado.Text == "")
                {
                    MessageBox.Show(Settings.Default.MensajeCamposVacios);
                }
                else
                {
                    if (yaExiste())
                    {
                        MessageBox.Show(Settings.Default.MensajeYaExiste);
                    }
                    else
                    {
                        PoliticaAprobacion pol = new PoliticaAprobacion();
                        pol.politica = txtResultado.Text;

                        using (DBEntities db = new DBEntities())
                        {
                            db.PoliticaAprobacions.Add(pol);
                            db.SaveChanges();
                            MessageBox.Show(Settings.Default.MensajeExito);
                        }
                    }
                }
            }
            else
            {
                if (fueModificado())
                {
                    if (yaExiste())
                    {
                        MessageBox.Show(Settings.Default.MensajeYaExiste);
                    }
                    else
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            PoliticaAprobacion mod = db.PoliticaAprobacions.Where(b => b.politica == this.politica.politica).FirstOrDefault();
                            mod.politica = txtResultado.Text;
                            db.SaveChanges();

                            MessageBox.Show(Settings.Default.MensajeExito);
                        }
                    }
                }
            }

            txtResultado.Text = "";

            txtPolitica.IsEnabled = true;

            btnBuscar.IsEnabled = true;

            btnCancelar.Visibility = Visibility.Hidden;
            btnAceptar.Visibility = Visibility.Hidden;

            txtResultado.IsEnabled = false;
            txtResultado.Text = "";
        }

        private bool fueModificado()
        {
            return txtResultado.Text == politica.politica;
        }

        private bool yaExiste()
        {
            using (DBEntities db = new DBEntities())
            {
                PoliticaAprobacion existe = db.PoliticaAprobacions.Where(b => b.politica == txtResultado.Text).FirstOrDefault();
                return existe == null;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtPolitica.IsEnabled = true;

            btnBuscar.IsEnabled = true;

            txtResultado.Text = "";
            txtResultado.IsEnabled = false;

            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;

            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }
    }
}
