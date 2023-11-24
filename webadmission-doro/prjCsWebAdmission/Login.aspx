<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="prjCsWebAdmission.Login" %>

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
        <h1 class="text-4xl font-bold mt-4">Étudier à l&#39;institut Teccart</h1>
        <h2 class="text-2xl mt-2">Admission en ligne</h2>
    </header>
    <form id="form1" runat="server" >
     <main class="flex justify-center">
        <div class="container">
            
                <asp:Label ID="LblUserCode" runat="server" CssClass="label" Text="Code de l'utilisateur"></asp:Label>
                <asp:TextBox ID="txtUserCode" runat="server" placeholder="Code de l'utilisateur" CssClass="textbox" required></asp:TextBox>

                <asp:Label ID="LblPassword" runat="server" CssClass="label"  Text="Mot de passe "></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Mot de passe" CssClass="textbox" required></asp:TextBox>

                <br />
           
                <asp:Button ID="btnLoginUserCode" runat="server" Text="Se connecter" CssClass="button" OnClick="btnLoginUserCode_Click" /><br />
                <br />
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="error" Font-Bold="True" ForeColor="Red"></asp:Label>
            
            
        </div><br />
        <a href="">Mot de passe oublié?</a> | 
        <a href="">Code Utilisateur oublié?</a>
    </form>
    <footer class="mt-8 text-center">
        <p class="text-gray-600">Institut Teccart</p>
        <a href="Dashboard.aspx" class="text-blue-500 hover:underline">Nouvelle Admission</a> |
        <a href="EtudConn.aspx" class="text-blue-500 hover:underline">Déja étudiant ?</a>
        
    </footer>
         </main>
</body>

</html>
