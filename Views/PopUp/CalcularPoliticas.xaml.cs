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
    /// Lógica de interacción para CalcularPoliticas.xaml
    /// </summary>
    public partial class CalcularPoliticas : Window
    {
        private static int minimo = 3;
        private int politicasAprobadas = 0;

        private bool calculoRealizado = false;

        private RealizarDictamen previous;
        public CalcularPoliticas(RealizarDictamen previous)
        {
            InitializeComponent();
            this.previous = previous;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            closeWindow();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!calculoRealizado)
            {
                MessageBox.Show("No se ha realizado el calculo de las políticas");
            }
            else
            {
                previous.porcentajeRealizado = true;
                previous.setResultado((String)lblResultado.Content);
                closeWindow();
            }
        }

        private void btnCalcularPoliticas_Click(object sender, RoutedEventArgs e)
        {
            if (politicasAprobadas > minimo)
            {
                lblResultado.Content = "aceptada";
            }
            else
            {
                lblResultado.Content = "rechazada";
            }
            calculoRealizado = true;
            
        }

        private void chbMayorEdad_Checked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas++;
        }

        private void chbMayorEdad_Unchecked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas--;
        }

        private void chbMexicano_Checked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas++;
        }

        private void chbMexicano_Unchecked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas--;
        }

        private void chbSueldoMensual_Checked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas++;
        }

        private void chbSueldoMensual_Unchecked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas++;
        }

        private void chbListaNegra_Checked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas++;
        }

        private void chbListaNegra_Unchecked(object sender, RoutedEventArgs e)
        {
            politicasAprobadas--;
        }

        private void closeWindow()
        {
            this.Close();
        }

    }
}
