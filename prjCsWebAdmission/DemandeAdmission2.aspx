<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandeAdmission2.aspx.cs" Inherits="prjCsWebAdmission.DemandeAdmission2"  Async="true" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Televermsenet  de Documents</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" />
    <style>
        body {
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
        }

        header {
            background-color: #007BFF;
            color: #fff;
            padding: 10px;
            text-align: center;
        }

        #logo {
            width: 150px;
            height: 150px;
            display: block;
            margin: 0 auto;
        }

        .vertical-menu {
            width: 220px;
            height: 100%;
            position: fixed;
            background-color: #333;
            padding-top: 20px;
            color: white;
            text-align: center;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            z-index: 1;
            transition: width 0.5s;
        }

        .vertical-menu a {
            padding: 15px;
            text-decoration: none;
            color: white;
            display: block;
            transition: background-color 0.5s;
        }

        .vertical-menu a:hover {
            background-color: #4CAF50;
        }

        .content {
            margin-left: 220px;
            padding: 20px;
            height: 1000px;
            transition: margin-left 0.5s;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .form-group {
            margin-bottom: 20px;
        }

        .btn-primary {
            background-color: #007BFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="vertical-menu">
            <h3>
                <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label></h3>
            <p>Ne lâchez pas, vous êtes sur la bonne voie !</p>
        </div>
        <div class="content">
            <header>
                <img id="logo" src="Teccart.jpeg" alt="logo" />
                <h1 class="text-4xl font-bold mt-4">Étudier à l'institut Teccart</h1>
                <h2 class="text-2xl mt-2">Admission en ligne</h2>
            </header>
            <div class="container">
                <h2 class="text-center">Téléversez vos Documents</h2>
                <div class="form-group">
                    <label for="filePhoto">Photo :</label>
                    <asp:FileUpload ID="filePhoto" runat="server" class="form-control" />
                </div>
                <div class="form-group">
                    <label for="fileActeNaissance">Acte de Naissance :</label>
                    <asp:FileUpload ID="fileActeNaissance" runat="server" class="form-control" />
                </div>
                <div class="form-group">
                    <label for="fileDernierDiplome">Dernier Diplôme :</label>
                    <asp:FileUpload ID="fileDernierDiplome" runat="server" class="form-control" />
                </div>
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label><br /><br />

                <asp:Button ID="btnTelecharger" runat="server" Text="Téléverser" CssClass="btn btn-primary" OnClick="btnTelecharger_Click"  OnClientClick="return confirm('Voux avez jusqu'à 7j pour Pouvoir Modifier votre candidature');" />
            </div>
        </div>
    </form>
</body>
</html>


