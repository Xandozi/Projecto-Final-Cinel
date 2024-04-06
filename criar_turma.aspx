<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="criar_turma.aspx.cs" Inherits="Projeto_Final.criar_turma" %>

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
                            <a href="criar_turma.aspx" class="list-group-item list-group-item-action active">Criar Turma</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action">Gestão</a>
                            <a href="turmas.aspx" class="list-group-item list-group-item-action">Voltar</a>
                        </div>
                    </div>
                    <!-- Form to create a new course -->
                    <div class="col-md-10" style="margin-top: 10px;">
                        <div class="card" style="border-color: #333; background-color:antiquewhite;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Criar Nova Turma</h2>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h5><asp:Label ID="lbl1" runat="server" Text="Informação Curso"></asp:Label></h5>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6" style="margin-top: 10px;">
                                            <asp:Label ID="lbl_curso" runat="server" Text="Curso"></asp:Label>
                                            <asp:DropDownList ID="ddl_curso" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" DataSourceID="curso" DataTextField="nome_curso" DataValueField="cod_curso" OnSelectedIndexChanged="ddl_curso_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="curso" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_curso], [nome_curso] FROM [Cursos] WHERE [ativo] = 1"></asp:SqlDataSource>
                                        </div>
                                        <div class="col-md-4" style="margin-top: 10px;">
                                            <asp:Label ID="lbl_data_inicio" runat="server" Text="Data de Início"></asp:Label>
                                            <asp:TextBox ID="tb_data_inicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2" style="margin-top: 10px;">
                                            <asp:Label ID="lbl_regime" runat="server" Text="Regime"></asp:Label>
                                            <asp:DropDownList ID="ddl_regime" runat="server" AppendDataBoundItems="true" CssClass="form-control" DataSourceID="regime" DataTextField="regime" DataValueField="cod_regime">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="regime" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Regime]"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6" style="margin-top: 20px;">
                                            <h5><asp:Label ID="lbl2" runat="server" Text="Formadores - Módulos"></asp:Label></h5>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-6">
                                            <asp:Label ID="lbl_lstb_formadores" runat="server" Text="Formadores"></asp:Label>
                                            <asp:ListBox ID="lstb_formadores" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="lstb_formadores_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lbl_lstb_modulos" runat="server" Text="Módulos"></asp:Label>
                                            <asp:ListBox ID="lstb_modulos" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Button ID="btn_add" CssClass="btn btn-success" runat="server" Text="Adicionar à Turma" OnClick="btn_add_Click" />
                                            <asp:Button ID="btn_remove" CssClass="btn btn-danger" runat="server" Text="Remover da Turma" OnClick="btn_remove_Click" />
                                            <asp:Label ID="lbl_horas_totais_formador" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_mensagem_formadores_modulos" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Label ID="lbl_formadores_modulos" runat="server" Text="Formadores | Módulos"></asp:Label>
                                            <asp:ListBox ID="lstb_formadores_modulos" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6" style="margin-top: 20px;">
                                            <h5><asp:Label ID="lbl3" runat="server" Text="Formandos"></asp:Label></h5>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Label ID="lbl_formandos" runat="server" Text="Formandos Elegíveis"></asp:Label>
                                            <asp:ListBox ID="lstb_formandos_legiveis" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Button ID="btn_add_formandos" CssClass="btn btn-success" runat="server" Text="Adicionar à Turma" OnClick="btn_add_formandos_Click" />
                                            <asp:Button ID="btn_remove_formandos" CssClass="btn btn-danger" runat="server" Text="Remover da Turma" OnClick="btn_remove_formandos_Click" />
                                            <asp:Label ID="lbl_mensagem_formandos" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <asp:Label ID="Label1" runat="server" Text="Formandos na Turma"></asp:Label>
                                            <asp:ListBox ID="lstb_formandos_turma" CssClass="form-control" runat="server"></asp:ListBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:Button ID="btn_criar_turma" CssClass="btn btn-primary" runat="server" Text="Criar Turma" OnClick="btn_criar_turma_Click" />
                                            <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
