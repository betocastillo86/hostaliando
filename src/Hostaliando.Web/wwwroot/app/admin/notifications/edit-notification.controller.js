(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditNotificationController', EditNotificationController);

    EditNotificationController.$inject = [
        '$routeParams',
        'notificationService',
        'modalService',
        'exceptionService',
        'routingService'];

    function EditNotificationController(
        $routeParams,
        notificationService,
        modalService,
        exceptionService,
        routingService) {
        var vm = this;
        vm.model = {};
        vm.model.id = $routeParams.id;
        vm.continueAfterSaving = false;
        vm.isSending = false;

        vm.save = save;

        activate();

        return vm;

        function activate() {
            getNotification();
        }

        function getNotification() {
            notificationService.getById(vm.model.id)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.model = response;
            }
        }

        function save() {
            if (vm.form.$valid && !vm.isSending) {
                vm.isSending = true;
                notificationService.put(vm.model.id, vm.model)
                    .then(putCompleted)
                    .catch(putError);

                function putCompleted(response) {
                    vm.isSending = false;
                    modalService.show({
                        message: 'Notificación actualizada correctamente',
                        redirectAfterClose: vm.continueAfterSaving ? undefined : routingService.getRoute('notifications')
                    });

                    vm.continueAfterSaving = false;
                }

                function putError(response) {
                    vm.isSending = false;
                    exceptionService.handle(response);
                }
            }
        }
    }
})();