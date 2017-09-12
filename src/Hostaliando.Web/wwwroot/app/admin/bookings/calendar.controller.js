(function() {
    'use strict';

    angular
        .module('hostaliando')
        .controller('CalendarController', CalendarController);

    CalendarController.$inject = [
        'roomService',
        'sessionService',
        'exceptionService',
        'bookingService',
        'modalService',
        'templateService',
        'hostelService'];

    function CalendarController(
        roomService,
        sessionService,
        exceptionService,
        bookingService,
        modalService,
        templateService,
        hostelService) {

        var vm = this;
        vm.hostelId = undefined;
        vm.rooms = [];
        vm.bookings = [];
        vm.model = {};
        vm.firstDate = undefined;
        vm.lastDate = undefined;
        vm.lastDateNumber = undefined;
        vm.bookingSources = [];
        vm.days = [];
        vm.isShowing = true;
        vm.todayNumber = undefined;

        vm.getSourceColor = getSourceColor;
        vm.addBooking = addBooking;
        vm.moveBooking = moveBooking;

        vm.contextMenuOptions = [
            { text: 'Editar reserva', click: callbackViewBooking/*, enabled: function (s, e, m) { return m.booking !== undefined; }*/ },
            { text: 'Eliminar reserva', click: callbackDeleteBooking/*, enabled: function (s, e, m) { return m.booking !== undefined; }*/ },
            //{ text: 'Crear reserva', click: callbackViewBooking, enabled: function (s, e, m) { return m.booking == undefined; } }
        ];


        activate();

        function activate() {

            vm.hostelId = sessionService.getCurrentUser().hostel.id;
            calculateCurrentDate();
            getBookingSources();
            

            if (vm.hostelId)
            {
                getRooms();
            }
        }

        function getRooms()
        {
            roomService.getAll({ hostelId: vm.hostelId, pageSize: 50 })
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.rooms = response.results;
                getBookings();
            }
        }

        function getBookings()
        {
            var filter = {
                page: 0,
                pageSize: 200,
                hostelId: vm.hostelId,
                fromDate: vm.firstDate.format("YYYY-MM-DD"),
                toDate: vm.lastDate.format("YYYY-MM-DD"),
                sortBy: 'FromDate',
                status: 'Booked'
            };

            bookingService.getAll(filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.bookings = response.results;
                for (var i = 0; i < vm.bookings.length; i++) {
                    var booking = vm.bookings[i];

                    // adds the Unix format for easily search
                    booking.fromNumber = parseFloat(moment(booking.fromDate).format('X'));
                    booking.toNumber = parseFloat(moment(booking.toDate).format('X'));
                }

                calculateCalendar();
            }
        }

        function callbackViewBooking($itemScope, $event, model)
        {
            addBooking(model.day, model.booking, model.room);
        }

        function callbackDeleteBooking($itemScope, $event, model)
        {
            if (confirm('¿Estás seguro de eliminar esta reserva?'))
            {
                deleteBooking(model.booking.id);
            }
        }

        function addBooking(day, booking, room)
        {
            modalService.show({
                controller: 'EditBookingController',
                controllerAs: 'editBooking',
                template: templateService.get('bookings/edit-booking'),
                params: {
                    booking: booking,
                    day: day,
                    room: room,
                    sources: vm.bookingSources
                },
                closed: bookingClosed
            });
        }

        function deleteBooking(id)
        {
            bookingService.delete(id)
                .then(deleteCompleted)
                .catch(exceptionService.handle);

            function deleteCompleted(response)
            {
                bookingClosed({reload:true});
                modalService.show({ message: 'Reserva cancelada' });
            }
        }

        function bookingClosed(response)
        {
            if (response.reload)
            {
                vm.isShowing = false;
                getBookings();
            }
        }

        function calculateCalendar()
        {
            var calendar = {};
            calendar.rooms = [];


            for (var iRoom = 0; iRoom < vm.rooms.length; iRoom++) {
                var room = vm.rooms[iRoom];

                var calendarRoom = {
                    rows: [],
                    room: room,
                    emptyRow: undefined
                };

                calendar.rooms.push(calendarRoom);

                var nextRoom = false;
                
                do {

                    var roomRow = { days: [] };

                    for (var iDay = 0; iDay < vm.days.length; iDay++) {

                        var day = vm.days[iDay];
                        var dayNumber = parseFloat(day.format('X'));

                        var calendarDay = { day: day, booking: undefined, room: room };
                        
                        roomRow.days.push(calendarDay);

                        for (var iBookingDay = 0; iBookingDay < vm.bookings.length; iBookingDay++) {
                            var booking = vm.bookings[iBookingDay];

                            if (!booking.alreadySelected && booking.room.id == room.id && (booking.fromNumber == dayNumber || (iDay == 0 && booking.fromNumber < dayNumber /**Condicion para las reservas que empiecen antes de lo que se muestra en el calendario**/))) {
                                calendarDay.booking = booking;
                                //calendarDay.room = room;

                                booking.alreadySelected = true;

                                if (iDay == 0 && booking.fromNumber < dayNumber)
                                {
                                    booking.nights = booking.nights - day.diff(moment(booking.fromDate), 'days');
                                }

                                if (booking.toNumber > vm.lastDateNumber)
                                {
                                    booking.nights = booking.nights - moment(booking.toDate).diff(vm.lastDate, 'days');
                                }

                                booking.source.color = getSourceColor(booking.source.id);

                                break;
                            }
                        }

                        if (calendarDay.booking) {
                            iDay = iDay + calendarDay.booking.nights - 1;
                        }

                    }
                    
                    calendarRoom.rows.push(roomRow);
                    nextRoom = _.find(vm.bookings, function (booking) { return !booking.alreadySelected && booking.room.id == room.id }) != undefined;

                } while (nextRoom);

                //// Si la habitación es privada y no tiene filas agrega la fila de reserva
                if (room.isPrivated && !calendarRoom.rows.length) {
                    calendarRoom.emptyRow = { availableRows: 1 };
                }
                else if (!room.isPrivated && calendarRoom.rows.length < room.beds)
                {
                    calendarRoom.emptyRow = { availableRows: room.beds - calendarRoom.rows.length };
                }
            }

            vm.calendar = calendar;
            vm.isShowing = true;

            console.log('calendar', calendar);
        }

        function getBookingSources()
        {
            hostelService.getSourcesByHostel(vm.hostelId)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.bookingSources = response;
            }
        }
        

        function calculateCurrentDate()
        {
            var daysToShow = 10;

            vm.todayNumber = parseFloat(moment().startOf('day').format('X'));
            vm.firstDate = moment().startOf('day');
            vm.lastDate = moment().startOf('day').add(daysToShow - 1, 'days');
            vm.lastDateNumber = parseFloat(vm.lastDate.format('X'));
            vm.days = [];

            for (var i = 0; i < daysToShow; i++) {
                var newday = moment().startOf('day').add(i, 'days');
                newday.numberDate = parseFloat(newday.format('X')); 
                vm.days.push(newday);
            }
        }

        function getSourceColor(sourceId)
        {
            return _.findWhere(vm.bookingSources, { id: sourceId }).color;
        }

        function moveBooking(from, to)
        {
            if (!to.booking && from.booking)
            {
                var fromDate = moment(to.day);
                var untilDate = moment(to.day).add(from.booking.nights - 1, 'days');

                var jsonPatch = [
                    { op: 'replace', path: '/room/id', value: to.room.id },
                    { op: 'replace', path: '/fromDate', value: fromDate.format('YYYY/MM/DD') },
                    { op: 'replace', path: '/toDate', value: untilDate.format('YYYY/MM/DD') },
                ];

                bookingService.patch(from.booking.id, jsonPatch)
                    .then(patchCompleted)
                    .catch(exceptionService.handle);

                function patchCompleted() {

                    modalService.show({
                        message: 'Reserva reasignada correctamente'
                    });

                    bookingClosed({reload:true});
                }
                
                
            }
        }
    }
})();
