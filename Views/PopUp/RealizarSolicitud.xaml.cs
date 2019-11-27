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
    /// Lógica de interacción para RealizarSolicitud.xaml
    /// </summary>
    public partial class RealizarSolicitud : Window
    {
        private List<CondicionCredito> condiciones;
        private int plazos = 12;


        private Usuario usuario;
        private Cliente cliente;

        private CondicionCredito selected;

        public RealizarSolicitud(Usuario usuario, Cliente cliente)
        {
            InitializeComponent();
            cargarInfo(usuario, cliente);
            
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (camposVacios() == true)
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                Solicitud solicitud = new Solicitud();
                solicitud.destinoCredito = txtDestino.Text;
                solicitud.estatus1 = Settings.Default.SolicitudEstatus1;
                solicitud.montoLetra = txtMontoLetra.Text;
                solicitud.montoNumero = int.Parse(txtMontoNumero.Text);
                //solicitud.Usuario = this.usuario;
                solicitud.vendedor = this.usuario.username;
                //solicitud.Cliente = this.cliente;
                solicitud.rfcCliente = this.cliente.rfc;
                solicitud.CondicionCredito = this.selected;
                solicitud.idCondicion = this.selected.idCondicionCredito;
                solicitud.disposicion = Settings.Default.Disposicion;
                DateTime date = DateTime.Now;
                solicitud.fecha = date;

                
                    using (DBEntities db = new DBEntities())
                    {
                        db.Solicituds.Add(solicitud);
                        db.SaveChanges();
                        MessageBox.Show(Settings.Default.MensajeExito);
                    }
                    EncuestaSolicitud encuestaSolicitud = new EncuestaSolicitud(solicitud);
                    encuestaSolicitud.WindowStartupLocation = this.WindowStartupLocation;
                    encuestaSolicitud.Show();
                    this.Close();
                
                

                CloseWindow();
            }

        }

        private void rbTasaCero_Checked(object sender, RoutedEventArgs e)
        {
            foreach (CondicionCredito condicion in condiciones)
            {
                if (condicion.condicion.Equals("tasa cero"))
                {
                    selected = condicion;
                    txtIva.Text = condicion.iva.ToString();
                    txtInterés.Text = condicion.interes.ToString();
                }
            }
        }

        private void rbTasaCinco_Checked(object sender, RoutedEventArgs e)
        {

            foreach (CondicionCredito condicion in condiciones)
            {
                if (condicion.condicion.Equals("tasa cinco"))
                {
                    selected = condicion;
                    txtIva.Text = condicion.iva.ToString();
                    txtInterés.Text = condicion.interes.ToString();
                }
            }
        }

        private bool camposVacios()
        {
            if (txtMontoLetra.Text.Equals("") || txtMontoNumero.Text.Equals("") || txtDestino.Text.Equals(""))
            {
                if (rbTasaCero.IsChecked == false && rbTasaCinco.IsChecked == false)
                {
                    return true;
                }
                return true;
            }
            int val;
            if (int.TryParse(txtMontoNumero.Text, out val))
            {
                return false;
            }
            else
            {
                MessageBox.Show("El monto en número no cuenta con el formato especificado");
                return true;
            }
            
        }

        private void cargarInfo(Usuario usuario, Cliente cliente)
        {
            this.usuario = usuario;
            this.cliente = cliente;
            lblRol.Content = usuario.nombre;
            lblNombreVendedor.Content = lblNombre.Content = usuario.nombre;
            cargarTasas();
        }

        private void cargarTasas()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    condiciones = db.CondicionCreditoes.ToList<CondicionCredito>();
                }
            }
            catch (Exception)
            {

            }
            txtPlazos.Text = plazos.ToString();
        }
        private void CloseWindow()
        {
            this.Close();
        }

        private void txtMontoNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
