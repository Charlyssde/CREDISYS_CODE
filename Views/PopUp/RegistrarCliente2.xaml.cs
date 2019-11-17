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
            cargarPaises();
           


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                if (txt_apellidopaterno.Text.Equals("") || txt_apellidomaterno.Text.Equals("") || txt_name.Text.Equals("")
                     || txt_curp.Text.Equals("") || txt_rfc.Text.Equals("") || combo_genero.SelectedItem == null ||
                     combo_pais.SelectedItem == null || combo_estado.SelectedItem == null || combo_ciudad.SelectedItem == null || cbEstadoCivil.SelectedItem == null)
                {
                    MessageBox.Show(Settings.Default.MensajeCamposVacios);
                }
                else
                {
                    Cliente existe = db.Clientes.Where(b => b.rfc == txt_rfc.Text).FirstOrDefault();
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
                                nuevo.Pai = pais;
                                nuevo.idPais = pais.idPais;
                            }
                        }
                        foreach (Estado estado in Estados)
                        {
                            if (estado.estado1.Equals(combo_estado.SelectedItem))
                            {
                                nuevo.Estado = estado;
                                nuevo.idEstado = estado.idEstado;
                            }
                        }
                        foreach (Ciudad ciudad in Ciudades)
                        {
                            if (ciudad.ciudad1.Equals(combo_ciudad.SelectedItem))
                            {
                                nuevo.Ciudad = ciudad;
                                nuevo.idCiudad = ciudad.idCiudad;
                            }
                        }
                        nuevo.curp = txt_curp.Text;
                        nuevo.fechaNacimiento = date_nacimiento.SelectedDate.Value;
                        nuevo.genero = combo_genero.Text;
                        nuevo.estadoCivil = cbEstadoCivil.Text;
                        nuevo.estatus = "activo";


                        db.Clientes.Add(nuevo);
                        db.SaveChanges();

                        MessageBox.Show(Settings.Default.MensajeExito);
                        RegistrarCliente registrarDomicilio = new RegistrarCliente(nuevo);
                        registrarDomicilio.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        registrarDomicilio.ShowDialog();
                        closeWindow();
                    }
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
                combo_estado.ItemsSource = listEstados;

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
                combo_ciudad.ItemsSource = listCiudades;

            }
        }

        private void combo_pais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Pai p in this.Paises)
            {
                if (p.pais.Equals(combo_pais.SelectedItem))
                {
                    cargarEstados(p.idPais);
                    break;
                }
            }

        }

        private void combo_estado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Estado p in this.Estados)
            {
                if (p.estado1.Equals(combo_estado.SelectedItem))
                {
                    cargarCiudades(p.idEstado);
                    break;
                }
            }
        }

        
    }
}
