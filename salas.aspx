<%@ Page Title="CINEL - Salas" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="salas.aspx.cs" Inherits="Projeto_Final.salas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="filterForm" runat="server" class="border-dark bg-secondary" style="display: none; margin-bottom: 10px; color:black; margin-top: 10px; border: 1px solid #ccc; padding: 10px;">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nome Sala:</label>
                                <asp:TextBox ID="tb_designacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Código Sala:</label>
                                <asp:TextBox ID="tb_cod_sala" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
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
                                        <label>Ordenação Código Sala:</label>
                                        <asp:DropDownList ID="ddl_cod_sala" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">Ascendente</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado:</label>
                                        <asp:DropDownList ID="ddl_estado" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem Value="2">Todos</asp:ListItem>
                                            <asp:ListItem Value="1">Ativa</asp:ListItem>
                                            <asp:ListItem Value="0">Inativa</asp:ListItem>
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
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="salas.aspx" class="list-group-item list-group-item-action active"><i class="fas fa-chair"></i> Salas</a>
                            <a href="criar_sala.aspx" class="list-group-item list-group-item-action"><i class="fas fa-plus-circle"></i> Criar Sala</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-user-shield"></i> Gestão</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action"><i class="fas fa-arrow-alt-circle-left"></i> Voltar</a>
                        </div>
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
                    </div>
                    <div class="col-md-10">
                        <div class="card bg-secondary" style="border-color: #333; color:black;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Consulta de Salas</h2>
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
                                    <asp:Repeater ID="rpt_salas" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <div class="card border-dark" style="margin: 5px;">
                                                    <a href="salas_detalhe.aspx?cod_sala=<%# Eval("cod_sala") %>" style="text-decoration: none;">
                                                        <div class="card-body">
                                                            <h5 class="card-title"><b><%# Eval("nome_sala") %></b></h5>
                                                            <p class="card-text" style="color:black;">Código da Sala: <span style="color: orangered"><%# Eval("cod_sala") %></span></p>
                                                            <p class="card-text" style="color:black;">Data de Criação: <span style="color: orangered"><%# Eval("data_criacao", "{0:dd/MM/yyyy}") %></span></p>
                                                            <p class="card-text" style="color:black;">Último Update: <span style="color: orangered"><%# Eval("ultimo_update") %></span></p>
                                                            <p class="card-text" style="color:black;">
                                                                Estado: 
                                                                <span class='<%# Convert.ToBoolean(Eval("ativo")) ? "ativo" : "inativo" %>'>
                                                                    <%# Convert.ToBoolean(Eval("ativo")) ? "Ativo" : "Inativo" %>
                                                                </span>
                                                            </p>
                                                        </div>
                                                    </a>
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
