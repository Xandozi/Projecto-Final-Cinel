<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="cursos_lista_detalhe.aspx.cs" Inherits="Projeto_Final.cursos_lista_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="margin-bottom: 10px;">
                <div class="row" style="margin-top: 30px;">
                    <div class="col-12">
                        <div class="card" style="border-color: #333; background-color:antiquewhite;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 40px; color: white;"><%= lbl_cod_qualificacao.Text %> - <%= lbl_nome_curso.Text %></h2>
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
                                                    <asp:ListBox ID="lb_ufcd" runat="server" Rows="5"></asp:ListBox>
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
                                            <div class="row justify-content-between" style="margin-left: 5px; margin-right:5px;">
                                                <div class="lead" style=" margin-bottom: 5px;">
                                                    <asp:Button ID="btn_inscrever_formando" runat="server" Text="Inscrever-me como Formando" CssClass="btn btn-primary" OnClick="btn_inscrever_formando_Click" />
                                                </div>
                                                <div class="lead">
                                                    <asp:Button ID="btn_inscrever_formador" runat="server" Text="Inscrever-me como Formador" CssClass="btn btn-danger" OnClick="btn_inscrever_formador_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top: 15px; margin-bottom: 15px; margin-left: 5px;">
                    <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
