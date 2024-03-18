<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="personal_zone_inscricoes.aspx.cs" Inherits="Projeto_Final.personal_zone_inscricoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="row" style="margin-top: 30px; margin-bottom: 100px;">
            <!-- Sidebar -->
            <div class="col-md-2 bg-light" style="margin-top: 10px;">
                <div class="list-group">
                    <a href="personal_zone.aspx" class="list-group-item list-group-item-action">Área Pessoal</a>
                    <a href="personal_zone_inscricoes.aspx" class="list-group-item list-group-item-action active">Minhas Inscrições</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeUsernameModal">Mudar o Username</a>
                    <% if (Session["googlefb_log"] != "yes")
                        { %>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changePasswordModal">Mudar a Password</a>
                    <a class="list-group-item list-group-item-action" data-toggle="modal" data-target="#changeEmailModal">Mudar o Email</a>
                    <% } %>
                    <% if (Session["perfil"].ToString().Contains("Staff") || Session["perfil"].ToString().Contains("Super Admin"))
                        { %>
                    <a href="dados_estatisticos.aspx" class="list-group-item list-group-item-action">Dados Estatísticos</a>
                    <a href="gestao.aspx" class="list-group-item list-group-item-action">Gestão</a>
                    <% } %>
                    <asp:Button ID="btn_logout2" class="list-group-item list-group-item-action" runat="server" Text="Terminar Sessão" OnClick="btn_logout2_Click" />
                </div>
            </div>
            <div class="container">
                <div class="row" style="margin-top: 10px; margin-bottom: 20px;">
                    <div class="col-md-12">
                        <%-- Panel for Formador or Super Admin --%>
                        <asp:Panel ID="pnlFormador" runat="server" Visible='<%# Session["perfil"].ToString().Contains("Formador") || Session["perfil"].ToString().Contains("Super Admin") %>'>
                            <div class="card" style="border-color: #333;">
                                <div class="card-header bg-dark text-white">
                                    <h2 class="display-4" style="font-size: 40px; color: white;">Formador</h2>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Repeater ID="rpt_formador" runat="server">
                                                <HeaderTemplate>
                                                    <h4>Inscrições</h4>
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Código UFCD</th>
                                                                <th>Nome da UFCD</th>
                                                                <th>Turma</th>
                                                                <th>Avaliações</th>
                                                            </tr>
                                                        </thead>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tbody>
                                                        <%-- Use data binding to populate table rows with inscrições data --%>
                                                    </tbody>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <%-- Panel for Formando or Super Admin --%>
                        <asp:Panel ID="pnlFormando" runat="server" Visible='<%# Session["perfil"].ToString().Contains("Formando") || Session["perfil"].ToString().Contains("Super Admin") %>'>
                            <div class="card" style="border-color: #333;">
                                <div class="card-header bg-dark text-white">
                                    <h2 class="display-4" style="font-size: 40px; color: white;">Formando</h2>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Repeater ID="rpt_formando" runat="server">
                                                <HeaderTemplate>
                                                    <h4>Inscrições</h4>
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Código Qualificação</th>
                                                                <th>Nome do Curso</th>
                                                                <th>Estado Inscrição</th>
                                                                <th>Avaliações</th>
                                                            </tr>
                                                        </thead>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tbody>
                                                        <%-- Use data binding to populate table rows with inscrições data --%>
                                                    </tbody>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Modals -->
    <!-- Modal for changing the password -->
    <div class="modal" id="changePasswordModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Password</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="changePasswordForm" runat="server">
                        <div class="form-group">
                            <label for="tb_pw">Password atual</label>
                            <asp:TextBox ID="tb_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw">Nova Password</label>
                            <asp:TextBox ID="tb_new_pw" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="rev_pw_nova" runat="server" ErrorMessage="Invalid Password" Text="*" ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{9,20}$" ControlToValidate="tb_new_pw"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw_repeat">Confirmar Nova Password</label>
                            <asp:TextBox ID="tb_new_pw_repeat" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_pw" class="btn btn-primary" runat="server" Text="Mudar Password" OnClick="btn_change_pw_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for changing the username -->
    <div class="modal" id="changeUsernameModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Username</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="Div1" runat="server">
                        <div class="form-group">
                            <label for="tb_pw">Novo Username</label>
                            <asp:TextBox ID="tb_new_username" class="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="tb_new_pw">Password</label>
                            <asp:TextBox ID="tb_pw_username" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_username" class="btn btn-primary" runat="server" Text="Mudar Username" OnClick="btn_change_username_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal for changing the email -->
    <div class="modal" id="changeEmailModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Mudar Email</h4>
                    <button type="button" class="close" data-dismiss="modal">×</button>
                </div>
                <div class="modal-body">
                    <div id="Div2" runat="server">
                        <div class="form-group">
                            <label for="tb_email">Novo Email</label>
                            <asp:TextBox ID="tb_new_email" class="form-control" runat="server" TextMode="SingleLine"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="rfv_new_email" runat="server" ErrorMessage="New email not inserted correctly" Text="*" ControlToValidate="tb_new_email" ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <label for="tb_pw_email">Password</label>
                            <asp:TextBox ID="tb_pw_email" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_change_email" class="btn btn-primary" runat="server" Text="Mudar Email" OnClick="btn_change_email_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
