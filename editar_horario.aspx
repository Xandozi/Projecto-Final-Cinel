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
                <div class="row border-dark">
                    <div class="col-md-2">
                        <label class="col-form-label">Módulo</label>
                        <asp:DropDownList ID="ddl_modulo" AutoPostBack="true" CssClass="form-control" runat="server" DataSourceID="modulos" DataTextField="nome_modulo" DataValueField="cod_modulo" OnSelectedIndexChanged="ddl_modulo_SelectedIndexChanged"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="modulos" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_modulo], [nome_modulo], [cod_ufcd] FROM [Modulos]"></asp:SqlDataSource>
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
                        <asp:DropDownList ID="ddl_sala" CssClass="form-control" runat="server" DataSourceID="salas" DataTextField="nome_sala" DataValueField="cod_sala"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="salas" ConnectionString='<%$ ConnectionStrings:CinelConnectionString %>' SelectCommand="SELECT [cod_sala], [nome_sala], [ativo] FROM [Salas]">
                        </asp:SqlDataSource>
                    </div>
                    <div class="col-md-1">
                        <label class="col-form-label">Cor do Evento</label>
                        <input class="form-control" type="color" id="colorPicker" value="000000">
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
            var MIN_SLOT_DURATION = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year

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

            // Function called server side when the ddl_modulo changes its selected index
            function dropdownChanged() {
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
                    url: "editar_horario.aspx/ProcessSelectedSlots",
                    data: JSON.stringify({ selectedSlots: formattedSlots, cod_turma: codTurmaValue }), // Pass formattedSlots with all properties
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // Handle success response if needed
                        if (response.d) {
                            // Render the calendar
                            renderCalendar();
                        } else {
                            // Show error message
                            alert("Ocorreu um erro ao submeter o horário da turma.");
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        // Handle error
                        console.error("Error occurred while sending data to server.");
                    }
                });
            }


            // Function to add holidays and Sundays to the selectedSlots array
            function addHolidaysAndSundaysToSelectedSlots() {
                holidays.forEach(function (holiday) {
                    Sundays_Holidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        color: '#ff0000'
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
                        color: '#ff0000'
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
                        color: event.color
                    });
                });
            }

            $.ajax({
                type: "POST",
                url: "/Disponibilidade_WebService.asmx/GetDisponibilidade_JSON",
                data: JSON.stringify({ cod_user: $('#<%= hf_cod_user.ClientID %>').val() }), // Retrieve the value of hf_cod_formador
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addAvailabilityToSelectedSlots(eventData);

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
                    initialView: 'dayGridMonth',
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
                            alert("Please select a slot of at least 1 hour starting at hour:00.");
                            return;
                        }

                        // Check if the selected event conflicts with any existing events
                        if (isEventConflict(info, Disponibilidade_Formador) || isEventConflict(info, Sundays_Holidays)) {
                            console.log(info.start)
                            alert("Selected time conflicts with existing events.");
                            return;
                        }

                        // Get the selected values from dropdown lists
                        var selectedModulo = $('#<%= ddl_modulo.ClientID %> option:selected').text();
                        var selectedModuloValue = $('#<%= ddl_modulo.ClientID %> option:selected').val();
                        var selectedFormador = $('#<%= lbl_nome_formador.ClientID %>').text();
                        var selectedFormadorValue = $('#<%= hf_cod_formador.ClientID %>').val();
                        var selectedSala = $('#<%= ddl_sala.ClientID %> option:selected').text();
                        var selectedSalaValue = $('#<%= ddl_sala.ClientID %> option:selected').val();

                        // Get the selected color from the color picker
                        var selectedColor = $('#colorPicker').val();

                        // Create the event title by concatenating the selected values
                        var eventTitle = selectedModulo + " | " + selectedFormador + " | " + selectedSala;

                        var eventToAdd = {
                            title: eventTitle,
                            start: info.start,
                            end: info.end,
                            rendering: 'background',
                            cod_modulo: selectedModuloValue,
                            cod_formador: selectedFormadorValue,
                            cod_sala: selectedSalaValue,
                            color: selectedColor
                        };

                        // Push the event object to selectedSlots array
                        selectedSlots.push(eventToAdd);

                        // Add the event to the calendar
                        calendar.addEvent(eventToAdd);

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentYear + '-01-01',
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
                        console.log(i)
                        console.log(event.start)
                        console.log(event.end)
                        console.log('-')
                        console.log(selectedEventStartString)
                        console.log(selectedEventEndString)
                        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                            console.log('denied')
                            console.log(i)
                            return true; // Conflict found
                        }
                        console.log('not denied')
                        console.log(i)
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
                    // Get the start time components
                    var start = info.event.start;
                    var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                        ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                    // Get the end time components
                    var end = info.event.end;
                    var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                        ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                    console.log(startString); // Log the start time in the desired format
                    console.log(endString); // Log the end time in the desired format
                    console.log(info.event.title);

                    selectedSlots = selectedSlots.filter(function (slot) {
                        return !(slot.start === startString && slot.end === endString);
                    });
                    info.event.remove();
                });

                Disponibilidade_Formador.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color
                    });
                });

                Sundays_Holidays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color
                    });
                });

                selectedSlots.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: slot.color
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
                        url: "editar_horario.aspx/ProcessSelectedSlots",
                        data: JSON.stringify({ selectedSlots: formattedSlots, cod_turma: codTurmaValue }), // Pass formattedSlots with all properties
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            // Handle success response if needed
                            if (response.d) {
                                // Show success message
                                alert("Horário submetido com sucesso! Será redirecionado para outra página.");
                                // Redirect to another page
                                window.location.href = "horarios.aspx"; // Change "new_page.aspx" to the desired page
                            } else {
                                // Show error message
                                alert("Ocorreu um erro ao submeter o horário da turma.");
                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            // Handle error
                            console.error("Error occurred while sending data to server.");
                        }
                    });
                    event.preventDefault(); // Prevent default button behavior
                });

            }
        });

    </script>
</asp:Content>
