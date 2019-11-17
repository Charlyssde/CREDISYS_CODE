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
        Pai pais = null;
        private bool lastClick; //False para cuando se seleccione editar, true para cuando se selecciona guardar
        public VisualizarPaises()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

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
            lastClick = true;
            txtResultado.Text = "";
            txtResultado.IsEnabled = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.IsEnabled = true;
            lastClick = false;

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

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
                            nuevo.pais = txtResultado.Text;

                            db.Pais.Add(nuevo);
                            db.SaveChanges();
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtResultado.IsEnabled = false;
            txtResultado.Text = this.pais.pais;
        }

        private void txtResultado_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
