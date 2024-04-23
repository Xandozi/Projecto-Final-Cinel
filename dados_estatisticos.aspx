<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="dados_estatisticos.aspx.cs" Inherits="Projeto_Final.dados_estatisticos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px;">
            <div class="col-md-2 bg-light" style="margin-bottom: 10px;">
                <div class="list-group">
                    <a href="dados_estatisticos.aspx" class="list-group-item list-group-item-action active"><i class="fas fa-list-ol"></i> Dados Estatísticos</a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                </div>
            </div>
            <div class="col-md-10">
                <div class="row bg-secondary" style="padding: 10px;">
                    <div class="col-md-4">
                        <h3>Total Cursos</h3>
                        <table class="table table-striped">
                            <tr>
                                <th>Nº Cursos Acabados</th>
                                <td>
                                    <asp:Label ID="lblTotalCompletedCourses" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>Nº Cursos a Decorrer</th>
                                <td>
                                    <asp:Label ID="lblTotalOngoingCourses" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4">
                        <h3>Nº Formandos Atualmente</h3>
                        <p>
                            <asp:Label ID="lblTotalTrainees" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-4">
                        <h3>Top 10 Formadores Horas</h3>
                        <ul>
                            <asp:Repeater ID="rptTopTrainers" runat="server">
                                <ItemTemplate>
                                    <li><%# Eval("nome_completo") %></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="col-md-4">
                        <h3>Total Cursos por Área</h3>
                        <ul>
                            <asp:Repeater ID="rpt_cursos_area" runat="server">
                                <HeaderTemplate>
                                    <div class="row bg-secondary" style="padding: 10px;">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="col-md-4">
                                        <h6><%# Eval("Area") %></h6>
                                        <p>Total Cursos: <%# Eval("TotalCursos") %></p>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
