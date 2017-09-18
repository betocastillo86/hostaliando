(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('UpdatePasswordRecoveryController', UpdatePasswordRecoveryController);

    UpdatePasswordRecoveryController.$inject = ['$routeParams', 'userService', 'modalService', 'routingService'];

    function UpdatePasswordRecoveryController($routeParams, userService, modalService, routingService) {
        var vm = this;
        vm.token = $routeParams.token;
        vm.model = {};
        vm.isSending = false;

        vm.save = save;

        activate();

        function activate()
        {
            getToken();
        }

        function getToken()
        {
            userService.getPasswordRecovery(vm.token)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response)
            {

            }

            function getError(response)
            {
                modalService.showError({
                    message: 'La url a la que intentas acceder ya expiro. Te enviaremos a una página donde puedas solicitar nuevamente cambio de clave',
                    redirectAfterClose: routingService.getRoute('passwordrecovery')
                });
            }
        }

        function save()
        {
            if (vm.form.$valid && !vm.isSending)
            {
                vm.isSending = true;

                userService.putPasswordRecovery(vm.token, vm.model)
                    .then(putCompleted)
                    .catch(putError);

                function putCompleted()
                {
                    modalService.show({
                        message: 'Tu clave ha sido cambiada correctamente, ingresa ahora a Hostaliando',
                        redirectAfterClose: routingService.getRoute('login')
                    });
                }

                function putError()
                {
                    modalService.showError({
                        message: 'No fue posible actualizar tus datos'
                    });
                }
            }
        }
    }
})();

