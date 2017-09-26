(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('AvailabilityController', AvailabilityController);

    AvailabilityController.$inject = [
        '$scope',
        'bookingService',
        'exceptionService',
        'sessionService',
        'modalService'];

    function AvailabilityController(
        $scope,
        bookingService,
        exceptionService,
        sessionService,
        modalService) {
        var vm = this;

        vm.hostels = [];
        vm.filter = {
            hostelId: sessionService.getHostel().id,
            fromDate: moment().startOf('day').format(app.Settings.general.dateFormat),
            toDate: moment().startOf('day').format(app.Settings.general.dateFormat),
            people: 1
        };

        vm.changePage = changePage;
        vm.search = search;
        vm.newBooking = newBooking;

        activate();

        function activate() {

        }

        function getAvailability() {
            bookingService.getAvailability(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.bookings = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            getAvailability();
        }

        function newBooking(room)
        {
            $scope.close({
                newBooking: {
                    room: room,
                    fromDate: vm.filter.fromDate,
                    toDate: vm.filter.toDate,
                    people: vm.filter.people
                }
            });
        }

        function search()
        {
            if (vm.form.$valid)
            {
                changePage(0);
            }
        }
    }
})();
