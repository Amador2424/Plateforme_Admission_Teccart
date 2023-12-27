using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace prjCsWebAdmission
{
    public partial class Login : System.Web.UI.Page

    {
        static SGACEntitiesFl db = new SGACEntitiesFl();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserCode.Focus();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }

        protected void btnLoginUserCode_Click(object sender, EventArgs e)
        {
            string userCode = txtUserCode.Text.Trim();
            string password = txtPassword.Text.Trim();
            if(userCode.Length<= 0 || password.Length<= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir tous les champs";

            }
            else if (userCode.Length <= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir le Code Utilisateur";

            }
            else if(password.Length<= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir le Mot de passe, svp!";

            }
            else if (userCode.Length >0 && password.Length > 0)
            {
                var candidatExistant = db.candidats.FirstOrDefault(c=> c.CodeUtilisateur == userCode) ;
                if (candidatExistant == null)
                {
                    lblErrorMessage.Text = "Code d'utilisateur incorrect";

                }
                else
                {
                    if (candidatExistant.MotDePasse == password)
                    {
                        HttpContext.Current.Session["codeU"] = candidatExistant.CodeUtilisateur;
                        // Tout est bon, redirigeons vers InfoUtil.aspx
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Cache.SetNoStore();
                        Response.Redirect("Info_utilisateur.aspx");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Cache.SetNoStore();


                    }
                    else
                    {
                        lblErrorMessage.Text = "Mot de passe Incorrect";
                    }
                }
            }
        }

      
    }
}