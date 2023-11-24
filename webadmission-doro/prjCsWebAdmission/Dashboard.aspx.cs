using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.EnterpriseServices;

namespace prjCsWebAdmission
{
    public partial class Dashboard : System.Web.UI.Page
    {
        static SGACEntitiesFinal db = new SGACEntitiesFinal();
        protected void Page_Load(object sender, EventArgs e)
        {

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
                if (UserCode.Length > 0 && Password.Length > 0 && PasswordConfirm.Length > 0 && Email.Length > 0 && EmailConfirm.Length > 0 && Nom.Length > 0 && Prenom.Length > 0 && Telephone.Length > 0 )
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

                                        if (CodeUtilExisteDeja(UserCode))
                                        {
                                            LblError.Text = "Ce code d'utilisateur est deja enregistré.";
                                        }
                                        else
                                        {
                                            if (EmailExsiteDeja(Email))
                                            {
                                                LblError.Text = "Cet Email  est deja enregistré.";
                                            }
                                            else
                                            {
                                                candidat newCandidat = new candidat();
                                                newCandidat.CodeUtilisateur = UserCode;
                                                newCandidat.MotDePasse = Password;
                                                newCandidat.CourrierPersonnel = Email;
                                                newCandidat.NomUtilisateur = Nom;
                                                newCandidat.PrenomUtilisateur= Prenom;
                                                newCandidat.Telephone = Telephone;
                                                newCandidat.AdresseUtilisateur = adresse;
                                                db.candidats.Add(newCandidat);
                                                db.SaveChanges();
                                                Server.Transfer("Login.aspx");

                                            }
                                    }
                                    }
                                    else
                                    {
                                        LblError.Text = "Les emails ne concorde pas !";
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
        public  bool VerifMail(string mail)
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


        public bool CompareEmail(string mail,string confirmEmail)
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

        public bool CodeUtilExisteDeja(string codeUtil)
        {
            using (SGACEntitiesFinal localContext = new SGACEntitiesFinal())
            {
                var candidatExistant = localContext.candidats.FirstOrDefault(c => c.CodeUtilisateur == codeUtil);
                return candidatExistant != null;
            }
        }

        public bool  EmailExsiteDeja(string email)
        {
            // Utilisez LINQ pour interroger la table Candidat
            using (SGACEntitiesFinal localContext = new SGACEntitiesFinal())
            {
                var candidatExistant = localContext.candidats.FirstOrDefault(c => c.CourrierPersonnel == email);


            // Si candidatExistant n'est pas null, cela signifie que le codeUtil existe déjà
            return candidatExistant != null;
            }
        }

        protected void TxtNom_TextChanged(object sender, EventArgs e)
        {

        }
    }
}