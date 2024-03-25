<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="turmas_adicionar_formandos.aspx.cs" Inherits="Projeto_Final.turmas_adicionar_formandos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-top: 10px;">
                        <div class="list-group">
                            <a href="#" class="list-group-item list-group-item-action active">Adicionar Formandos a <%= lbl_nome_turma.Text %></a>
                            <a href="turmas_detalhe.aspx?cod_turma=<%= cod_turma_hidden.Value.ToString()  %>" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <!-- Main Content -->
                    <div class="col-md-10" style="margin-top: 10px;">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Adicionar Formandos a <%= lbl_nome_turma.Text %></h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="txtNomeTurma">Nome Turma:</label>
                                            <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="cod_turma_hidden" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="lstb_formandos_legiveis">Formandos Elegíveis:</label>
                                            <asp:ListBox ID="lstb_formandos_elegiveis" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btn_add_formandos" CssClass="btn btn-info" runat="server" Text="Adicionar à Turma" CausesValidation="false" OnClick="btn_add_formandos_Click" />
                                            <asp:Button ID="btn_remove_formandos" CssClass="btn btn-danger" runat="server" Text="Remover da Turma" CausesValidation="false" OnClick="btn_remove_formandos_Click" />
                                            <asp:Label ID="lbl_mensagem_formandos" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label for="lstb_formandos_turma">Formandos Adicionados à Turma:</label>
                                            <asp:ListBox ID="lstb_formandos_turma" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btn_salvar_alteracoes" CssClass="btn btn-success" runat="server" Text="Guardar Alterações" OnClick="btn_salvar_alteracoes_Click" />
                                            <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lstb_formandos_elegiveis" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lstb_formandos_turma" EventName="SelectedIndexChanged" />
    </Triggers>
    </asp:UpdatePanel>
</asp:Content>
