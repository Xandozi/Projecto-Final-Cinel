<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="personal_zone_inscricoes.aspx.cs" Inherits="Projeto_Final.personal_zone_inscricoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style="margin-top: 10px;">
                <div class="list-group">
                    <a href="personal_zone_inscricoes.aspx" class="list-group-item list-group-item-action active">Minhas Inscrições</a>
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                    <% if (Session["perfil"].ToString().Contains("Formador"))
                     { %>
                    <a href="formador_disponibilidade.aspx" class="list-group-item list-group-item-action">Disponibilidade Formador</a>
                    <% } %>
                    <asp:Button ID="btn_logout2" class="list-group-item list-group-item-action" runat="server" Text="Terminar Sessão" OnClick="btn_logout2_Click" />
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div id="div_inscricoes" class="col-md-10" runat="server">
                        <div id="card_formadores" class="card" style="border-color: #333; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Módulos como Formador</h2>
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
                                                    <th>Turma</th>
                                                    <th>Curso</th>
                                                    <th>Módulo</th>
                                                    <th>Avaliações</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpt_formadores" runat="server" OnItemDataBound="rpt_formadores_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval ("nome_turma") %></td>
                                                            <td><%# Eval("nome_curso") %></td>
                                                            <td><%# Eval("nome_modulo") %></td>
                                                            <td><asp:LinkButton ID="lb_avaliacoes" runat="server" Style="margin-right: 5px;" OnClick="lb_avaliacoes_Click"><i class="fas fa-clipboard-list"></i></asp:LinkButton></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formadores" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formadores_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formadores" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formadores_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div id="card_formandos" class="card" style="border-color: #333; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Cursos como Formando</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_formandos_top" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_formadores_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_formandos_top" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_formadores_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Turma</th>
                                                    <th>Curso</th>
                                                    <th>Data Insc.</th>
                                                    <th>Horário</th>
                                                    <th>Notas</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpt_formandos" runat="server" OnItemDataBound="rpt_formandos_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("nome_turma") %></td>
                                                            <td><%# Eval("nome_curso") %></td>
                                                            <td><%# Eval("data_inscricao", "{0:dd/MM/yyyy}") %></td>
                                                            <td><asp:LinkButton ID="lb_horario" runat="server" Style="margin-right: 5px;" OnClick="lb_horario_Click"><i class="fas fa-calendar"></i></asp:LinkButton>
                                                            <td><asp:LinkButton ID="lb_notas" runat="server" Style="margin-right: 5px;" OnClick="lb_notas_Click"><i class="fas fa-clipboard-list"></i></asp:LinkButton></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
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
        </div>
    </div>
</asp:Content>
