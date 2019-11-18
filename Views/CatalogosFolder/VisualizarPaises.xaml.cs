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

namespace CREDISYS.Views.Catalogos
{
    /// <summary>
    /// Lógica de interacción para VisualizarPaises.xaml
    /// </summary>
    public partial class VisualizarPaises : Window
    {
        Usuario usuario;

        Pai pais = null;
        private bool lastClick; //False para cuando se seleccione editar, true para cuando se selecciona guardar
        public VisualizarPaises(Usuario usuario)
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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtPais.Text.Equals(""))
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                using (DBEntities db = new DBEntities())
                {
                    this.pais = db.Pais.Where(b => b.pais == txtPais.Text).FirstOrDefault();
                    if (this.pais == null)
                    {
                        MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                    }
                    else
                    {
                        txtResultado.Text = this.pais.pais;
                        btnEditar.IsEnabled = true;
                        btnEliminar.IsEnabled = true;
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            lastClick = true;
            btnBuscar.IsEnabled = false;
            txtResultado.Text = "";
            txtResultado.IsEnabled = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            btnBuscar.IsEnabled = false;
            txtResultado.IsEnabled = true;
            lastClick = false;

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea eliminar?", "Confirmación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.pais != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.Pais.Remove(this.pais);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (lastClick)
            {
                if (!yaExiste())
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            Pai nuevo = new Pai();
                            nuevo.estatus = "activo";
                            nuevo.pais = txtResultado.Text;

                            db.Pais.Add(nuevo);
                            db.SaveChanges();

                            MessageBox.Show("Operación exitosa");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(Settings.Default.MensajeErrorBD);
                    }
                }
                else
                {
                    MessageBox.Show("Ya existe un elemento con este nombre");
                }

            }
            else
            {
                if (fueModificado())
                {
                    if (!yaExiste())
                    {
                        try
                        {
                            using (DBEntities db = new DBEntities())
                            {
                                Pai pais = db.Pais.Where(b => b.idPais == this.pais.idPais).SingleOrDefault();
                                pais.pais = txtResultado.Text;
                                db.SaveChanges();

                                MessageBox.Show("Operación exitosa");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show(Settings.Default.MensajeErrorBD);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un elemento con este nombre");
                    }
                }
            }
            txtResultado.Text = "";
            txtResultado.IsEnabled = false;
            btnBuscar.IsEnabled = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.IsEnabled = false;
            txtResultado.Text = this.pais.pais;
            btnBuscar.IsEnabled = true;
        }

        private bool fueModificado()
        {
            if (txtResultado.Text == this.pais.pais)
            {
                return false;
            }
            return true;
        }

        private bool yaExiste()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    Pai existe = db.Pais.Where(b => b.pais == txtResultado.Text).FirstOrDefault();
                    if (existe != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Settings.Default.MensajeErrorBD);
            }
            return false;
        }
    }
}
