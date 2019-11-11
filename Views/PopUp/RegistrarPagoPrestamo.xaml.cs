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
    /// Lógica de interacción para RegistrarPagoPrestamo.xaml
    /// </summary>
    public partial class RegistrarPagoPrestamo : Window
    {
        public RegistrarPagoPrestamo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                try
                {
                    if (txtFolio.Text.Equals(""))
                    {
                        MessageBox.Show(Settings.Default.MensajeCamposVacios);
                    }
                    else
                    {
                      
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show(Settings.Default.MensajeErrorBD);
                }

            }
        }
    }
}
