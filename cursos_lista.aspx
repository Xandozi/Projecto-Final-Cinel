<%@ Page Title="CINEL - Lista de Cursos" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="cursos_lista.aspx.cs" Inherits="Projeto_Final.cursos_lista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center display-4">
        <h5 class="text-primary text-uppercase mb-3" style="letter-spacing: 5px; margin-top: 20px;">Cursos</h5>
        <h1>Os Nossos Cursos Populares</h1>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <asp:LinkButton ID="btn_filtros" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClientClick="toggleFilters(); return false;" Style="margin-top: 10px;">
                    <i class="fas fa-filter">Filtros</i>
                </asp:LinkButton>
                <script type="text/javascript">
                    function toggleFilters() {
                        var filterForm = document.getElementById('<%= filterForm.ClientID %>');
                        if (filterForm.style.display === 'none') {
                            filterForm.style.display = 'block';
                        } else {
                            filterForm.style.display = 'none';
                        }
                    }
                </script>
                <div id="filterForm" class="border-dark bg-secondary" runat="server" style="display: none; margin-bottom: 10px; margin-top: 10px; border: 1px solid #ccc; color:black; padding: 10px;">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Área:</label>
                                <asp:DropDownList ID="ddl_area" CssClass="form-control" AppendDataBoundItems="true" runat="server" Style="margin-left: 5px;" DataSourceID="areas" DataTextField="area" DataValueField="cod_area"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="areas" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Areas]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Designação Curso:</label>
                                <asp:TextBox ID="tb_designacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Duração</label>
                                <asp:DropDownList ID="ddl_duracao" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                    <asp:ListItem>Todas</asp:ListItem>
                                    <asp:ListItem Value="curta">Curta Duração</asp:ListItem>
                                    <asp:ListItem Value="longa">Longa Duração</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Código da Qualificação:</label>
                                <asp:TextBox ID="tb_cod_qualificacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Início do Intervalo de Criação:</label>
                                        <asp:TextBox ID="tb_data_inicio" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de início"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Fim do Intervalo de Criação:</label>
                                        <asp:TextBox ID="tb_data_fim" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de fim"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ordenação Código Qualificação:</label>
                                        <asp:DropDownList ID="ddl_cod_ufcd" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">Ascendente</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 d-flex justify-content-end align-items-end">
                            <div class="form-group justify-content-around">
                                <asp:Button ID="btn_aplicar_filtros" runat="server" Text="Aplicar Filtros" CssClass="btn btn-dark" OnClick="btn_aplicar_filtros_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
                    <div class="col-md-12">
                        <div class="card bg-secondary" style="border-color: #333; color:black;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Lista de Cursos do Cinel</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true" style="margin-top: 10px;">
                                <asp:LinkButton ID="btn_previous_top" CssClass="btn btn-light border-dark m-2" runat="server" OnClick="btn_previous_Click">
                                    <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next_top" runat="server" CssClass="btn btn-light border-dark m-2" OnClick="btn_next_Click">
                                    <i class="fas fa-arrow-right"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:Repeater ID="rpt_cursos" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <div class="card border-dark" style="margin: 5px;">
                                                    <a href="cursos_lista_detalhe.aspx?cod_curso=<%# Eval("cod_curso") %>" style="text-decoration: none;">
                                                        <div class="card-body">
                                                            <h5 class="card-title"><b><%# Eval("nome_curso") %></b></h5>
                                                            <p class="card-text" style="color: black;">Área: <span style="color: orangered"><%# Eval("area") %></span></p>
                                                            <p class="card-text" style="color: black;">Duração: <span style="color: orangered"><%# Eval("duracao_curso") %> horas</span></p>
                                                            <p class="card-text" style="color: black;">Data de Criação: <span style="color: orangered"><%# Eval("data_criacao", "{0:dd/MM/yyyy}") %></span></p>
                                                            <p class="card-text" style="color: black;">Código Qualificação: <span style="color: orangered"><%# Eval("cod_qualificacao") %></span></p>
                                                            <p class="card-text" style="color: black;">Último Update: <span style="color: orangered"><%# Eval("ultimo_update") %></span></p>
                                                        </div>
                                                    </a>
                                                    <div class="card-body">
                                                        <a href="cursos_lista_detalhe.aspx?cod_curso=<%# Eval("cod_curso") %>" class="btn btn-primary">Inscrever-me</a>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true" style="margin-bottom: 10px;">
                                <asp:LinkButton ID="btn_previous" CssClass="btn btn-light border-dark m-2" runat="server" OnClick="btn_previous_Click">
                                    <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next" runat="server" CssClass="btn btn-light border-dark m-2" OnClick="btn_next_Click">
                                    <i class="fas fa-arrow-right"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
