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
        public RegistrarCliente()
        {
            InitializeComponent();
            cargartipoDomicilio();
            cargarPaises();
        }

      
    
        public void cargartipoDomicilio()
        {
            using (DBEntities db = new DBEntities())
            {

                tiposdomicilios = db.TipoDomicilios.ToList<TipoDomicilio>();
                listTiposDomicilios = new List<String>();

                foreach (TipoDomicilio r in tiposdomicilios)
                {
                    listTiposDomicilios.Add(r.tipodomicilio1);
                }
                cb_tipoDomicilio.ItemsSource = listTiposDomicilios;

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
                cb_estado.ItemsSource = listEstados;

            }
        }

        private void cb_pais_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                cargarEstados();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnContinuar_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
