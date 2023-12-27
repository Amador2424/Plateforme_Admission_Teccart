using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class DemandeAdmission1 : System.Web.UI.Page

    {
        static SGACEntitiesFl db = new SGACEntitiesFl();
        static List<programme> programmes = new List<programme>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Chargement Des Programmes
                drpCycle.DataSource = db.NivEtudes.ToList();
                // Remplacez "Text" par le nom de la propriété que vous souhaitez afficher dans la liste déroulante
                drpCycle.DataTextField = "Niveau";

                // Remplacez "Id" par le nom de la propriété que vous souhaitez utiliser comme valeur de chaque élément
                drpCycle.DataValueField = "Id";
                drpCycle.DataBind();

                //Chargement Session 
                radSession.DataSource = db.sessions.ToList();
                radSession.DataTextField = "CodeSession";
                radSession.DataValueField = "id";
                radSession.DataBind();
                radSession.SelectedIndex = 0;

                //Chargement des Programme

                //1er Programme
                NivEtude cycleChoisi = db.NivEtudes.FirstOrDefault(c => c.id.ToString() == drpCycle.SelectedItem.Value.ToString());

                programmes = db.programmes.Where(c => c.IdNiveauEtude == cycleChoisi.id).ToList();

                // 1er choix
                drpFirstChoix.DataSource = programmes;
                drpFirstChoix.DataTextField = "Intitule";
                drpFirstChoix.DataValueField = "id";
                drpFirstChoix.DataBind();

            }
        }

        protected void btnSuivant_Click(object sender, EventArgs e)
        {
            if(drpCycle.SelectedIndex !=-1 && drpFirstChoix.SelectedIndex!=-1 && drpSecondChoix.SelectedIndex !=-1 && drpThrirdChoix.SelectedIndex != -1)
            {
                demandeadmission mydmd = new demandeadmission();
                mydmd.IDCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]); 
               
                mydmd.IDNivEtud =Convert.ToInt32(drpCycle.SelectedItem.Value);
                mydmd.TypeDemande = drpCycle.SelectedItem.Text;
                mydmd.IDSession = Convert.ToInt32(radSession.SelectedItem.Value);
                mydmd.Statut = "Debut";
                var dernierElement = db.demandeadmissions
                .OrderByDescending(elt => elt.id)
                .FirstOrDefault();
                if(dernierElement == null)
                {
                    mydmd.id = 0;
                }
                else
                {
                    mydmd.id = dernierElement.id + 1;
                }
                
                 db.demandeadmissions.Add(mydmd);
                
                regime myrm = new regime();
                myrm.IdProgramme = Convert.ToInt32(drpFirstChoix.SelectedItem.Value);
                myrm.IdAdmission = mydmd.id;
                myrm.regime1 = 0;
                
                myrm.Type = radRegimeC1.SelectedItem.Text;
                db.regimes.Add(myrm);
                myrm = new regime();
                myrm.IdProgramme = Convert.ToInt32(drpSecondChoix.SelectedItem.Value);
                myrm.IdAdmission = mydmd.id;
                myrm.Type = radRegimeC2.SelectedItem.Text;
                myrm.regime1 = 1;
                db.regimes.Add(myrm);
                myrm = new regime();
                myrm.IdProgramme = Convert.ToInt32(drpThrirdChoix.SelectedItem.Value);
                myrm.IdAdmission = mydmd.id;
                myrm.Type = radRegimeC2.SelectedItem.Text;
                myrm.regime1 = 2;
                db.regimes.Add(myrm);
                db.SaveChanges();

                HttpContext.Current.Session["idDemande"] = mydmd.id;

                if (drpCycle.SelectedItem.Text == "Premier Cycle")
                {
                    
                    Response.Redirect("DemandeAdmission2.aspx");
                }
                else
                {
                    Server.Transfer("ContactProf.aspx");
                }

            }
            else
            {
                lblError.Text = "Veuillez Choisir vos 03 programmes";
            }
           
        }

        protected void drpCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            NivEtude cycleChoisi = db.NivEtudes.FirstOrDefault(c => c.id.ToString() == drpCycle.SelectedItem.Value.ToString());

            programmes = db.programmes.Where(c => c.IdNiveauEtude == cycleChoisi.id).ToList();

            // 1er choix
            drpFirstChoix.DataSource = programmes;
            drpFirstChoix.DataTextField = "Intitule";
            drpFirstChoix.DataValueField = "id";
            drpFirstChoix.DataBind();

            if (radRegimeC1.SelectedIndex == -1)
            {
                radRegimeC1.Items.Add(new ListItem("Temps Partiel", "0"));
                radRegimeC1.Items.Add(new ListItem("Temps Complet", "1"));
                radRegimeC1.SelectedIndex = 0;
            }
        }

        protected void drpFirstChoix_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 2ème choix
            NivEtude cycleChoisi = db.NivEtudes.FirstOrDefault(c => c.id.ToString() == drpCycle.SelectedItem.Value.ToString());

            programmes = db.programmes.Where(c => c.IdNiveauEtude == cycleChoisi.id).ToList();
            programme tmp1 = programmes.FirstOrDefault(c => c.id.ToString() == drpFirstChoix.SelectedItem.Value.ToString());
            programmes.Remove(tmp1);
            drpSecondChoix.DataSource = programmes;
            drpSecondChoix.DataTextField = "Intitule";
            drpSecondChoix.DataValueField = "id";
            drpSecondChoix.DataBind();
            //regime 2 
            if(radRegimeC2.SelectedIndex == -1)
            {
                radRegimeC2.Items.Add(new ListItem("Temps Partiel", "0"));
                radRegimeC2.Items.Add(new ListItem("Temps Complet", "1"));
                radRegimeC2.SelectedIndex = 0;
            }
        }

        protected void drpSecondChoix_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 3ème choix
            programme tmp2 = programmes.FirstOrDefault(c => c.id.ToString() == drpSecondChoix.SelectedItem.Value.ToString());
            programmes.Remove(tmp2);
            drpThrirdChoix.DataSource = programmes;
            drpThrirdChoix.DataTextField = "Intitule";
            drpThrirdChoix.DataValueField = "id";
            drpThrirdChoix.DataBind();
            //Regime3
            if(radRegimeC3.SelectedIndex == -1)
            {
                radRegimeC3.Items.Add(new ListItem("Temps Partiel", "0"));
                radRegimeC3.Items.Add(new ListItem("Temps Complet", "1"));
                radRegimeC3.SelectedIndex = 0;
            }
        }
    }
}