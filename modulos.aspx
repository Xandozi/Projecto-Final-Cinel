<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="modulos.aspx.cs" Inherits="Projeto_Final.modulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-3 bg-light">
                <div class="list-group">
                    <a href="criar_modulo.aspx" class="list-group-item list-group-item-action">Criar Módulo</a>
                    <a href="editar_modulo.aspx" class="list-group-item list-group-item-action">Editar Módulo</a>
                    <a href="eliminar_modulo.aspx" class="list-group-item list-group-item-action">Eliminar Módulo</a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action">Voltar</a>
                </div>
            </div>
            <div class="col-md-9">
                <div class="card" style="border-color: #333;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 40px; color: white;">Consulta de Módulos</h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Repeater ID="rpt_modulos" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-4">
                                            <div class="card" style="width: 90%; height: 75%; padding: 10px; margin: 5px;">
                                                123
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
