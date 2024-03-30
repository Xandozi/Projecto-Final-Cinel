<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="formador_disponibilidade.aspx.cs" Inherits="Projeto_Final.formador_disponibilidade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card" style="border-color: #333; margin-top: 30px; margin-bottom: 30px;">
            <div class="card-header bg-dark text-white">
                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Disponibilidade Formador</h2>
            </div>
            <div class="card" style="margin: 5px;">
                <div class="card-body">
                    <div id='calendar' style="margin-top: 30px; margin-bottom: 10px;"></div>
                    <div class="row justify-content-between">
                        <div class="form-group" style="margin: 10px;">
                            <asp:Label ID="lbl_sabados" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                            <div class="row">
                                <div class="col-md-12">
                                    <button id="btn_SelecionarSabados" class="btn btn-dark">Sábados</button>
                                    <button id="btn_SelecionarSabadosManha" class="btn btn-dark">Sábados de manhã</button>
                                    <button id="btn_SelecionarSabadosTarde" class="btn btn-dark">Sábados de tarde</button>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="margin: 10px;">
                            <asp:Label ID="Label1" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <button id="btn_SelecionarDiasSemanaManha" class="btn btn-dark">Dias de semana manhã</button>
                                    <button id="btn_SelecionarDiasSemanaTarde" class="btn btn-dark">Dias de semana de tarde</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin: 10px;">
            <button class="btn btn-success" id="btn_SaveSelectedSlots">Guardar Disponibilidade</button>
            <asp:HiddenField ID="hf_cod_user" runat="server" />
        </div>
    </div>

    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
    <script>
        // Ensure the script runs after the DOM is fully loaded
        document.addEventListener('DOMContentLoaded', function () {
            console.log("DOMContentLoaded event fired."); // Log DOMContentLoaded event

            var selectedSlots = []; // Array to store selected time slots
            var MIN_SLOT_DURATION = 60 * 60 * 1000; // Minimum slot duration in milliseconds (1 hour)
            var currentYear = new Date().getFullYear(); // Get the current year

            // Function to check if the selected slot duration meets the minimum requirement and starts and ends on the hour
            function isSlotDurationValid(info) {
                var slotDuration = info.end.getTime() - info.start.getTime();
                return slotDuration >= MIN_SLOT_DURATION && info.start.getMinutes() === 0 && info.end.getMinutes() === 0;
            }

            // Add events for national holidays to the selectedSlots array
            var holidays = [
                {
                    title: 'Ano Novo',
                    start: '2024-01-01T08:00:00',
                    end: '2024-01-01T23:00:00',
                },
                {
                    title: 'Dia da Liberdade',
                    start: '2024-04-25T08:00:00',
                    end: '2024-04-25T23:00:00',
                },
                {
                    title: 'Dia do Trabalhador',
                    start: '2024-05-01T08:00:00',
                    end: '2024-05-01T23:00:00',
                },
                {
                    title: 'Dia de Portugal',
                    start: '2024-06-10T08:00:00',
                    end: '2024-06-10T23:00:00',
                },
                {
                    title: 'Assunção de Nossa Senhora',
                    start: '2024-08-15T08:00:00',
                    end: '2024-08-15T23:00:00',
                },
                {
                    title: 'Implantação da República',
                    start: '2024-10-05T08:00:00',
                    end: '2024-10-05T23:00:00',
                },
                {
                    title: 'Dia de Todos os Santos',
                    start: '2024-11-01T08:00:00',
                    end: '2024-11-01T23:00:00',
                },
                {
                    title: 'Restauração da Independência',
                    start: '2024-12-01T08:00:00',
                    end: '2024-12-01T23:00:00',
                },
                {
                    title: 'Natal',
                    start: '2024-12-25T08:00:00',
                    end: '2024-12-25T23:00:00',
                },
            ];


            holidays.forEach(function (holiday) {
                selectedSlots.push({
                    startStr: holiday.start,
                    endStr: holiday.end,
                });
            });

            // Function to generate Sundays for all years
            function generateSundays() {
                var sundays = [];
                var currentDate = new Date();
                currentDate.setDate(currentDate.getDate() + (7 - currentDate.getDay())); // Move to the next Sunday
                var currentYear = currentDate.getFullYear();

                while (currentYear === currentDate.getFullYear()) {
                    sundays.push({
                        start: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Convert date to ISO string format (YYYY-MM-DD)
                        end: currentDate.toISOString().slice(0, 10) + 'T23:00:00', // End date can be the same as start date for single-day events
                    });
                    currentDate.setDate(currentDate.getDate() + 7); // Move to the next Sunday
                }
                return sundays;
            }

            // Add events for all Sundays to the selectedSlots array
            var sundays = generateSundays();
            sundays.forEach(function (sunday) {
                selectedSlots.push({
                    startStr: sunday.start,
                    endStr: sunday.end,
                });
            });

            var calendarEl = document.getElementById('calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                slotLabelFormat: {
                    hour: '2-digit',
                    minute: '2-digit',
                    hour12: false
                }, // 24 hours format
                firstDay: 1, // Start week on Monday
                slotMinTime: '08:00:00', // Start time
                slotMaxTime: '23:00:00', // End time (changed to 23:00)
                allDaySlot: true,
                selectable: true, // Enable selection
                timeZone: 'UTC', // Display dates in utc timezone
                select: function (info) {
                    // Check if the selection is an all-day event
                    var isAllDay = info.allDay;

                    if (isAllDay) {
                        // Get the selected date and set the time to 08:00:00 UTC
                        var selectedDateStart = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 8, 0, 0));

                        // Create a new date object for the end time and set it to 23:00:00 UTC
                        var selectedDateEnd = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 23, 0, 0));

                        // Update the start and end times in the info object
                        info.startStr = selectedDateStart.toISOString();
                        info.endStr = selectedDateEnd.toISOString();

                        info.start = new Date(selectedDateStart);
                        info.end = new Date(selectedDateEnd);
                    }

                    if (!isSlotDurationValid(info)) {
                        alert("Please select a slot of at least 1 hour starting at hour:00.");
                        return;
                    }
                    // Add selected time slot to the array
                    selectedSlots.push(info);
                    // Highlight selected time slot on the calendar
                    calendar.unselect(); // Deselect previously selected slots
                    calendar.addEvent({
                        title: 'Não Disponível',
                        start: info.startStr,
                        end: info.endStr,
                        rendering: 'background',
                        color: '#ff0000'
                    });
                },
                contentHeight: 'auto', // Adjusts the calendar's height automatically
                aspectRatio: 1.5,
                initialDate: currentYear + '-01-01', // Set initial date to January 1st of the current year
                validRange: { // Restrict the valid range to the current year
                    start: currentYear + '-01-01',
                    end: (currentYear + 1) + '-01-01' // January 1st of the next year
                }
            });

            // Add events from selectedSlots array to the calendar
            selectedSlots.forEach(function (slot) {
                calendar.addEvent({
                    title: 'Não Disponível',
                    start: slot.startStr,
                    end: slot.endStr,
                    rendering: 'background',
                    color: '#ff0000'
                });
            });

            // Add an eventClick callback to handle event deselection
            calendar.setOption('eventClick', function (info) {
                // Remove the event from selectedSlots array
                selectedSlots = selectedSlots.filter(function (slot) {
                    return !(slot.startStr === info.event.startStr && slot.endStr === info.event.endStr);
                });
                // Remove the event from the calendar
                info.event.remove();
            });

            calendar.render();

            function SelecionarDiasSemanaManha() {
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear(); // Get the current year

                // Set the time to 8:00 AM
                currentDate.setHours(8, 0, 0, 0);

                // Loop through each day of the current year
                while (currentDate.getFullYear() === currentYear) {
                    // Check if the current day is not Saturday or Sunday
                    if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6) {
                        console.log("Marking:", currentDate);
                        // Add the time slot from 8:00 to 16:00 as "Não Disponível"
                        calendar.addEvent({
                            title: 'Não Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                            end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                            rendering: 'background',
                            color: '#ff0000'
                        });
                        selectedSlots.push({
                            startStr: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                            endStr: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // End time
                        });
                    }
                    // Move to the next day
                    currentDate.setDate(currentDate.getDate() + 1);
                }
            }

            function SelecionarDiasSemanaTarde() {
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear(); // Get the current year

                // Set the time to 8:00 AM
                currentDate.setHours(8, 0, 0, 0);

                // Loop through each day of the current year
                while (currentDate.getFullYear() === currentYear) {
                    // Check if the current day is not Saturday or Sunday
                    if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6) {
                        // Add the time slot from 8:00 to 16:00 as "Não Disponível"
                        calendar.addEvent({
                            title: 'Não Disponível',
                            start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                            end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                            rendering: 'background',
                            color: '#ff0000'
                        });
                        selectedSlots.push({
                            startStr: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // Start time
                            endStr: currentDate.toISOString().slice(0, 10) + 'T23:00:00', // End time
                        });
                    }
                    // Move to the next day
                    currentDate.setDate(currentDate.getDate() + 1);
                }
            }

            // Function to select all Saturdays and mark them as "Não Disponível"
            function SelecionarSabados() {
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear(); // Get the current year
                currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday

                while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                    calendar.addEvent({
                        title: 'Não Disponível',
                        start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                        end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                        rendering: 'background',
                        color: '#ff0000'
                    });
                    selectedSlots.push({
                        startStr: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                        endStr: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // End time
                    });
                    currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                }
            }

            function SelecionarSabadosManha() {
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear(); // Get the current year
                currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday

                while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                    calendar.addEvent({
                        title: 'Não Disponível',
                        start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                        end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                        rendering: 'background',
                        color: '#ff0000'
                    });
                    selectedSlots.push({
                        startStr: currentDate.toISOString().slice(0, 10) + 'T08:00:00', // Start time
                        endStr: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // End time
                    });
                    currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                }
            }

            function SelecionarSabadosTarde() {
                var currentDate = new Date();
                var currentYear = currentDate.getFullYear(); // Get the current year
                currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay())); // Move to the next Saturday

                while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                    calendar.addEvent({
                        title: 'Não Disponível',
                        start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                        end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                        rendering: 'background',
                        color: '#ff0000'
                    });
                    selectedSlots.push({
                        startStr: currentDate.toISOString().slice(0, 10) + 'T16:00:00', // Start time
                        endStr: currentDate.toISOString().slice(0, 10) + 'T23:00:00', // End time
                    });
                    currentDate.setDate(currentDate.getDate() + 7); // Move to the next Saturday
                }
            }

            // Button click event handler to select all Saturdays
            document.getElementById('btn_SelecionarDiasSemanaManha').addEventListener('click', function (event) {
                SelecionarDiasSemanaManha();
                event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
            });

            // Button click event handler to select all Saturdays
            document.getElementById('btn_SelecionarDiasSemanaTarde').addEventListener('click', function (event) {
                SelecionarDiasSemanaTarde();
                event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
            });

            // Button click event handler to select all Saturdays
            document.getElementById('btn_SelecionarSabados').addEventListener('click', function (event) {
                SelecionarSabados();
                event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
            });

            // Button click event handler to select all Saturdays
            document.getElementById('btn_SelecionarSabadosManha').addEventListener('click', function (event) {
                SelecionarSabadosManha();
                event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
            });

            // Button click event handler to select all Saturdays
            document.getElementById('btn_SelecionarSabadosTarde').addEventListener('click', function (event) {
                SelecionarSabadosTarde();
                event.preventDefault(); // Prevent default button behavior (e.g., form submission or postback)
            });

            document.getElementById('btn_SaveSelectedSlots').addEventListener('click', function (event) {
                // Extract startStr and endStr values from selectedSlots array
                var slotStrings = selectedSlots.map(function (slot) {
                    return slot.startStr + ',' + slot.endStr;
                });

                // Call the server-side method using AJAX
                $.ajax({
                    type: "POST",
                    url: "formador_disponibilidade.aspx/ProcessSelectedSlots", // Specify your page and method name
                    data: JSON.stringify({ selectedSlots: slotStrings, cod_user: codUserValue }), // Send the string array directly
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // Handle success response if needed
                        if (response.d) {
                            // Show success message
                            alert("Disponibilidade de Formador actualizada com sucesso! Será redireccionado para outra página.");
                            // Redirect to another page
                            window.location.href = "personal_zone_inscricoes.aspx"; // Change "new_page.aspx" to the desired page
                        } else {
                            // Show error message
                            alert("Ocorreu um erro ao actualizar a disponibilidade do formador.");
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        // Handle error
                        console.error("Error occurred while sending data to server.");
                    }
                });
                event.preventDefault(); // Prevent default button behavior
            });

            var codUserValue; // Variable to store the cod_user value

            // Function to set the cod_user value received from server-side
            function setCodUser(cod_user) {
                codUserValue = cod_user;
            }

            // Function to parse the URL and extract the value of the cod_user variable
            function getUrlParameter(name) {
                name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
                var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
                var results = regex.exec(location.search);
                return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
            }

            // Call setCodUser function with the value of the cod_user URL variable
            var codUserFromUrl = getUrlParameter('cod_user');
            setCodUser(codUserFromUrl);
        });

    </script>

</asp:Content>
