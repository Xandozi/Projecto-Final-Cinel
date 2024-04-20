<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="horarios_detalhe.aspx.cs" Inherits="Projeto_Final.horarios_detalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card bg-secondary" style="border-color: #333; color:black; margin-top: 30px; margin-bottom: 30px;">
            <div class="card-header bg-dark text-white">
                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Horário da Turma
                    <asp:Label ID="lbl_nome_turma" runat="server" Text=""></asp:Label></h2>
            </div>
            <div class="card-body">
                <div id='calendar' class="bg-light border-dark" style="padding: 10px; margin-top: 30px; margin-bottom: 10px;"></div>
            </div>
        </div>
        <div class="row justify-content-between" style="margin: 10px;">
            <asp:Button ID="btn_editar" CssClass="btn btn-dark" runat="server" Text="Editar Horário" OnClick="btn_editar_Click" />
            <asp:HiddenField ID="hf_cod_user" runat="server" />
            <a href="horarios.aspx" class="btn btn-info">Voltar para a página Horários</a>
        </div>
    </div>
    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
    <script>
        // Define the codTurmaValue variable
        var codTurmaValue;

        // Function to parse the URL and extract the value of the cod_turma variable
        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        // Function to set the cod_turma value received from server-side
        function setCodTurma(cod_turma) {
            codTurmaValue = cod_turma;
        }

        // Call setCodTurma function with the value of the cod_turma URL variable
        var codTurmaFromUrl = getUrlParameter('cod_turma');
        setCodTurma(codTurmaFromUrl);

        // Ensure the script runs after the DOM is fully loaded
        document.addEventListener('DOMContentLoaded', function () {
            console.log("DOMContentLoaded event fired."); // Log DOMContentLoaded event

            var selectedSlots = []; // Array to store selected time slots
            var Sundays_Holidays = []; // Array to store holidays and sundays
            var MIN_SLOT_DURATION = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year
            var currentDate = new Date();

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
                url: "/Horarios_WebService.asmx/GetHorarios_JSON",
                data: JSON.stringify({ cod_turma: codTurmaValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d); // Extract the data array from the response

                    addHolidaysAndSundaysToSelectedSlots();
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
                    selectable: false, // Disable selecting slots
                    timeZone: 'UTC',
                    select: function (info) {
                        var isAllDay = info.allDay;

                        if (isAllDay) {
                            var selectedDateStart = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 8, 0, 0));
                            var selectedDateEnd = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 23, 0, 0));

                            info.start = selectedDateStart.toISOString();
                            info.end = selectedDateEnd.toISOString();

                            info.start = new Date(selectedDateStart);
                            info.end = new Date(selectedDateEnd);
                        }

                        if (!isSlotDurationValid(info)) {
                            alert("Please select a slot of at least 1 hour starting at hour:00.");
                            return;
                        }

                        var eventToAdd = {
                            title: info.title,
                            start: info.start,
                            end: info.end,
                            rendering: 'background',
                            color: info.color
                        };

                        // Push the event object to selectedSlots array
                        selectedSlots.push(eventToAdd);

                        // Add the event to the calendar
                        calendar.addEvent(eventToAdd);

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentDate,
                    validRange: {
                        start: currentYear + '-01-01',
                        end: (currentYear + 3) + '-01-01'
                    }
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
            }
        });
    </script>
</asp:Content>
