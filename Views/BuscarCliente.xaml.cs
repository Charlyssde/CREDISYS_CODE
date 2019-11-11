using CREDISYS.Views.PopUp;
using System;
﻿using CREDISYS.Properties;
using CREDISYS.Views.PopUp;
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

namespace CREDISYS.Views
{
    /// <summary>
    /// Lógica de interacción para BuscarCliente.xaml
    /// </summary>
    public partial class BuscarCliente : Window
    {
        Cliente cliente = null;
        Usuario usuario;
        bool emptyFields;
        public BuscarCliente(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            consultar();
        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregarSolicitud_Click(object sender, RoutedEventArgs e)
        {
            RealizarSolicitud realizarSolicitud = new RealizarSolicitud(this.usuario, this.cliente);
            realizarSolicitud.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            realizarSolicitud.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_Capturista cap= new Dashboard_Capturista();
            cap.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cap.Show();
            closeWindow();
        }

        private void consultar()
        {
            if (txtBusqueda.Text == "")
            {
                MessageBox.Show(Settings.Default.MensajeCamposVacios);
            }


            else
            {
                try
                {
                    using (DBEntities db = new DBEntities())
                    {
                        
                        var items = db.Clientes.Where(b => b.rfc == txtBusqueda.Text).ToList<Cliente>();
                        if (items.Count == 0)
                                {
                                    MessageBox.Show(Settings.Default.MensajeNoEncontrado);
                            btnAgregarCliente.Visibility = System.Windows.Visibility.Visible;
                                }
                                else
                                {
                                    dg_Cliente.ItemsSource = items;
                                    btnEliminar.Visibility = System.Windows.Visibility.Visible;
                            btnEditar.Visibility = System.Windows.Visibility.Visible;
                        }
                                txtDate.Text = "";
                                
                           
                                                    

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

        private void btnAgregarCliente_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente2 registrarCliente = new RegistrarCliente2();
            registrarCliente.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registrarCliente.Show();
            closeWindow();

        }
    }
}
