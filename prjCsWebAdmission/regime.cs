//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class regime
    {
        public int Id { get; set; }
        public int IdAdmission { get; set; }
        public int IdProgramme { get; set; }
        public Nullable<int> regime1 { get; set; }
        public string Type { get; set; }
    
        public virtual demandeadmission demandeadmission { get; set; }
        public virtual programme programme { get; set; }
    }
}