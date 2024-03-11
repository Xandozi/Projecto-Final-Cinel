﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="criar_curso.aspx.cs" Inherits="Projeto_Final.criar_curso" %>

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
                            <a href="criar_curso.aspx" class="list-group-item list-group-item-action active">Criar Curso</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="cursos.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4">
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;">Criação de Curso</h2>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Código Qualificação</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_cod_qualificacao" runat="server" TextMode="Number" MaxLength="9" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_cod_qualificacao" runat="server" ErrorMessage="Código qualificação obrigatório" Text="*" ControlToValidate="tb_cod_qualificacao"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Designação do Curso</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_designacao" runat="server" MaxLength="100" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_designacao" runat="server" ErrorMessage="Designação do curso obrigatória." Text="*" ControlToValidate="tb_designacao"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">UFCDs do Curso</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_cod_ufcd" runat="server" Style="width: 90%;" TextMode="Number"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_cod_ufcd" runat="server" ErrorMessage="Código UFCD obrigatório." ControlToValidate="tb_cod_ufcd" Text="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 5px;">
                                                    <asp:Button ID="btn_add_ufcd" CssClass="btn btn-primary" runat="server" Text="Adicionar ao Curso" OnClick="btn_add_ufcd_Click" />
                                                    <asp:Button ID="btn_remove_ufcd" CssClass="btn btn-danger" runat="server" Text="Remover do Curso" OnClick="btn_remove_ufcd_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:ListBox ID="lb_selected_ufcds" runat="server" CssClass="form-control" SelectionMode="Multiple" Style="width: 90%;"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <p class="font-weight-bold">Duração do Estágio</p>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:TextBox ID="tb_duracao_estagio" runat="server" TextMode="Number" MaxLength="3" Style="width: 90%;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_duracao_estagio" runat="server" ErrorMessage="Duração do estágio é obrigatória." Text="*" ControlToValidate="tb_duracao_estagio"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btn_criar" CssClass="btn btn-primary py-2 px-4 ml-2" runat="server" Text="Criar Curso" OnClick="btn_criar_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-9 col-lg-5 col-xl-4" style="margin-left: 10px; margin-top: 12px;">
                        <div style="margin-bottom: 30px;">
                            <asp:Label ID="lbl_mensagem" CssClass="mt-3" runat="server" Text=""></asp:Label>
                        </div>
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
