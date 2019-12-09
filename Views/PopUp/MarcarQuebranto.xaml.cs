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
    /// Lógica de interacción para MarcarQuebranto.xaml
    /// </summary>
    public partial class MarcarQuebranto : Window
    {
        public MarcarQuebranto()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                var cliente = new Cliente { rfc = RFC.Text };
                db.Clientes.Attach(cliente);
                cliente.estatus = "Cliente deudor";

                db.SaveChanges();
                System.Windows.MessageBox.Show(Properties.Settings.Default.MensajeExito);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                var solicitud = new Solicitud { folio = Convert.ToInt32(foliotxt.Text) };
                db.Solicituds.Attach(solicitud);
                solicitud.estatus1 = "Quebranto fiscal";

                db.SaveChanges();
                System.Windows.MessageBox.Show(Properties.Settings.Default.MensajeExito);
            }
        }
    }
}
