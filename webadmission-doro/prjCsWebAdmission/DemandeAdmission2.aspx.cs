using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class DemandeAdmission2 : System.Web.UI.Page
    {
        static SGACEntitiesFinal db =new  SGACEntitiesFinal();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
               // HttpContext.Current.Session["idCandidat"] = 1;
                string nom = db.candidats.Find(Convert.ToInt32(HttpContext.Current.Session["idCandidat"])).NomUtilisateur;
                lblWelcome.Text = "Welcome "+nom;
            }
        }

        protected void btnTelecharger_Click(object sender, EventArgs e)
        {
           if(fileDernierDiplome.HasFile && fileActeNaissance.HasFile && filePhoto.HasFile)
            {
                byte[] photo, acteN, lastDip;
                using (Stream fs = filePhoto.PostedFile.InputStream)
                {
                    photo = new byte[fs.Length];
                    fs.Read(photo, 0, (int)fs.Length);
                }
                using (Stream fs = fileActeNaissance.PostedFile.InputStream)
                {
                    acteN = new byte[fs.Length];
                    fs.Read(acteN, 0, (int)fs.Length);
                }
                using (Stream fs = fileDernierDiplome.PostedFile.InputStream)
                {
                    lastDip = new byte[fs.Length];
                    fs.Read(lastDip, 0, (int)fs.Length);
                }
                document mydoc = new document();
                mydoc.idCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                mydoc.idAdmission = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);
                mydoc.Photo = photo;
                mydoc.ActeNaiss = acteN;
                mydoc.DernierDiplome = lastDip;
                db.demandeadmissions.Find(Convert.ToInt32(HttpContext.Current.Session["idDemande"])).DateSousmission = DateTime.Now;
                db.demandeadmissions.Find(Convert.ToInt32(HttpContext.Current.Session["idDemande"])).Statut = "Soumis";
                db.documents.Add(mydoc);
                db.SaveChanges();
                //var candidatExistant = db.candidats.FirstOrDefault(c => c.id == mydoc.idCandidat);
                Server.Transfer("Login.aspx");

            }
            else
            {
                lblMessage.Text = "Veillez Televersez tout vos documents";
            }
        }
    }
}