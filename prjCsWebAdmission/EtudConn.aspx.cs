using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class EtudConn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginUserCode_Click(object sender, EventArgs e)
        {
            string permanentCode = txtUserPermanent.Text.Trim();
            string nip = txtNIP.Text.Trim();

            if (permanentCode.Length <= 0 && nip.Length <= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir tous les champs";
            }
            else if (permanentCode.Length <= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir le Code Permanent";

            }
            else if (nip.Length <= 0)
            {
                lblErrorMessage.Text = "Veuillez Remplir le Nip";

            }
            else
            {
                //connexion à la base Verification
                Server.Transfer("PageEtu.aspx");
            }

        }
    }
}