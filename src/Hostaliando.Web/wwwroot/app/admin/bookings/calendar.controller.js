(function() {
    'use strict';

    angular
        .module('hostaliando')
        .controller('CalendarController', CalendarController);

    CalendarController.$inject = [
        'roomService',
        'sessionService',
        'exceptionService',
        'bookingService'];

    function CalendarController(
        roomService,
        sessionService,
        exceptionService,
        bookingService) {

        var vm = this;
        vm.hostelId = undefined;
        vm.rooms = [];
        vm.bookings = [];
        vm.model = {};
        vm.firstDate = undefined;
        vm.lastDate = undefined;
        vm.days = [];


        activate();

        function activate() {

            vm.hostelId = sessionService.getCurrentUser().hostel.id;
            calculateCurrentDate();
            

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
                sortBy: 'FromDate'
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

        function calculateCalendar()
        {
            var calendar = {};
            calendar.rooms = [];


            for (var iRoom = 0; iRoom < vm.rooms.length; iRoom++) {
                var room = vm.rooms[iRoom];

                var calendarRoom = {
                    rows: []
                };

                calendar.rooms.push(calendarRoom);

                var nextRoom = false;
                
                do {

                    var roomRow = { days: [] };

                    for (var iDay = 0; iDay < vm.days.length; iDay++) {

                        var day = vm.days[iDay];
                        var dayNumber = parseFloat(day.format('X'));

                        var calendarDay = { day: dayNumber, booking: undefined };

                        roomRow.days.push(calendarDay);

                        for (var iBookingDay = 0; iBookingDay < vm.bookings.length; iBookingDay++) {
                            var booking = vm.bookings[iBookingDay];

                            if (!booking.alreadySelected && booking.room.id == room.id && (booking.fromNumber == dayNumber || (iDay == 0 && booking.fromNumber < dayNumber /**Condicion para las reservas que empiecen antes de lo que se muestra en el calendario**/))) {
                                calendarDay.booking = booking;
                                booking.alreadySelected = true;

                                if (iDay == 0 && booking.fromNumber < dayNumber)
                                {
                                    booking.nigths = booking.nigths - day.diff(moment(booking.fromDate), 'days');
                                }

                                break;
                            }
                        }

                        if (calendarDay.booking) {
                            iDay = iDay + calendarDay.booking.nigths - 1;
                        }

                    }
                    
                    calendarRoom.rows.push(roomRow);
                    nextRoom = _.find(vm.bookings, function (booking) { return !booking.alreadySelected && booking.room.id == room.id }) != undefined;

                } while (nextRoom);
            }

            vm.calendar = calendar;

            console.log('calendar', calendar);
        }
        

        function calculateCurrentDate()
        {
            var daysToShow = 10;

            vm.firstDate = moment().startOf('day');
            vm.lastDate = moment().startOf('day').add(daysToShow, 'days');
            vm.days = [];

            for (var i = 0; i < daysToShow; i++) {
                var newday = moment().startOf('day').add(i, 'days');
                newday.numberDate = parseFloat(newday.format('X')); 
                vm.days.push(newday);
            }
        }
    }
})();
