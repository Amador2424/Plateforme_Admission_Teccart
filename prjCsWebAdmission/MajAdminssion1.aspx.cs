using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class MajAdminssion1 : System.Web.UI.Page
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
            if (drpCycle.SelectedIndex != -1 && drpFirstChoix.SelectedIndex != -1 && drpSecondChoix.SelectedIndex != -1 && drpThrirdChoix.SelectedIndex != -1)
            {
                

                int IDCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                int IDDemande = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);
                demandeadmission mydmd = db.demandeadmissions.FirstOrDefault(c=>c.IDCandidat == IDCandidat && c.id==IDDemande);
                mydmd.IDNivEtud = Convert.ToInt32(drpCycle.SelectedItem.Value);
                mydmd.TypeDemande = drpCycle.SelectedItem.Text;
                mydmd.Statut = "Modification en Cours...";
                string tmp = drpCycle.SelectedItem.Text;
                mydmd.IDSession = Convert.ToInt32(radSession.SelectedItem.Value);
                // mydmd.Statut = "Debut";
                db.demandeadmissions.AddOrUpdate(mydmd);
                var listRegime = db.regimes.Where(c => c.IdAdmission == IDDemande).ToList();

                //Update 1
                listRegime[0].IdProgramme = Convert.ToInt32(drpFirstChoix.SelectedItem.Value);
                listRegime[0].IdAdmission = mydmd.id;
                listRegime[0].regime1 = 0;
                listRegime[0].Type = radRegimeC1.SelectedItem.Text;
                db.regimes.AddOrUpdate(listRegime[0]);

                //Update 2
                listRegime[1].IdProgramme = Convert.ToInt32(drpSecondChoix.SelectedItem.Value);
                listRegime[1].IdAdmission = mydmd.id;
                listRegime[1].Type = radRegimeC2.SelectedItem.Text;
                listRegime[1].regime1 = 1;
                db.regimes.AddOrUpdate(listRegime[1]);
                //Update 3
                listRegime[2].IdProgramme = Convert.ToInt32(drpThrirdChoix.SelectedItem.Value);
                listRegime[2].IdAdmission = mydmd.id;
                listRegime[2].Type = radRegimeC2.SelectedItem.Text;
                listRegime[2].regime1 = 2;
                db.regimes.AddOrUpdate(listRegime[2]);
                db.SaveChanges();

                HttpContext.Current.Session["idDemande"] = mydmd.id;

                if (drpCycle.SelectedItem.Text == "Premier Cycle")
                {
                        foreach (var malttre in db.lettrerecommandations.Where(c => c.IDAdmission == IDDemande).ToList())
                        {
                            db.lettrerecommandations.Remove(malttre);
                        }
                        db.SaveChanges();
                    

                    Response.Redirect("majAdmission3.aspx");
                }
                else
                {
                    db.SaveChanges();
                    Server.Transfer("majAdmission2.aspx");
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
            if (radRegimeC2.SelectedIndex == -1)
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
            if (radRegimeC3.SelectedIndex == -1)
            {
                radRegimeC3.Items.Add(new ListItem("Temps Partiel", "0"));
                radRegimeC3.Items.Add(new ListItem("Temps Complet", "1"));
                radRegimeC3.SelectedIndex = 0;
            }
        }

        protected void drpThrirdChoix_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}