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
                toDate: vm.lastDate.format("YYYY-MM-DD")
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
                room.bookingRows = [];

                var calendarRoom = {
                    room: room,
                    days: []
                };

                calendar.rooms.push(calendarRoom);
                
                for (var iDay = 0; iDay < vm.days.length; iDay++) {

                    var day = vm.days[iDay];
                    var dayNumber = parseFloat(day.format('X'));

                    var calendarDay = { day: dayNumber, bookings: [] };
                    calendarDay.bookings = _.filter(vm.bookings, function(booking) {
                        return booking.room.id == room.id && booking.fromNumber == dayNumber;
                    });

                    calendarRoom.days.push(calendarDay);


                    //var bookingFound = _.findWhere(vm.bookings, function(booking) {
                    //    return booking.room.id == room.id && booking.fromNumber == dayNumber;
                    //});

                    //if (bookingFound)
                    //{
                    //    room.bookingRows.push(bookingFound);
                    //    iDay = iDay + bookingFound.nigths;
                    //}

                }

                
                
                //for (var iDay = 0; iDay < vm.days.length; iDay++) {
                //    var day = vm.days[iDay];
                //    var dayNumber = parseFloat(day.format('X'));

                //    var bookingDay = { day:day.format('YYYYMMDD'), bookings: [] };
                //    //console.log(dayNumber, 'dayNumber');
                //    bookingDay.bookings = _.filter(vm.bookings, function(booking) {
                //        return booking.fromNumber == dayNumber && booking.room.id == room.id;
                //    });

                //    room.days.push(bookingDay);
                //}
            }

            console.log('calendar', calendar);


            ////for (var iDay = 0; iDay < vm.days.length; iDay++) {

            ////    var day = vm.days[iDay];



            ////    for (var iRoom = 0; iRoom < vm.rooms.length; iRoom++) {
            ////        var room = vm.rooms[iRoom];

                    
            ////    }
            ////}
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
