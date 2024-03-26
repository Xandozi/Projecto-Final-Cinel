<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="utilizadores.aspx.cs" Inherits="Projeto_Final.utilizadores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="filterForm" runat="server" style="display: none; margin-bottom: 10px; margin-top: 10px; border: 1px solid #ccc; padding: 10px;">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Username:</label>
                                <asp:TextBox ID="tb_username" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Código User:</label>
                                <asp:TextBox ID="tb_cod_user" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Perfil:</label>
                                <asp:DropDownList ID="ddl_perfil" CssClass="form-control" runat="server" AppendDataBoundItems="true" DataSourceID="perfis" DataTextField="perfil" DataValueField="cod_perfil"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="perfis" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Perfis]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Email:</label>
                                <asp:TextBox ID="tb_email" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="SingleLine" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Início do Intervalo da Data de Nascimento:</label>
                                        <asp:TextBox ID="tb_data_inicio" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de início"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Fim do Intervalo da Data de Nascimento:</label>
                                        <asp:TextBox ID="tb_data_fim" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de fim"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ordenação Username:</label>
                                        <asp:DropDownList ID="ddl_sort_username" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">A-Z</asp:ListItem>
                                            <asp:ListItem Value="desc">Z-A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Estado:</label>
                                        <asp:DropDownList ID="ddl_estado" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem Value="2">Todos</asp:ListItem>
                                            <asp:ListItem Value="1">Ativo</asp:ListItem>
                                            <asp:ListItem Value="0">Inativo</asp:ListItem>
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
                            <a href="utilizadores.aspx" class="list-group-item list-group-item-action active">Utilizadores</a>
                            <a href="formadores.aspx" class="list-group-item list-group-item-action">Formadores</a>
                            <a href="formandos.aspx" class="list-group-item list-group-item-action">Formandos</a>
                            <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                            <a href="gestao.aspx" class="list-group-item list-group-item-action">Voltar</a>
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
                        <div class="card" style="border-color: #333;">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4" style="font-size: 30px; color: white;">Consulta de Utilizadores</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_top" CssClass="btn btn-dark m-2" runat="server" OnClick="btn_previous_Click">
                                    <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next_top" runat="server" CssClass="btn btn-dark m-2" OnClick="btn_next_Click">
                                    <i class="fas fa-arrow-right"></i>
                                </asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <asp:Repeater ID="rpt_users" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <div class="card" style="margin: 5px;">
                                                    <a href="utilizadores_detalhe.aspx?cod_user=<%# Eval("cod_user") %>" style="text-decoration: none;">
                                                        <div class="card-body">
                                                            <h5 class="card-title"><b><%# Eval("username") %></b></h5>
                                                            <p class="card-text" style="color: black;">Nome Completo: <span style="color: orangered"><%# Eval("nome_proprio") + " " + Eval("apelido") %></span></p>
                                                            <p class="card-text" style="color: black;">Email: <span style="color: orangered"><%# Eval("email") %></span></p>
                                                            <p class="card-text" style="color: black;">Data de Nascimento: <span style="color: orangered"><%# Eval("data_nascimento", "{0:dd/MM/yyyy}") %></span></p>
                                                            <p class="card-text" style="color: black;">Perfil/Perfis: <span style="color: orangered"><%# Eval("perfis") %></span></p>
                                                            <p class="card-text" style="color: black;">
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
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous" CssClass="btn btn-dark m-2" runat="server" OnClick="btn_previous_Click">
                                    <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next" runat="server" CssClass="btn btn-dark m-2" OnClick="btn_next_Click">
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
