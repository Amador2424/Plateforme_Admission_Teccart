using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjCsWebAdmission
{
    public partial class majCandidat : System.Web.UI.Page
    {
        SGACEntitiesFinal db =new  SGACEntitiesFinal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                candidat user = db.candidats.FirstOrDefault(c => c.id == id);
                userCode.Text = user.CodeUtilisateur;
                TxtNom.Text = user.NomUtilisateur;
                 TxtPrenom.Text = user.PrenomUtilisateur;
                 TxtPhone.Text = user.Telephone;
                string Password = user.MotDePasse;
                 email.Text = user.CourrierPersonnel;
                txtAdresse.Text = user.AdresseUtilisateur;

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string UserCode = userCode.Text;
            string Nom = TxtNom.Text;
            string Prenom = TxtPrenom.Text;
            string Telephone = TxtPhone.Text;
            string Password = password.Text;
            string PasswordConfirm = passwordConfirm.Text;
            string Email = email.Text;
            string EmailConfirm = emailConfirm.Text;
            string adresse = txtAdresse.Text;
            if (UserCode.Length > 0 && Password.Length > 0 && PasswordConfirm.Length > 0 && Email.Length > 0 && EmailConfirm.Length > 0 && Nom.Length > 0 && Prenom.Length > 0 && Telephone.Length > 0)
            {
                if (UserCode.Length < 7)
                {
                    LblError.Text = "Le code d'utilisatuer doit contenir plus de 7 carateres";

                }
                else
                {
                    if (verifMdp(Password) == true && verifMdp(PasswordConfirm) == true)
                    {
                        if (Password == PasswordConfirm)
                        {
                            if (VerifMail(Email) == true)
                            {
                                if (CompareEmail(Email, EmailConfirm) == true)
                                {
                                            int id = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                                            candidat user = db.candidats.FirstOrDefault(c => c.id == id);
                                            user.CodeUtilisateur = UserCode;
                                            user.MotDePasse = Password;
                                            user.CourrierPersonnel = Email;
                                            user.NomUtilisateur = Nom;
                                            user.PrenomUtilisateur = Prenom;
                                            user.Telephone = Telephone;
                                            user.AdresseUtilisateur = adresse;
                                            db.candidats.AddOrUpdate(user);
                                            db.SaveChanges();
                                            Server.Transfer("Login.aspx");

                                        
                                    
                                }
                                else
                                {
                                    LblError.Text = "Les emails ne concordent pas !";
                                }
                            }
                            else
                            {
                                LblError.Text = "Veuillez entrer un email valide !";
                            }


                        }
                        else
                        {
                            LblError.Text = "Les 02 Mots de Passe ne sont pas identiques";
                        }
                    }
                    else
                    {
                        LblError.Text = "Le mot doit contenir une lettre majuscule , un chiffre , et Superieur à 7";
                    }
                }



            }
            else
            {
                LblError.Text = "Veuillez remplir tous les champs, svp!";
            }
        }
        public bool verifMdp(string mdp)
        {


            // Utilisez une expression régulière pour effectuer la vérification
            if (Regex.IsMatch(mdp, @"^(?=.*[A-Z])(?=.*\d).{7,}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerifMail(string mail)
        {
            // Utilisez une expression régulière pour vérifier l'adresse e-mail
            if (Regex.IsMatch(mail, @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool CompareEmail(string mail, string confirmEmail)
        {
            if (mail == confirmEmail)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CodeUtilExisteDeja(string nouveauCodeUtil, string codeUtilActuel)
        {
            using (SGACEntitiesFinal localContext = new SGACEntitiesFinal())
            {
                var candidatExistant = localContext.candidats.FirstOrDefault(c => c.CodeUtilisateur == nouveauCodeUtil && c.CodeUtilisateur != codeUtilActuel);
                return candidatExistant != null;
            }
        }
        public bool EmailExsiteDeja(string NouveauEmail, string emailActu)
        {
            // Utilisez LINQ pour interroger la table Candidat   
            using (SGACEntitiesFinal localContext = new SGACEntitiesFinal())
            {
                var candidatExistant = localContext.candidats.FirstOrDefault(c => c.CourrierPersonnel == NouveauEmail && c.CourrierPersonnel != emailActu);
                // Si candidatExistant n'est pas null, cela signifie que le codeUtil existe déjà
                return candidatExistant != null;  
            }
        }
    }
}