<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="modulos_detalhe.aspx.cs" Inherits="Projeto_Final.modulos_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px;">
                    <!-- Sidebar -->
                    <div class="col-md-3 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="#" class="list-group-item list-group-item-action active">Módulo <%= lbl_cod_ufcd.Text %> - <%= lbl_nome_modulo.Text %></a>
                            <a href="criar_modulo.aspx" class="list-group-item list-group-item-action">Criar Módulo</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="modulos.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Módulo <%= lbl_cod_ufcd.Text %></h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <p class="lead">
                                                Código UFCD:
                                                <asp:Label ID="lbl_cod_ufcd" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Designação da UFCD:
                                                <asp:Label ID="lbl_nome_modulo" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Duração (Horas):
                                                <asp:Label ID="lbl_duracao" runat="server" Text=""></asp:Label>
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
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <asp:Button ID="btn_editar" CssClass="btn btn-info py-2 px-4 ml-2" runat="server" Text="Editar Módulo" OnClick="btn_editar_Click" />
                        <asp:Button ID="btn_apagar" CssClass="btn btn-danger py-2 px-4 ml-2" runat="server" Text="Apagar Módulo" data-toggle="modal" data-target="#deleteModal" />
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
                    Tem a certeza que deseja apagar este módulo?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btn_confirm_delete" runat="server" Text="Apagar" CssClass="btn btn-danger" OnClick="btn_apagar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
