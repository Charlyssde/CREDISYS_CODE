
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
    
public partial class Correo
{

    public int idCorreo { get; set; }

    public string correo1 { get; set; }

    public string rfcCliente { get; set; }

    public string estatus { get; set; }



    public virtual Cliente Cliente { get; set; }

}

}
