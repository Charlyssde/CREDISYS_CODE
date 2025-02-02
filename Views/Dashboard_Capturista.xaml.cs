﻿using CREDISYS.Views.PopUp;
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
    /// Lógica de interacción para Dashboard_Capturista.xaml
    /// </summary>
    public partial class Dashboard_Capturista : Window
    {
        Usuario usuario;
        
        public Dashboard_Capturista(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            
        }

        private void btnSolicitudes_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            BuscarCliente buscarcliente = new BuscarCliente(this.usuario);
            buscarcliente.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            buscarcliente.Show();
            closeWindow();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }
        private void closeWindow()
        {
            this.Close();
        }

        private void btnSubir_Click(object sender, RoutedEventArgs e)
        {

            ModificarDocumentos modificarDoc = new ModificarDocumentos();
            modificarDoc.WindowStartupLocation = this.WindowStartupLocation;
            this.Hide();
            modificarDoc.ShowDialog();
            closeWindow();
        }
    }
}
