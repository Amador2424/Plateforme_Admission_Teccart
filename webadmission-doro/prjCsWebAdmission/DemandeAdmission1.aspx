<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemandeAdmission1.aspx.cs" Inherits="prjCsWebAdmission.DemandeAdmission1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        /* Reset some default styles */
        /* Reset some default styles */
        body, h1, h2, h3, p, ul, li, form {
            margin: 0;
            padding: 0;
        }

        /* Apply a background color and font style to the body */
        body {
            background-color: #f8f9fa;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            line-height: 1.6;
            text-align: center;
            color: #333;
        }

        /* Style the header */
        header {
            background-color: #007bff;
            color: #fff;
            padding: 10px;
            text-align: center;
        }

        /* Style the main content area */
        form {
            margin: 20px auto;
            max-width: 600px;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        /* Style the table */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        /* Style table cells */
        td {
            padding: 10px;
            border-bottom: 1px solid #ccc;
        }

        /* Style form controls */
        select, input[type="radio"], input[type="button"] {
            padding: 8px;
            margin: 5px 0;
            width: calc(100% - 20px);
            box-sizing: border-box;
        }

        /* Style the button */
        #btnSuivant {
            background-color: #28a745;
            color: #fff;
            padding: 10px;
            cursor: pointer;
            border: none;
            border-radius: 5px;
        }

        /* Styles for headers */
        header h1 {
            font-size: 2.5rem;
            font-weight: bold;
            margin-top: 20px;
        }

        header h2 {
            font-size: 1.5rem;
            margin-top: 10px;
            color: #007bff;
        }
        #logo {
    width: 150px;
    height: 150px;
}

    </style>
</head>
<body>
    <img id="logo" src="Teccart.jpeg" alt="logo" />
    <header>
        <h1 class="text-4xl font-bold mt-4">Info Admission</h1>
        <h2 class="text-2xl mt-2">Admission en ligne</h2>
    </header>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    Type de demande :
                </td>
                <td>
                    <asp:DropDownList ID="drpCycle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCycle_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Session :
                </td>
                <td>
                    <asp:RadioButtonList ID="radSession" runat="server" BorderWidth="0px"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    1er Choix :
                </td>
                <td>
                    <asp:DropDownList ID="drpFirstChoix" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpFirstChoix_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Regime
                </td>
                <td>
                    <asp:RadioButtonList ID="radRegimeC1" runat="server"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    2eme Choix :
                </td>
                <td>
                    <asp:DropDownList ID="drpSecondChoix" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpSecondChoix_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Regime :
                </td>
                <td>
                    <asp:RadioButtonList ID="radRegimeC2" runat="server"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    3eme Choix :
                </td>
                <td>
                    <asp:DropDownList ID="drpThrirdChoix" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Regime :
                </td>
                <td>
                    <asp:RadioButtonList ID="radRegimeC3" runat="server"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btnSuivant" runat="server" Text="Suivant" OnClick="btnSuivant_Click" />
                </td>
                <td>

                    <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
