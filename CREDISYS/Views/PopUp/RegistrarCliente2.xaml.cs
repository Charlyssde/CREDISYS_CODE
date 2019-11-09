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

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para RegistrarCliente2.xaml
    /// </summary>
    public partial class RegistrarCliente2 : Window
    {
        List<Pai> Paises;
        List<String> listPaises;
        List<Estado> Estados;
        List<String> listEstados;
        List<Ciudad> Ciudades;
        List<String> listCiudades;
        public RegistrarCliente2()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txt_apellidopaterno.Text.Equals("") || txt_apellidomaterno.Text.Equals("") || txt_name.Text.Equals("")
                         || txt_curp.Text.Equals("") || txt_rfc.Text.Equals("") || combo_genero.SelectedItem == null ||
                         combo_pais.SelectedItem == null || combo_estado.SelectedItem == null || combo_ciudad.SelectedItem == null)
                    {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {
                        Cliente existe = db.Clientes.Where(b => b.rfc.Equals(txt_rfc.Text)).FirstOrDefault();
                        if (existe != null)
                        {
                            MessageBox.Show(Settings.Default.MensajeYaExiste);
                            txt_rfc.Text = "";
                        }
                        else
                        {
                            Cliente nuevo = new Cliente();
                            nuevo.rfc = txt_rfc.Text;
                            nuevo.nombre = txt_name.Text;
                            nuevo.apellidoPaterno = txt_apellidopaterno.Text;
                            nuevo.apellidoMaterno = txt_apellidomaterno.Text;
                           

                            foreach (Pai pais in Paises)
                            {
                                if (pais.pais.Equals(combo_pais.SelectedItem))
                                {
                                    nuevo.idPais = pais.idPais;
                                }
                            }
                            foreach (Estado estado in Estados)
                            {
                                if (estado.estado1.Equals(combo_estado.SelectedItem))
                                {
                                    nuevo.idEstado = estado.idEstado;
                                }
                            }
                            foreach (Ciudad ciudad in Ciudades)
                            {
                                if (ciudad.ciudad1.Equals(combo_ciudad.SelectedItem))
                                {
                                    nuevo.idCiudad = ciudad.idCiudad;
                                }
                            }
                            nuevo.idCorreo = 0;
                            nuevo.idEmpleo = 0;


                            db.Clientes.Add(nuevo);
                            db.SaveChanges();
                            MessageBox.Show(Settings.Default.MensajeExito);
                            closeWindow();
                        }
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                }

            }
        }
        private void closeWindow()
        {
            this.Close();
        }
        public void cargarPaises()
        {
            using (DBEntities db = new DBEntities())
            {

                Paises = db.Pais.ToList<Pai>();
                listPaises = new List<String>();

                foreach (Pai p in Paises)
                {
                    listPaises.Add(p.pais);
                }
                combo_pais.ItemsSource = listPaises;


            }

        }

        public void cargarEstados()
        {
            using (DBEntities db = new DBEntities())
            {
                Estados = db.Estadoes.ToList<Estado>();
                listEstados = new List<String>();

                foreach (Estado e in Estados)
                {
                    listEstados.Add(e.estado1);
                }
                combo_estado.ItemsSource = listEstados;
                cargarCiudades();

            }
        }
        public void cargarCiudades()
        {
            using (DBEntities db = new DBEntities())
            {
                Ciudades = db.Ciudads.ToList<Ciudad>();
                listCiudades = new List<String>();

                foreach (Ciudad c in Ciudades)
                {
                    listCiudades.Add(c.ciudad1);
                }
                combo_ciudad.ItemsSource = listCiudades;

            }
        }

        private void cb_pais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                cargarEstados();
            }
        }

    }
}
