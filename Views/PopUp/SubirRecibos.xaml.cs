using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para SubirRecibos.xaml
    /// </summary>
    public partial class SubirRecibos : Window
    {
        byte[] arregloPdf;
        public SubirRecibos()
        {
            InitializeComponent();
            
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string file_name = string.Empty;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.ShowDialog();
                 string dir = openFileDialog1.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);


            
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Dashboard_Capturista dashboard_Capturista = new Dashboard_Capturista();
            dashboard_Capturista.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dashboard_Capturista.Show();
            closeWindow();
        }
        private void closeWindow()
        {
            this.Close();
        }
    }
}
