
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
    
public partial class Expediente
{

    public int idExpediente { get; set; }

    public byte[] caratula { get; set; }

    public byte[] solicitud { get; set; }

    public byte[] domicializacion { get; set; }

    public byte[] pagare { get; set; }

    public byte[] INE { get; set; }

    public byte[] comprobanteDomicilio { get; set; }

    public byte[] estadoCuenta { get; set; }

    public byte[] reciboPago { get; set; }

    public string rfcCliente { get; set; }

    public int folio { get; set; }



    public virtual Cliente Cliente { get; set; }

}

}
