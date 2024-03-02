<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="portal_login.aspx.cs" Inherits="Projeto_Final.portal_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style_portal_login.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <title></title>
</head>
<body>
    <div class="container">
            <div class="img-box">
                <img src="img/logo-cinel.JPG" />
            </div>
            <div class="content-box">
                <div class="content">
                    <h2>Login</h2>
                    <form id="form1" runat="server">
                        <div class="input-box">
                            <label for="inputUsername">Username</label>
                            <input type="text" placeholder="Introduza o username..." id="inputUsername" />
                        </div>
                        <div class="input-box">
                            <label for="inputPass">Palavra-passe</label>
                            <input type="password" placeholder="Introduza a palavra-passe..." id="inputPass"/>
                        </div>
                        <div class="input-box">
                            <input type="submit" value="Login"/>
                        </div>
                        <div class="social-media">
                            <i class="fa-brands fa-facebook"></i>
                            <i class="fa-brands fa-google"></i>
                        </div>
                        <div class="input-box">
                            <asp:LinkButton ID="lnk_recuperarpw" runat="server">Recuperar a palavra-passe</asp:LinkButton>
                        </div>
                        <div class="input-box">
                            <asp:LinkButton ID="lnk_registar" runat="server">Registar-se</asp:LinkButton>
                        </div>
                    </form>
                </div>
            </div>
        </div>
</body>
</html>
