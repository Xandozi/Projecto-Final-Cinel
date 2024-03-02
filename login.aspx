<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Projeto_Final.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Login Form -->
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">Login</div>
                    <div class="card-body">
                        <form>
                            <div class="form-group">
                                <label for="username">Username:</label>
                                <input type="text" class="form-control" id="username" placeholder="Enter username">
                            </div>
                            <div class="form-group">
                                <label for="password">Password:</label>
                                <input type="password" class="form-control" id="password" placeholder="Enter password">
                            </div>
                            <button type="submit" class="btn btn-primary">Login</button>
                            <a href="#" class="btn btn-link">Esqueci-me da Password</a>
                            <hr>
                            <button type="button" class="btn btn-danger">
                                <i class="fab fa-google"></i> Sign in with Google
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Login Form -->
</asp:Content>
