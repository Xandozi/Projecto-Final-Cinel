<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="criar_turma.aspx.cs" Inherits="Projeto_Final.criar_turma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-top: 10px;">
                        <div class="list-group">
                            <a href="criar_turma.aspx" class="list-group-item list-group-item-action active">Criar Turma</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action">Gestão</a>
                            <a href="turmas.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <!-- Form to create a new course -->
                    <div class="col-md-10" style="margin-top: 10px;">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Criar Nova Turma</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6" style="margin-top: 10px;">
                                        <asp:Label ID="lbl_curso" runat="server" Text="Curso"></asp:Label>
                                        <asp:DropDownList ID="ddl_curso" runat="server" CssClass="form-control" AutoPostBack="true" DataSourceID="curso" DataTextField="nome_curso" DataValueField="cod_curso">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="curso" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_curso], [nome_curso] FROM [Cursos]"></asp:SqlDataSource>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 10px;">
                                        <asp:Label ID="lbl_data_inicio" runat="server" Text="Data de Início"></asp:Label>
                                        <asp:TextBox ID="tb_data_inicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 10px;">
                                        <asp:Label ID="lbl_regime" runat="server" Text="Regime"></asp:Label>
                                        <asp:DropDownList ID="ddlRegime" runat="server" CssClass="form-control" DataSourceID="regime" DataTextField="regime" DataValueField="cod_regime">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="regime" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Regime]"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-md-6">
                                        <asp:Label ID="lbl_lstb_formadores" runat="server" Text="Formadores"></asp:Label>
                                        <asp:ListBox ID="lstb_formadores" CssClass="form-control" runat="server"></asp:ListBox>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lbl_lstb_modulos" runat="server" Text="Módulos"></asp:Label>
                                        <asp:ListBox ID="lstb_modulos" CssClass="form-control" runat="server"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px;">
                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <asp:Button ID="btn_add" CssClass="btn btn-primary" runat="server" Text="Adicionar"/>
                                        <asp:Button ID="btn_remove" CssClass="btn btn-danger" runat="server" Text="Remover" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin: 13px;">
                            <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
