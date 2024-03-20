<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="turmas.aspx.cs" Inherits="Projeto_Final.turmas" %>
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
                            <a href="#" class="list-group-item list-group-item-action active">Turmas</a>
                            <a href="criar_turma.aspx" class="list-group-item list-group-item-action">Criar Turma</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div id="div_turmas" class="col-md-10" runat="server">
                        <div id="card_turmas" class="card" style="border-color: #333; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Consultar Turmas</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_turmas_top" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_turmas_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_turmas_top" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_turmas_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Nome</th>
                                                    <th>Cód. Qual</th>
                                                    <th>Curso</th>
                                                    <th>Regime</th>
                                                    <th>Estado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpt_turmas" runat="server" OnItemDataBound="rpt_turmas_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><a href="turmas_detalhe.aspx?cod_user=<%# Eval("cod_turma") %>"><%# Eval("nome_turma") %></a></td>
                                                            <td><%# Eval("cod_qualificacao") %></td>
                                                            <td><%# Eval("nome_curso") %></td>
                                                            <td><%# Eval("regime") %></td>
                                                            <td><%# Eval("turma_estado") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_turmas" CssClass="btn btn-dark m-1" runat="server" OnClick="btn_previous_turmas_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_turmas" runat="server" CssClass="btn btn-dark m-1" OnClick="btn_next_turmas_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 13px;">
                        <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
