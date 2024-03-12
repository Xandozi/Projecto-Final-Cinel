<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_curso.aspx.cs" Inherits="Projeto_Final.editar_curso" %>

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
                            <a href="#" class="list-group-item list-group-item-action active">Editar Curso <%= tb_cod_qualificacao.Text %> - <%= tb_designacao.Text %></a>
                            <a href="criar_curso.aspx" class="list-group-item list-group-item-action">Criar Curso</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="cursos.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Curso <%= tb_cod_qualificacao.Text %></h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <p class="lead">
                                                Código Qualificação:
                                                <asp:TextBox ID="tb_cod_qualificacao" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_cod_qualificacao" runat="server" ErrorMessage="Código Qualificação obrigatório" Text="*" ControlToValidate="tb_cod_qualificacao"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev_cod_qualificacao" runat="server" ErrorMessage="Código Qualificação inválido" ValidationExpression="^[0-9]{1,10}$" ControlToValidate="tb_cod_qualificacao" Text="*"></asp:RegularExpressionValidator>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Designação da UFCD:
                                                <asp:TextBox ID="tb_designacao" runat="server" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_designacao" runat="server" ErrorMessage="Designação da UFCD obrigatória." Text="*" ControlToValidate="tb_designacao"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev_designacao" runat="server" ErrorMessage="Designação do curso inválida" ControlToValidate="tb_designacao" Text="*" ValidationExpression="^.{1,100}$"></asp:RegularExpressionValidator>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">UFCDs do Curso</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_cod_ufcd" runat="server" Style="width: 90%;" TextMode="Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_cod_ufcd" runat="server" ErrorMessage="Código UFCD obrigatório." ControlToValidate="tb_cod_ufcd" Text="*" ValidationGroup="insert_ufcd"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <asp:Button ID="btn_add_ufcd" CssClass="btn btn-primary" runat="server" Text="Adicionar ao Curso" OnClick="btn_add_ufcd_Click" ValidationGroup="insert_ufcd" />
                                                    <asp:Button ID="btn_remove_ufcd" CssClass="btn btn-danger" runat="server" Text="Remover do Curso" OnClick="btn_remove_ufcd_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:ListBox ID="lb_ufcds" runat="server" CssClass="form-control" SelectionMode="Single" Style="width: 90%;"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Duração Curso (Estágio incluído):
                                                <asp:Label ID="lbl_duracao_curso" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                        <div class="form-group">
                                            <p class="lead">
                                                Duração Estágio:
                                                <asp:TextBox ID="tb_duracao" runat="server" TextMode="Number" MaxLength="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_duracao" runat="server" ErrorMessage="Duração é obrigatória." Text="*" ControlToValidate="tb_duracao"></asp:RequiredFieldValidator>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <asp:Button ID="btn_editar" CssClass="btn btn-info py-2 px-4 ml-2" runat="server" Text="Gravar Alterações" OnClick="btn_editar_Click" />
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div style="margin-bottom: 30px;">
                        <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
