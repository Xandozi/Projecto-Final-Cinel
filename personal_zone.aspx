<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="personal_zone.aspx.cs" Inherits="Projeto_Final.personal_zone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style=" margin-top: 10px;">
                <div class="list-group">
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action active">Área Pessoal</a>
                    <a href="personal_zone_inscricoes.aspx" class="list-group-item list-group-item-action">Minhas Inscrições</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeUsernameModal">Mudar o Username</a>
                    <% if (Session["googlefb_log"] != "yes")
                        { %>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changePasswordModal">Mudar a Password</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeEmailModal">Mudar o Email</a>
                    <% } %>
                    <% if (Session["perfil"].ToString().Contains("Staff") || Session["perfil"].ToString().Contains("Super Admin"))
                        { %>
                    <a href="dados_estatisticos.aspx" class="list-group-item list-group-item-action">Dados Estatísticos</a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action">Gestão</a>
                    <% } %>
                    <asp:Button ID="btn_logout2" class="list-group-item list-group-item-action" runat="server" Text="Terminar Sessão" OnClick="btn_logout2_Click" />
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-10" style=" margin-top: 10px;">
                <div class="card" style="border-color: #333; background-color:antiquewhite;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 40px; color: white;">Bem-vindo à sua zona pessoal, <%= lbl_nome_completo.Text %></h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <p class="lead">Código de Utilizador: <asp:Label ID="lbl_cod_user" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Username: <asp:Label ID="lbl_username" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Nome Completo: <asp:Label ID="lbl_nome_completo" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Morada: <asp:Label ID="lbl_morada" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Código Postal: <asp:Label ID="lbl_cod_postal" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Perfil/Perfis: <asp:Label ID="lbl_perfis" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Email: <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Data de Nascimento: <asp:Label ID="lbl_data_nascimento" runat="server" Text=""></asp:Label></p>
                                <p class="lead">Número de Contacto: <asp:Label ID="lbl_num_contacto" runat="server" Text=""></asp:Label></p>
                            </div>
                            <div class="col-md-6">
                                <div class="text-right mt-3">
                                    <asp:Image ID="img_user" runat="server" class="rounded-circle" style="height: 200px; width: 200px; border:5px solid orange;"/>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex justify-content-lg-between mt-3">
                            <asp:Button ID="btn_editar" class="btn btn-primary btn-lg" runat="server" Text="Editar Informação" CausesValidation="false" OnClick="btn_editar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom: 30px; margin-top: 30px; margin-left: 10px;">
                <div>
                    <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Modals -->
    <!-- Modal for changing the password -->
    <div class="modal" id="changePasswordModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Password</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="changePasswordForm" runat="server">
                        <div class="form-group">
                            <label for="tb_pw">Password atual</label>
                            <asp:TextBox ID="tb_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw">Nova Password</label>
                            <asp:TextBox ID="tb_new_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="rev_pw_nova" runat="server" ErrorMessage="Invalid Password" Text="*" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,20}$" ControlToValidate="tb_new_pw"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw_repeat">Confirmar Nova Password</label>
                            <asp:TextBox ID="tb_new_pw_repeat" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_pw" class="btn btn-primary" runat="server" Text="Mudar Password" OnClick="btn_change_pw_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for changing the username -->
    <div class="modal" id="changeUsernameModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Username</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="Div1" runat="server">
                        <div class="form-group">
                            <label for="tb_pw">Novo Username</label>
                            <asp:TextBox ID="tb_new_username" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw">Password</label>
                            <asp:TextBox ID="tb_pw_username" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_username" class="btn btn-primary" runat="server" Text="Mudar Username" OnClick="btn_change_username_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for changing the email -->
    <div class="modal" id="changeEmailModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Email</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="Div2" runat="server">
                        <div class="form-group">
                            <label for="tb_email">Novo Email</label>
                            <asp:TextBox ID="tb_new_email" class="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="rfv_new_email" runat="server" ErrorMessage="New email not inserted correctly" Text="*" ControlToValidate="tb_new_email" ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="tb_pw_email">Password</label>
                            <asp:TextBox ID="tb_pw_email" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_email" class="btn btn-primary" runat="server" Text="Mudar Email" OnClick="btn_change_email_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
