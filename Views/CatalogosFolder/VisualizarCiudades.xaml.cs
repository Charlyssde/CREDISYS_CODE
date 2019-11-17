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
        Usuario usuario;
        public VisualizarCiudades(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogosC catalogos = new CatalogosC(this.usuario);
        }

        private void cbEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbPaises_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            txtResultado.Text = "";
            txtResultado.IsEnabled = true;
            txtCiudad.Text = "";
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
