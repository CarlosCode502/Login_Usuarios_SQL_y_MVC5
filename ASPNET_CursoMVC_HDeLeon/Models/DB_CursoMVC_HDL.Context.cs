﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASPNET_CursoMVC_HDeLeon.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BD_CursoMVC_HDeLeonEntities : DbContext
    {
        public BD_CursoMVC_HDeLeonEntities()
            : base("name=BD_CursoMVC_HDeLeonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cstate> cstates { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
