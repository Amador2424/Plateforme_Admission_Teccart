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
    
    public partial class laisonCP
    {
        public int ID { get; set; }
        public int IDProgramme { get; set; }
        public int IDCours { get; set; }
    
        public virtual cour cour { get; set; }
        public virtual programme programme { get; set; }
    }
}
