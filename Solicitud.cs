
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
    
public partial class Solicitud
{

    public int folio { get; set; }

    public string vendedor { get; set; }

    public System.DateTime fecha { get; set; }

    public double montoNumero { get; set; }

    public string montoLetra { get; set; }

    public string destinoCredito { get; set; }

    public string disposicion { get; set; }

    public string rfcCliente { get; set; }

    public int idCondicion { get; set; }

    public string estatus1 { get; set; }



    public virtual CondicionCredito CondicionCredito { get; set; }

    public virtual Usuario Usuario { get; set; }

    public virtual Cliente Cliente { get; set; }

}

}
