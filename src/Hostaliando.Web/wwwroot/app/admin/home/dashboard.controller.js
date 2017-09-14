(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = [
        'bookingService',
        'sessionService',
        'exceptionService',
        'roomService',
        'hostelService'];

    function DashboardController(
        bookingService,
        sessionService,
        exceptionService,
        roomService,
        hostelService) {

        var vm = this;
        vm.hostelId = undefined;
        vm.bookings = [];
        vm.rooms = [];
       
        vm.totalSharedBeds = 0;
        vm.totalPrivateRooms = 0;
        vm.todayNumber = parseFloat(moment().startOf('day').format('X'));

        vm.getStatusName = getStatusName;
        vm.checkinBooking = checkinBooking;
        vm.cancelBooking = cancelBooking;
        vm.viewBooking = viewBooking;

        activate();

        function activate() {
            vm.hostelId = sessionService.getCurrentUser().hostel.id;

            if (vm.hostelId)
            {
                getBookings();
                getEarnings();
            }
        }

        function getBookings()
        {
            vm.model = {
                arrivals: [],
                departures: []
            };

            var filter = {
                fromDate: moment().startOf('day').add(-1, 'days').format(app.Settings.general.dateFormat), // Se le quita un día para poder saber las personas que se van
                toDate: moment().startOf('day').format(app.Settings.general.dateFormat),
                notStatus: 'Canceled',
                hostelId: vm.hostelId,
                page: 0,
                pageSize: 200
                //toDate: moment().startOf('day').add(1, 'day').format(app.Settings.general.dateFormat)
            };
            bookingService.getAll(filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.bookings = response.results;

                vm.model.todayNumbers = {
                    arrivals: 0,
                    departures: 0,
                    freeBeds: 0,
                    freeRooms: 0
                };

                for (var i = 0; i < vm.bookings.length; i++) {
                    var booking = vm.bookings[i];
                    booking.fromNumber = parseFloat(moment(booking.fromDate).format('X'));
                    booking.toNumber = parseFloat(moment(booking.toDate).add(1, 'days').format('X'));

                    if (vm.todayNumber == booking.fromNumber) {
                        vm.model.todayNumbers.arrivals++;
                        vm.model.arrivals.push(booking);
                    }
                    else if (vm.todayNumber == booking.toNumber) {
                        vm.model.todayNumbers.departures++;
                        booking.leavesToday = true;
                        vm.model.departures.push(booking);
                    }
                }

                getRooms();
            }
        }

        function getEarnings()
        {
            hostelService.getEarnings(vm.hostelId)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.model.earnings = response;
                vm.model.earnings.percentageToday = parseInt(((vm.model.earnings.today / (vm.model.earnings.twoDays - vm.model.earnings.today)) * 100) - 100) ;
                vm.model.earnings.percentageWeek = parseInt(((vm.model.earnings.week / (vm.model.earnings.twoWeeks - vm.model.earnings.week)) * 100) - 100);
                vm.model.earnings.percentageMonth = parseInt(((vm.model.earnings.month / (vm.model.earnings.twoMonths - vm.model.earnings.month)) * 100) - 100);
            }

        }

        function getRooms()
        {
            var filter = {
                hostelId: vm.hostelId,
                page: 0,
                pageSize: 50
            };

            roomService.getAll(filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.rooms = response.results;

                for (var i = 0; i < vm.rooms.length; i++) {
                    var room = vm.rooms[i];

                    var bookingsByRoom = _.filter(vm.bookings, function (booking) { return booking.room.id == room.id && !booking.leavesToday });

                    if (room.isPrivated) {
                        vm.totalPrivateRooms++;
                        vm.model.todayNumbers.freeRooms += (bookingsByRoom.length ? 0 : 1);
                    }
                    else {
                        vm.totalSharedBeds += room.beds;
                        vm.model.todayNumbers.freeBeds += (room.beds - bookingsByRoom.length);
                    }
                }

                console.log('vm.totalPrivateRooms', vm.totalPrivateRooms);
                console.log('vm.totalSharedBeds', vm.totalSharedBeds);
            }
        }

        function getStatusName(booking)
        {
            return bookingService.getStatusName(booking.status);
        }

        function checkinBooking(booking) {
            bookingService.checkin(booking.id)
                .then(getBookings)
                .catch(exceptionService.handle);
        }

        function cancelBooking(booking) {

            if (confirm('¿Seguro desea realizar esta acción?'))
            {
                bookingService.cancel(booking.id)
                    .then(getBookings)
                    .catch(exceptionService.handle);
            }
        }

        function viewBooking(booking)
        {

        }
    }
})();

