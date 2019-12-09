using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps.Packaging;

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para VisualizarExpediente.xaml
    /// </summary>
    public partial class VisualizarExpediente : Window
    {
        private Expediente expediente;
        private Solicitud solicitud;
        public VisualizarExpediente(Solicitud solicitud)
        {
            InitializeComponent();
            cargarDocumentos();
            this.solicitud = solicitud;
        }

        private void chbDocumentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            byte[] byteFile = cargarBytes();

            using (DBEntities db = new DBEntities())
            {
                Cliente cliente = db.Clientes.Where(b => b.rfc == this.solicitud.rfcCliente).FirstOrDefault();
                String path = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio + "\\";

                switch (chbDocumentos.SelectedItem.ToString())
                {
                    case "Solicitud":
                        path = path + "Solicitud.pdf";
                        break;
                    case "Domicializacion":
                        path = path + "Domiciliacion.pdf";
                        break;
                    case "Pagare":
                        path = path + "Pagare.pdf";
                        break;
                    case "INE":
                        path = path + "Solicitud.pdf";
                        break;
                    case "Comprobante de domicilio":
                        path = path + "Solicitud.pdf";
                        break;
                    case "Estado de cuenta":
                        path = path + "Solicitud.pdf";
                        break;
                    case "Recibo de pago":
                        path = path + "Solicitud.pdf";
                        break;
                    case "Caratula de apertura":
                        path = path + "Caratula.pdf";
                        break;

                    default:
                        path = path + "Solicitud.pdf";
                        break;
                }
                XpsDocument doc = new XpsDocument(path, FileAccess.Read);
                pdfViewer.Document = doc.GetFixedDocumentSequence();

            }

                

           
        }

        private byte[] cargarBytes()
        {
            
            using (DBEntities db = new DBEntities())
            {
                this.expediente = db.Expedientes.Where(b => b.idExpediente == this.solicitud.folio).SingleOrDefault();
                switch (chbDocumentos.SelectedItem.ToString())
                {
                    case "Solicitud":
                        return this.expediente.solicitud;
                    case "Domicializacion":
                        return this.expediente.domicializacion;
                    case "Pagare":
                        return this.expediente.pagare;
                    case "INE":
                        return this.expediente.INE;
                    case "Comprobante de domicilio":
                        return this.expediente.comprobanteDomicilio;
                    case "Estado de cuenta":
                        return this.expediente.estadoCuenta;
                    case "Recibo de pago":
                        return this.expediente.reciboPago;
                    case "Caratula de apertura":
                        return this.expediente.caratula;

                    default:
                        return null;
                        
                }
            }
        }

        private void cargarDocumentos()
        {
            String[] docs = {"Caratula", "Solicitud", "Domicializacion", "Pagare", "INE", "Comprobante de domicilio", "Estado de cuenta", "Recibo de pago", "Caratula de apertura"};
            chbDocumentos.ItemsSource = docs;
        }

        private void btnHecho_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
