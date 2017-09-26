(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListBookingsController', ListBookingController);

    ListBookingController.$inject = [
        'bookingService',
        'exceptionService',
        'sessionService',
        'modalService',
        'roomService',
        'templateService'];

    function ListBookingController(
        bookingService,
        exceptionService,
        sessionService,
        modalService,
        roomService,
        templateService)
    {
        var vm = this;
        vm.hostelId = sessionService.getHostel().id;
        vm.rooms = [];

        vm.hostels = [];
        vm.filter = {
            pageSize: 10,
            page: 0,
            hostelId: vm.hostelId,
            orderBy: 'FromDate',
            fromDate: moment().format(app.Settings.general.dateFormat),
            toDate: moment().format(app.Settings.general.dateFormat)
        };

        vm.changePage = changePage;
        vm.getStatusName = getStatusName;
        vm.showBooking = showBooking;

        activate();

        function activate() {
            getRooms();
        }

        function getBookings() {
            bookingService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.bookings = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function getRooms()
        {
            var filterRoom = {
                page: 0,
                pageSize: 50,
                hostelId: vm.hostelId
            };
            roomService.getAll(filterRoom)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.rooms = response.results;
                getBookings();
            }
        }

        function getStatusName(booking) {
            return bookingService.getStatusName(booking.status);
        }

        function showBooking(booking)
        {
            modalService.show({
                controller: 'EditBookingController',
                controllerAs: 'editBooking',
                template: templateService.get('bookings/edit-booking'),
                params: {
                    booking: booking//,
                    //day: booki,
                    //room: room,
                    //sources: vm.bookingSources,
                    //nigths: nigths
                }
            });
        }

        function changePage(page) {
            vm.filter.page = page;
            getBookings();
        }
    }
})();
