<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="editar_horario.aspx.cs" Inherits="Projeto_Final.editar_horario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card" style="border-color: #333; background-color: antiquewhite; margin-top: 30px; margin-bottom: 30px;">
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
                    <div class="col-md-1">
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
                    <div class="col-md-4">
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
        // Define the codUserValue variable
        var codTurmaValue;

        // Function to parse the URL and extract the value of the cod_user variable
        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        // Function to set the cod_user value received from server-side
        function setCodTurma(cod_turma) {
            codTurmaValue = cod_turma;
        }

        // Call setCodUser function with the value of the cod_user URL variable
        var codTurmaFromUrl = getUrlParameter('cod_turma');
        setCodTurma(codTurmaFromUrl);

        // Ensure the script runs after the DOM is fully loaded
        document.addEventListener('DOMContentLoaded', function () {
            console.log("DOMContentLoaded event fired."); // Log DOMContentLoaded event

            var selectedSlots = []; // Array to store selected time slots
            var Sundays_Holidays = []; // Array to store holidays and sundays
            var Disponibilidade_Formador = []; // Array to store availability of trainer
            var Disponibilidade_Sala = []; // Array to store availability of classrooms
            var MIN_SLOT_DURATION = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year
            var currentDate = new Date();

            // Extract the year, month, and day
            var currentYear = currentDate.getFullYear();

            // Get the selected values from dropdown lists
            var selectedModulo = $('#<%= ddl_modulo.ClientID %> option:selected').text();
            var selectedModuloValue = $('#<%= ddl_modulo.ClientID %> option:selected').val();
            var totalHoursAllowed = $('#<%= hf_duracao_modulo.ClientID %>').val();
            var selectedFormador = $('#<%= lbl_nome_formador.ClientID %>').text();
            var selectedFormadorValue = $('#<%= hf_cod_formador.ClientID %>').val();
            var selectedSala = $('#<%= ddl_sala.ClientID %> option:selected').text();
            var selectedSalaValue = $('#<%= ddl_sala.ClientID %> option:selected').val();
            var nomeTurma = $('#<%= lbl_nome_turma.ClientID %>').text();
            var codTurma = $('#<%= hf_cod_turma.ClientID %>').val();

            // Function to check if the selected slot duration meets the minimum requirement and starts and ends on the hour
            function isSlotDurationValid(info) {
                var slotDuration = info.end.getTime() - info.start.getTime();
                return slotDuration >= MIN_SLOT_DURATION && info.start.getMinutes() === 0 && info.end.getMinutes() === 0;
            }

            // Define the number of years to include (current year + next two years)
            var numberOfYears = 3;

            // Initialize an empty array to store holidays
            var holidays = [];

            // Loop through each year
            for (var i = 0; i < numberOfYears; i++) {
                // Calculate the year based on the current year and the loop index
                var year = currentYear + i;

                // Add events for national holidays for each year
                holidays.push(
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

            // Function to calculate total hours of a modulo in selectedSlots array
            function calculateTotalHoursOfModulo(moduloId) {
                let totalHours = 0;
                selectedSlots.forEach(function (event) {
                    if (event.cod_modulo == moduloId) {
                        totalHours += Math.abs(new Date(event.end) - new Date(event.start)) / 36e5;
                    }
                });
                return totalHours;
            }

            // Function to update lbl_horas_modulo with total hours information
            function updateHorasModulo() {
                var totalHoursOfModulo = calculateTotalHoursOfModulo(selectedModuloValue);
                $('#lbl_horas_modulo').text(`${totalHoursOfModulo} horas de ${totalHoursAllowed} horas`);
                if (totalHoursOfModulo == totalHoursAllowed) {
                    $('#lbl_horas_modulo').addClass('form-control border-danger');
                }
                else {
                    $('#lbl_horas_modulo').removeClass('form-control border-danger');
                    $('#lbl_horas_modulo').addClass('form-control');
                }
            }

            // Call updateHorasModulo() when the page is loaded
            $(document).ready(function () {
                updateHorasModulo();
            });

            function formatDateToISOString(dateString) {
                var date = new Date(dateString);
                var utcString = date.toUTCString();
                var isoString = new Date(utcString).toISOString();
                return isoString.slice(0, 10) + 'T' + isoString.slice(11, 19);
            }

            // Function to add holidays and Sundays to the selectedSlots array
            function addHolidaysAndSundaysToSelectedSlots() {
                holidays.forEach(function (holiday) {
                    Sundays_Holidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#ff0000',
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Function to add events to the selectedSlots array
            function addAvailabilityToSelectedSlots(eventData) {
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

            // Function to add events to the selectedSlots array
            function addAvailabilityClassroomsToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    Disponibilidade_Sala.push({
                        title: event.title,
                        start: event.start,
                        end: event.end,
                        color: event.color,
                        cod_turma: event.cod_turma,
                        dataType: 'non_unselectable'
                    });
                });
            }

            // Function to add events to the selectedSlots array
            function addEventsToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    selectedSlots.push({
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

                updateHorasModulo();
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

                    addAvailabilityToSelectedSlots(eventData); // Execução da função para inserir os dados do eventData para dentro de um array utilizado na renderização do calendário

                    // Renderizar o calendário
                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    // Se a chamada á BD falhar, adicionar á mesma os domingos e feriados ao array Sundays_Holidays
                    addHolidaysAndSundaysToSelectedSlots();

                    // Renderizar o calendário
                    renderCalendar();
                }
            });

            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetDisponibilidade_Sala_JSON",
                data: JSON.stringify({ cod_sala: $('#<%= hf_cod_sala.ClientID %>').val() }), // Retrieve the value of hf_cod_sala
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addAvailabilityClassroomsToSelectedSlots(eventData);

                    // Render calendar after adding events to selectedSlots array
                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    // If AJAX call fails, add holidays and Sundays to selectedSlots array
                    addHolidaysAndSundaysToSelectedSlots();

                    // Render calendar after adding events to selectedSlots array
                    renderCalendar();
                }
            });

            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetHorarios_JSON",
                data: JSON.stringify({ cod_turma: codTurmaValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addEventsToSelectedSlots(eventData);
                    addHolidaysAndSundaysToSelectedSlots();

                    // Render calendar after adding events to selectedSlots array
                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    // If AJAX call fails, add holidays and Sundays to selectedSlots array
                    addHolidaysAndSundaysToSelectedSlots();
                    // Render calendar even if AJAX call fails
                    renderCalendar();
                }
            });

            function renderCalendar() {
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
                        var currentDate = new Date(); // Get the current date                       

                        // Check if the selected date is before the current date
                        if (info.start < currentDate) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Só pode criar eventos a partir da data atual.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                              return;
                        }

                        // Assuming info.start contains the date string "Wed Apr 17 2024 09:00:00 GMT+0100 (Western European Summer Time)"
                        var selectedEventDate = new Date(info.start);

                        // Extract year, month, and day
                        var year = selectedEventDate.getFullYear();
                        var month = ('0' + (selectedEventDate.getMonth() + 1)).slice(-2); // Add leading zero if needed
                        var day = ('0' + selectedEventDate.getDate()).slice(-2); // Add leading zero if needed

                        // Form the formatted date string
                        var formattedDate = year + '-' + month + '-' + day;

                        // Retrieve the date stored in the hidden field
                        var hiddenFieldDateStr = document.getElementById('<%= hf_data_inicio.ClientID %>').value;

                        if (formattedDate < hiddenFieldDateStr) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Só pode criar eventos a partir da data de início da turma.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }

                        // Determine start and end times based on regime
                        var regime = $('#<%= hf_regime.ClientID %>').val();
                        var selectedStartTime = regime === 'Laboral' ? '08:00:00' : '16:00:00';
                        var selectedEndTime = regime === 'Laboral' ? '16:00:00' : '23:00:00';

                        if (isAllDay) {
                            // If it's an all-day event, set the start and end times accordingly
                            var selectedDateStart = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), selectedStartTime.split(':')[0], selectedStartTime.split(':')[1], 0));
                            var selectedDateEnd = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), selectedEndTime.split(':')[0], selectedEndTime.split(':')[1], 0));

                            info.start = selectedDateStart.toISOString();
                            info.end = selectedDateEnd.toISOString();

                            info.start = new Date(selectedDateStart);
                            info.end = new Date(selectedDateEnd);
                        }

                        if (!isSlotDurationValid(info)) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Por favor introduza um evento de pelo menos 1h e que comece á hora certa.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }

                        // Check if the selected event conflicts with any existing events
                        if (isEventConflict(info, Disponibilidade_Formador) || isEventConflict(info, Sundays_Holidays) || isEventConflict(info, Disponibilidade_Sala)) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("O evento escolhido entra em conflito com outro evento.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }

                        // Calculate the total hours of the selected modulo
                        var totalHoursOfModulo = calculateTotalHoursOfModulo(selectedModuloValue);

                        // Get the duration of the selected event
                        var eventDuration = Math.abs(new Date(info.end) - new Date(info.start)) / 36e5;

                        // Check if adding the event will surpass the total hours allowed for the modulo
                        if (totalHoursOfModulo + eventDuration > totalHoursAllowed) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Adicionar este evento excederá o total de horas permitidas para o módulo selecionado.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }

                        // Get the selected color from the color picker
                        var selectedColor = $('#colorPicker').val();

                        // Create the event title by concatenating the selected values
                        var eventTitle = nomeTurma + " | " + selectedModulo + " | " + selectedFormador + " | " + selectedSala;

                        var eventToAdd = {
                            title: eventTitle,
                            start: formatDateToISOString(info.start),
                            end: formatDateToISOString(info.end),
                            rendering: 'background',
                            cod_modulo: selectedModuloValue,
                            cod_formador: selectedFormadorValue,
                            cod_sala: selectedSalaValue,
                            color: selectedColor,
                            cod_turma: codTurma,
                            dataType: 'unselectable'
                        };

                        // Push the event object to selectedSlots array
                        selectedSlots.push(eventToAdd);

                        // Add the event to the calendar
                        calendar.addEvent(eventToAdd);

                        // Ensure selectedSlots array is properly structured
                        var formattedSlots = selectedSlots.map(function (slot) {
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

                        // Call the server-side method using AJAX
                        $.ajax({
                            type: "POST",
                            url: "editar_horario.aspx/Gravar_Horario_Turma",
                            data: JSON.stringify({ selectedSlots: formattedSlots, cod_turma: codTurmaValue, cod_formador: selectedFormadorValue, cod_sala: selectedSalaValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Aula inserida com sucesso!");
                                    $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Ocorreu um erro ao guardar a aula inserida.");
                                    $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Ocorreu um erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        });

                        updateHorasModulo();

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentDate,
                    validRange: {
                        start: currentYear + '-01-01',
                        end: (currentYear + 2) + '-01-01'
                    }
                });

                // Function to check if there is a conflict between the selected event and existing events
                function isEventConflict(selectedEvent, eventsArray) {
                    for (var i = 0; i < eventsArray.length; i++) {
                        var event = eventsArray[i];
                        var selectedEventStartString = selectedEvent.start.toISOString().slice(0, -5); // Trim milliseconds
                        var selectedEventEndString = selectedEvent.end.toISOString().slice(0, -5); // Trim milliseconds
                        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                            return true; // Conflict found
                        }
                    }
                    return false; // No conflict
                }

                // Function to update slotMinTime and slotMaxTime based on hf_regime value
                function updateSlotTimes(regime) {
                    if (regime === 'Laboral') {
                        calendar.setOption('slotMinTime', '08:00:00');
                        calendar.setOption('slotMaxTime', '16:00:00');
                    } else if (regime === 'Pós-Laboral') {
                        calendar.setOption('slotMinTime', '16:00:00');
                        calendar.setOption('slotMaxTime', '23:00:00');
                    }
                }

                // Initial call to update slot times based on hf_regime value
                updateSlotTimes($('#<%= hf_regime.ClientID %>').val());

                // Event listener to update slot times when hf_regime value changes
                $('#<%= hf_regime.ClientID %>').change(function () {
                    updateSlotTimes($(this).val());
                });

                calendar.setOption('eventClick', function (info) {
                    // Check if the clicked event is unselectable
                    if (info.event.extendedProps.dataType === 'unselectable') {
                        // Get the start time components of the clicked event
                        var start = info.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        // Get the end time components of the clicked event
                        var end = info.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        // Remove the event from the selectedSlots array
                        selectedSlots = selectedSlots.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        // Remove the event from Disponibilidade_Formador array
                        Disponibilidade_Formador = Disponibilidade_Formador.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remove the event from Disponibilidade_Sala array
                        Disponibilidade_Sala = Disponibilidade_Sala.filter(function (event) {
                            return !(event.start === startString && event.end === endString);
                        });

                        // Remove the event from the calendar
                        info.event.remove();

                        // Ensure selectedSlots array is properly structured
                        var formattedSlots = selectedSlots.map(function (slot) {
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

                        // Call the server-side method using AJAX
                        $.ajax({
                            type: "POST",
                            url: "editar_horario.aspx/Gravar_Horario_Turma",
                            data: JSON.stringify({ selectedSlots: formattedSlots, cod_turma: codTurmaValue, cod_formador: selectedFormadorValue, cod_sala: selectedSalaValue }), // Pass formattedSlots with all properties
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                // Handle success response if needed
                                if (response.d) {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Aula removida e horário guardado com sucesso.");
                                    $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                } else {
                                    // Show error message in lbl_mensagem
                                    $('#lbl_mensagem').text("Erro ao guardar o horário na base de dados.");
                                    $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                    $('#lbl_mensagem').fadeIn();

                                    // Fade out the error message after 3 seconds
                                    setTimeout(function () {
                                        $('#lbl_mensagem').fadeOut(function () {
                                            $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                        });
                                    }, 3000);

                                    return;
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        });

                        updateHorasModulo();

                    } else {
                        // Show error message in lbl_mensagem
                        $('#lbl_mensagem').text("Não pode remover este evento.");
                        $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                        $('#lbl_mensagem').fadeIn();

                        // Fade out the error message after 3 seconds
                        setTimeout(function () {
                            $('#lbl_mensagem').fadeOut(function () {
                                $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                            });
                        }, 3000);

                        return;
                    }
                });

                selectedSlots.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color,
                        dataType: 'unselectable'
                    });
                });

                Disponibilidade_Formador.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatDateToISOString(event.start) == slot.start && formatDateToISOString(event.end) == slot.end;
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

                Disponibilidade_Sala.forEach(function (slot) {
                    const existingEvent = calendar.getEvents().find(event => {
                        return formatDateToISOString(event.start) == slot.start && formatDateToISOString(event.end) == slot.end;
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

                document.getElementById('btn_SaveSelectedSlots').addEventListener('click', function (event) {
                    // Ensure selectedSlots array is properly structured
                    var formattedSlots = selectedSlots.map(function (slot) {
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

                    // Call the server-side method using AJAX
                    $.ajax({
                        type: "POST",
                        url: "editar_horario.aspx/Gravar_Horario_Turma",
                        data: JSON.stringify({ selectedSlots: formattedSlots, cod_turma: codTurmaValue, cod_formador: selectedFormadorValue, cod_sala: selectedSalaValue }), // Pass formattedSlots with all properties
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success response if needed
                            if (response.d) {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Horário guardado com sucesso!");
                                $('#lbl_mensagem').addClass('alert alert-success'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-success'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            } else {
                                // Show error message in lbl_mensagem
                                $('#lbl_mensagem').text("Erro ao guardar o horário da turma.");
                                $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                                $('#lbl_mensagem').fadeIn();

                                // Fade out the error message after 3 seconds
                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                    });
                                }, 3000);

                                return;
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Show error message in lbl_mensagem
                            $('#lbl_mensagem').text("Erro ao enviar os dados para a base de dados.");
                            $('#lbl_mensagem').addClass('alert alert-danger'); // Add CSS class to lbl_mensagem
                            $('#lbl_mensagem').fadeIn();

                            // Fade out the error message after 3 seconds
                            setTimeout(function () {
                                $('#lbl_mensagem').fadeOut(function () {
                                    $(this).removeClass('alert alert-danger'); // Remove CSS class from lbl_mensagem after fadeOut
                                });
                            }, 3000);

                            return;
                        }
                    });
                    event.preventDefault(); // Prevent default button behavior
                });

            }
        });

    </script>
</asp:Content>
