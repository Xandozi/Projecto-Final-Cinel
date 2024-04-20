<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_turma.aspx.cs" Inherits="Projeto_Final.editar_turma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style="margin-top: 10px;">
                <div class="list-group">
                    <a href="#" class="list-group-item list-group-item-action active"><i class="fas fa-users"></i> Informações da Turma <%= lbl_nome_turma.Text %></a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                    <a href="turmas_detalhe.aspx?cod_turma=<%= lbl_cod_turma.Text %>" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                </div>
            </div>
            <!-- Main Content -->
            <div class="col-md-4" style="margin-top: 10px;">
                <div class="card bg-secondary" style="border-color: #333; color:black;">
                    <div class="card-header bg-dark text-white">
                        <h2 class="display-4" style="font-size: 40px; color: white;">Informação da Turma <%= lbl_nome_turma.Text %></h2>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <p class="lead">
                                        Nome Turma:
                                        <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Código de Turma:
                                        <asp:Label ID="lbl_cod_turma" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Curso:
                                        <asp:Label ID="lbl_nome_curso" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Regime:
                                        <asp:DropDownList ID="ddl_regime" CssClass="form-control" runat="server" DataSourceID="regime" DataTextField="regime" DataValueField="cod_regime"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="regime" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Regime]"></asp:SqlDataSource>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Duração:
                                        <asp:Label ID="lbl_duracao" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Data Início:
                                        <asp:Label ID="lbl_data_inicio" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Data Fim (Previsão):
                                        <asp:Label ID="lbl_data_fim" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group">
                                    <p class="lead">
                                        Estado:
                                        <asp:DropDownList ID="ddl_estado" CssClass="form-control" runat="server" DataSourceID="estado_turma" DataTextField="turma_estado" DataValueField="cod_turmas_estado"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="estado_turma" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Turmas_Estado]"></asp:SqlDataSource>
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex justify-content-lg-between mt-3">
                            <asp:Button ID="btn_guardar" class="btn btn-success btn-lg" runat="server" Text="Guardar Alterações" CausesValidation="false" OnClick="btn_guardar_Click" />
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
</asp:Content>
