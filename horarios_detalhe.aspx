<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="horarios_detalhe.aspx.cs" Inherits="Projeto_Final.horarios_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card" style="border-color: #333; background-color: antiquewhite; margin-top: 30px; margin-bottom: 30px;">
            <div class="card-header bg-dark text-white">
                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Horário da Turma
                    <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label></h2>
            </div>
            <div class="card-body">
                <div class="row border-dark">
                    <div class="col-md-2">
                        <label class="col-form-label">Módulo</label>
                        <asp:DropDownList ID="ddl_modulo" AutoPostBack="true" CssClass="form-control" runat="server" DataSourceID="modulos" DataTextField="nome_modulo" DataValueField="cod_modulo"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="modulos" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_modulo], [nome_modulo], [cod_ufcd] FROM [Modulos]"></asp:SqlDataSource>
                    </div>
                    <div class="col-md-2">
                        <label class="col-form-label">Formador</label>
                        <asp:Label ID="lbl_nome_formador" CssClass="form-control" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hf_cod_formador" runat="server" />
                    </div>
                </div>
                <div id='calendar' class="bg-light border-dark" style="padding: 10px; margin-top: 30px; margin-bottom: 10px;"></div>
                <div class="row justify-content-between">
                    <div class="form-group" style="margin: 10px;">
                        <asp:Label ID="lbl_sabados" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <button id="btn_SelecionarSabados" class="btn btn-dark">Sábados</button>
                                <button id="btn_SelecionarSabadosManha" class="btn btn-dark">Sábados de manhã</button>
                                <button id="btn_SelecionarSabadosTarde" class="btn btn-dark">Sábados de tarde</button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="margin: 10px;">
                        <asp:Label ID="Label1" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <button id="btn_SelecionarDiasSemanaManha" class="btn btn-dark">Dias de semana manhã</button>
                                <button id="btn_SelecionarDiasSemanaTarde" class="btn btn-dark">Dias de semana de tarde</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-between" style="margin: 10px;">
            <button class="btn btn-success" id="btn_SaveSelectedSlots">Submeter Horário</button>
            <asp:HiddenField ID="hf_cod_user" runat="server" />
            <a href="horarios.aspx" class="btn btn-info">Voltar para a página Horários</a>
        </div>
    </div>
</asp:Content>
