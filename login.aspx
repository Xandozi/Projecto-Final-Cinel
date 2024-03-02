<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Projeto_Final.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Login Form -->
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card border-primary">
                    <div class="card-header bg-primary text-white text-center">
                        <h4 class="mb-0" style="color: white;">Login</h4>
                    </div>
                    <div class="card-body">
                        <div>
                            <div class="form-group">
                                <input type="text" class="form-control" id="username" placeholder="Username">
                            </div>
                            <div class="form-group">
                                <input type="password" class="form-control" id="password" placeholder="Password">
                            </div>
                            <button type="submit" class="btn btn-primary btn-block">Login</button>
                            <a href="#" class="btn btn-link btn-block text-primary">Esqueci-me da Password</a>
                            <hr>
                            <button type="button" class="btn btn-google btn-block">
                                <i class="fab fa-google mr-2"></i>
                                Sign in with Google
                       
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Login Form -->


</asp:Content>
