using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;


namespace prjCsWebAdmission
{
    public partial class ContactProf : System.Web.UI.Page
    {
        static int nbreProf=0;
        static SGACEntitiesFl db = new SGACEntitiesFl();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //HttpContext.Current.Session["idDemande"] = 2;
                //HttpContext.Current.Session["idCandidat"] = 1;

                var idDemande = HttpContext.Current.Session["idDemande"].ToString();
                
                // Récupérer la demande d'admission
                var demande = db.demandeadmissions
                    .FirstOrDefault(c => c.id.ToString() == idDemande);

                // Vérifier si la demande existe
                if (demande != null)
                {
                    // Charger les lettres de recommandation en mémoire
                    var lettresReco = demande.lettrerecommandations.ToList();

                    // Obtenir le nombre de lettres de recommandation
                    nbreProf = lettresReco.Count;
                    lblMessage.Text = "Fichier " + nbreProf + "/3 téléchargé et enregistré avec succès dans la base de données!";


                    // Utiliser nbreLettresReco comme nécessaire
                }

            }
        }

        protected void btnSoumettre_Click(object sender, EventArgs e)
        {
            if (nbreProf < 3)
            {
                //Creer une liste de prof et affecter à une var
                lettrerecommandation maRecom = new lettrerecommandation();
                maRecom.NomProfesseur = txtNomProf.Text;
                maRecom.PrenomProfesseur = txtPrenomProf.Text;
                maRecom.CourrielProfesseur = txtCourriel.Text;
                maRecom.TelephoneProfesseur = txtTelPhone.Text;
                maRecom.IDAdmission = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);
                maRecom.PosteOccupe = txtPosteProf.Text;
                maRecom.Organisation = txtInsitut.Text;
                maRecom.IDCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                if (fileLettreReco.HasFile)
                {
                    
                  
                        // Récupérer le contenu du fichier en tant que tableau d'octets
                        byte[] fileContent;
                        using (Stream fs = fileLettreReco.PostedFile.InputStream)
                        {
                            fileContent = new byte[fs.Length];
                            fs.Read(fileContent, 0, (int)fs.Length);
                        }

                        // Enregistrer le contenu dans la base de données
                        // Assurez-vous d'utiliser une connexion sécurisée et de gérer les erreurs
                        
                        maRecom.Cv= (fileContent);

                        // Afficher un message de réussite
                        
                        db.lettrerecommandations.Add(maRecom);
                        db.SaveChanges();
                        nbreProf += 1;
                        lblMessage.Text = "Fichier " + nbreProf + "/3 téléchargé et enregistré avec succès dans la base de données!";

                        //db.SaveChanges();
                        if (chkEnvoyerCourriel.Checked == true)
                        {
                            // Adresse e-mail de l'expéditeur
                            string fromAddress = "danielste555@gmail.com";

                            // Adresse e-mail du destinataire
                            string toAddress = txtCourriel.Text;

                            // Objet du message
                            string subject = "Recommandation";

                            // Corps du message
                            string body = "L'etudiant.... vous a cité comme reference dans une demande d'admission";

                            // Configuration du client SMTP
                            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromAddress, "Delight1234"),
                                EnableSsl = true,
                            };

                            // Création du message
                            MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                            // Ajout d'une pièce jointe (facultatif)

                            //Piece Jointe

                           // Attachment attachment = new Attachment("chemin/vers/votre/fichier.pdf");
                            //mailMessage.Attachments.Add(attachment);

                            try
                            {
                                // Envoi du message
                                smtpClient.Send(mailMessage);
                                Console.WriteLine("E-mail envoyé avec succès.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Erreur lors de l'envoi de l'e-mail : " + ex.Message);
                            }
                        }
                        EffacerFormulaire();
                    if (nbreProf == 3)
                    {
                        Response.Redirect("DemandeAdmission2.aspx");
                    }
                    
                   
                }
                else
                {
                    // Aucun fichier sélectionné, afficher un message d'avertissement si nécessaire
                    lblMessage.Text = "Veuillez sélectionner un fichier avant de cliquer sur le bouton d'envoi.";
                }
            }
            else
            {
                Response.Redirect("DemandeAdmission2.aspx");
            }
            
                
        }
            

        
        private void EffacerFormulaire()
        {
            // Effacez les champs du formulaire
            txtNomProf.Text = string.Empty;
            txtPrenomProf.Text = string.Empty;
            txtPosteProf.Text = string.Empty;
            txtInsitut.Text = string.Empty;
            txtTelPhone.Text = string.Empty;
            txtCourriel.Text = string.Empty;
            // Effacez d'autres champs du formulaire
        }
    }
}