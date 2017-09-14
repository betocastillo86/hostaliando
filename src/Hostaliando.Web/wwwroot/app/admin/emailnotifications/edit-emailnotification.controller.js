
(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditEmailNotificationController', EditEmailNotificationController);

    EditEmailNotificationController.$inject = [
        '$routeParams',
        'emailNotificationService',
        'exceptionService',
        'routingService',
        'modalService'];

    function EditEmailNotificationController(
        $routeParams,
        emailNotificationService,
        exceptionService,
        routingService,
        modalService) {

        var vm = this;
        vm.model = {
            id: $routeParams.id
        };
        vm.isSending = false;
        vm.requeue = false;

        vm.save = save;
        vm.markQueue = markQueue;

        activate();

        function activate() {
            getNotification();
        }

        function getNotification() {
            emailNotificationService.get(vm.model.id)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.model = response;
            }
        }

        function save() {
            if (vm.form.$valid && !vm.isSending) {
                vm.isSending = true;

                if (vm.requeue) {
                    if (confirm('¿Seguro desea encolar nuevamente el correo?')) {
                        emailNotificationService.post(vm.model)
                            .then(saveCompleted)
                            .catch(saveError);
                    }
                    else {
                        vm.requeue = false;
                        vm.isSending = false;
                        return;
                    }
                }
                else {
                    emailNotificationService.put(vm.model.id, vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }

                function saveCompleted(response) {
                    vm.isSending = false;

                    var message = vm.requeue ? 'Notificación en cola nuevamente' : 'Notificación actualizada';
                    modalService.show({
                        message: message,
                        redirectAfterClose: routingService.getRoute('emailnotifications')
                    });
                }

                function saveError(response) {
                    vm.isSending = false;
                    vm.requeue = false;
                    exceptionService.handle(response);
                }
            }
        }

        function markQueue() {
            vm.requeue = true;
        }
    }
})();
