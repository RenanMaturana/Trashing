﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasureroWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class basureroEntities : DbContext
    {
        public basureroEntities()
            : base("name=basureroEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<area> area { get; set; }
        public virtual DbSet<basurero> basurero { get; set; }
        public virtual DbSet<cargousuario> cargousuario { get; set; }
        public virtual DbSet<ciudad> ciudad { get; set; }
        public virtual DbSet<detalleusuariobasurero> detalleusuariobasurero { get; set; }
        public virtual DbSet<estadobasurero> estadobasurero { get; set; }
        public virtual DbSet<estadousuario> estadousuario { get; set; }
        public virtual DbSet<ubicacion> ubicacion { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
    }
}
