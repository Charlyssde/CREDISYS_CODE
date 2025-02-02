﻿using CREDISYS.Properties;
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
            using (DBEntities db = new DBEntities())
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
                    foreach (Banco b in bancos)
                    {
                        if (cbBancoDeposito.SelectedItem.Equals(b.banco1))
                        {
                            tarjeta.idBanco = b.idBanco;
                            break;

                        }
                    }
                    

                    Tarjeta tarjeta2 = new Tarjeta();
                    tarjeta2.rfcCliente = this.cliente.rfc;
                    tarjeta2.numTarjeta = txtNumeroCuentaDos.Text;
                    tarjeta2.numTelefono = txtTelefonoDos.Text;
                    tarjeta2.clabeBancaria = txtNumeroClabeDos.Text;
                    tarjeta2.estatus = "Activo";
                    foreach (Banco b in bancos)
                    {
                        if (cbBancoCobro.SelectedItem.Equals(b.banco1))
                        {
                            tarjeta2.idBanco = b.idBanco;
                            break;

                        }
                    }
                    db.Tarjetas.Add(tarjeta);
                    db.Tarjetas.Add(tarjeta2);
                    

                    db.SaveChanges();
                    MessageBox.Show(Settings.Default.MensajeExito);
                    SubirRecibos modificarRefe = new SubirRecibos(cliente);
                    modificarRefe.WindowStartupLocation = this.WindowStartupLocation;
                    this.Hide();
                    modificarRefe.ShowDialog();
                    closeWindow();
                }
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
