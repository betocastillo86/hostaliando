(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditUserController', EditUserController);

    EditUserController.$inject = [
        '$routeParams',
        'routingService',
        'exceptionService',
        'modalService',
        'userService',
        'sessionService'];

    function EditUserController(
        $routeParams,
        routingService,
        exceptionService,
        modalService,
        userService,
        sessionService) {

        var vm = this;
        vm.model = { id: $routeParams.id };
        vm.isSending = false;
        vm.continueAfterSaving = false;
        vm.timezones = [{ name: 'Bogotá, Lima, Quito (-5)', value: -5 }];
        vm.roles = ['Admin', 'HostelOwner'];
        vm.changePassword = vm.model.id ? false : true;
        
        vm.save = save;
        vm.changeHostel = changeHostel;

        activate();

        function activate() {
            getUser();
        }

        function getUser() {
            if (vm.model.id) {
                userService.get(vm.model.id)
                    .then(getCompleted)
                    .catch(exceptionService.handle);

                function getCompleted(response) {
                    vm.model = response;
                }
            }
        }

        function save() {
            if (!vm.isSending && vm.form.$valid) {
                if (vm.model.id) {
                    userService.put(vm.model.id, vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
                else {
                    userService.post(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }

                function saveCompleted(response) {
                    var message = response.id ? 'Usuario creado correctamente' : 'Usuario actualizado correctamente';
                    vm.model.id = vm.model.id || response.id;
                    var redirectAfterClose = routingService.getRoute(vm.continueAfterSaving ? 'edituser' : 'users', { id: vm.model.id });

                    modalService.show({ message: message, redirectAfterClose: redirectAfterClose });
                    vm.isSending = false;
                }

                function saveError(error) {
                    exceptionService.handle(error);
                    vm.isSending = false;
                }
            }
        }

        function changeHostel(selected) {
            vm.model.hostel = selected ? selected.originalObject : {};
        }
    }
})();

