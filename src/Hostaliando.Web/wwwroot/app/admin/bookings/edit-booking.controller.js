(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditBookingController', EditBookingController);

    EditBookingController.$inject = [
        '$scope',
        'bookingService',
        'exceptionService',
        'modalService',
        'sessionService',
        'hostelService'];

    function EditBookingController(
        $scope,
        bookingService,
        exceptionService,
        modalService,
        sessionService,
        hostelService) {

        var vm = this;
        vm.model = $scope.params.booking || {};
        vm.room = $scope.params.room;
        vm.day = $scope.params.day;
        vm.sources = $scope.params.sources || [];
        vm.isSending = false;
        vm.hostelId = sessionService.getCurrentUser().hostel.id;

        vm.save = save;
        vm.close = close;

        activate();

        function activate() {

            vm.model.fromDate = vm.model.fromDate ? moment(vm.model.fromDate, 'YYYY/MM/DD').format('YYYY/MM/DD') : vm.day.format('YYYY/MM/DD');

            if (!vm.sources.length)
            {
                getSources();
            }

            if (!vm.room && vm.model)
            {
                vm.room = vm.model.room;
            }
        }

        function changeLocation(selected)
        {
            vm.model.location = selected ? selected.originalObject : undefined;
        }

        function save()
        {
            if (!vm.isSending && vm.form.$valid)
            {
                var toDate = moment(vm.model.fromDate, 'YYYY/MM/DD').add(vm.model.nights - 1, 'days'); 
                vm.model.toDate = toDate.format('YYYY/MM/DD');

                if (vm.model.id) {
                    bookingService.put(vm.model.id, vm.model)
                        .then(saveCompleted)
                        .catch(exceptionService.handle);
                }
                else {
                    vm.model.room = { id: vm.room.id };
                    bookingService.post(vm.model)
                        .then(saveCompleted)
                        .catch(exceptionService.handle);
                }

                function saveCompleted(response)
                {
                    modalService.show({message: 'Reserva guardada correctamente'});
                    close({ reload: true });
                }
            }
        }

        function close(options) {
            $scope.close(options || { accept: true });
        }

        function getSources()
        {
            hostelService.getSourcesByHostel(vm.hostelId)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.sources = response;
            }
        }
    }
})();
