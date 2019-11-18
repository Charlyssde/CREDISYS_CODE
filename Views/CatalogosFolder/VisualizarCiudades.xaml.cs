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
    /// Lógica de interacción para VisualizarCiudades.xaml
    /// </summary>
    public partial class VisualizarCiudades : Window
    {
        private Usuario usuario;

        private bool lastClick;

        private Pai pais;
        private Estado estado;
        private Ciudad ciudad;
        public VisualizarCiudades(Usuario usuario)
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
                    List<Pai> listaPaises = db.Pais.ToList<Pai>();
                    cbPaises.ItemsSource = listaPaises;
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogosC catalogos = new CatalogosC(this.usuario);
        }

        private void cbPaises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cargarEstados(cbPaises.SelectedItem.ToString());
        }

        private void cargarEstados(string v)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    Pai pais = db.Pais.Where(b => b.pais == v).SingleOrDefault();
                    List<Estado> estados = db.Estadoes.Where(b => b.idPais == pais.idPais).ToList<Estado>();
                    cbEstados.ItemsSource = estados;
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCiudad.Text.Equals(""))
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {

                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        Pai pais = db.Pais.Where(b => b.pais == cbPaises.SelectedItem.ToString()).SingleOrDefault();
                        Estado estado = db.Estadoes.Where(b => b.estado1 == cbEstados.SelectedItem.ToString()
                        && b.idPais == pais.idPais).SingleOrDefault();
                        Ciudad ciudad = db.Ciudads.Where(b => b.ciudad1 == txtCiudad.Text && b.idEstado == estado.idEstado).SingleOrDefault();

                        if (ciudad == null)
                        {
                            MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                        }
                        else
                        {
                            txtResultado.Text = ciudad.ciudad1;
                            btnEditar.IsEnabled = true;
                            btnEliminar.IsEnabled = true;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            btnAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;
            btnBuscar.IsEnabled = false;
            txtResultado.Text = "";
            txtResultado.IsEnabled = true;
            txtCiudad.Text = "";
            cbEstados.IsEnabled = false;
            cbPaises.IsEnabled = false;


            lastClick = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            cbEstados.IsEnabled = false;
            cbPaises.IsEnabled = false;
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
                if (this.ciudad != null)
                {
                    try
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            db.Ciudads.Remove(this.ciudad);
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
            txtResultado.Text = this.ciudad.ciudad1;
            txtResultado.IsEnabled = false;
            cbEstados.IsEnabled = true;
            cbPaises.IsEnabled = true;
            btnBuscar.IsEnabled = true;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (lastClick)
            {
                if (yaExiste())
                {
                    MessageBox.Show("Ya existe un elemento con este nombre");
                }
                else
                {
                    using (DBEntities db = new DBEntities())
                    {
                        Ciudad nuevo = new Ciudad();
                        nuevo.ciudad1 = txtResultado.Text;
                        nuevo.idEstado = this.ciudad.idEstado;

                        db.Ciudads.Add(nuevo);
                        db.SaveChanges();
                        MessageBox.Show("Exito");
                    }

                    
                }
            }
            else
            {
                if (fueModificado())
                {
                    if (yaExiste())
                    {
                        MessageBox.Show("Ya existe un elemento con este nombre");
                    }
                    else
                    {
                        using (DBEntities db = new DBEntities())
                        {
                            Ciudad ciudad = db.Ciudads.Where(b => b.ciudad1 == this.ciudad.ciudad1 && b.idEstado == this.ciudad.idEstado).FirstOrDefault();
                            ciudad.ciudad1 = txtResultado.Text;
                            db.SaveChanges();
                            MessageBox.Show("Exito");
                        }
                    }
                }
            }

            cbEstados.IsEnabled = true;
            cbPaises.IsEnabled = true;
            btnAceptar.Visibility = Visibility.Hidden;
            btnCancelar.Visibility = Visibility.Hidden;
            txtResultado.Text = "";
            txtResultado.IsEnabled = false;
            btnBuscar.IsEnabled = true;
        }

        private bool fueModificado()
        {
            return txtResultado.Text == this.ciudad.ciudad1;
        }

        private bool yaExiste()
        {
            using (DBEntities db = new DBEntities())
            {
                Ciudad existe = db.Ciudads.Where(b => b.ciudad1 == txtResultado.Text && b.idEstado == this.ciudad.idEstado ).SingleOrDefault();
                if (existe == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
