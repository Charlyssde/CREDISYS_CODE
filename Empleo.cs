//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CREDISYS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleo
    {
        public int idEmpleo { get; set; }
        public string nombreEmpresa { get; set; }
        public string rfcCliente { get; set; }
        public string numEmpleado { get; set; }
        public string centroDeTrabajo { get; set; }
        public int antiguedadMeses { get; set; }
        public string ocupacion { get; set; }
        public string puesto { get; set; }
        public string periodoPresentacion { get; set; }
        public string estatus { get; set; }
    
        public virtual Cliente Cliente { get; set; }
    }
}
