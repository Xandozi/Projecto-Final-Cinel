﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Projeto_Final.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Register Form -->
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card border-primary">
                    <div class="card-header bg-primary text-white text-center">
                        <h4 class="mb-0" style="color: white;">Formulário de Registo</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="lbl_primeiro_nome" runat="server">Nome Próprio:</asp:Label>
                            <asp:TextBox ID="tb_primeiro_nome" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_apelido" runat="server">Apelido:</asp:Label>
                            <asp:TextBox ID="tb_apelido" runat="server" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lbl_data_nascimento" runat="server">Data de Nascimento:</asp:Label>
                            <asp:TextBox ID="tb_data_nascimento" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
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
                        <asp:Button ID="btn_registar" runat="server" Text="Registar" CssClass="btn btn-primary btn-block" OnClick="btn_registar_Click" />
                        <asp:LinkButton ID="btn_register_google" runat="server" CssClass="btn btn-google btn-block" OnClick="Login" CausesValidation="false">
                                        <i class="fab fa-google mr-2"></i> Sign up with Google
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Register Form -->
</asp:Content>

