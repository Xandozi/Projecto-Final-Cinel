<%@ Page Title="CINEL - Editar Horário" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_horario.aspx.cs" Inherits="Projeto_Final.editar_horario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card bg-secondary" style="border-color: #333; color:black; margin-top: 30px; margin-bottom: 30px;">
            <div class="card-header bg-dark text-white">
                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Horário da Turma
                    <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label></h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <label class="col-form-label">Módulo</label>
                        <asp:DropDownList ID="ddl_modulo" AutoPostBack="true" CssClass="form-control" runat="server" DataSourceID="modulos" DataTextField="nome_modulo" DataValueField="cod_modulo"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="modulos" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_modulo], [nome_modulo], [cod_ufcd] FROM [Modulos]"></asp:SqlDataSource>
                        <asp:HiddenField ID="hf_duracao_modulo" runat="server" />
                        <asp:HiddenField ID="hf_data_inicio" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <label class="col-form-label">Horas do Módulo Alocadas</label>
                        <label id="lbl_horas_modulo" class="form-control">Alocação de horas do módulo</label>
                    </div>
                    <div class="col-md-2">
                        <label class="col-form-label">Formador</label>
                        <asp:Label ID="lbl_nome_formador" CssClass="form-control" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hf_cod_formador" runat="server" />
                        <asp:HiddenField ID="hf_cod_user" runat="server" />
                        <asp:HiddenField ID="hf_regime" runat="server" />
                    </div>
                    <div class="col-md-2">
                        <label class="col-form-label">Sala</label>
                        <asp:DropDownList ID="ddl_sala" AutoPostBack="true" CssClass="form-control" runat="server" DataSourceID="salas" DataTextField="nome_sala" DataValueField="cod_sala"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="salas" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_sala], [nome_sala], [ativo] FROM [Salas]"></asp:SqlDataSource>
                        <asp:HiddenField ID="hf_cod_sala" runat="server" />
                        <asp:HiddenField ID="hf_cod_turma" runat="server" />
                    </div>
                    <div class="col-md-1">
                        <label class="col-form-label">Cor do Evento</label>
                        <input class="form-control" type="color" id="colorPicker" value="000000">
                    </div>
                    <div class="col-md-3">
                        <label id="lbl_mensagem" style="margin-top: 20px;"></label>
                    </div>
                </div>
                <div id='calendar' class="bg-light border-dark" style="padding: 10px; margin-top: 30px; margin-bottom: 10px;"></div>
            </div>
        </div>
        <div class="row justify-content-between" style="margin: 10px;">
            <button class="btn btn-success" id="btn_SaveSelectedSlots">Submeter Horário</button>
            <a href="horarios.aspx" class="btn btn-info">Voltar para a página Horários</a>
        </div>
    </div>
    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
    <script>
        // Instancializar a variável que recebe o cod_turma
        var cod_turma_valor;

        // Função para extrair um determinado valor do URL da página
        function get_parametro_url(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        // Função para determinar o valor de cod_turma
        function set_cod_turma(cod_turma) {
            cod_turma_valor = cod_turma;
        }

        // Extrair e definir o cod_turma
        var cod_turma_url = get_parametro_url('cod_turma');
        set_cod_turma(cod_turma_url);

        // EventListener do page_load
        document.addEventListener('DOMContentLoaded', function () {
            var Aulas_Turma = []; // Array para guardar as aulas da turma
            var Sundays_Holidays = []; // Array para guardar domingos e feriados
            var Disponibilidade_Formador = []; // Array para guardar a disponibilidade do formador
            var Disponibilidade_Sala = []; // Array para guardar a disponibilidade das salas
            var min_duracao_aula = 60 * 60 * 1000; // Duração mínima da aula (1h)
            var ano_atual = new Date().getFullYear();
            var dia_atual = new Date();
            var ano_atual = dia_atual.getFullYear(); // Variável para guardar o ano atual

            // Ir buscar os valores que estão nas DDLs e HiddenFields
            var nome_modulo = $('#<%= ddl_modulo.ClientID %> option:selected').text();
            var ddl_cod_modulo = $('#<%= ddl_modulo.ClientID %> option:selected').val();
            var total_max_horas_modulo = $('#<%= hf_duracao_modulo.ClientID %>').val();
            var nome_formador = $('#<%= lbl_nome_formador.ClientID %>').text();
            var ddl_cod_formador = $('#<%= hf_cod_formador.ClientID %>').val();
            var nome_sala = $('#<%= ddl_sala.ClientID %> option:selected').text();
            var ddl_cod_sala = $('#<%= ddl_sala.ClientID %> option:selected').val();
            var nome_turma = $('#<%= lbl_nome_turma.ClientID %>').text();
            var ddl_cod_turma = $('#<%= hf_cod_turma.ClientID %>').val();

            // Função para determinar se o event seleccionado tem a duração mínima da aula
            function isDuracao_Valida(info) {
                var duracao_slot = info.end.getTime() - info.start.getTime();
                return duracao_slot >= min_duracao_aula && info.start.getMinutes() === 0 && info.end.getMinutes() === 0;
            }

            // Definir o número de anos que o calendário terá
            var total_anos_calendario = 3;

            // Inicializar array para colocar os feriados manualmente
            var feriados = [];

            // Loop through each year
            for (var i = 0; i < total_anos_calendario; i++) {
                // Ano a inserir baseado no ano atual
                var year = ano_atual + i;

                // Adicionar os feriados ao array
                feriados.push(
                    {
                        title: 'Ano Novo',
                        start: year + '-01-01T08:00:00',
                        end: year + '-01-01T23:00:00',
                    },
                    {
                        title: 'Dia da Liberdade',
                        start: year + '-04-25T08:00:00',
                        end: year + '-04-25T23:00:00',
                    },
                    {
                        title: 'Dia do Trabalhador',
                        start: year + '-05-01T08:00:00',
                        end: year + '-05-01T23:00:00',
                    },
                    {
                        title: 'Dia de Portugal',
                        start: year + '-06-10T08:00:00',
                        end: year + '-06-10T23:00:00',
                    },
                    {
                        title: 'Assunção de Nossa Senhora',
                        start: year + '-08-15T08:00:00',
                        end: year + '-08-15T23:00:00',
                    },
                    {
                        title: 'Implantação da República',
                        start: year + '-10-05T08:00:00',
                        end: year + '-10-05T23:00:00',
                    },
                    {
                        title: 'Dia de Todos os Santos',
                        start: year + '-11-01T08:00:00',
                        end: year + '-11-01T23:00:00',
                    },
                    {
                        title: 'Restauração da Independência',
                        start: year + '-12-01T08:00:00',
                        end: year + '-12-01T23:00:00',
                    },
                    {
                        title: 'Natal',
                        start: year + '-12-25T08:00:00',
                        end: year + '-12-25T23:00:00',
                    },
                    {
                        title: 'Véspera de Natal',
                        start: year + '-12-24T08:00:00',
                        end: year + '-12-24T23:00:00',
                    }
                );
            }

            // Função para calcular as horas que já estão inseridas nas Aulas_Turma com o cod_modulo = moduloId
            function calcular_total_horas_modulo(moduloId) {
                let totalHours = 0;
                Aulas_Turma.forEach(function (event) {
                    if (event.cod_modulo == moduloId) {
                        totalHours += Math.abs(new Date(event.end) - new Date(event.start)) / 36e5;
                    }
                });
                return totalHours;
            }

            // Função para dar update a lbl_horas_modulo com as horas totais do modulo na turma
            function update_horas_modulo() {
                var total_horas_modulo = calcular_total_horas_modulo(ddl_cod_modulo);
                $('#lbl_horas_modulo').text(`${total_horas_modulo} horas de ${total_max_horas_modulo} horas`);
                if (total_horas_modulo == total_max_horas_modulo) {
                    $('#lbl_horas_modulo').addClass('form-control border-danger');
                }
                else {
                    $('#lbl_horas_modulo').removeClass('form-control border-danger');
                    $('#lbl_horas_modulo').addClass('form-control');
                }
            }

            // Chamar a função anterior quando a página for carregada.
            $(document).ready(function () {
                update_horas_modulo();
            });

            // Função para formatar datas YYYY-MM-DDTHH:MM:SS
            function formatar_data_iso_string(dateString) {
                var date = new Date(dateString);
                var utcString = date.toUTCString();
                var isoString = new Date(utcString).toISOString();
                return isoString.slice(0, 10) + 'T' + isoString.slice(11, 19);
            }

            // Função para adicionar os feriados ao array Sundays_Holidays
            function add_feriados_sundays_holidays() {
                feriados.forEach(function (holiday) {
                    Sundays_Holidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#ff0000',
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Função para adicionar o que vem da BD referente à disponibilidade do formador ao array Disponibilidade_Formador
            function add_disponibilidadeDB_Disponibilidade_Formador(eventData) {
                eventData.forEach(function (event) {
                    Disponibilidade_Formador.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: '#ff0000',
                        cod_turma: event.cod_turma,
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Função para adicionar o que vem da BD referente à disponibilidade das salas ao array Disponibilidade_Salas
            function add_disponibilidadeSalasDB_Disponibilidade_Salas(eventData) {
                eventData.forEach(function (event) {
                    Disponibilidade_Sala.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: '#ff0000',
                        cod_turma: event.cod_turma,
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Função para adicionar o que vem da DB referente às aulas da turma ao array Aulas_Turma
            function add_aulasDB_Aulas_Turma(eventData) {
                eventData.forEach(function (event) {
                    Aulas_Turma.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        cod_modulo: event.cod_modulo,
                        cod_formador: event.cod_formador,
                        cod_sala: event.cod_sala,
                        color: event.color,
                        cod_turma: event.cod_turma,
                        dataType: 'unselectable'
                    });
                });

                update_horas_modulo();
            }

            // Chamada á base de dados para ir buscar os dados da disponibilidade do formador
            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetDisponibilidade_JSON", // Método do WebService utilizado para ir buscar os dados á BD
                data: JSON.stringify({ cod_user: $('#<%= hf_cod_user.ClientID %>').val() }), // Parâmetro passado no método
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extração do array baseado no ficheiro JSON

                    add_disponibilidadeDB_Disponibilidade_Formador(eventData); // Execução da função para inserir os dados do eventData para dentro de um array utilizado na renderização do calendário

                    // Renderizar o calendário
                    Render_Calendario();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Erro ao extrair dados da BD. WebService");
                    // Se a chamada á BD falhar, adicionar á mesma os domingos e feriados ao array Sundays_Holidays
                    add_feriados_sundays_holidays();

                    // Renderizar o calendário
                    Render_Calendario();
                }
            });

            // Chamada á base de dados para ir buscar os dados da disponibilidade das salas
            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetDisponibilidade_Sala_JSON", // Método do WebService utilizado para ir buscar os dados á BD
                data: JSON.stringify({ cod_sala: $('#<%= hf_cod_sala.ClientID %>').val() }), // Parâmetro passado no método
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extração do array baseado no ficheiro JSON

                    add_disponibilidadeSalasDB_Disponibilidade_Salas(eventData); // Execução da função para inserir os dados do eventData para dentro de um array utilizado na renderização do calendário

                    // Renderizar o calendário
                    Render_Calendario();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Erro ao extrair dados da BD. WebService");
                    // Se a chamada á BD falhar, adicionar á mesma os domingos e feriados ao array Sundays_Holidays
                    add_feriados_sundays_holidays();

                    // Renderizar o calendário
                    Render_Calendario();
                }
            });

            // Chamada á base de dados para ir buscar os dados do horário da turma
            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetHorarios_JSON", // Método do WebService utilizado para ir buscar os dados á BD
                data: JSON.stringify({ cod_turma: cod_turma_valor }), // Parâmetro passado no método
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extração do array baseado no ficheiro JSON

                    add_aulasDB_Aulas_Turma(eventData); // Execução da função para inserir os dados do eventData para dentro de um array utilizado na renderização do calendário
                    add_feriados_sundays_holidays(); // Execução da função para inserir os dados referentes aos domingos e feriados

                    // Renderizar o calendário
                    Render_Calendario();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Erro ao extrair dados da BD. WebService");
                    // Se a chamada á BD falhar, adicionar á mesma os domingos e feriados ao array Sundays_Holidays
                    add_feriados_sundays_holidays();
                    // Renderizar o calendário
                    Render_Calendario();
                }
            });

            function Render_Calendario() {
                var calendarEl = document.getElementById('calendar');
                var calendar = new FullCalendar.Calendar(calendarEl, {
                    locale: 'pt',
                    hiddenDays: [0],
                    initialView: 'timeGridWeek',
                    headerToolbar: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'dayGridMonth,timeGridWeek,timeGridDay'
                    },
                    buttonText: {
                        today: 'Hoje',
                        timeGridDay: 'Dia',
                        dayGridMonth: 'Mês',
                        timeGridWeek: 'Semana'
                    },
                    slotLabelFormat: {
                        hour: '2-digit',
                        minute: '2-digit',
                        hour12: false
                    },
                    firstDay: 1,
                    slotMinTime: '08:00:00',
                    slotMaxTime: '23:00:00',
                    allDaySlot: true,
                    selectable: true,
                    timeZone: 'UTC',
                    select: function (info) {
                        var isAllDay = info.allDay;
                        var dia_atual = new Date(); // Get data atual

                        // Dar check a ver se a data seleccionada é inferior ao dia atual
                        if (info.start < dia_atual) {
                            // Mostrar mensagem de erro
                            $('#lbl_mensagem').text("Só pode criar eventos a partir da data atual.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                              return;
                        }

                        // Formatação correta da string de comparação do evento seleccionado
                        var data_evento_seleccionado = new Date(info.start);
                        var year = data_evento_seleccionado.getFullYear();
                        var month = ('0' + (data_evento_seleccionado.getMonth() + 1)).slice(-2);
                        var day = ('0' + data_evento_seleccionado.getDate()).slice(-2);
                        var data_formatada = year + '-' + month + '-' + day;

                        // Ir buscar o valor dentro do hidden field referente à data de início da turma
                        var hf_valor_data_inicio = document.getElementById('<%= hf_data_inicio.ClientID %>').value;

                        // Condição para ver se a data seleccionada é menor que a data de início da turma
                        if (data_formatada < hf_valor_data_inicio) {
                            // Mostrar mensagem de erro
                            $('#lbl_mensagem').text("Só pode criar eventos a partir da data de início da turma.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                            return;
                        }

                        // Determinar os slots visiveis do dia e semana de acordo com o regime que a turma tiver
                        var regime = $('#<%= hf_regime.ClientID %>').val();
                        var primeiro_slot = regime === 'Laboral' ? '08:00:00' : '16:00:00';
                        var ultimo_slot = regime === 'Laboral' ? '16:00:00' : '23:00:00';

                        if (isAllDay) {
                            // Se for um evento diário, colocar as horas de inicio e fim de acordo com o regime
                            var data_seleccionada_inicio = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), primeiro_slot.split(':')[0], primeiro_slot.split(':')[1], 0));
                            var data_seleccionada_fim = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), ultimo_slot.split(':')[0], ultimo_slot.split(':')[1], 0));

                            info.start = data_seleccionada_inicio.toISOString();
                            info.end = data_seleccionada_fim.toISOString();

                            info.start = new Date(data_seleccionada_inicio);
                            info.end = new Date(data_seleccionada_fim);
                        }

                        if (!isDuracao_Valida(info)) {
                            // Mostrar mensagem de erro
                            $('#lbl_mensagem').text("Por favor introduza um evento de pelo menos 1h e que comece á hora certa.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                            return;
                        }

                        // Dar check se o evento seleccionado entra em conflicto com outro evento dentro da Disponibilidade_Formador, Sundays_Holidays ou Disponibilidade_Sala
                        if (isEventConflict(info, Disponibilidade_Formador) || isEventConflict(info, Sundays_Holidays) || isEventConflict(info, Disponibilidade_Sala)) {
                            // Mostrar mensagem de erro
                            $('#lbl_mensagem').text("O evento escolhido entra em conflito com outro evento.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                            return;
                        }

                        // Calcular o total de horas do módulo seleccionado
                        var total_horas_modulo = calcular_total_horas_modulo(ddl_cod_modulo);

                        // Duração do evento seleccionado
                        var duracao_evento_seleccionado = Math.abs(new Date(info.end) - new Date(info.start)) / 36e5;

                        // Check se o total de horas inseridas nas aulas mais a duração do evento seleccionado se ultrapassa o máximo de horas do módulo
                        if (total_horas_modulo + duracao_evento_seleccionado > total_max_horas_modulo) {
                            // Mostrar mensagem de erro
                            $('#lbl_mensagem').text("Adicionar este evento excederá o total de horas permitidas para o módulo selecionado.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                            return;
                        }

                        // Guardar a cor seleccionada pelo utilizador
                        var selectedColor = $('#colorPicker').val();

                        // Titulo do evento criado que consiste no nome da turma, nome do modulo, nome do formador e o nome da sala
                        var titulo_aula = nome_turma + " | " + nome_modulo + " | " + nome_formador + " | " + nome_sala;

                        var eventToAdd = {
                            title: titulo_aula,
                            start: formatar_data_iso_string(info.start),
                            end: formatar_data_iso_string(info.end),
                            rendering: 'background',
                            cod_modulo: ddl_cod_modulo,
                            cod_formador: ddl_cod_formador,
                            cod_sala: ddl_cod_sala,
                            color: selectedColor,
                            cod_turma: ddl_cod_turma,
                            dataType: 'unselectable'
                        };

                        // Adicionar o eventToAdd ao array das aulas da turma
                        Aulas_Turma.push(eventToAdd);

                        // Adicionar o eventToAdd ao calendário
                        calendar.addEvent(eventToAdd);

                        // Assegurar que cada var dentro do array Aulas_Turma tem os dados necessários antes de guardar.
                        var formattedSlots = Aulas_Turma.map(function (slot) {
                            return {
                                title: slot.title,
                                start: slot.start,
                                end: slot.end,
                                cod_modulo: slot.cod_modulo,
                                cod_formador: slot.cod_formador,
                                cod_sala: slot.cod_sala,
                                color: slot.color
                            };
                        });

                        // Chamada á base de dados para ir buscar os dados do horário da turma
                        $.ajax({
                            type: "POST",
                            url: "editar_horario.aspx/Gravar_Horario_Turma",
                            data: JSON.stringify({ Aulas_Turma: formattedSlots, cod_turma: cod_turma_valor, cod_formador: ddl_cod_formador, cod_sala: ddl_cod_sala }),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response.d) {
                                    $('#lbl_mensagem').text("Aula inserida com sucesso!");
                                    $('#lbl_mensagem').addClass('alert alert-success');
                                    $('#lbl_mensagem').fadeIn();

                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success');
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    $('#lbl_mensagem').text("Ocorreu um erro ao guardar a aula inserida.");
                                    $('#lbl_mensagem').addClass('alert alert-danger');
                                    $('#lbl_mensagem').fadeIn();

                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger');
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                $('#lbl_mensagem').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger');
                                $('#lbl_mensagem').fadeIn();

                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger');
                                    });
                                }, 3000);

                                return;
                            }
                        });

                        update_horas_modulo();

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: dia_atual,
                    validRange: {
                        start: ano_atual + '-01-01',
                        end: (ano_atual + 2) + '-01-01'
                    }
                });

                // Função para determinar se há conflicto entre um evento seleccionado e os eventos dentro de um array
                function isEventConflict(selectedEvent, eventsArray) {
                    for (var i = 0; i < eventsArray.length; i++) {
                        var event = eventsArray[i];
                        var selectedEventStartString = selectedEvent.start.toISOString().slice(0, -5);
                        var selectedEventEndString = selectedEvent.end.toISOString().slice(0, -5);
                        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                            return true; // Conflicto encontrado
                        }
                    }
                    return false; // Não foi encontrado conflicto
                }

                // Função para dar update ao calendário de acordo com o valor dos hf_regime
                function updateSlotTimes(regime) {
                    if (regime === 'Laboral') {
                        calendar.setOption('slotMinTime', '08:00:00');
                        calendar.setOption('slotMaxTime', '16:00:00');
                    } else if (regime === 'Pós-Laboral') {
                        calendar.setOption('slotMinTime', '16:00:00');
                        calendar.setOption('slotMaxTime', '23:00:00');
                    }
                }

                updateSlotTimes($('#<%= hf_regime.ClientID %>').val());

                $('#<%= hf_regime.ClientID %>').change(function () {
                    updateSlotTimes($(this).val());
                });

                // EventListener para quando o utilizador clicar num evento (neste caso para o remover do calendário)
                calendar.setOption('eventClick', function (info) {
                    // Checkar se o DataType do evento seleccionado é unselectable
                    if (info.event.extendedProps.dataType === 'unselectable') {

                        var start = info.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        var end = info.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        // Remover o evento do array Aulas_Turma
                        Aulas_Turma = Aulas_Turma.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        // Remover o evento do array Disponibilidade_Formador
                        Disponibilidade_Formador = Disponibilidade_Formador.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remover o evento do array Disponibilidade_Salas
                        Disponibilidade_Sala = Disponibilidade_Sala.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remover o evento do calendário
                        info.event.remove();

                        // Assegurar que o Aulas_Turma está bem estruturado
                        var formattedSlots = Aulas_Turma.map(function (slot) {
                            return {
                                title: slot.title,
                                start: slot.start,
                                end: slot.end,
                                cod_modulo: slot.cod_modulo,
                                cod_formador: slot.cod_formador,
                                cod_sala: slot.cod_sala,
                                color: slot.color
                            };
                        });

                        $.ajax({
                            type: "POST",
                            url: "editar_horario.aspx/Gravar_Horario_Turma",
                            data: JSON.stringify({ Aulas_Turma: formattedSlots, cod_turma: cod_turma_valor, cod_formador: ddl_cod_formador, cod_sala: ddl_cod_sala }), 
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response.d) {
                                    $('#lbl_mensagem').text("Aula removida e horário guardado com sucesso.");
                                    $('#lbl_mensagem').addClass('alert alert-success');
                                    $('#lbl_mensagem').fadeIn();

                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success');
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    $('#lbl_mensagem').text("Erro ao guardar o horário na base de dados.");
                                    $('#lbl_mensagem').addClass('alert alert-danger');
                                    $('#lbl_mensagem').fadeIn();

                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger');
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger');
                                $('#lbl_mensagem').fadeIn();

                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger');
                                    });
                                }, 3000);

                                return;
                            }
                        });

                        update_horas_modulo();

                    } else {
                        $('#lbl_mensagem').text("Não pode remover este evento.");
                        $('#lbl_mensagem').addClass('alert alert-danger');
                        $('#lbl_mensagem').fadeIn();

                        setTimeout(function () {
                            $('#lbl_mensagem').fadeOut(function () {
                                $(this).removeClass('alert alert-danger');
                            });
                        }, 3000);

                        return;
                    }
                });

                // Inserir todos os eventos dentro do array Aulas_Turma ao calendário
                Aulas_Turma.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'unselectable'
                    });
                });

                // Inserir todos os eventos dentro do array Disponibilidade_Formador ao calendário dando primeiro check a ver se já existe uma aula da turma inserida primeiro
                Disponibilidade_Formador.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatar_data_iso_string(event.start) == slot.start && formatar_data_iso_string(event.end) == slot.end;
                    });

                    if (!existingEvent) {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            color: slot.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });

                // Inserir todos os eventos dentro do array Disponibilidade_Salas ao calendário dando primeiro check a ver se já existe uma aula da turma inserida primeiro
                Disponibilidade_Sala.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatar_data_iso_string(event.start) == slot.start && formatar_data_iso_string(event.end) == slot.end;
                    });

                    if (!existingEvent) {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            color: slot.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });

                // Inserir os feriados no calendário
                Sundays_Holidays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'non_unselectable'
                    });
                });

                calendar.render();

                // EventListener do botão btn_saveselectedslots
                document.getElementById('btn_SaveSelectedSlots').addEventListener('click', function (event) {
                    var formattedSlots = Aulas_Turma.map(function (slot) {
                        return {
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            cod_modulo: slot.cod_modulo,
                            cod_formador: slot.cod_formador,
                            cod_sala: slot.cod_sala,
                            color: slot.color
                        };
                    });

                    $.ajax({
                        type: "POST",
                        url: "editar_horario.aspx/Gravar_Horario_Turma",
                        data: JSON.stringify({ Aulas_Turma: formattedSlots, cod_turma: cod_turma_valor, cod_formador: ddl_cod_formador, cod_sala: ddl_cod_sala }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d) {
                                $('#lbl_mensagem').text("Horário guardado com sucesso!");
                                $('#lbl_mensagem').addClass('alert alert-success');
                                $('#lbl_mensagem').fadeIn();

                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-success');
                                    });
                                }, 3000);

                                return;
                            } else {
                                $('#lbl_mensagem').text("Erro ao guardar o horário da turma.");
                                $('#lbl_mensagem').addClass('alert alert-danger');
                                $('#lbl_mensagem').fadeIn();

                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger');
                                    });
                                }, 3000);

                                return;
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                            $('#lbl_mensagem').addClass('alert alert-danger');
                            $('#lbl_mensagem').fadeIn();

                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger');
                                });
                            }, 3000);

                            return;
                        }
                    });
                    event.preventDefault();
                });

            }
        });

    </script>
</asp:Content>
