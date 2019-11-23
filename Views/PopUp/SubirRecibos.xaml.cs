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
            if (txtName.Text.Trim().Equals("") || txtFile.Text.Trim().Equals(""))
            {
                System.Windows.MessageBox.Show("El nombre es obligatorio");
                return;
            }
            byte[] file = null;
            Stream mystream = openFileDialog1.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {
                mystream.CopyTo(ms);
                file = ms.ToArray();
            }
            
            using (DBEntities db = new DBEntities())
            {
                Expediente expediente = new Expediente();
                expediente.rfcCliente = "HOLA";
                expediente.INE = file;
                expediente.pagare = file;
                expediente.reciboPago = file;
                expediente.solicitud = file;
                expediente.caratula = file;
                expediente.domicializacion = file;
                expediente.comprobanteDomicilio = file;
                expediente.estadoCuenta = file; 
                expediente.folio = 3; 

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
    }
}

