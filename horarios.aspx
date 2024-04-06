<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="horarios.aspx.cs" Inherits="Projeto_Final.horarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div id="filterForm" runat="server" class="border-dark" style="display: none; margin-bottom: 10px; margin-top: 10px; background-color: antiquewhite; border: 1px solid #ccc; padding: 10px;">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Nome Turma:</label>
                                <asp:TextBox ID="tb_nome_turma" CssClass="form-control" runat="server" Style="margin-left: 5px;" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Código Qualificação:</label>
                                <asp:TextBox ID="tb_cod_qualificacao" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Number"></asp:TextBox>
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
                                <label>Início do Intervalo do Início do Curso:</label>
                                <asp:TextBox ID="tb_data_inicio_inicio" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de início"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Fim do Intervalo do Início do Curso:</label>
                                <asp:TextBox ID="tb_data_fim_inicio" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de fim"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Início do Intervalo do Fim do Curso:</label>
                                <asp:TextBox ID="tb_data_inicio_fim" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de início"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Fim do Intervalo do Fim do Curso:</label>
                                <asp:TextBox ID="tb_data_fim_fim" CssClass="form-control" runat="server" Style="margin-left: 5px;" TextMode="Date" placeholder="Data de fim"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Área:</label>
                                        <asp:DropDownList ID="ddl_area" CssClass="form-control" AppendDataBoundItems="true" runat="server" Style="margin-left: 5px;" DataSourceID="areas" DataTextField="area" DataValueField="cod_area"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="areas" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Areas]"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ordenação Nome Turma:</label>
                                        <asp:DropDownList ID="ddl_sort_nome_turma" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">A-Z</asp:ListItem>
                                            <asp:ListItem Value="desc">Z-A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Ordenação Código Qualificação:</label>
                                        <asp:DropDownList ID="ddl_sort_cod_qualificacao" CssClass="form-control" runat="server" Style="margin-left: 5px;">
                                            <asp:ListItem>Nenhuma</asp:ListItem>
                                            <asp:ListItem Value="asc">Ascendente</asp:ListItem>
                                            <asp:ListItem Value="desc">Descendente</asp:ListItem>
                                        </asp:DropDownList>
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
                                        <label>Estado:</label>
                                        <asp:DropDownList ID="ddl_estado" CssClass="form-control" runat="server" AppendDataBoundItems="true" DataSourceID="estados" DataTextField="turma_estado" DataValueField="cod_turmas_estado"></asp:DropDownList>
                                        <asp:SqlDataSource runat="server" ID="estados" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT * FROM [Turmas_Estado]"></asp:SqlDataSource>
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
                    <div class="col-md-2 bg-light" style="margin-top: 10px;">
                        <div class="list-group">
                            <a href="#" class="list-group-item list-group-item-action active">Horários</a>
                            <a href="criar_horario.aspx" class="list-group-item list-group-item-action">Criar Horário</a>
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
                        <div id="card_horarios" class="card" style="border-color: #333; background-color: antiquewhite; margin-top: 10px;" runat="server">
                            <div class="card-header bg-dark text-white">
                                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Consultar Horários</h2>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_horarios_top" CssClass="btn btn-light border-dark m-1" runat="server" OnClick="btn_previous_horarios_Click"><i class="fas fa-arrow-left"></i></asp:LinkButton>
                                <asp:LinkButton ID="btn_next_horarios_top" runat="server" CssClass="btn btn-light border-dark m-1" OnClick="btn_next_horarios_Click"><i class="fas fa-arrow-right"></i></asp:LinkButton>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-bordered bg-light">
                                                <thead class="bg-dark" style="color: white;">
                                                    <tr>
                                                        <th>Turma</th>
                                                        <th>Curso</th>
                                                        <th>Regime</th>
                                                        <th>Início</th>
                                                        <th>Estado</th>
                                                        <th>Horário</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpt_horarios" runat="server" OnItemDataBound="rpt_horarios_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("nome_turma") %></a></td>
                                                                <td><%# Eval("nome_curso") %></td>
                                                                <td><%# Eval("regime") %></td>
                                                                <td><%# Eval("data_inicio", "{0:dd/MM/yyyy}") %></td>
                                                                <td><%# Eval("estado") %></td>
                                                                <td>
                                                                    <asp:LinkButton ID="lb_horarios" runat="server" Style="margin-right: 5px;" OnClick="lb_horarios_Click"><i class="fas fa-calendar fa-lg"></i></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="cod-md-12" style="margin: 13px;">
                                        <asp:Label ID="lbl_mensagem_formadores" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex justify-content-center" causesvalidation="true">
                                <asp:LinkButton ID="btn_previous_horarios" CssClass="btn btn-light border-dark m-1" runat="server" OnClick="btn_previous_horarios_Click">
                <i class="fas fa-arrow-left"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_next_horarios" runat="server" CssClass="btn btn-light border-dark m-1" OnClick="btn_next_horarios_Click">
                <i class="fas fa-arrow-right"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin: 13px;">
                        <asp:Label ID="lbl_mensagem" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
