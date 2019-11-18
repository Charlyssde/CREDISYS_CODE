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
    /// Lógica de interacción para VisualizarEstados.xaml
    /// </summary>
    public partial class VisualizarEstados : Window
    {
        Usuario usuario;

        private bool lastClick;
        private Estado estado;
        private Estado estadoNuevo;
        public VisualizarEstados(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            cargarPaises();
        }

        private void cargarPaises()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    List<Pai> listPaises = db.Pais.ToList<Pai>();
                    cbPaises.ItemsSource = listPaises;
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            this.lastClick = true;
            txtResultado.Text = "";
            txtResultado.IsEnabled = true;
            cbPaises.IsEnabled = false;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            txtResultado.IsEnabled = true;
            lastClick = false;
            cbPaises.IsEnabled = false;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Desea eliminar?", "Confirmación", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.estado != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.Estadoes.Remove(this.estado);
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
            txtResultado.Text = this.estado.estado1;
            txtResultado.IsEnabled = false;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    if (lastClick)
                    {
                        if (yaExiste())
                        {
                            MessageBox.Show("Ya existe un elemento con este nombre");
                        }
                        else
                        {
                            db.Estadoes.Add(this.estadoNuevo);
                            db.SaveChanges();
                            MessageBox.Show(Settings.Default.MensajeExito);
                        }
                    }
                    else
                    {
                        if (fueModificado())
                        {
                            if (!yaExiste())
                            {
                                Estado estado = db.Estadoes.Where(b => b.idEstado == this.estado.idEstado).FirstOrDefault();
                                estado.estado1 = txtResultado.Text;
                                db.SaveChanges();
                                MessageBox.Show(Settings.Default.MensajeExito);
                            }
                            else
                            {
                                MessageBox.Show("Ya existe");
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show(Settings.Default.MensajeErrorBD);
            }
            txtResultado.Text = "";
            txtResultado.IsEnabled = false;
            cbPaises.IsEnabled = true;
        }

        private bool fueModificado()
        {
            return txtResultado.Text == this.estado.estado1;
        }

        private bool yaExiste()
        {
            using (DBEntities db = new DBEntities())
            {
                Pai pais = db.Pais.Where(b => b.pais == cbPaises.SelectedItem.ToString()).FirstOrDefault();
                
                Estado est = db.Estadoes.Where(b => b.estado1 == txtResultado.Text && b.idPais == pais.idPais).FirstOrDefault();
                if (est == null)
                {
                    return false;
                }
                else
                {
                    this.estadoNuevo = est;
                    return false;
                }
            }
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
            if (txtEstado.Text == "" || cbPaises.SelectedItem == null )
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        Pai pais = db.Pais.Where(b => b.pais == cbPaises.SelectedItem.ToString()).FirstOrDefault();
                        this.estado = db.Estadoes.Where(b => b.estado1 == txtEstado.Text && b.idPais == pais.idPais).FirstOrDefault();
                        if (this.estado == null)
                        {
                            MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                        }
                        else
                        {
                            txtResultado.Text = estado.estado1;
                            btnEditar.IsEnabled = true;
                            btnEliminar.IsEnabled = true;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                } 
            }
        }
    }
}
