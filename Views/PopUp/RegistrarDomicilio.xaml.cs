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
    /// Lógica de interacción para RegistrarCliente.xaml
    /// </summary>
    public partial class RegistrarCliente : Window
    {
        List<TipoDomicilio> tiposdomicilios;
        List<String> listTiposDomicilios;
        List<Pai> Paises;
        List<String> listPaises;
        List<Estado> Estados;
        List<String> listEstados;
        List<Ciudad> Ciudades;
        List<String> listCiudades;
        Cliente clientenuevo;
        public RegistrarCliente(Cliente cliente)
        {
            InitializeComponent();
            cargartipoDomicilio();
            cargarPaises();
            clientenuevo = cliente;
        }



        public void cargartipoDomicilio()
        {
            using (DBEntities db = new DBEntities())
            {

                tiposdomicilios = db.TipoDomicilios.ToList<TipoDomicilio>();
                listTiposDomicilios = new List<String>();

                foreach (TipoDomicilio r in tiposdomicilios)
                {
                    listTiposDomicilios.Add(r.tipoDomicilio1);
                }
                cb_tipoDomicilio.ItemsSource = listTiposDomicilios;

            }
        }

        private void cb_pais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Pai p in this.Paises)
            {
                if (p.pais.Equals(cb_pais.SelectedItem))
                {
                    cargarEstados(p.idPais);
                    break;
                }
            }
        }

        private void cb_estado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Estado p in this.Estados)
            {
                if (p.estado1.Equals(cb_estado.SelectedItem))
                {
                    cargarCiudades(p.idEstado);
                    break;
                }
            }
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
                cb_pais.ItemsSource = listPaises;


            }

        }

        public void cargarEstados(int idPais)
        {
            using (DBEntities db = new DBEntities())
            {
                Estados = db.Estadoes.Where(b => b.idPais == idPais).ToList<Estado>();
                listEstados = new List<String>();

                foreach (Estado e in Estados)
                {
                    listEstados.Add(e.estado1);
                }
                cb_estado.ItemsSource = listEstados;

            }
        }
        public void cargarCiudades(int idEstado)
        {
            using (DBEntities db = new DBEntities())
            {
                Ciudades = db.Ciudads.Where(b => b.idEstado == idEstado).ToList<Ciudad>();
                listCiudades = new List<String>();

                foreach (Ciudad c in Ciudades)
                {
                    listCiudades.Add(c.ciudad1);
                }
                cb_ciudad.ItemsSource = listCiudades;

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txtCalle.Text.Equals("") || txtCP.Text.Equals("") || txtColonia.Text.Equals("")
                         || txtNumeEx.Text.Equals("") || txtNumeroIn.Text.Equals("") || txtTiempo.Text.Equals("")
                         || cb_tipoDomicilio.SelectedItem == null || cb_pais.SelectedItem == null || cb_estado.SelectedItem == null || cb_ciudad.SelectedItem == null)
                    {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {


                        Domicilio nuevo = new Domicilio();
                        nuevo.colonia = txtColonia.Text;
                        nuevo.calle = txtCalle.Text;
                        nuevo.codPostal = txtCP.Text;
                        nuevo.numExt = txtNumeEx.Text;
                        nuevo.numIn = txtNumeroIn.Text;
                        nuevo.tiempoResidencia = txtTiempo.Text;
                        nuevo.rfcCliente = clientenuevo.rfc;
                        string v = "activo";
                        nuevo.estatus = v;


                        foreach (Pai pais in Paises)
                        {
                            if (pais.pais.Equals(cb_pais.SelectedItem))
                            {
                                nuevo.idPais = pais.idPais;
                            }
                        }
                        foreach (Estado estado in Estados)
                        {
                            if (estado.estado1.Equals(cb_estado.SelectedItem))
                            {
                                nuevo.idEstado = estado.idEstado;
                            }
                        }
                        foreach (Ciudad ciudad in Ciudades)
                        {
                            if (ciudad.ciudad1.Equals(cb_ciudad.SelectedItem))
                            {
                                nuevo.idCiudad = ciudad.idCiudad;
                            }
                        }

                        this.clientenuevo.Domicilios = new List<Domicilio>();
                        this.clientenuevo.Domicilios.Add(nuevo);
                        
                        MessageBox.Show(Settings.Default.MensajeExito);
                        
                        RegistroContacto registrarcontacto = new RegistroContacto(clientenuevo);
                        registrarcontacto.WindowStartupLocation = this.WindowStartupLocation;
                        registrarcontacto.ShowDialog();
                        
                        closeWindow();

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


    }

}
