﻿<%@ Page Title="CINEL - Detalhes do Curso" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="cursos_detalhe.aspx.cs" Inherits="Projeto_Final.cursos_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px;">
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="#" class="list-group-item list-group-item-action active"><i class="fas fa-chalkboard"></i> Curso <%= lbl_cod_qualificacao.Text %> - <%= lbl_nome_curso.Text %></a>
                            <a href="criar_curso.aspx" class="list-group-item list-group-item-action"><i class="fas fa-plus-circle"></i> Criar Curso</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                            <a href="cursos.aspx" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card bg-secondary" style="border-color: #333; color:black;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 30px; color: white;">Curso <%= lbl_cod_qualificacao.Text %></h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <p class="lead">
                                                Área:
                                                <asp:Label ID="lbl_area" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Código Qualificação:
                                                <asp:Label ID="lbl_cod_qualificacao" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Designação do Curso:
                                                <asp:Label ID="lbl_nome_curso" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Duração Curso (Estágio incluído):
                                                <asp:Label ID="lbl_duracao_curso" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                UFCDs do curso:
                                                <div class="listbox-container">
                                                    <asp:ListBox ID="lb_ufcd" runat="server"></asp:ListBox>
                                                </div>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Duração Estágio:
                                                <asp:Label ID="lbl_duracao_estagio" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Data de Criação:
                                                <asp:Label ID="lbl_data_criacao" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Último Update:
                                                <asp:Label ID="lbl_ultimo_update" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Estado:
                                                <asp:Label ID="lbl_estado" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3" style="margin-left: 10px; margin-top: 12px;">
                        <asp:Button ID="btn_editar" CssClass="btn btn-info py-2 px-4 ml-2" runat="server" Text="Editar Curso" OnClick="btn_editar_Click" style="margin-top: 5px;" />
                        <asp:Button ID="btn_apagar" CssClass="btn btn-danger py-2 px-4 ml-2" runat="server" Text="Apagar Curso" data-toggle="modal" data-target="#deleteModal" style="margin-top: 5px;" />
                    </div>
                </div>
                <div class="row" style="margin-top: 30px;">
                    <div class="col">
                        <div class="message-container" style="max-width: 100%; margin-top: 15px;">
                            <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Confirmar Exclusão</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Tem a certeza que deseja apagar este curso?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btn_confirm_delete" runat="server" Text="Apagar" CssClass="btn btn-danger" OnClick="btn_apagar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
