<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_sala.aspx.cs" Inherits="Projeto_Final.editar_sala" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px;">
                    <!-- Sidebar -->
                    <div class="col-md-3 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="#" class="list-group-item list-group-item-action active"><i class="fas fa-chair"></i> Editar Sala <%= lbl_cod_sala.Text %> - <%= tb_designacao.Text %></a>
                            <a href="criar_sala.aspx" class="list-group-item list-group-item-action"><i class="fas fa-plus-circle"></i> Criar Sala</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                            <a href="salas.aspx" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card bg-secondary" style="border-color: #333; color:black;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 30px; color: white;">Sala <%= lbl_cod_sala.Text %></h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <p class="lead">
                                                Código Sala:
                                                <asp:Label ID="lbl_cod_sala" runat="server" Text="Label"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Nome da Sala:
                                                <asp:TextBox ID="tb_designacao" runat="server" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_designacao" runat="server" ErrorMessage="Nome da sala é obrigatório." Text="*" ControlToValidate="tb_designacao"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev_designacao" runat="server" ErrorMessage="Nome de sala inválido" ControlToValidate="tb_designacao" ValidationExpression="^.{1,50}$" Text="*"></asp:RegularExpressionValidator>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Data de Criação:
                                                <asp:Label ID="lbl_data_criacao" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Último Update:
                                                <asp:Label ID="lbl_ultimo_update" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Ativo:
                                                <asp:CheckBox ID="cb_ativo" CssClass="btn btn-dark m-2" runat="server" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <asp:Button ID="btn_editar" CssClass="btn btn-info py-2 px-4 ml-2" runat="server" Text="Guardar Alterações" OnClick="btn_editar_Click" />
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <div style="margin-bottom: 30px;">
                            <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
