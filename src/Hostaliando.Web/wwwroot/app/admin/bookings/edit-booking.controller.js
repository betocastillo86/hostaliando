(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditBookingController', EditBookingController);

    EditBookingController.$inject = [
        '$scope',
        'bookingService',
        'exceptionService',
        'modalService'];

    function EditBookingController(
        $scope,
        bookingService,
        exceptionService,
        modalService) {

        var vm = this;
        vm.model = $scope.params.booking || {};
        vm.room = $scope.params.room;
        vm.day = $scope.params.day;
        vm.sources = $scope.params.sources;
        vm.isSending = false;

        vm.save = save;
        vm.close = close;

        activate();

        function activate() {

            vm.model.fromDate = vm.model.fromDate ? moment(vm.model.fromDate, 'YYYY/MM/DD').format('YYYY/MM/DD') : vm.day.format('YYYY/MM/DD');
        }

        function changeLocation(selected)
        {
            vm.model.location = selected ? selected.originalObject : undefined;
        }

        function save()
        {
            if (!vm.isSending && vm.form.$valid)
            {
                var toDate = moment(vm.model.fromDate, 'YYYY/MM/DD').add(vm.model.nigths - 1, 'days'); 
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
    }
})();
