<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtudConn.aspx.cs" Inherits="prjCsWebAdmission.EtudConn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="styles.css" />
</head>
<body>
    <img id="logo" src="Teccart.jpeg" alt="logo" />
     <header class="text-center">
        <h1 class="text-4xl font-bold mt-4">Connexion etudiant à l&#39;institut Teccart</h1>
        <h2 class="text-2xl mt-2">Admission en ligne</h2>
    </header>
    <form id="form1" runat="server" >
     <main class="flex justify-center">
        <div class="container">
            
                <asp:Label ID="LblUserPermanent" runat="server" CssClass="label" Text="Code Permanent"></asp:Label>
                <asp:TextBox ID="txtUserPermanent" runat="server" placeholder="Code de l'utilisateur" CssClass="textbox" required></asp:TextBox>

                <asp:Label ID="LblNIP" runat="server" CssClass="label"  Text="NIP"></asp:Label>
                <asp:TextBox ID="txtNIP" runat="server" TextMode="Password" placeholder="Mot de passe" CssClass="textbox" required></asp:TextBox>

                <br />
           
                <asp:Button ID="btnLoginUserPermanent" runat="server" Text="Se connecter" CssClass="button" OnClick="btnLoginUserCode_Click" /><br />
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error" Visible="false"></asp:Label>
            
            
        </div><br />
        <a href="">Code d'utilisateur oublié?</a> | 
        <a href="">NIP oublié?</a>
    </form>
    <footer class="mt-8 text-center">
        <p class="text-gray-600">Institut Teccart</p>
        <a href="Dashboard.aspx" class="text-blue-500 hover:underline">Creer un Compte</a> |
        <a href="Login.aspx" class="text-blue-500 hover:underline">Suivre sa demande</a> |
        <a href="#" class="text-blue-500 hover:underline">Nous joindre</a>
        
    </footer>
         </main>
</body>
</html>
