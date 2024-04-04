<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="gestao.aspx.cs" Inherits="Projeto_Final.gestao" %>

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
                            <a href="gestao.aspx" class="list-group-item list-group-item-action active">Gestão</a>
                            <a href="cursos.aspx" class="list-group-item list-group-item-action">Cursos</a>
                            <a href="formadores.aspx" class="list-group-item list-group-item-action">Formadores</a>
                            <a href="formandos.aspx" class="list-group-item list-group-item-action">Formandos</a>
                            <a href="horarios.aspx" class="list-group-item list-group-item-action">Horários</a>
                            <a href="modulos.aspx" class="list-group-item list-group-item-action">Módulos</a>
                            <a href="salas.aspx" class="list-group-item list-group-item-action">Salas</a>
                            <a href="turmas.aspx" class="list-group-item list-group-item-action">Turmas</a>
                            <a href="utilizadores.aspx" class="list-group-item list-group-item-action">Utilizadores</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div id="div_validacoes" class="col-md-10" runat="server">
                        <div id="card_formadores" class="card" style="border-color: #333; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Formadores por Validar Inscrição</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formadores_top" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formadores_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formadores_top" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formadores_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Nome</th>
                                                    <th>Cód. Qual.</th>
                                                    <th>Curso</th>
                                                    <th>Data Insc.</th>
                                                    <th>Ação</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpt_formadores" runat="server" OnItemDataBound="rpt_formadores_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><a href="utilizadores_detalhe.aspx?cod_user=<%# Eval("cod_user") %>"><%# Eval("nome_proprio") + " " + Eval("apelido") %></a></td>
                                                            <td><%# Eval("cod_qualificacao") %></td>
                                                            <td><%# Eval("nome_curso") %></td>
                                                            <td><%# Eval("data_inscricao", "{0:dd/MM/yyyy}") %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lb_validar_formadores" runat="server" Style="margin-right: 5px;" OnClick="lb_validar_formadores_Click"><i class="fas fa-check-square fa-lg" style="color: #18b620;"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lb_revogar_formadores" runat="server" Style="margin-right: 5px;" OnClick="lb_revogar_formadores_Click"><i class="fas fa-window-close fa-lg" style="color: #cd4532;"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="cod-md-12" style="margin: 13px;">
                                        <asp:Label ID="lbl_mensagem_formadores" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formadores" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formadores_Click">
                <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formadores" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formadores_Click">
                <i class="fas fa-arrow-right"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div id="card_formandos" class="card" style="border-color: #333; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Formandos por Validar Inscrição</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formandos_top" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formandos_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formandos_top" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formandos_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Nome</th>
                                                    <th>Cód. Qual.</th>
                                                    <th>Curso</th>
                                                    <th>Data Insc.</th>
                                                    <th>Ação</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpt_formandos" runat="server" OnItemDataBound="rpt_formandos_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><a href="formandos_detalhe.aspx?cod_user=<%# Eval("cod_user") %>"><%# Eval("nome_proprio") + " " + Eval("apelido") %></a></td>
                                                            <td><%# Eval("cod_qualificacao") %></td>
                                                            <td><%# Eval("nome_curso") %></td>
                                                            <td><%# Eval("data_inscricao", "{0:dd/MM/yyyy}") %></td>
                                                            <td>
                                                                <asp:LinkButton ID="lb_validar_formandos" runat="server" Style="margin-right: 5px;" OnClick="lb_validar_formandos_Click"><i class="fas fa-check-square fa-lg" style="color: #18b620;"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lb_revogar_formandos" runat="server" Style="margin-right: 5px;" OnClick="lb_revogar_formandos_Click"><i class="fas fa-window-close fa-lg" style="color: #cd4532;"></i></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <div class="row" style="margin: 13px;">
                                            <asp:Label ID="lbl_mensagem_formandos" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formandos" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formandos_Click"><i class="fas fa-arrow-left"></i> </asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formandos" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formandos_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
