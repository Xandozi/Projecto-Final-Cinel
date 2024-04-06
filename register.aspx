<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Projeto_Final.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <!-- Register Form -->
            <div class="container mt-5" style="margin-bottom:40px;">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <div class="card border-primary" style="background-color: antiquewhite;">
                            <div class="card-header bg-primary text-white text-center">
                                <h4 class="mb-0" style="color: white;">Formulário de Registo</h4>
                            </div>
                            <div class="card-body">
                                <div class="form-group" style="margin:0px;">
                                    <asp:Label ID="lbl_primeiro_nome" runat="server">Nome Próprio:</asp:Label>
                                    <asp:TextBox ID="tb_primeiro_nome" runat="server" CssClass="form-control" MaxLength="50" />
                                    <asp:RequiredFieldValidator ID="rfv_primeiro_nome" runat="server" ErrorMessage="Nome Próprio obrigatório" Text="*" ControlToValidate="tb_primeiro_nome"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_primeiro_nome" runat="server" ErrorMessage="Nome Próprio inválido" ValidationExpression="^[a-zA-ZÀ-ÿ'-]{1,50}$" ControlToValidate="tb_primeiro_nome"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group" style="margin:0px;">
                                    <asp:Label ID="lbl_apelido" runat="server">Apelido:</asp:Label>
                                    <asp:TextBox ID="tb_apelido" runat="server" CssClass="form-control" MaxLength="50" />
                                    <asp:RequiredFieldValidator ID="rfv_apelido" runat="server" ErrorMessage="Apelido obrigatório" ControlToValidate="tb_apelido" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_apelido" runat="server" ErrorMessage="Apelido inválido" ControlToValidate="tb_apelido" Text="*" ValidationExpression="^[a-zA-ZÀ-ÿ'-]{1,50}$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group" style="margin:0px;">
                                    <asp:Label ID="lbl_data_nascimento" runat="server">Data de Nascimento:</asp:Label>
                                    <asp:TextBox ID="tb_data_nascimento" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_data_nascimento" runat="server" ErrorMessage="Data de nascimento obrigatória" Text="*" ControlToValidate="tb_data_nascimento"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group" style="margin:0px;">
                                    <asp:Label ID="lbl_email" runat="server">Email:</asp:Label>
                                    <asp:TextBox ID="tb_email" runat="server" CssClass="form-control" TextMode="Email" />
                                    <asp:RequiredFieldValidator ID="rfv_email" runat="server" ErrorMessage="Email obrigatório" ControlToValidate="tb_email" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_email" runat="server" ErrorMessage="Email inválido" ControlToValidate="tb_email" Text="*" ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group" style="margin:0px;">
                                    <asp:Label ID="lbl_username" runat="server">Username:</asp:Label>
                                    <asp:TextBox ID="tb_username" runat="server" CssClass="form-control" MaxLength="50" />
                                    <asp:RequiredFieldValidator ID="rfv_username" runat="server" ErrorMessage="Username é obrigatório" ControlToValidate="tb_username" Text="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group" style="margin:0px;"> 
                                    <asp:Label ID="lbl_password" runat="server">Password:</asp:Label>
                                    <asp:TextBox ID="tb_pw" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ErrorMessage="Password obrigatória" ControlToValidate="tb_pw" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rev_pw" runat="server" ErrorMessage="Password inválida. Pelo menos 9 caracteres com letras, números e caracteres especiais." ControlToValidate="tb_pw" Text="*" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,20}$"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-group" style="margin:0px; margin-bottom: 20px;">
                                    <asp:Label ID="Label1" runat="server">Repita a Password:</asp:Label>
                                    <asp:TextBox ID="tb_pw_rpt" runat="server" TextMode="Password" CssClass="form-control" />
                                </div>
                                <asp:Button ID="btn_registar" runat="server" Text="Registar" CssClass="btn btn-primary btn-block" OnClick="btn_registar_Click" />
                                <asp:LinkButton ID="btn_register_google" runat="server" CssClass="btn btn-google btn-block" OnClick="Login" CausesValidation="false">
                                        <i class="fab fa-google mr-2"></i> Sign up with Google
                                </asp:LinkButton>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                            <asp:Label ID="lbl_mensagem" runat="server" Text="" Style="margin: 10px;"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Register Form -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

