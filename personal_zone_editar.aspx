﻿<%@ Page Title="CINEL - Editar Informação Pessoal" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="personal_zone_editar.aspx.cs" Inherits="Projeto_Final.personal_zone_editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light">
                <div class="list-group">
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action active"><i class="fas fa-user-cog"></i> Área Pessoal</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeUsernameModal"><i class="fas fa-file-signature"></i> Mudar o Username</a>
                    <% if (Session["googlefb_log"] != "yes")
                        { %>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changePasswordModal"><i class="fas fa-lock"></i> Mudar a Password</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeEmailModal"><i class="fas fa-envelope"></i> Mudar o Email</a>
                    <% } %>
                    <% if (Session["perfil"].ToString() == "Staff" || Session["perfil"].ToString() == "Super Admin")
                        { %>
                    <a href="dados_estatisticos.aspx" class="list-group-item list-group-item-action"><i class="fas fa-list-ol"></i> Dados Estatísticos</a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                    <% } %>
                    <asp:LinkButton ID="lb_logout2" class="list-group-item list-group-item-action" runat="server" OnClick="btn_logout2_Click"><i class="fas fa-power-off"></i> Terminar Sessão</asp:LinkButton>
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-10">
                <div class="card bg-secondary" style="border-color: #333; color:black;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 30px; color: white;">Informação pessoal de <%= tb_nome_proprio.Text + " " + tb_apelido.Text %></h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <p class="lead">Código de Utilizador: <asp:Label ID="lbl_cod_user" runat="server" Text=""></asp:Label></p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">Username: <asp:Label ID="lbl_username" runat="server" Text=""></asp:Label></p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Nome Próprio:
                                    <asp:TextBox ID="tb_nome_proprio" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_nome_proprio" runat="server" ErrorMessage="Nome próprio obrigatório." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_nome_proprio"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_nome_proprio" runat="server" ErrorMessage="Nome próprio inválido." Text="*" ValidationGroup="editar_info" ValidationExpression="^[A-Za-zÀ-ÿ0-9 ]{1,50}$" ControlToValidate="tb_nome_proprio"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Apelido:
                                    <asp:TextBox ID="tb_apelido" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_apelido" runat="server" ErrorMessage="Apelido é obrigatório." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_apelido"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_apelido" runat="server" ErrorMessage="Apelido inválido." Text="*" ValidationExpression="^[A-Za-zÀ-ÿ0-9 ]{1,50}$" ValidationGroup="editar_info" ControlToValidate="tb_apelido"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Morada:
                                    <asp:TextBox ID="tb_morada" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_morada" runat="server" ErrorMessage="Morada é obrigatória." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_morada"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_morada" runat="server" ErrorMessage="Morada inválida." ValidationExpression="^[A-Za-zÀ-ÿ0-9 ]{1,50}$" ValidationGroup="editar_info" Text="*" ControlToValidate="tb_morada"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Código Postal:
                                    <asp:TextBox ID="tb_cod_postal" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_cod_postal" runat="server" ErrorMessage="Código Postal é obrigatório." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_cod_postal"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_cod_postal" runat="server" ErrorMessage="Código postal inválido." Text="*" ValidationExpression="^\d{4}-\d{3}$" ValidationGroup="editar_info" ControlToValidate="tb_cod_postal"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Perfil/Perfis:
                                    <asp:Label ID="lbl_perfis" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Email:
                                    <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Data de Nascimento:
                                    <asp:TextBox ID="tb_data_nascimento" runat="server" TextMode="Date"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_data_nascimento" runat="server" ErrorMessage="Data de nascimento obrigatória." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_data_nascimento"></asp:RequiredFieldValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Número de Contacto:
                                    <asp:TextBox ID="tb_num_contacto" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_num_contacto" runat="server" ErrorMessage="Número de contacto obrigatório." Text="*" ValidationGroup="editar_info" ControlToValidate="tb_num_contacto"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rev_num_contacto" runat="server" ErrorMessage="Número de contacto inválido." Text="*" ValidationExpression="^\d{9}$" ValidationGroup="editar_info" ControlToValidate="tb_num_contacto"></asp:RegularExpressionValidator>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Foto:
                                    <asp:FileUpload ID="fu_foto" runat="server" />
                                    </p>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="text-right mt-3">
                                    <asp:Image ID="img_user" runat="server" class="rounded-circle" Style="height: 200px; width: 200px; border: 5px solid orange;" />
                                </div>
                            </div>
                        </div>
                        <div class="d-flex justify-content-lg-between mt-3">
                            <asp:Button ID="btn_editar" class="btn btn-primary btn-lg" runat="server" Text="Guardar Alterações" CausesValidation="false" OnClick="btn_editar_Click" ValidationGroup="editar_info" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div style="margin-bottom: 30px; margin-top: 30px; margin-left: 10px;">
                <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
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
