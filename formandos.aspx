<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="formandos.aspx.cs" Inherits="Projeto_Final.formandos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="filterForm" runat="server" class="border-dark bg-secondary" style="display: none; margin-bottom: 10px; margin-top: 10px; color:black; border: 1px solid #ccc; padding: 10px;">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Nome Turma:</label>
                                <asp:TextBox ID="tb_nome_turma" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="50" placeholder="Nome Turma..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Nome Formando:</label>
                                <asp:TextBox ID="tb_nome_formando" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="SingleLine" placeholder="Nome Formando..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Curso:</label>
                                <asp:DropDownList ID="ddl_curso" CssClass="form-control" runat="server" AppendDataBoundItems="true" DataSourceID="cursos" DataTextField="nome_curso" DataValueField="cod_curso"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="cursos" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [nome_curso], [cod_curso] FROM [Cursos]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Regime:</label>
                                <asp:DropDownList ID="ddl_regime" CssClass="form-control" runat="server" AppendDataBoundItems="true" DataSourceID="regimes" DataTextField="regime" DataValueField="cod_regime"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="regimes" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Regime]"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Ordenação Nome Formando:</label>
                                <asp:DropDownList ID="ddl_ordem_nome_formando" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                    <asp:ListItem>Nenhuma</asp:ListItem>
                                    <asp:ListItem Value="asc">A-Z</asp:ListItem>
                                    <asp:ListItem Value="desc">Z-A</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Ordenação Nome Turma:</label>
                                <asp:DropDownList ID="ddl_ordem_nome_turma" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                    <asp:ListItem>Nenhuma</asp:ListItem>
                                    <asp:ListItem Value="asc">A-Z</asp:ListItem>
                                    <asp:ListItem Value="desc">Z-A</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Estado Inscrição:</label>
                                <asp:DropDownList ID="ddl_estado_inscricao" CssClass="form-control" runat="server" AppendDataBoundItems="true" DataSourceID="estado_inscricao" DataTextField="situacao" DataValueField="cod_situacao"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="estado_inscricao" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_situacao], [situacao] FROM [Situacao]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-1 d-flex justify-content-end align-items-end">
                            <div class="form-group justify-content-around">
                                <asp:Button ID="btn_aplicar_filtros" runat="server" Text="Aplicar Filtros" CssClass="btn btn-dark" OnClick="btn_aplicar_filtros_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
                    <!-- Sidebar -->
                    <div class="col-md-2 bg-light" style="margin-bottom: 10px;">
                        <div class="list-group">
                            <a href="formandos.aspx" class="list-group-item list-group-item-action active"><i class="fas fa-user-graduate"></i> Formandos</a>
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
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Consulta de Formandos</h2>
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
                                    <asp:Repeater ID="rpt_formandos" runat="server">
                                        <ItemTemplate>
                                            <div class="col-md-4">
                                                <div class="card border-dark" style="margin: 5px;">
                                                    <a href="utilizadores_detalhe.aspx?cod_user=<%# Eval("cod_user") %>" style="text-decoration: none;">
                                                        <div class="card-body">
                                                            <h5 class="card-title"><b><%# Eval("nome_proprio") + " " + Eval("apelido") %></b></h5>
                                                            <p class="card-text" style="color: black;">Turma: <span style="color: orangered"><%# Eval("nome_turma") %></span></p>
                                                            <p class="card-text" style="color: black;">Curso: <span style="color: orangered"><%# Eval("nome_curso") %></span></p>
                                                            <p class="card-text" style="color: black;">Regime: <span style="color: orangered"><%# Eval("regime") %></span></p>
                                                            <p class="card-text" style="color: black;">Estado Matrícula: <span style="color: orangered"><%# Eval("situacao") %></span></p>
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
