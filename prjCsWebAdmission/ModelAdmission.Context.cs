﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjCsWebAdmission
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GestionAdmissionEntitiesFramework : DbContext
    {
        public GestionAdmissionEntitiesFramework()
            : base("name=GestionAdmissionEntitiesFramework")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admission> admissions { get; set; }
        public virtual DbSet<candidat> candidats { get; set; }
        public virtual DbSet<lettre> lettres { get; set; }
        public virtual DbSet<programme> programmes { get; set; }
        public virtual DbSet<session> sessions { get; set; }
        public virtual DbSet<utilisateur> utilisateurs { get; set; }
        public virtual DbSet<lval> lvals { get; set; }
    }
}
