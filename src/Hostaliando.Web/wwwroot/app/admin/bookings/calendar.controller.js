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
        'hostelService',
        'routingService'];

    function CalendarController(
        roomService,
        sessionService,
        exceptionService,
        bookingService,
        modalService,
        templateService,
        hostelService,
        routingService) {

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
        vm.isShowingHeader = true;
        vm.todayNumber = undefined;
        vm.calendarRange = 'Week';
        vm.title = '';

        vm.getSourceColor = getSourceColor;
        vm.addBooking = addBooking;
        vm.moveBooking = moveBooking;
        vm.changeRange = changeRange;
        vm.showPrevious = showPrevious;
        vm.showNext = showNext;
        vm.setToday = setToday;

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
                fromDate: vm.firstDate.format(app.Settings.general.dateFormat),
                toDate: vm.lastDate.format(app.Settings.general.dateFormat),
                sortBy: 'FromDate',
                notStatus: 'Canceled'
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

                if (!vm.calendar.rooms.length)
                {
                    modalService.showError({
                        message: 'No puedes revisar el calendario si no tienes habitaciones creadas. Dale Aceptar y te enviaremos a crear una.',
                        redirectAfterClose: routingService.getRoute('newroom')
                    });
                }

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

        function changeRange(range)
        {
            vm.calendarRange = range;
            vm.isShowing = false;

            calculateCurrentDate(vm.firstDate);
            getBookings();
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
                    calendarRoom.emptyRow =
                        {
                            days: getEmptyDays(room),
                            availableRows: room.beds - calendarRoom.rows.length
                        };
                }
            }

            vm.calendar = calendar;
            vm.isShowing = true;
            vm.isShowingHeader = true;

            console.log('calendar', calendar);
        }

        function getEmptyDays(room)
        {
            var emptyDays = [];

            for (var i = 0; i < vm.days.length; i++) {
                emptyDays.push({
                    day: vm.days[i],
                    room: room,
                    booking: undefined
                });
            }

            return emptyDays;
        }

        function setToday()
        {
            vm.firstDate = moment().startOf('day');
            rewriteCalendar();
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
                    { op: 'replace', path: '/fromDate', value: fromDate.format(app.Settings.general.dateFormat) },
                    { op: 'replace', path: '/toDate', value: untilDate.format(app.Settings.general.dateFormat) },
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

        function calculateCurrentDate(startDate) {
            var daysToShow = getDaysToAddByRange();

            vm.todayNumber = parseFloat(moment().startOf('day').format('X'));

            vm.firstDate = startDate || moment().startOf('day').add(-1, 'days'); // Resta un día para poder ver quién se va hoy
            vm.lastDate = moment(vm.firstDate).startOf('day').add(daysToShow - 1, 'days');

            vm.lastDateNumber = parseFloat(vm.lastDate.format('X'));
            vm.days = [];

            for (var i = 0; i < daysToShow; i++) {
                var newday = moment(vm.firstDate).startOf('day').add(i, 'days');
                newday.numberDate = parseFloat(newday.format('X'));
                vm.days.push(newday);
            }

            vm.title = vm.firstDate.format('MMMM') + ' ' + vm.firstDate.format('YYYY');
        }

        function showPrevious()
        {
            vm.firstDate = moment(vm.firstDate).add(getDaysToAddByRange() * -1, 'days');
            rewriteCalendar();
        }

        function showNext()
        {
            vm.firstDate = moment(vm.firstDate).add(getDaysToAddByRange(), 'days');
            rewriteCalendar();
        }

        function rewriteCalendar()
        {
            vm.isShowing = false;
            vm.isShowingHeader = false;

            calculateCurrentDate(vm.firstDate);
            getBookings();
        }

        function getDaysToAddByRange()
        {
            switch (vm.calendarRange) {
                default:
                case 'Week':
                    return 7;
                case 'Month':
                    return 30;
                case 'Day':
                    return 1;
            }
        }
        
    }
})();
