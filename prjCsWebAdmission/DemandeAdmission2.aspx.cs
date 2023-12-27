using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;        
using iTextSharp.text.pdf.parser;
using Path = System.IO.Path;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using iTextSharp.text;
using System.Net.Http.Headers;

namespace prjCsWebAdmission
{
    public partial class DemandeAdmission2 : System.Web.UI.Page
    {
        static SGACEntitiesFl db = new SGACEntitiesFl();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            if (!IsPostBack)
            {
                // HttpContext.Current.Session["idCandidat"] = 1;
                string nom = db.candidats.Find(Convert.ToInt32(HttpContext.Current.Session["idCandidat"])).NomUtilisateur;
                lblWelcome.Text = "Welcome " + nom;
            }
        }

        protected void btnTelecharger_Click(object sender, EventArgs e)
        {
            if (fileDernierDiplome.HasFile && fileActeNaissance.HasFile && filePhoto.HasFile)
            {
                byte[] photo, acteN, lastDip;

                // Traitement du fichier photo
                using (Stream fs = filePhoto.PostedFile.InputStream)
                {
                    photo = new byte[fs.Length];
                    fs.Read(photo, 0, (int)fs.Length);
                }

                // Traitement du fichier acte de naissance
                using (Stream fs = fileActeNaissance.PostedFile.InputStream)
                {
                    acteN = new byte[fs.Length];
                    fs.Read(acteN, 0, (int)fs.Length);
                }

                // Traitement du dernier diplôme
                string lastDipFileName = Path.GetFileName(fileDernierDiplome.PostedFile.FileName);
                using (Stream fs = fileDernierDiplome.PostedFile.InputStream)
                {
                    lastDip = new byte[fs.Length];
                    fs.Read(lastDip, 0, (int)fs.Length);
                }

                // Création et sauvegarde du document dans la base de données
                document mydoc = new document
                {
                    idCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]),
                    idAdmission = Convert.ToInt32(HttpContext.Current.Session["idDemande"]),
                    Photo = photo,
                    ActeNaiss = acteN,
                    DernierDiplome = lastDip
                };
                db.documents.Add(mydoc);
                db.SaveChanges();

                // Mise à jour de la demande d'admission
                UpdateAdmission(Convert.ToInt32(HttpContext.Current.Session["idDemande"]));

                // Envoi asynchrone du dernier diplôme pour évaluation
                Page.RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    bool isDiplomaRelevant = await SendPdfToFlask(lastDip, lastDipFileName);
                    int idCandidat = Convert.ToInt32(HttpContext.Current.Session["idCandidat"]);
                    int idDemande = Convert.ToInt32(HttpContext.Current.Session["idDemande"]);

                    if (isDiplomaRelevant)
                    {
                        GenerateAndStoreLetter(true, idCandidat);
                        UpdateApplicationStatus("Accepté", idDemande);
                        Server.Transfer("EtudConn.aspx");
                    }
                    else
                    {
                        GenerateAndStoreLetter(false, idCandidat);
                        UpdateApplicationStatus("Refusé", idDemande);
                        Server.Transfer("Login.aspx");
                    }

                    
                }));
            }
            else
            {
                lblMessage.Text = "Veillez téléverser tous vos documents.";
            }
        }

        private void UpdateAdmission(int idDemande)
        {
            var demande = db.demandeadmissions.Find(idDemande);
            if (demande != null)
            {
                demande.DateSousmission = DateTime.Now;
                demande.Statut = "Soumis";
                db.SaveChanges();
            }
        }

        // ... Reste du code, y compris SendPdfToFlask et autres méthodes ...





        private async Task<bool> SendPdfToFlask(byte[] lastDip, string fileName)
        {
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    if (lastDip != null && lastDip.Length > 0)
                    {
                        var streamContent = new ByteArrayContent(lastDip);
                        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "file",
                            FileName = fileName // Assurez-vous que ce nom de fichier est unique et correct
                        };
                        content.Add(streamContent, "file", fileName);

                        var response = await client.PostAsync("http://127.0.0.1:5000/evaluate_diploma", content);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, bool>>(responseContent);
                            return jsonResponse.TryGetValue("isRelevant", out bool isRelevant) && isRelevant;
                        }
                    }

                    return false;
                }
            }
        }


        private void GenerateAndStoreLetter(bool isAccepted, int candidatId)
        {
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                document.Add(new Paragraph(isAccepted ? "Félicitations, votre demande a été acceptée." : "Nous sommes désolés, votre demande a été refusée."));
                document.Close();

                byte[] bytes = memoryStream.ToArray();
                string encodedPdf = Convert.ToBase64String(bytes);

                // Stocker la lettre encodée dans la base de données
                using (var context = new SGACEntitiesFl())
                {
                    var lettre = new lettre
                    {
                        Contenu = encodedPdf,
                        DateEnvoi = DateTime.Now,
                        IdCandidat = candidatId
                    };
                    context.lettres.Add(lettre);
                    context.SaveChanges();
                }
            }
        }

        private void UpdateApplicationStatus(string status, int idDemande)

        {
            using (var context = new SGACEntitiesFl())
            {
                var demande = context.demandeadmissions.FirstOrDefault(d => d.id == idDemande);
                if (demande != null)
                {
                    demande.Statut = status;
                    context.SaveChanges();

                    // Si le candidat est accepté, ajoutez-le en tant qu'étudiant
                    if (status == "Accepté")
                    {
                        var candidatId = demande.IDCandidat; // Supposons que c'est le champ qui relie à la table candidat
                        var candidat = context.candidats.FirstOrDefault(c => c.id == candidatId);

                        if (candidat != null)
                        {
                            var etudiant = new etudiant
                            {
                                CodePermanent = candidat.CodeUtilisateur, // Générer un code permanent
                                Nom = candidat.NomUtilisateur,
                                Prenom = candidat.PrenomUtilisateur,
                                Courriel = candidat.CourrierPersonnel,
                                Adresse = candidat.AdresseUtilisateur,

                            };
                            context.etudiants.Add(etudiant);
                            context.SaveChanges();
                        }
                    }
                }
            }




        }
    }
    }