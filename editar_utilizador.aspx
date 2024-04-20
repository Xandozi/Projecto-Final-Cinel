<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_utilizador.aspx.cs" Inherits="Projeto_Final.editar_utilizador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style="margin-top: 10px;">
                <div class="list-group">
                    <a href="#" class="list-group-item list-group-item-action active"><i class="fas fa-user"></i> Editar informações de <%= tb_nome_proprio.Text + " " + tb_apelido.Text %></a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                    <a href="utilizadores_detalhe.aspx?cod_user=<%= lbl_cod_user.Text %>" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
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
                                    <p class="lead">
                                        Código de Utilizador:
                                        <asp:Label ID="lbl_cod_user" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Username:
                                        <asp:Label ID="lbl_username" runat="server" Text=""></asp:Label>
                                    </p>
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
                                <div class="form-group">
                                    <p class="lead">
                                        Ativo:
                                        <asp:CheckBox ID="cb_ativo" runat="server" />
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
        <div class="row" style="margin-bottom: 30px; margin-top: 30px; margin-left: 10px;">
            <div>
                <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
