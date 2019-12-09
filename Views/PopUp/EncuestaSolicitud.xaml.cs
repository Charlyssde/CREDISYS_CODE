using CREDISYS.Properties;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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

namespace CREDISYS.Views.PopUp
{
    /// <summary>
    /// Lógica de interacción para EncuestaSolicitud.xaml
    /// </summary>
    public partial class EncuestaSolicitud : Window
    {
        public String comentarios;

        private Solicitud solicitud;
        private Usuario usuario;

        public EncuestaSolicitud(Solicitud solicitud, Usuario usuario)
        {
            InitializeComponent();
            cargarCbParentesco();
            this.solicitud = solicitud;
            this.usuario = usuario;
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


                //PAGARÉ
                ReportDocument pagare = new ReportDocument();
                // Aquí pones la ruta del archivo .rpt de tu reporte
                pagare.Load("C:\\Users\\texch\\Desktop\\7o Semestre\\Desarrollo De Software\\CREDISYS_CODE\\Pagare.rpt");
                //parametros:
                Cliente cliente = db.Clientes.Where(b => b.rfc == this.solicitud.rfcCliente).SingleOrDefault();
                CondicionCredito condicion = db.CondicionCreditoes.Where(b => b.idCondicionCredito == this.solicitud.idCondicion).FirstOrDefault();
                pagare.SetParameterValue("MontoNumero", this.solicitud.montoNumero);
                pagare.SetParameterValue("montoLetra", this.solicitud.montoLetra);
                pagare.SetParameterValue("amortizacion", this.solicitud.amortizacion);
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
                    Directory.CreateDirectory("C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio);
                    CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio + "\\Pagare.pdf";
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
                ReportDocument solicitudRpt = new ReportDocument();
                // Aquí pones la ruta del archivo .rpt de tu reporte
                solicitudRpt.Load("C:\\Users\\texch\\Desktop\\7o Semestre\\Desarrollo De Software\\CREDISYS_CODE\\Solicitud.rpt");
                //parametros:

                Domicilio domicilio = db.Domicilios.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();
                Ciudad ciudadDom = db.Ciudads.Where(b => b.idCiudad == domicilio.idCiudad).FirstOrDefault();
                Estado estadoDom = db.Estadoes.Where(b => b.idEstado == domicilio.idEstado).FirstOrDefault();
                Pai paisDom = db.Pais.Where(b => b.idPais == domicilio.idPais).FirstOrDefault();
                TipoDomicilio tipo = db.TipoDomicilios.Where(b => b.idTipoDomicilio == domicilio.idTipoDomicilio).FirstOrDefault();

                Telefono celular = db.Telefonoes.Where(b => b.tipoTelefono == "Móvil" && b.rfcCliente == cliente.rfc).FirstOrDefault();
                //Telefono casa = db.Telefonoes.Where(b => b.tipoTelefono == "Casa" && b.rfcCliente == cliente.rfc).FirstOrDefault();
                Correo correo = db.Correos.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();

                Estado nacEstado = db.Estadoes.Where(b => b.idEstado == cliente.idEstado).FirstOrDefault();
                Pai nacPais = db.Pais.Where(b => b.idPais == cliente.idPais).FirstOrDefault();

                Empleo empleo = db.Empleos.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();

                Referencia[] referencias = db.Referencias.Where(b => b.rfcCliente == cliente.rfc).ToArray<Referencia>();
                Referencia ref1 = referencias[0];
                Referencia ref2 = referencias[1];

                solicitudRpt.SetParameterValue("vendedor", this.usuario.nombre);
                solicitudRpt.SetParameterValue("supervisor", this.usuario.nombre);
                solicitudRpt.SetParameterValue("folio", this.solicitud.folio);
                solicitudRpt.SetParameterValue("fecha", this.solicitud.fecha);
                solicitudRpt.SetParameterValue("montoNumero", this.solicitud.montoNumero);
                solicitudRpt.SetParameterValue("montoLetra", this.solicitud.montoLetra);
                solicitudRpt.SetParameterValue("interes", condicion.interes);
                solicitudRpt.SetParameterValue("apellidoPaterno", cliente.apellidoPaterno);
                solicitudRpt.SetParameterValue("apellidoMaterno", cliente.apellidoMaterno);
                solicitudRpt.SetParameterValue("nombre", cliente.nombre);
                solicitudRpt.SetParameterValue("fechaNacimiento", cliente.fechaNacimiento.ToShortDateString());
                solicitudRpt.SetParameterValue("genero", cliente.genero);
                solicitudRpt.SetParameterValue("curp", cliente.curp);
                solicitudRpt.SetParameterValue("rfc", cliente.rfc);
                solicitudRpt.SetParameterValue("calle", domicilio.calle);
                solicitudRpt.SetParameterValue("numExt", domicilio.numExt);
                solicitudRpt.SetParameterValue("numInt", domicilio.numIn);
                solicitudRpt.SetParameterValue("colonia", domicilio.colonia);
                solicitudRpt.SetParameterValue("cp", domicilio.codPostal);
                solicitudRpt.SetParameterValue("ciudad", ciudadDom.ciudad1);
                solicitudRpt.SetParameterValue("estado", estadoDom.estado1);
                solicitudRpt.SetParameterValue("pais", paisDom.pais);
                solicitudRpt.SetParameterValue("residencia", domicilio.tiempoResidencia);
                solicitudRpt.SetParameterValue("tipoDomicilio", tipo.tipoDomicilio1);
                solicitudRpt.SetParameterValue("celular", celular.numero);
                //solicitudRpt.SetParameterValue("casa", casa.numero);
                solicitudRpt.SetParameterValue("estadoCivil", cliente.estadoCivil);
                solicitudRpt.SetParameterValue("nacionalidad", "Mexicano(a)");
                solicitudRpt.SetParameterValue("corre", correo.correo1);
                solicitudRpt.SetParameterValue("nacEstado", nacEstado.estado1);
                solicitudRpt.SetParameterValue("nacPais", nacPais.pais);
                solicitudRpt.SetParameterValue("destino", this.solicitud.destinoCredito);
                solicitudRpt.SetParameterValue("disposicion", "Efectivo");
                solicitudRpt.SetParameterValue("empresa", empleo.nombreEmpresa);
                solicitudRpt.SetParameterValue("noEmpleado", empleo.numEmpleado);
                solicitudRpt.SetParameterValue("centroTrabajo", empleo.centroDeTrabajo);
                solicitudRpt.SetParameterValue("antigEmpleo", empleo.antiguedadMeses);
                solicitudRpt.SetParameterValue("ocupacion", empleo.ocupacion);
                solicitudRpt.SetParameterValue("puesto", empleo.puesto);
                solicitudRpt.SetParameterValue("periodoPres", empleo.periodoPresentacion);
                solicitudRpt.SetParameterValue("refNombreUno", ref1.nombre);
                solicitudRpt.SetParameterValue("refRelUno", ref1.relacion);
                solicitudRpt.SetParameterValue("refTelUno", ref1.telefono);
                solicitudRpt.SetParameterValue("refDirUno", ref1.direccion);
                solicitudRpt.SetParameterValue("refNombreDos", ref2.nombre);
                solicitudRpt.SetParameterValue("refRelacionDos", ref2.relacion);
                solicitudRpt.SetParameterValue("refTelefonoDos", ref2.telefono);
                solicitudRpt.SetParameterValue("RefDirDos", ref2.direccion);

                try
                {
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    // Pones la ruta y el nombre del archivo pdf que se va a generar
                    CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio + "\\Solicitud.pdf";
                    CrExportOptions = solicitudRpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    solicitudRpt.Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                //DOMICILIACION
                ReportDocument domiciliacion = new ReportDocument();
                // Aquí pones la ruta del archivo .rpt de tu reporte
                domiciliacion.Load("C:\\Users\\texch\\Desktop\\7o Semestre\\Desarrollo De Software\\CREDISYS_CODE\\Domiciliación.rpt");
                //parametros:

                var tarjetas = db.Tarjetas.Where(b => b.rfcCliente == cliente.rfc).ToArray<Tarjeta>();
                Tarjeta tar1 = tarjetas[0];
                Tarjeta tar2 = tarjetas[1];
                Banco b1 = db.Bancoes.Where(b => b.idBanco == tar1.idBanco).FirstOrDefault();
                Banco b2 = db.Bancoes.Where(b => b.idBanco == tar2.idBanco).FirstOrDefault();

                domiciliacion.SetParameterValue("dia", this.solicitud.fecha.Day);
                domiciliacion.SetParameterValue("mes", this.solicitud.fecha.Month);
                domiciliacion.SetParameterValue("anio", this.solicitud.fecha.Year);
                domiciliacion.SetParameterValue("proveedor", "Financiera Independiente");
                domiciliacion.SetParameterValue("montoTotal", this.solicitud.montoNumero);
                domiciliacion.SetParameterValue("idCliente", cliente.rfc);
                domiciliacion.SetParameterValue("bancoUno", b1.banco1);
                domiciliacion.SetParameterValue("tarjetaUno", tar1.numTarjeta);
                domiciliacion.SetParameterValue("clabeUno", tar1.clabeBancaria);
                domiciliacion.SetParameterValue("telefonoUno", tar1.numTelefono);
                domiciliacion.SetParameterValue("bancoDos", b2.banco1);
                domiciliacion.SetParameterValue("tarjetaDos", tar2.numTarjeta);
                domiciliacion.SetParameterValue("clabeDos", tar2.clabeBancaria);
                domiciliacion.SetParameterValue("telefonoDos", tar2.numTelefono);
                domiciliacion.SetParameterValue("amortizacion", this.solicitud.amortizacion);
                domiciliacion.SetParameterValue("vence", this.solicitud.fecha.AddYears(1));

                try
                {
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    // Pones la ruta y el nombre del archivo pdf que se va a generar
                    CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio + "\\Domiciliacion.pdf";
                    CrExportOptions = domiciliacion.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    domiciliacion.Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }



                //CARATULA
                ReportDocument caratula = new ReportDocument();
                // Aquí pones la ruta del archivo .rpt de tu reporte
                caratula.Load("C:\\Users\\texch\\Desktop\\7o Semestre\\Desarrollo De Software\\CREDISYS_CODE\\CaratulaApertura.rpt");
                //parametros:
                caratula.SetParameterValue("nombreCliente", cliente.nombre);
                caratula.SetParameterValue("montoTotal", this.solicitud.montoNumero);
                caratula.SetParameterValue("interes", condicion.interes);
                Double x = this.solicitud.montoNumero - (this.solicitud.montoNumero * (condicion.interes / 100));
                caratula.SetParameterValue("creditoTotal", x);
                caratula.SetParameterValue("totalPagar", this.solicitud.montoNumero);
                caratula.SetParameterValue("amortizacion", this.solicitud.amortizacion);

                try
                {
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    // Pones la ruta y el nombre del archivo pdf que se va a generar
                    CrDiskFileDestinationOptions.DiskFileName = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio + "\\Caratula.pdf";
                    CrExportOptions = caratula.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    caratula.Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                Expediente expediente = db.Expedientes.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();
                String path = "C:\\Users\\texch\\Desktop\\Docs\\Exp\\" + cliente.rfc + "_" + this.solicitud.folio;
                Byte[] caratulaFile = File.ReadAllBytes(path + "\\Caratula.pdf");
                Byte[] solicitudFile = File.ReadAllBytes(path + "\\Solicitud.pdf");
                Byte[] pagareFile = File.ReadAllBytes(path + "\\Pagare.pdf");
                Byte[] domiciliacionFile = File.ReadAllBytes(path + "\\Domiciliacion.pdf");

                expediente.caratula = caratulaFile;
                expediente.solicitud = solicitudFile;
                expediente.pagare = pagareFile;
                expediente.domicializacion = domiciliacionFile;

                db.SaveChanges();


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
