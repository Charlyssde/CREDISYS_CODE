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
    /// Lógica de interacción para RegistrarPago.xaml
    /// </summary>
    public partial class RegistrarPago : Window
    {
        public RegistrarPago()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                var solicitud = new Solicitud { rfcCliente = rfc.Text  };
                db.Solicituds.Attach(solicitud);
                solicitud.amortizacion = Convert.ToInt32(folio.Text);
                
                db.SaveChanges();
                System.Windows.MessageBox.Show(Settings.Default.MensajeExito);
            }
        }
    }
}
