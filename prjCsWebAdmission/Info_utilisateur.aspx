<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info_utilisateur.aspx.cs" Inherits="prjCsWebAdmission.Info_utilisateur" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="infoUtil.css" />
    <title>Admission à l'institut Teccart</title>
    
    <style type="text/css">

        /* Style de base pour le GridView */
        .gridview {
            width: 100%;
            margin: 10px 0;
            border-collapse: collapse;
        }

        /* Style de l'en-tête du GridView */
        .gridview th {
            background-color: #007bff;
            color: #fff;
            padding: 10px;
            border: 1px solid #ddd;
        }

        /* Style des lignes impaires du GridView */
        .gridview tr:nth-child(odd) {
            background-color: #f2f2f2;
        }

        /* Style des lignes paires du GridView */
        .gridview tr:nth-child(even) {
            background-color: #fff;
        }

        /* Style des cellules du GridView */
        .gridview td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        /* Style des boutons dans la colonne Actions */
        .gridview .btnModifier,
        .gridview .btnSupprimer {
            display: inline-block;
            padding: 5px 10px;
            margin: 2px;
            text-align: center;
            text-decoration: none;
            background-color: #28a745;
            color: #fff;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        /* Style des boutons au survol */
        .gridview .btnModifier:hover,
        .gridview .btnSupprimer:hover {
            background-color: #218838;
        }


        .btnModifier {
            border-style: hidden;
            border-color: inherit;
            border-width: medium;
            background: #007bff;
            color: #fff;
            border-radius: 5px;
            padding: 10px 20px;
            font-size: 1.1rem;
            cursor: pointer;
            transition: background-color 0.3s;
            background: #007bff;
            border:hidden;
        }
        .btnModifier:hover {
        background: #0056b3;
    }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <img id="logo" src="Teccart.jpeg" alt="logo" />

            <header class="text-center">
                <h1>Étudier à l'Institut Teccart</h1>
                <h2>Admission en ligne</h2>
            </header>

            <main class="flex justify-center">
                <div class="container">
                    Nom complet
                 <asp:Label ID="LblNom" runat="server" CssClass="label" Text="Nom :"></asp:Label>
                    <br />
                    Code Utilisateur :
                 <asp:Label ID="LblCompte" runat="server" CssClass="label" Text=""></asp:Label>&nbsp;<br />
                    Email :
                <asp:Label ID="LblEmail" runat="server" CssClass="label" Text=" "></asp:Label>&nbsp;<br />
                    Adresse :
                <asp:Label ID="lblAdresse" runat="server" CssClass="label" Text=" "></asp:Label>
                    <br />
                    <asp:Button ID="BtnInfo" runat="server" Text="Modifier les informations du compte" cssclass="btnModifier" Font-Bold="True" Width="396px" OnClick="BtnInfo_Click"/>

                    <br />
                    <br />
                    <br />

                    <br />

                    <h3>Faire une nouvelle demande d'admission</h3>
                    <div class="button-container">
                        <asp:Button ID="btnPremierCycle" runat="server" Text="Faire une Demande" CssClass="btnModifier"  Font-Bold="True" OnClick="btnPremierCycle_Click" Width="201px" />
                    </div>

                    <br />
                    <br />

                    <h3>Liste de vos candidatures</h3>
                    <asp:GridView ID="GridViewCandidatures" runat="server" AutoGenerateColumns="False" CssClass="gridview" EmptyDataText="Aucune candidature trouvée" OnSelectedIndexChanged="GridViewCandidatures_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="TypeDemande" HeaderText="Demande" />
                            <asp:BoundField DataField="DateSousmission" HeaderText="Date de la demande" />
                            <asp:BoundField DataField="Statut" HeaderText="Statut" />
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModifier" runat="server" Text="Modifier" CommandArgument='<%# Eval("id") %>' OnClick="btnModifier_Click" />
                                    <asp:LinkButton ID="btnSupprimer" runat="server" Text="Annuler" CommandArgument='<%# Eval("id") %>' OnClick="btnSupprimer_Click" OnClientClick="return confirm('Êtes-vous sûr de vouloir Annuler cette candidature ?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>


                    <asp:EntityDataSource ID="EntityDataSource1" runat="server">
                    </asp:EntityDataSource>


                </div>
            </main>
        </div>
    </form><br />
    <footer class="mt-8 text-center">
        <p class="text-gray-600">&copy; TECCART - Institut Teccart de Montréal</p>
        <a href="Login.aspx" class="text-blue-500 hover:underline">Se déconnecter</a> 
    </footer>
</body>
</html>