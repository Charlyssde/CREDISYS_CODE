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
using System.IO;
using System.Windows.Forms;
using CREDISYS.Properties;

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para ModificarDocumentos.xaml
    /// </summary>
    public partial class ModificarDocumentos : Window
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        OpenFileDialog openFileDialog2 = new OpenFileDialog();
        OpenFileDialog openFileDialog3 = new OpenFileDialog();
        OpenFileDialog openFileDialog4 = new OpenFileDialog();
        OpenFileDialog openFileDialog5 = new OpenFileDialog();
        public ModificarDocumentos()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] file = null;
            Stream mystream = openFileDialog1.OpenFile();
            byte[] file2 = null;
            Stream mystream2 = openFileDialog2.OpenFile();
            byte[] file3 = null;
            Stream mystream3 = openFileDialog3.OpenFile();
            byte[] file4 = null;
            Stream mystream4 = openFileDialog4.OpenFile();
            byte[] file5 = null;
            Stream mystream5 = openFileDialog5.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {
                mystream.CopyTo(ms);
                file = ms.ToArray();
                mystream2.CopyTo(ms);
                file2 = ms.ToArray();
                mystream3.CopyTo(ms);
                file3 = ms.ToArray();
                mystream4.CopyTo(ms);
                file4 = ms.ToArray();
                mystream5.CopyTo(ms);
                file5 = ms.ToArray();

            }



            using (DBEntities db = new DBEntities())
            {
                var expediente = new Expediente { rfcCliente = rfcCliente.Text };
                db.Expedientes.Attach(expediente);
                expediente.rfcCliente = rfcCliente.Text;
                expediente.folio = Convert.ToInt32(foliotxt.Text); 
                expediente.INE = file;
                expediente.reciboPago = file4;
                expediente.comprobanteDomicilio = file2;
                expediente.estadoCuenta = file3;
                
                db.SaveChanges();
                System.Windows.MessageBox.Show(Settings.Default.MensajeExito);
            }
        }



        private void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog1.ShowDialog();
            string dir = openFileDialog1.FileName;
            string destino = System.IO.Path.GetDirectoryName(dir);
            txtFile.Text = dir;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string file_name = string.Empty;



            openFileDialog2.ShowDialog();
            string dir = openFileDialog2.FileName;
            string destino = System.IO.Path.GetDirectoryName(dir);
            txtFile2.Text = dir;

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog3.ShowDialog();
            string dir = openFileDialog3.FileName;
            string destino = System.IO.Path.GetDirectoryName(dir);
            txtFile3.Text = dir;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog4.ShowDialog();
            string dir = openFileDialog4.FileName;
            string destino = System.IO.Path.GetDirectoryName(dir);
            txtFile4.Text = dir;
        }
    }
}
