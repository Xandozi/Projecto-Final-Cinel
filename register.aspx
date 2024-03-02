<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Projeto_Final.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
            <h2 style="text-align:center">Formulário de Registo</h2>
            <hr>
            <div class="row">
                <div class="col-md-6 offset-md-3">
                    <asp:Panel ID="pnlRegistration" runat="server">
                        <div class="form-group">
                            <asp:Label ID="lbl_primeiro_nome" runat="server">Nome Próprio:</asp:Label>
                            <asp:TextBox ID="tb_primeiro_nome" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_apelido" runat="server">Apelido:</asp:Label>
                            <asp:TextBox ID="tb_apelido" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_perfil" runat="server">Perfil:</asp:Label>
                            <asp:DropDownList ID="ddl_perfil" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_data_nascimento" runat="server">Data de Nascimento:</asp:Label>
                            <asp:TextBox ID="tb_data_nascimento" runat="server" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_email" runat="server">Email:</asp:Label>
                            <asp:TextBox ID="tb_email" runat="server" CssClass="form-control" TextMode="Email" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_username" runat="server">Username:</asp:Label>
                            <asp:TextBox ID="tb_username" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_password" runat="server">Password:</asp:Label>
                            <asp:TextBox ID="tb_pw" runat="server" TextMode="Password" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server">Repita a Password:</asp:Label>
                            <asp:TextBox ID="tb_pw_rpt" runat="server" TextMode="Password" CssClass="form-control" />
                        </div>
                        <asp:Button ID="btn_registar" runat="server" Text="Registar" CssClass="btn btn-primary" OnClick="btn_registar_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
</asp:Content>
