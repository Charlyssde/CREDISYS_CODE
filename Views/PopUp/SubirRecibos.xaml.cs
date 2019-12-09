using CREDISYS.Properties;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;


namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para SubirRecibos.xaml
    /// </summary>
    public partial class SubirRecibos : Window
    {
        Cliente cliente;

        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        OpenFileDialog openFileDialog2 = new OpenFileDialog();
        OpenFileDialog openFileDialog3 = new OpenFileDialog();
        OpenFileDialog openFileDialog4 = new OpenFileDialog();
        OpenFileDialog openFileDialog5 = new OpenFileDialog();
        public SubirRecibos(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (DBEntities db = new DBEntities())
            {
                string file_name = string.Empty;



                openFileDialog1.ShowDialog();
                string dir = openFileDialog1.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);
                txtFile.Text = dir;


            }

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.Trim().Equals("") || txtFile.Text.Trim().Equals("") || txtFile2.Text.Trim().Equals("")
                 ||txtFile3.Text.Trim().Equals("") || txtFile4.Text.Trim().Equals("") )
            {
                System.Windows.MessageBox.Show("Elegir un archivo es obligatorio");
                return;
            }
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
                Expediente expediente = new Expediente();
                expediente.rfcCliente = cliente.rfc;
                expediente.INE = file;
                expediente.reciboPago = file4;
                expediente.comprobanteDomicilio = file2;
                expediente.estadoCuenta = file3;
            
                
                 

                try
                {
                    db.Expedientes.Add(expediente);
                    db.SaveChanges();
                    System.Windows.MessageBox.Show(Settings.Default.MensajeExito);
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                validationError.PropertyName,
                                validationError.ErrorMessage);
                        }
                    }
                }

            }
        }

        private void Buttonbuscar7_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                string file_name = string.Empty;



                openFileDialog5.ShowDialog();
                string dir = openFileDialog5.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);
                txtFile5.Text = dir;


            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                string file_name = string.Empty;



                openFileDialog2.ShowDialog();
                string dir = openFileDialog2.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);
                txtFile2.Text = dir;


            }

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                string file_name = string.Empty;



                openFileDialog3.ShowDialog();
                string dir = openFileDialog3.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);
                txtFile3.Text = dir;


            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            using (DBEntities db = new DBEntities())
            {
                string file_name = string.Empty;



                openFileDialog4.ShowDialog();
                string dir = openFileDialog4.FileName;
                string destino = System.IO.Path.GetDirectoryName(dir);
                txtFile4.Text = dir;


            }
        }
    }
}

