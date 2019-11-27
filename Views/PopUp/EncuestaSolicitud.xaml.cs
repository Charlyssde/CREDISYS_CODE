using CREDISYS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    /// Lógica de interacción para EncuestaSolicitud.xaml
    /// </summary>
    public partial class EncuestaSolicitud : Window
    {
        public String comentarios;

        private Solicitud solicitud;
        public EncuestaSolicitud(Solicitud solicitud)
        {
            InitializeComponent();
            cargarCbParentesco();
            this.solicitud = solicitud;
        }

        private void rbNoUno_Checked(object sender, RoutedEventArgs e)
        {
            txtPeriodoFinal.IsEnabled = false;
            txtPeriodoInicio.IsEnabled = false;
            txtPuestoUno.IsEnabled = false;
            txtPuestoUno.Text = "";
            txtPeriodoFinal.Text = "";
            txtPeriodoInicio.Text = "";
        }

        private void rbSiUno_Checked(object sender, RoutedEventArgs e)
        {
            txtPeriodoFinal.IsEnabled = true;
            txtPeriodoInicio.IsEnabled = true;
            txtPuestoUno.IsEnabled = true;
        }

        private void rbNoDos_Checked(object sender, RoutedEventArgs e)
        {
            txtPuestoDos.IsEnabled = false;
            txtPeriodoFinalDos.IsEnabled = false;
            txtPeriodoInicioDos.IsEnabled = false;
            txtApellidoMaterno.IsEnabled = false;
            txtApellidoPaterno.IsEnabled = false;
            txtNombre.IsEnabled = false;
        }

        private void rbSiDos_Checked(object sender, RoutedEventArgs e)
        {
            txtPuestoDos.IsEnabled = true;
            txtPeriodoFinalDos.IsEnabled = true;
            txtPeriodoInicioDos.IsEnabled = true;
            txtApellidoMaterno.IsEnabled = true;
            txtApellidoPaterno.IsEnabled = true;
            txtNombre.IsEnabled = true;
        }

        private void btnComentarios_Click(object sender, RoutedEventArgs e)
        {
            Comentarios comentarios = new Comentarios(this);
            comentarios.ShowDialog();

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

            generarDocumentos();
            MessageBox.Show("Documentos generados: Puede buscarlos en su escritorio");
            closeWindow();
        }

        private void generarDocumentos()
        {
            using (DBEntities db = new DBEntities())
            {
                Cliente cliente = db.Clientes.Where(b => b.rfc == this.solicitud.rfcCliente).SingleOrDefault();
                CondicionCredito condicion = db.CondicionCreditoes.Where(b => b.idCondicionCredito == this.solicitud.idCondicion).FirstOrDefault();

                //PAGARÉ
                ReportDocument pagare = new ReportDocument();
                // Aquí pones la ruta del archivo .rpt de tu reporte
                pagare.Load("C:\\Users\\texch\\Desktop\\7o Semestre\\Desarrollo De Software\\CREDISYS_CODE\\Pagare.rpt");
                //parametros:
                pagare.SetParameterValue("MontoNumero", this.solicitud.montoNumero);
                pagare.SetParameterValue("montoLetra", this.solicitud.montoLetra);
                pagare.SetParameterValue("amortizacion", this.solicitud.montoLetra);
                pagare.SetParameterValue("interes", condicion.interes);
                pagare.SetParameterValue("lugar", "Xalapa, Veracruz");
                pagare.SetParameterValue("dia", this.solicitud.fecha.Day);
                pagare.SetParameterValue("mes", this.solicitud.fecha.Month);
                pagare.SetParameterValue("anio", this.solicitud.fecha.Year);

                try
                {
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    // Pones la ruta y el nombre del archivo pdf que se va a generar
                    CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\texch\\Desktop\\" + cliente.rfc + "_" + this.solicitud.folio + ".pdf";
                    CrExportOptions = pagare.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    pagare.Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                //SOLICITUD


                //DOMICILIACION


                //CARATULA

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DBEntities db = new DBEntities())
                {
                    Solicitud cancelada = db.Solicituds.Where(b => b.folio == solicitud.folio).SingleOrDefault();
                    cancelada.estatus1 = Settings.Default.SolicitudEstatus2;
                    db.SaveChanges();
                    closeWindow();
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Settings.Default.MensajeErrorBD);
            }
                
        }

        private void cargarCbParentesco()
        {
            String[] lista = { "Padre", "Hermano", "Hijo", "Abuelo", "Tio", "Primo", "Cuñado", "Suegro", "Nuera/Yerno", "Conyuge"};
            cbParentensco.ItemsSource = lista;
        }

        private void closeWindow()
        {
            this.Close();
        }
    }
}
