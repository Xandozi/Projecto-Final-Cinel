<%@ Page Title="" Language="C#" MasterPageFile="~/Cinel.Master" AutoEventWireup="true" CodeBehind="formador_disponibilidade.aspx.cs" Inherits="Projeto_Final.formador_disponibilidade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
    <script>

        document.addEventListener('DOMContentLoaded', function () {
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
                slotMaxTime: '24:00:00', // End time
                events: [
                    // Add events for national holidays
                    {
                        title: 'New Year',
                        start: '2024-01-01',
                        rendering: 'background',
                        color: '#ff9f89'
                    },
                    // Add more national holidays here...
                    // Add a background event for all days (availability)
                    {
                        start: '00:00', // Start of the day
                        end: '24:00', // End of the day
                        rendering: 'background',
                        color: '#00ff00', // Green
                        overlap: true,
                        allDay: true
                    },
                    // Add a background event for all Sundays (unavailability)
                    {
                        daysOfWeek: [0], // 0 represents Sunday
                        start: '00:00', // Start of the day
                        end: '24:00', // End of the day
                        rendering: 'background',
                        color: '#ff0000', // Red
                        overlap: false,
                        allDay: true
                    }
                ],
                contentHeight: 'auto', // Adjusts the calendar's height automatically
                aspectRatio: 1.5
            });
            calendar.render();
        });




    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card" style="border-color: #333; margin-top: 30px; margin-bottom:30px;">
        <div class="card-header bg-dark text-white">
            <h2 class="display-4 text-center" style="font-size: 30px; color: white;">Disponibilidade Formando</h2>
        </div>
        <div class="card" style="margin: 5px;">
            <div class="card-body">
                <div id='calendar' style="margin-top: 30px; margin-bottom: 10px;"></div>
            </div>
        </div>
    </div>
    
</asp:Content>


