﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Banco> Bancoes { get; set; }
        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CondicionCredito> CondicionCreditoes { get; set; }
        public virtual DbSet<Correo> Correos { get; set; }
        public virtual DbSet<Domicilio> Domicilios { get; set; }
        public virtual DbSet<Empleo> Empleos { get; set; }
        public virtual DbSet<Estado> Estadoes { get; set; }
        public virtual DbSet<Expediente> Expedientes { get; set; }
        public virtual DbSet<Pai> Pais { get; set; }
        public virtual DbSet<PoliticaAprobacion> PoliticaAprobacions { get; set; }
        public virtual DbSet<Referencia> Referencias { get; set; }
        public virtual DbSet<Relacion> Relacions { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Solicitud> Solicituds { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tarjeta> Tarjetas { get; set; }
        public virtual DbSet<Telefono> Telefonoes { get; set; }
        public virtual DbSet<TipoDomicilio> TipoDomicilios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
