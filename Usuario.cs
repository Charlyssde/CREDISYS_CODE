
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
    
public partial class Usuario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Usuario()
    {

        this.Solicituds = new HashSet<Solicitud>();

    }


    public string username { get; set; }

    public byte[] password { get; set; }

    public string nombre { get; set; }

    public int idRol { get; set; }



    public virtual Rol Rol { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Solicitud> Solicituds { get; set; }

}

}
