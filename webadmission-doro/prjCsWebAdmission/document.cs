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
    
    public partial class document
    {
        public int Id { get; set; }
        public Nullable<int> idAdmission { get; set; }
        public Nullable<int> idCandidat { get; set; }
        public byte[] Photo { get; set; }
        public byte[] ActeNaiss { get; set; }
        public byte[] DernierDiplome { get; set; }
    
        public virtual candidat candidat { get; set; }
        public virtual demandeadmission demandeadmission { get; set; }
    }
}
