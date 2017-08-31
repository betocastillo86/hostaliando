(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditRoomController', EditRoomController);

    EditRoomController.$inject = [
        '$routeParams',
        'routingService',
        'exceptionService',
        'modalService',
        'roomService',
        'sessionService'];

    function EditRoomController(
        $routeParams,
        routingService,
        exceptionService,
        modalService,
        roomService,
        sessionService) {

        var vm = this;
        vm.model = { id: $routeParams.id };
        vm.isSending = false;
        vm.continueAfterSaving = false;
        vm.canSelectHostel = sessionService.isAdmin();
        
        vm.save = save;
        vm.changeHostel = changeHostel;

        activate();

        function activate()
        {
            getRoom();
        }

        function getRoom()
        {
            if (vm.model.id)
            {
                roomService.get(vm.model.id)
                    .then(getCompleted)
                    .catch(exceptionService.handle);

                function getCompleted(response) {
                    vm.model = response;
                }            
            }
        }

        function save()
        {
            if (!vm.isSending && vm.form.$valid) {
                if (vm.model.id) {
                    roomService.put(vm.model.id, vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
                else {
                    roomService.post(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }

                function saveCompleted(response) {
                    var message = response.id ? 'Habitación creada correctamente' : 'Habitación actualizada correctamente';
                    vm.model.id = vm.model.id || response.id;
                    var redirectAfterClose = routingService.getRoute(vm.continueAfterSaving ? 'editroom' : 'rooms', { id: vm.model.id });

                    modalService.show({ message: message, redirectAfterClose: redirectAfterClose });
                    vm.isSending = false;
                }

                function saveError(error) {
                    exceptionService.handle(error);
                    vm.isSending = false;
                }
            }
        }

        function changeHostel(selected)
        {
            vm.model.hostel = selected ? selected.originalObject : {};
        }
    }
})();

