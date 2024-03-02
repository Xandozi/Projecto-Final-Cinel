<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="portal.aspx.cs" Inherits="Projeto_Final.portal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style_portal.css" rel="stylesheet" />
    <title>Portal Cinel</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="logo-container">
            <img src="img/logocinel.png" />
        </div>
        <div class="space"></div>
        <div class="login-container">
            <div class="login-container-input">
                <asp:TextBox ID="tb_user" runat="server" placeholder="Introduza o seu username"></asp:TextBox>
                <asp:TextBox ID="tb_pw" runat="server" placeholder="Introduza a sua password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="login-container-btn">
                <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click" />
                <asp:LinkButton ID="lnk_forgot_pw" runat="server" style="font-size: 9px; width: 100%; padding-top: 5px;">Recuperar palavra-passe</asp:LinkButton>
            </div>
            <div class="space-container"></div>
            <div class="login-container-registo">
                <asp:LinkButton ID="lnk_registar" runat="server" style="font-size: 9px;">Registe-se</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
