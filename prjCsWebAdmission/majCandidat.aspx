<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="majCandidat.aspx.cs" Inherits="prjCsWebAdmission.majCandidat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="Style.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
                <img id="logo" src="Teccart.jpeg" alt="logo" />
            <header class="text-center">
                <h1 class="text-4xl font-bold mt-4">Étudier à l&#39;institut Teccart</h1>
                <h2 class="text-2xl mt-2">Admission en ligne</h2>
            </header>
    
            <main class="flex justify-center">
                <div class="w-full max-w-md bg-white p-8 rounded shadow-md">
                    <h3 class="text-xl font-semibold mb-4">Modification du compte utilisateur</h3>

                     <div class="mb-4">
                        <label for="Nom" class="block text-sm font-medium text-gray-700">Nom</label>
                        <asp:TextBox ID="TxtNom" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Nom"></asp:TextBox>
                    </div>
                    <div class="mb-4">
                        <label for="Prenom" class="block text-sm font-medium text-gray-700">Prénom</label>
                        <asp:TextBox ID="TxtPrenom" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Prénom"></asp:TextBox>
                    </div>
                    <div class="mb-4">
                        <label for="Telephone" class="block text-sm font-medium text-gray-700">Téléphone</label>
                        <asp:TextBox ID="TxtPhone" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Téléphone" ></asp:TextBox>
                    </div>
                    
                     <div class="mb-4">
                        <label for="Adresse" class="block text-sm font-medium text-gray-700">Adresse</label>
                        <asp:TextBox ID="txtAdresse" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Adresse"></asp:TextBox>
                    </div>

                    <div class="mb-4">
                        <label for="userCode" class="block text-sm font-medium text-gray-700">Code d'utilisateur</label>
                        <asp:TextBox ID="userCode" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Code d'utilisateur"></asp:TextBox>
                    </div>
    
                    <div class="mb-4">
                        <label for="password" class="block text-sm font-medium text-gray-700">Mot de passe</label>
                        <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Mot de passe"></asp:TextBox>
                    </div>
    
                    <div class="mb-4">
                        <label for="passwordConfirm" class="block text-sm font-medium text-gray-700">Confirmer le mot de passe</label>
                        <asp:TextBox ID="passwordConfirm" runat="server" TextMode="Password" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Confirmer le mot de passe"></asp:TextBox>
                    </div>
    
                    <div class="mb-4">
                        <label for="email" class="block text-sm font-medium text-gray-700">Adresse e-mail</label>
                        <asp:TextBox ID="email" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Adresse e-mail"></asp:TextBox>
                       
                    </div>
    
                    <div class="mb-4">
                        <label for="emailConfirm" class="block text-sm font-medium text-gray-700">Confirmer l'adresse e-mail</label>
                        <asp:TextBox ID="emailConfirm" runat="server" CssClass="w-full p-2 border border-gray-300 rounded" placeholder="Confirmer l'adresse e-mail"></asp:TextBox>
                    </div>
    
                    <div class="mb-4">
                        <asp:Button ID="btnSubmit" runat="server" Text="Sauvegarder" CssClass="button" OnClick="btnSubmit_Click" Font-Bold="True" />
                    </div>
    
                    <div class="text-center text-gray-600">
                        <p>Vous recevrez un courriel dans les minutes suivantes vous confirmant la création de votre compte.</p>
                        <asp:Label ID="LblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </main>
        </div>
    </form>

    <footer class="mt-8 text-center">
        <p class="text-gray-600">Institut Teccart</p>
        <a href="Login.aspx" class="text-blue-500 hover:underline">Se Connecter</a> |
        <a href="#" class="text-blue-500 hover:underline">Nous joindre</a>
    </footer>
</body>
</html>
