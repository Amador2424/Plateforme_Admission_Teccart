using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class Info_utilisateur : System.Web.UI.Page
    {
        static SGACEntitiesFl context = new SGACEntitiesFl();
        static string codeU;
        protected void Page_Load(object sender, EventArgs e)

        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            context.SaveChanges();
            codeU = HttpContext.Current.Session["codeU"].ToString();
            candidat user = context.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeU);
            HttpContext.Current.Session["idCandidat"] = user.id;
            string script = @"<script type='text/javascript'>
                        if (window.opener && !window.opener.closed) {
                            window.opener.location.reload(true);
                        }
                    </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "ActualiserPageParente", script);
            if (!IsPostBack)
            {

                
                
                LblEmail.Text =user.CourrierPersonnel;
                LblCompte.Text = codeU;
                lblAdresse.Text = user.AdresseUtilisateur;
                LblNom.Text = user.NomUtilisateur +" "+ user.PrenomUtilisateur;

            }
            GridViewCandidatures.DataSource = user.demandeadmissions.ToList();
            GridViewCandidatures.DataBind();
        }
         protected void GridViewCandidatures_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnModifier_Click(object sender, EventArgs e)
        {
            LinkButton btnModifier = (LinkButton)sender;
            string idCandidature = btnModifier.CommandArgument;
            HttpContext.Current.Session["idDemande"] = idCandidature;
            candidat user = context.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeU);
            HttpContext.Current.Session["idCandidat"] = user.id;
            Server.Transfer("MajAdminssion1.aspx");



        }

        protected void btnSupprimer_Click(object sender, EventArgs e)
        {
            
            LinkButton btnSupprimer = (LinkButton)sender;
            int idCandidature = Convert.ToInt32(btnSupprimer.CommandArgument) ;
            //LblNom.Text = idCandidature.ToString();
            candidat user = context.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeU);
            demandeadmission mydmd = user.demandeadmissions.FirstOrDefault(c => c.id == idCandidature);
            //lblAdresse.Text = mydmd.Statut;
            foreach (var monregime in context.regimes.Where(c => c.IdAdmission == mydmd.id).ToList())
            {
                context.regimes.Remove(monregime);
            }

            foreach (var mydoc in context.documents.Where(c => c.idAdmission == mydmd.id).ToList())
            {
                context.documents.Remove(mydoc);
            }

            foreach (var malttre in context.lettrerecommandations.Where(c => c.IDAdmission == mydmd.id).ToList())
            {
                context.lettrerecommandations.Remove(malttre);
            }

            // En dehors des boucles, supprimer la candidature
            context.demandeadmissions.Remove(mydmd);

            // Sauvegarder les modifications dans la base de données
            context.SaveChanges();
            var candidatExistant = context.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeU);
            Server.Transfer("Info_utilisateur.aspx?codeU=" + candidatExistant.CodeUtilisateur);
        }

        protected void BtnInfo_Click(object sender, EventArgs e)
        {
            candidat user = context.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeU);
            HttpContext.Current.Session["idCandidat"] = user.id;
            Response.Redirect("majCandidat.aspx");
            
        }

        protected void btnPremierCycle_Click(object sender, EventArgs e)
        {
            Server.Transfer("DemandeAdmission1.aspx");
        }
    }
}
