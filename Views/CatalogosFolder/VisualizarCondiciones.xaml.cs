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
    /// Lógica de interacción para VisualizarCondiciones.xaml
    /// </summary>
    public partial class VisualizarCondiciones : Window
    {
        private CondicionCredito condicion;

        private Boolean lastClick;

        private Usuario usuario;
        public VisualizarCondiciones(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCondicion.Text.Equals(""))
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                using (DBEntities db = new DBEntities())
                {
                    this.condicion = db.CondicionCreditoes.Where(b => b.condicion == txtCondicion.Text).SingleOrDefault();
                }
                if (this.condicion == null)
                {
                    MessageBox.Show(Settings.Default.MensajeElementoNoEcontrado);
                }
                else
                {
                    txtResultado.Text = this.condicion.condicion;
                    txtInteres.Text = this.condicion.interes.ToString();
                    txtIva.Text = this.condicion.iva.ToString();

                    btnEditar.IsEnabled = true;
                    btnEliminar.IsEnabled = true;
                }
            }
            txtCondicion.Text = "";
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            lastClick = true;

            txtCondicion.Text = "";
            txtCondicion.IsEnabled = false;

            btnBuscar.IsEnabled = false;

            txtInteres.Text = "";
            txtInteres.IsEnabled = true;

            txtIva.Text = "";
            txtIva.IsEnabled = true;

            txtResultado.Text = "";
            txtResultado.IsEnabled = true;

            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            lastClick = false;

            txtCondicion.Text = "";
            txtCondicion.IsEnabled = false;

            btnBuscar.IsEnabled = false;

            txtInteres.IsEnabled = true;

            txtIva.IsEnabled = true;

            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea eliminar?", "Confirmación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.condicion != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.CondicionCreditoes.Remove(this.condicion);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarInfo();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (lastClick)
            {
                if (yaExiste())
                {
                    MessageBox.Show(Settings.Default.MensajeYaExiste);
                }
                else
                {
                    using (DBEntities db = new DBEntities())
                    {
                        CondicionCredito condicionCredito = new CondicionCredito();
                        condicionCredito.condicion = txtResultado.Text;
                        condicionCredito.iva = Double.Parse(txtIva.Text);
                        condicionCredito.interes = Double.Parse(txtInteres.Text);
                        db.CondicionCreditoes.Add(condicionCredito);
                        db.SaveChanges();
                        MessageBox.Show(Settings.Default.MensajeExito);
                        limpiarInfo();
                    }
                }
            }
            else
            {
                if (fueModificado())
                {
                    using (DBEntities db = new DBEntities())
                    {
                        CondicionCredito condicionCredito = db.CondicionCreditoes.Where(b => b.condicion == this.condicion.condicion).SingleOrDefault();
                        condicionCredito.iva = Double.Parse(txtIva.Text);
                        condicionCredito.interes = Double.Parse(txtInteres.Text);
                        db.SaveChanges();
                        MessageBox.Show(Settings.Default.MensajeExito);
                        limpiarInfo();
                    }
                }
            }
        }

        private bool yaExiste()
        {
            using (DBEntities db = new DBEntities())
            {
                CondicionCredito existe = db.CondicionCreditoes.Where(b => b.condicion == txtCondicion.Text).FirstOrDefault();
                if (existe == null)
                {
                    return false;
                }
                return true;
            }
        }

        private bool fueModificado()
        {
            return txtInteres.Text != this.condicion.interes.ToString() && txtIva.Text != this.condicion.iva.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogosC catalogos = new CatalogosC(this.usuario);
            catalogos.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            catalogos.Show();
            this.Close();
        }

        private void limpiarInfo()
        {
            txtCondicion.Text = "";
            txtCondicion.IsEnabled = true;

            btnBuscar.IsEnabled = true;

            txtInteres.Text = "";
            txtInteres.IsEnabled = false;

            txtIva.Text = "";
            txtIva.IsEnabled = false;

            txtResultado.Text = "";
            txtResultado.IsEnabled = false;

            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;

            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }
    }
}
