﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="cursos.aspx.cs" Inherits="Projeto_Final.cursos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="filterForm" runat="server" style="margin-bottom: 10px; margin-top: 10px; border: 1px solid #ccc; padding: 10px;">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Designação Curso:</label>
                                <asp:TextBox ID="tb_designacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Duração Curso:</label>
                                <asp:TextBox ID="tb_duracao" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Código da Qualificação:</label>
                                <asp:TextBox ID="tb_cod_qualificacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Início do Intervalo de Criação:</label>
                                        <asp:TextBox ID="tb_data_inicio" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de início"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Fim do Intervalo de Criação:</label>
                                        <asp:TextBox ID="tb_data_fim" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de fim"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ordenação Código Qualificação:</label>
                                        <asp:DropDownList ID="ddl_cod_ufcd" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">Ascendente</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 d-flex justify-content-end align-items-end">
                            <div class="form-group justify-content-around">
                                <asp:Button ID="btn_aplicar_filtros" runat="server" Text="Aplicar" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="cursos.aspx" class="list-group-item list-group-item-action active">Cursos</a>
                            <a href="criar_curso.aspx" class="list-group-item list-group-item-action">Criar Curso</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-10">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 30px; color: white;">Consulta de Cursos</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:Button ID="btn_previous_top" runat="server" Text="Previous" CssClass="btn btn-primary m-2" OnClick="btn_previous_Click" />
                                <asp:Button ID="btn_next_top" runat="server" Text="Next" CssClass="btn btn-primary m-2" OnClick="btn_next_Click" />
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:Repeater ID="rpt_cursos" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <div class="card" style="margin: 5px;">
                                                    <a href="cursos_detalhe.aspx?cod_curso=<%# Eval("cod_curso") %>" style="text-decoration: none;">
                                                        <div class="card-body">
                                                            <h5 class="card-title"><%# Eval("nome_curso") %></h5>
                                                            <p class="card-text">Duração: <%# Eval("duracao_curso") %> horas</p>
                                                            <p class="card-text">Data de Criação: <%# Eval("data_criacao", "{0:dd/MM/yyyy}") %></p>
                                                            <p class="card-text">Código Qualificação: <%# Eval("cod_qualificacao") %></p>
                                                            <p class="card-text">Último Update: <%# Eval("ultimo_update") %></p>
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:Button ID="btn_previous" runat="server" Text="Previous" CssClass="btn btn-primary m-2" OnClick="btn_previous_Click" />
                                <asp:Button ID="btn_next" runat="server" Text="Next" CssClass="btn btn-primary m-2" OnClick="btn_next_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
