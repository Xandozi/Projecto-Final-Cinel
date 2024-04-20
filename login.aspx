<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Projeto_Final.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- Login Form -->
            <div class="container mt-5 justify-content-center">
                <div class="row justify-content-center" style=" margin-bottom: 20px;">
                    <div class="col-md-6">
                        <div class="card border-primary bg-secondary" style="color:black;">
                            <div class="card-header bg-primary text-white text-center">
                                <h4 class="mb-0" style="color: white;">Login</h4>
                            </div>
                            <div class="card-body">
                                <div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tb_username" class="form-control" runat="server" placeholder="Username"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tb_pw" class="form-control" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ErrorMessage="Password obrigatória" Text="*" ControlToValidate="tb_pw"></asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Button ID="btn_login" class="btn btn-primary btn-block" runat="server" Text="Login" OnClick="btn_login_Click" />
                                    <a data-toggle="modal" data-target="#resetPasswordModal" class="btn btn-link btn-block text-primary">Esqueci-me da Password</a>
                                    <hr>
                                    <asp:LinkButton ID="btn_login_google" runat="server" CssClass="btn btn-google btn-block" OnClick="Login" CausesValidation="false">
                                        <i class="fab fa-google mr-2"></i> Sign in with Google
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <asp:Label ID="lbl_mensagem" runat="server" Text="" style="margin: 10px;"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="alert alert-danger" runat="server" />
                    </div>
                </div>
            </div>
            <!-- End Login Form -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Modal for resetting the password -->
    <div class="modal" id="resetPasswordModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Reset your password</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div class="container" style="font-size: 20px;">
                        Email:
                        <asp:TextBox ID="tb_email" runat="server" TextMode="Email"></asp:TextBox>
                        <br />
                        <br />
                        <asp:Button ID="btn_forgot_pw" runat="server" Text="Reset Password" OnClick="btn_forgot_pw_Click" CausesValidation="false" />
                        <br />
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
