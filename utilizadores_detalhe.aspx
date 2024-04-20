<%@ Page Title="CINEL - Detalhes do Utilizador" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="utilizadores_detalhe.aspx.cs" Inherits="Projeto_Final.utilizadores_detalhe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style=" margin-top: 10px;">
                <div class="list-group">
                    <a href="#" class="list-group-item list-group-item-action active"><i class="fas fa-user"></i> Informações de <%= lbl_nome_completo.Text %></a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                    <a href="utilizadores.aspx" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-10" style=" margin-top: 10px;">
                <div class="card bg-secondary" style="border-color: #333; color:black;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 30px; color: white;">Informação do utilizador <%= lbl_nome_completo.Text %></h2>
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
</asp:Content>
