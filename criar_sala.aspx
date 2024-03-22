<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="criar_sala.aspx.cs" Inherits="Projeto_Final.criar_sala" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px;">
                    <!-- Sidebar -->
                    <div class="col-md-3 bg-light" style="margin-bottom:10px;">
                        <div class="list-group">
                            <a href="criar_msala.aspx" class="list-group-item list-group-item-action active">Criar Sala</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="salas.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Criação de Sala</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Nome da Sala</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_designacao" CssClass="form-control" runat="server" MaxLength="50" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_designacao" runat="server" ErrorMessage="Nome da sala obrigatória." Text="*" ControlToValidate="tb_designacao"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rev_designacao" runat="server" ErrorMessage="Nome da sala inválida" ControlToValidate="tb_designacao" Text="*" ValidationExpression="^.{1,50}$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btn_criar" CssClass="btn btn-primary py-2 px-4 ml-2" runat="server" Text="Criar Sala" OnClick="btn_criar_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <div style="margin-bottom: 30px;">
                            <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
