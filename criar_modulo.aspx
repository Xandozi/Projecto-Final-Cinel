<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="criar_modulo.aspx.cs" Inherits="Projeto_Final.criar_modulo" %>

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
                            <a href="criar_modulo.aspx" class="list-group-item list-group-item-action active">Criar Módulo</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="modulos.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Criação de Módulo</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Código UFCD</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_cod_ufcd" runat="server" TextMode="Number" MaxLength="9" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_cod_ufcd" runat="server" ErrorMessage="Código UFCD obrigatório" Text="*" ControlToValidate="tb_cod_ufcd"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Designação da UFCD</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_designacao" runat="server" MaxLength="100" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_designacao" runat="server" ErrorMessage="Designação da UFCD obrigatória." Text="*" ControlToValidate="tb_designacao"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Duração</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_duracao" runat="server" TextMode="Number" MaxLength="2" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_duracao" runat="server" ErrorMessage="Duração é obrigatória." Text="*" ControlToValidate="tb_duracao"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btn_criar" CssClass="btn btn-primary py-2 px-4 ml-2" runat="server" Text="Criar Módulo" OnClick="btn_criar_Click" />
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
