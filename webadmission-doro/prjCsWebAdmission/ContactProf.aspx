<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContactProf.aspx.cs" Inherits="prjCsWebAdmission.ContactProf" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <link rel="stylesheet" href="ContactProf.css"/>
    <style>
        #logo {
            max-width: 150px;
            height: auto;
            margin: 0 auto;
            display: block;
        }
    </style>
     <title>Formulaire de Recommandation</title>
</head>
<body>
    <header>
        <img id="logo" src="Teccart.jpeg" alt="logo" />
        <h1 class="text-4xl font-bold mt-4">Recommandation</h1>
        <h2 class="text-2xl mt-2">Contact Professeurs</h2>
    </header>
    <form id="form1" runat="server">
               <div>
            <h1>Formulaire de Recommandation</h1>
            
            <h2>Informations sur le Professeur</h2>
            <div>
                <label>Nom :</label>
                <asp:TextBox ID="txtNomProf" runat="server" Required="true"></asp:TextBox>
            </div>
            <div>
                <label>Prénom :</label>
                <asp:TextBox ID="txtPrenomProf" runat="server" Required="true"></asp:TextBox>
            </div>
            <div>
                <label>Poste :</label>
                <asp:TextBox ID="txtPosteProf" runat="server" Required="true"></asp:TextBox>
            </div>
            <div>
                <label>Institution :</label>
                <asp:TextBox ID="txtInsitut" runat="server" Required="true"></asp:TextBox>
            </div>
            <div>
                <label>Telephone :</label>
                <asp:TextBox ID="txtTelPhone" runat="server" Required="true"></asp:TextBox>
            </div>
            <div>
                <label>Courriel :</label>
                <asp:TextBox ID="txtCourriel" runat="server" Required="true"></asp:TextBox>
            </div>
            <h2>Téléversement de la Lettre de Recommandation</h2>
            <div>
                <label>Téléverser la Lettre :</label>
                <asp:FileUpload ID="fileLettreReco" runat="server" />
            </div>

            <p>
                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                   </p>
                   <h2>Options d'Envoi</h2>
            <div>
                <asp:CheckBox ID="chkEnvoyerCourriel" runat="server" Text="Envoyer le formulaire par courriel" />
            </div>

            <asp:Button ID="btnSoumettre" runat="server" Text="Soumettre" OnClick="btnSoumettre_Click" />
        </div>

    </form>
</body>
</html>
