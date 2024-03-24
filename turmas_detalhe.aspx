<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="turmas_detalhe.aspx.cs" Inherits="Projeto_Final.turmas_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style="margin-top: 10px;">
                <div class="list-group">
                    <a href="#" class="list-group-item list-group-item-action active">Informações da Turma <%= lbl_nome_turma.Text %></a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#modal_adicionar_formandos">Adicionar Formandos à Turma</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#modal_cancelar_formandos">Cancelar Formandos da Turma</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#modal_adicionar_formadores">Adicionar Formadores à Turma</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#modal_adicionar_formadores">Remover Formadores da Turma</a>
                    <a href="turmas.aspx" class="list-group-item list-group-item-action">Voltar</a>
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-10" style="margin-top: 10px;">
                <div class="card" style="border-color: #333;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 40px; color: white;">Informação da Turma <%= lbl_nome_turma.Text %></h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-7">
                                <p class="lead">
                                    Nome Turma:
                                <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Código de Turma:
                                <asp:Label ID="lbl_cod_turma" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Curso:
                                <asp:Label ID="lbl_nome_curso" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Regime:
                                <asp:Label ID="lbl_regime" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Duração:
                                <asp:Label ID="lbl_duracao" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Data Início:
                                <asp:Label ID="lbl_data_inicio" runat="server" Text=""></asp:Label>
                                </p>
                                 <p class="lead">
                                    Data Fim (Previsão):
                                <asp:Label ID="lbl_data_fim" runat="server" Text=""></asp:Label>
                                </p>
                                <p class="lead">
                                    Estado:
                                <asp:Label ID="lbl_estado" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <div class="col-md-5">
                                <div class="row" style="margin-left: 2px;">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <div class="row">
                                            <p class="lead">
                                                <asp:Label ID="lbl_formadores_modulos" runat="server" Text="Formadores - Módulos"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="row">
                                            <asp:ListBox ID="lstb_formadores_modulos" CssClass="form-control" runat="server" Style="width: 90%;"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-left: 2px;">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <div class="row">
                                            <p class="lead">
                                                <asp:Label ID="lbl_formandos" runat="server" Text="Formandos"></asp:Label>
                                            </p>
                                        </div>
                                        <div class="row">
                                            <asp:ListBox ID="lstb_formandos" CssClass="form-control" runat="server" Style="width: 90%;"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex justify-content-lg-between mt-3">
                            <asp:Button ID="btn_cancelar" class="btn btn-danger btn-lg" runat="server" Text="Cancelar Turma" CausesValidation="false" OnClick="btn_cancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" style="margin-bottom: 30px; margin-top: 30px; margin-left: 10px;">
                <div>
                    <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="modal_adicionar_formandos" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Adicionar Formandos à Turma</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-6" style="margin-top: 20px;">
                            <h5>
                                <asp:Label ID="lbl_formandos_modal" runat="server" Text="Formandos"></asp:Label></h5>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12" style="margin-top: 5px;">
                            <asp:Label ID="lbl_formandos_elegiveis_modal" runat="server" Text="Formandos Elegíveis"></asp:Label>
                            <asp:ListBox ID="lstb_formandos_legiveis" CssClass="form-control" runat="server"></asp:ListBox>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12" style="margin-top: 5px;">
                            <asp:Button ID="btn_add_formandos" CssClass="btn btn-info" runat="server" Text="Adicionar à Turma" CausesValidation="false" OnClick="btn_add_formandos_Click" OnClientClick="return false;" />
                            <asp:Button ID="btn_remove_formandos" CssClass="btn btn-danger" runat="server" Text="Remover da Turma" CausesValidation="false" OnClick="btn_remove_formandos_Click" OnClientClick="return false;" />
                            <asp:Label ID="lbl_mensagem_formandos" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-md-12" style="margin-top: 5px;">
                            <asp:Label ID="lbl_formandos_turma_modal" runat="server" Text="Formandos Adicionados à Turma"></asp:Label>
                            <asp:ListBox ID="lstb_formandos_turma" CssClass="form-control" runat="server"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_gravar_alteracoes_formandos" CssClass="btn btn-dark" runat="server" Text="Gravar Alterações" OnClick="btn_gravar_alteracoes_formandos_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
