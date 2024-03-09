<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="gestao.aspx.cs" Inherits="Projeto_Final.gestao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-3 bg-light">
                <div class="list-group">
                    <a href="cursos.aspx" class="list-group-item list-group-item-action">Cursos</a>
                    <a href="formadores_gestao.aspx" class="list-group-item list-group-item-action">Formadores</a>
                    <a href="formandos.aspx" class="list-group-item list-group-item-action">Formandos</a>
                    <a href="horarios.aspx" class="list-group-item list-group-item-action">Horários</a>
                    <a href="modulos.aspx" class="list-group-item list-group-item-action">Módulos</a>
                    <a href="salas.aspx" class="list-group-item list-group-item-action">Salas</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Voltar</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
