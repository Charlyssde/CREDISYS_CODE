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
    /// Lógica de interacción para RegistrarTarjetas.xaml
    /// </summary>
    public partial class RegistrarTarjetas : Window
    {
        List<Banco> bancos;
        List<String> stringBancos;

        Cliente cliente;
        public RegistrarTarjetas(Cliente cliente)
        {
            InitializeComponent();
            cargarBancos();
            this.cliente = cliente;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (camposVacios())
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }
            else
            {
                Tarjeta tarjeta = new Tarjeta();
                tarjeta.rfcCliente = this.cliente.rfc;
                tarjeta.numTarjeta = txtNumeroCuentaUno.Text;
                tarjeta.numTelefono = txtTelefonoUno.Text;
                tarjeta.clabeBancaria = txtNumeroClabeUno.Text;
                tarjeta.estatus = "Activo";
                tarjeta.Cliente = this.cliente;
                foreach (Banco b in bancos)
                {
                    if (cbBancoDeposito.SelectedItem.Equals(b.banco1))
                    {
                        tarjeta.Banco = b;
                        tarjeta.idBanco = b.idBanco;
                        break;

                    }
                }
                this.cliente.Tarjetas = new List<Tarjeta>();
                this.cliente.Tarjetas.Add(tarjeta);

                tarjeta.numTarjeta = txtNumeroCuentaDos.Text;
                tarjeta.numTelefono = txtTelefonoDos.Text;
                tarjeta.clabeBancaria = txtNumeroClabeDos.Text;
                foreach (Banco b in bancos)
                {
                    if (cbBancoCobro.SelectedItem.Equals(b.banco1))
                    {
                        tarjeta.Banco = b;
                        tarjeta.idBanco = b.idBanco;
                        break;

                    }
                }
                this.cliente.Tarjetas.Add(tarjeta);

                guardarCliente();
            }
        }

        private void guardarCliente()
        {
            using (DBEntities db = new DBEntities())
            {
                db.Correos.Add(this.cliente.Correo);
                /*foreach (Domicilio d in this.cliente.Domicilios)
                {
                    db.Domicilios.Add(d);
                }
                db.Empleos.Add(this.cliente.Empleo);
                foreach (Referencia r in this.cliente.Referencias)
                {
                    db.Referencias.Add(r);
                }
                foreach (Tarjeta t in this.cliente.Tarjetas)
                {
                    db.Tarjetas.Add(t);
                }
                foreach (Telefono t in this.cliente.Telefonoes)
                {
                    db.Telefonoes.Add(t);
                }
                */
                db.SaveChanges();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void cargarBancos()
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    bancos = db.Bancoes.ToList<Banco>();
                    stringBancos = new List<string>();
                    foreach (Banco b in bancos)
                    {
                        stringBancos.Add(b.banco1);
                    }
                    cbBancoCobro.ItemsSource = stringBancos;
                    cbBancoDeposito.ItemsSource = stringBancos;
                }
            }
            catch (Exception)
            {

            }
        }

        private bool camposVacios()
        {
            if (txtNumeroClabeDos.Text.Equals("") || txtNumeroClabeUno.Text.Equals("") || txtNumeroCuentaDos.Text.Equals("") || 
                txtNumeroCuentaUno.Text.Equals("") || txtTelefonoDos.Text.Equals("") || txtTelefonoUno.Text.Equals("") || 
                cbBancoCobro.SelectedItem == null || cbBancoDeposito.SelectedItem == null)
            {
                return true;
            }
            return false;
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
