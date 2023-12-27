using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class majAdmission3 : System.Web.UI.Page
    {
        SGACEntitiesFl db = new SGACEntitiesFl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // HttpContext.Current.Session["idCandidat"] = 1;
                string nom = db.candidats.Find(Convert.ToInt32(HttpContext.Current.Session["idCandidat"])).NomUtilisateur;
                lblWelcome.Text = "Welcome " + nom;
                int id = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);
                foreach (var mydoc in db.documents.Where(c => c.idAdmission == id).ToList())
                {
                    db.documents.Remove(mydoc);
                }
            }
        }

        protected void btnTelecharger_Click(object sender, EventArgs e)
        {
            if (fileDernierDiplome.HasFile && fileActeNaissance.HasFile && filePhoto.HasFile)
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
                Int32 idC = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                Int32 idDmd = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);
                document mydoc = new document();
                mydoc.Photo = photo;
                mydoc.ActeNaiss = acteN;
                mydoc.DernierDiplome = lastDip;
                mydoc.idCandidat = idC;
                mydoc.idAdmission = idDmd;
                db.demandeadmissions.Find(Convert.ToInt32(HttpContext.Current.Session["idDemande"])).DateSousmission = DateTime.Now;
                db.demandeadmissions.Find(Convert.ToInt32(HttpContext.Current.Session["idDemande"])).Statut = "Soumis";
                db.documents.AddOrUpdate(mydoc);
                db.demandeadmissions.AddOrUpdate(db.demandeadmissions.Find(Convert.ToInt32(HttpContext.Current.Session["idDemande"])));
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