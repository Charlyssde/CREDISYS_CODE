
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
    
public partial class Referencia
{

    public int idReferencia { get; set; }

    public string nombre { get; set; }

    public string telefono { get; set; }

    public string rfcCliente { get; set; }

    public string estatus { get; set; }

    public string relacion { get; set; }

    public string direccion { get; set; }

    public string horario { get; set; }



    public virtual Cliente Cliente { get; set; }

}

}
