<%@ Page Title="CINEL - Disponibilidade Formador" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="formador_disponibilidade.aspx.cs" Inherits="Projeto_Final.formador_disponibilidade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="card bg-secondary" style="border-color: #333; color:black; margin-top: 30px; margin-bottom: 30px;">
            <div class="card-header bg-dark text-white">
                <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Disponibilidade Formador</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <label id="lbl_mensagem" style="margin-top: 20px; margin-left: 20px;"></label>
                </div>
                <div id='calendar' class="bg-light border-dark" style="padding: 10px; margin-top: 30px; margin-bottom: 10px;"></div>
                <div class="row justify-content-between">
                    <div class="form-group" style="margin: 10px;">
                        <asp:Label ID="lbl_sabados" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <button id="btn_SelecionarSabados" class="btn btn-dark">Sábados</button>
                                <button id="btn_SelecionarSabadosManha" class="btn btn-dark">Sábados de manhã</button>
                                <button id="btn_SelecionarSabadosTarde" class="btn btn-dark">Sábados de tarde</button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="margin: 10px;">
                        <asp:Label ID="lbl_dias_semana" runat="server" Text="Selecionar como Indisponível"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <button id="btn_SelecionarDiasSemanaManha" class="btn btn-dark">Dias de semana manhã</button>
                                <button id="btn_SelecionarDiasSemanaTarde" class="btn btn-dark">Dias de semana de tarde</button>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="margin: 10px;">
                        <asp:Label ID="lbl_limpar" runat="server" Text="Limpar"></asp:Label>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px;">
                                <button id="btn_limpar" class="btn btn-dark">Limpar Tudo</button>
                                <button id="btn_limpar_manhas" class="btn btn-dark">Limpar Manhãs</button>
                                <button id="btn_limpar_tardes" class="btn btn-dark">Limpar Tardes</button>
                                <button id="btn_limpar_sabados" class="btn btn-dark">Limpar Sabados</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-between" style="margin: 10px;">
            <button class="btn btn-success" id="btn_SaveSelectedSlots">Submeter Disponibilidade</button>
            <asp:HiddenField ID="hf_cod_user" runat="server" />
            <a href="personal_zone_inscricoes.aspx" class="btn btn-info">Voltar para Minhas Inscrições</a>
        </div>
    </div>

    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
    <script>
        var codUserValue;

        function getUrlParameter(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var results = regex.exec(location.search);
            return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
        }

        function setCodUser(cod_user) {
            codUserValue = cod_user;
        }

        var codUserFromUrl = getUrlParameter('cod_user');
        setCodUser(codUserFromUrl);

        document.addEventListener('DOMContentLoaded', function () {
            var selectedSlots = [];
            var Sundays_Holidays = [];
            var MIN_SLOT_DURATION = 60 * 60 * 1000;
            var currentYear = new Date().getFullYear();
            var currentDate = new Date();

            var currentYear = currentDate.getFullYear();

            function isSlotDurationValid(info) {
                var slotDuration = info.end.getTime() - info.start.getTime();
                return slotDuration >= MIN_SLOT_DURATION && info.start.getMinutes() === 0 && info.end.getMinutes() === 0;
            }

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
                {
                    title: 'Véspera de Natal',
                    start: '2024-12-24T08:00:00',
                    end: '2024-12-24T23:00:00',
                },
            ];

            function generateSundays() {
                var sundays = [];
                var currentDate = new Date();
                currentDate.setDate(currentDate.getDate() + (7 - currentDate.getDay()));
                var currentYear = currentDate.getFullYear();

                while (currentYear === currentDate.getFullYear()) {
                    sundays.push({
                        title: 'Domingo',
                        start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                        end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                    });
                    currentDate.setDate(currentDate.getDate() + 7);
                }
                return sundays;
            }

            function addHolidaysAndSundaysToSelectedSlots() {
                holidays.forEach(function (holiday) {
                    Sundays_Holidays.push({
                        title: holiday.title,
                        start: holiday.start,
                        end: holiday.end,
                        dataType: 'non_unselectable'
                    });
                });

                var sundays = generateSundays();
                sundays.forEach(function (sunday) {
                    Sundays_Holidays.push({
                        title: sunday.title,
                        start: sunday.start,
                        end: sunday.end,
                        dataType: 'non_unselectable'
                    });
                });
            }

            function addEventsToSelectedSlots(eventData) {
                eventData.forEach(function (event) {
                    if (event.cod_turma == 0) {
                        selectedSlots.push({
                            title: event.title,
                            start: event.start,
                            end: event.end,
                            cod_turma: event.cod_turma,
                            color: event.color,
                            dataType: 'unselectable'
                        });
                    } else {
                        selectedSlots.push({
                            title: event.title,
                            start: event.start,
                            end: event.end,
                            cod_turma: event.cod_turma,
                            color: event.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });
            }

            $.ajax({
                type: "POST",
                url: "/Horarios_WebService.asmx/GetDisponibilidade_JSON",
                data: JSON.stringify({ cod_user: codUserValue }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var eventData = JSON.parse(response.d);

                    addHolidaysAndSundaysToSelectedSlots();
                    addEventsToSelectedSlots(eventData);

                    renderCalendar();
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error("Error occurred while fetching event data. WEBSERVICE");
                    addHolidaysAndSundaysToSelectedSlots();
                    renderCalendar();
                }
            });

            function renderCalendar() {
                var calendarEl = document.getElementById('calendar');
                var calendar = new FullCalendar.Calendar(calendarEl, {
                    locale: 'pt',
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
                        var currentDate = new Date();

                        if (info.start < currentDate) {
                            return;
                        }

                        if (isAllDay) {
                            var selectedDateStart = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 8, 0, 0));
                            var selectedDateEnd = new Date(Date.UTC(info.start.getUTCFullYear(), info.start.getUTCMonth(), info.start.getUTCDate(), 23, 0, 0));

                            info.start = selectedDateStart.toISOString();
                            info.end = selectedDateEnd.toISOString();

                            info.start = new Date(selectedDateStart);
                            info.end = new Date(selectedDateEnd);
                        }

                        if (!isSlotDurationValid(info)) {
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

                        if (isEventConflict(info, selectedSlots) || isEventConflict(info, Sundays_Holidays)) {
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

                        var eventToAdd = {
                            title: 'Não Disponível',
                            start: info.start,
                            end: info.end,
                            rendering: 'background',
                            color: '#ff0000',
                            cod_turma: 0,
                            dataType: 'unselectable'
                        };

                        selectedSlots.push(eventToAdd);

                        calendar.addEvent(eventToAdd);

                        calendar.unselect();
                    },
                    contentHeight: 'auto',
                    aspectRatio: 1.5,
                    initialDate: currentDate,
                    validRange: {
                        start: currentYear + '-01-01',
                        end: (currentYear + 1) + '-01-01'
                    }
                });

                function isEventConflict(selectedEvent, eventsArray) {
                    for (var i = 0; i < eventsArray.length; i++) {
                        var event = eventsArray[i];
                        var selectedEventStartString = selectedEvent.start.toISOString().slice(0, -5);
                        var selectedEventEndString = selectedEvent.end.toISOString().slice(0, -5);
                        console.log(selectedEventStartString)
                        console.log(event.start)
                        if ((selectedEventStartString >= event.start && selectedEventStartString < event.end) ||
                            (selectedEventEndString > event.start && selectedEventEndString < event.end) ||
                            (selectedEventStartString <= event.start && selectedEventEndString > event.start)) {
                            return true;
                        }
                    }
                    return false;
                }

                calendar.setOption('eventClick', function (info) {
                    if (info.event.extendedProps.dataType === 'unselectable') {
                        var start = info.event.start;
                        var startString = start.getUTCFullYear() + '-' + ('0' + (start.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + start.getUTCDate()).slice(-2) + 'T' +
                            ('0' + start.getUTCHours()).slice(-2) + ':' + ('0' + start.getUTCMinutes()).slice(-2) + ':' + ('0' + start.getUTCSeconds()).slice(-2);

                        var end = info.event.end;
                        var endString = end.getUTCFullYear() + '-' + ('0' + (end.getUTCMonth() + 1)).slice(-2) + '-' + ('0' + end.getUTCDate()).slice(-2) + 'T' +
                            ('0' + end.getUTCHours()).slice(-2) + ':' + ('0' + end.getUTCMinutes()).slice(-2) + ':' + ('0' + end.getUTCSeconds()).slice(-2);

                        selectedSlots = selectedSlots.filter(function (slot) {
                            return !(slot.start === startString && slot.end === endString);
                        });

                        info.event.remove();
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

                Sundays_Holidays.forEach(function (slot) {
                    calendar.addEvent({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        rendering: 'background',
                        color: '#ff0000',
                        dataType: 'non_unselectable'
                    });
                });

                selectedSlots.forEach(function (slot) {
                    if (slot.cod_turma == 0) {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            cod_turma: slot.cod_turma,
                            color: slot.color,
                            dataType: 'unselectable'
                        });
                    } else {
                        calendar.addEvent({
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            rendering: 'background',
                            cod_turma: slot.cod_turma,
                            color: slot.color,
                            dataType: 'non_unselectable'
                        });
                    }
                });

                calendar.render();

                function Limpar_Disponibilidade() {
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Tarde Não Disponível') ||
                            event.title.includes('Manhã Não Disponível') ||
                            event.title.includes('Sábado Não Disponível') ||
                            event.title.includes('Não Disponível')) {
                            event.remove();
                        }
                    });

                    selectedSlots = selectedSlots.filter(function (slot) {
                        return !(slot.title.includes('Tarde Não Disponível') ||
                            slot.title.includes('Manhã Não Disponível') ||
                            slot.title.includes('Sábado Não Disponível') ||
                            slot.title.includes('Não Disponível'));
                    });
                }

                function Limpar_Disponibilidade_Manhas() {
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Manhã Não Disponível')) {
                            event.remove();
                        }
                    });

                    selectedSlots = selectedSlots.filter(function (slot) {
                        return !(slot.title.includes('Manhã Não Disponível'));
                    });
                }

                function Limpar_Disponibilidade_Tardes() {
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Tarde Não Disponível')) {
                            event.remove();
                        }
                    });

                    selectedSlots = selectedSlots.filter(function (slot) {
                        return !(slot.title.includes('Tarde Não Disponível'));
                    });
                }

                function Limpar_Disponibilidade_Sabados() {
                    calendar.getEvents().forEach(function (event) {
                        if (event.title.includes('Sábado Não Disponível')) {
                            event.remove();
                        }
                    });

                    selectedSlots = selectedSlots.filter(function (slot) {
                        return !(slot.title.includes('Sábado Não Disponível'));
                    });
                }


                function SelecionarDiasSemanaManha() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear();

                    currentDate.setHours(8, 0, 0, 0);

                    while (currentDate.getFullYear() === currentYear) {
                        if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6 && !isDateInArray(currentDate, Sundays_Holidays)) {
                            var selectedEvent = {
                                start: new Date(currentDate),
                                end: new Date(currentDate)
                            };
                            selectedEvent.start.setHours(8, 0, 0, 0);
                            selectedEvent.end.setHours(16, 0, 0, 0);

                            if (!isEventConflict(selectedEvent, selectedSlots)) {
                                console.log("Marking:", currentDate);
                                calendar.addEvent({
                                    title: 'Manhã Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    rendering: 'background',
                                    color: '#ff0000',
                                    dataType: 'unselectable'
                                });
                                selectedSlots.push({
                                    title: 'Manhã Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    color: '#ff0000',
                                    cod_turma: 0,
                                    dataType: 'unselectable'
                                });
                            }
                        }
                        currentDate.setDate(currentDate.getDate() + 1);
                    }
                }


                function SelecionarDiasSemanaTarde() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear();

                    currentDate.setHours(16, 0, 0, 0);

                    while (currentDate.getFullYear() === currentYear) {
                        if (currentDate.getDay() !== 0 && currentDate.getDay() !== 6 && !isDateInArray(currentDate, Sundays_Holidays)) {
                            var selectedEvent = {
                                start: new Date(currentDate),
                                end: new Date(currentDate)
                            };
                            selectedEvent.start.setHours(16, 0, 0, 0);
                            selectedEvent.end.setHours(23, 0, 0, 0);

                            if (!isEventConflict(selectedEvent, selectedSlots)) {
                                console.log("Marking:", currentDate);
                                calendar.addEvent({
                                    title: 'Tarde Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    rendering: 'background',
                                    color: '#ff0000',
                                    dataType: 'unselectable'
                                });
                                selectedSlots.push({
                                    title: 'Tarde Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    color: '#ff0000',
                                    cod_turma: 0,
                                    dataType: 'unselectable'
                                });
                            }
                        }
                        currentDate.setDate(currentDate.getDate() + 1);
                    }
                }


                function SelecionarSabados() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear();
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay()));

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                        if (!isDateInArray(currentDate, Sundays_Holidays)) {
                            var selectedEvent = {
                                start: new Date(currentDate),
                                end: new Date(currentDate)
                            };
                            selectedEvent.start.setHours(8, 0, 0, 0);
                            selectedEvent.end.setHours(23, 0, 0, 0);

                            if (!isEventConflict(selectedEvent, selectedSlots)) {
                                console.log("Marking:", currentDate);
                                calendar.addEvent({
                                    title: 'Sábado Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    rendering: 'background',
                                    color: '#ff0000',
                                    dataType: 'unselectable'
                                });
                                selectedSlots.push({
                                    title: 'Sábado Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    color: '#ff0000',
                                    cod_turma: 0,
                                    dataType: 'unselectable'
                                });
                            }
                        }
                        currentDate.setDate(currentDate.getDate() + 7);
                    }
                }



                function SelecionarSabadosManha() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear();
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay()));

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                        if (!isDateInArray(currentDate, Sundays_Holidays)) {
                            var selectedEvent = {
                                start: new Date(currentDate),
                                end: new Date(currentDate)
                            };
                            selectedEvent.end.setHours(8, 0, 0, 0);
                            selectedEvent.end.setHours(16, 0, 0, 0);

                            if (!isEventConflict(selectedEvent, selectedSlots)) {
                                console.log("Marking:", currentDate);
                                calendar.addEvent({
                                    title: 'Manhã Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    rendering: 'background',
                                    color: '#ff0000',
                                    dataType: 'unselectable'
                                });
                                selectedSlots.push({
                                    title: 'Manhã Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T08:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    color: '#ff0000',
                                    cod_turma: 0,
                                    dataType: 'unselectable'
                                });
                            }
                        }
                        currentDate.setDate(currentDate.getDate() + 7);
                    }
                }


                function SelecionarSabadosTarde() {
                    var currentDate = new Date();
                    var currentYear = currentDate.getFullYear();
                    currentDate.setDate(currentDate.getDate() + (6 - currentDate.getDay()));

                    while (currentDate.getFullYear() === currentYear && currentDate.getDay() === 6) {
                        if (!isDateInArray(currentDate, Sundays_Holidays)) {
                            var selectedEvent = {
                                start: new Date(currentDate),
                                end: new Date(currentDate)
                            };
                            selectedEvent.start.setHours(16, 0, 0, 0);
                            selectedEvent.end.setHours(23, 0, 0, 0);

                            if (!isEventConflict(selectedEvent, selectedSlots)) {
                                console.log("Marking:", currentDate);
                                calendar.addEvent({
                                    title: 'Tarde Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    rendering: 'background',
                                    color: '#ff0000',
                                    dataType: 'unselectable'
                                });
                                selectedSlots.push({
                                    title: 'Tarde Não Disponível',
                                    start: currentDate.toISOString().slice(0, 10) + 'T16:00:00',
                                    end: currentDate.toISOString().slice(0, 10) + 'T23:00:00',
                                    color: '#ff0000',
                                    cod_turma: 0,
                                    dataType: 'unselectable'
                                });
                            }
                        }
                        currentDate.setDate(currentDate.getDate() + 7);
                    }
                }


                function isDateInArray(date, array) {
                    var dateString = date.toISOString().slice(0, 10);
                    for (var i = 0; i < array.length; i++) {
                        if (array[i].start.slice(0, 10) === dateString) {
                            return true;
                        }
                    }
                    return false;
                }


                document.getElementById('btn_SelecionarDiasSemanaManha').addEventListener('click', function (event) {
                    SelecionarDiasSemanaManha();
                    event.preventDefault();
                });

                document.getElementById('btn_SelecionarDiasSemanaTarde').addEventListener('click', function (event) {
                    SelecionarDiasSemanaTarde();
                    event.preventDefault();
                });

                document.getElementById('btn_SelecionarSabados').addEventListener('click', function (event) {
                    SelecionarSabados();
                    event.preventDefault();
                });

                document.getElementById('btn_SelecionarSabadosManha').addEventListener('click', function (event) {
                    SelecionarSabadosManha();
                    event.preventDefault();
                });

                document.getElementById('btn_SelecionarSabadosTarde').addEventListener('click', function (event) {
                    SelecionarSabadosTarde();
                    event.preventDefault();
                });

                document.getElementById('btn_limpar').addEventListener('click', function (event) {
                    Limpar_Disponibilidade();
                    event.preventDefault();
                });

                document.getElementById('btn_limpar_manhas').addEventListener('click', function (event) {
                    Limpar_Disponibilidade_Manhas();
                    event.preventDefault();
                });

                document.getElementById('btn_limpar_tardes').addEventListener('click', function (event) {
                    Limpar_Disponibilidade_Tardes();
                    event.preventDefault();
                });

                document.getElementById('btn_limpar_sabados').addEventListener('click', function (event) {
                    Limpar_Disponibilidade_Sabados();
                    event.preventDefault();
                });

                document.getElementById('btn_SaveSelectedSlots').addEventListener('click', function (event) {
                    var formattedSlots = selectedSlots.map(function (slot) {
                        return {
                            title: slot.title,
                            start: slot.start,
                            end: slot.end,
                            color: slot.color,
                            cod_turma: slot.cod_turma
                        };
                    });

                    $.ajax({
                        type: "POST",
                        url: "formador_disponibilidade.aspx/Gravar_Disponibilidade_Formador",
                        data: JSON.stringify({ selectedSlots: formattedSlots, cod_user: codUserValue }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d) {
                                $('#lbl_mensagem').text("Disponibilidade guardada com sucesso!");
                                $('#lbl_mensagem').addClass('alert alert-success');
                                $('#lbl_mensagem').fadeIn();

                                setTimeout(function () {
                                    $('#lbl_mensagem').fadeOut(function () {
                                        $(this).removeClass('alert alert-success');
                                    });
                                }, 3000);

                                return;
                            } else {
                                $('#lbl_mensagem').text("Erro ao guardar a disponibilidade.");
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
