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
    /// Lógica de interacción para VisualizarCliente.xaml
    /// </summary>
    public partial class VisualizarCliente : Window
    {
        Cliente cliente;
        public VisualizarCliente(Cliente cliente)
        {
            this.cliente = cliente;
            InitializeComponent();
            cargarDatos();
        }
        public void cargarDatos()
        {
            using (DBEntities db = new DBEntities())
            {
                
                rfc.Content = cliente.rfc;
                nombre.Content = cliente.nombre;
                apellidoPaterno.Content = cliente.apellidoPaterno;
                apellidoMaterno.Content = cliente.apellidoMaterno;
               
                Pai pais = db.Pais.Where(b => b.idPais == cliente.idPais).FirstOrDefault();
                paislbl.Content = pais.pais;
                Estado estado = db.Estadoes.Where(b => b.idEstado == cliente.idEstado).FirstOrDefault();
                estadolbl.Content = estado.estado1;
                Ciudad ciu = db.Ciudads.Where(b => b.idCiudad == cliente.idCiudad).FirstOrDefault();
                ciudadlbl.Content = ciu.ciudad1;
                curp.Content = cliente.curp;
                estadocivil.Content = cliente.estadoCivil;
                genero.Content = cliente.genero;
                fecha.Content = cliente.fechaNacimiento;
                Domicilio domi = db.Domicilios.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();
                colonia.Content = domi.colonia;
                calle.Content = domi.calle;
                CP.Content = domi.codPostal;
                TipoDomicilio tipodomicilio = db.TipoDomicilios.Where(b => b.idTipoDomicilio == domi.idTipoDomicilio).FirstOrDefault();
                tipoDomi.Content = tipodomicilio.tipoDomicilio1;
                numex.Content = domi.numExt;
                numint.Content = domi.numIn;
                Pai paisDomi = db.Pais.Where(b => b.idPais == domi.idPais).FirstOrDefault();
                paisDom.Content = pais.pais;
                Estado estadoDomi = db.Estadoes.Where(b => b.idEstado == domi.idEstado).FirstOrDefault();
                estadoDom.Content = estado.estado1;
                Ciudad ciuDomi = db.Ciudads.Where(b => b.idCiudad == domi.idCiudad).FirstOrDefault();
                ciudadDom.Content = ciu.ciudad1;
                Tiempo.Content = domi.tiempoResidencia;
                Empleo empleo = db.Empleos.Where(b => b.rfcCliente == cliente.rfc).FirstOrDefault();
                nombreEmpresa.Content = empleo.nombreEmpresa;
                numEmple.Content = empleo.numEmpleado;
                centroTrabajo.Content = empleo.centroDeTrabajo;
                antiguedad.Content = empleo.antiguedadMeses;
                ocupacion.Content = empleo.ocupacion;
                puesto.Content = empleo.puesto;
                presentacion.Content = empleo.periodoPresentacion;



            }

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
