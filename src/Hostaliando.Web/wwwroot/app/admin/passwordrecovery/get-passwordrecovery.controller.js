(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('GetPasswordRecoveryController', GetPasswordRecoveryController);

    GetPasswordRecoveryController.$inject = ['userService', 'modalService', 'routingService'];

    function GetPasswordRecoveryController(userService, modalService, routingService) {
        var vm = this;

        vm.isSending = false;
        vm.model = {};
        vm.errorToken = undefined;

        vm.save = save;

        activate();

        function activate() { }

        function save()
        {
            if (vm.form.$valid && !vm.isSending)
            {
                userService.postPasswordRecovery(vm.model)
                    .then(postCompleted)
                    .catch(postError);

                function postCompleted(response)
                {
                    modalService.show(
                        {
                            message: 'Hemos enviado a tu correo un link para actualizar tu clave',
                            redirectAfterClose: routingService.getRoute('login')
                        });
                    vm.errorToken = undefined;
                }

                function postError(response)
                {
                    vm.errorToken = 'Valida que tu correo si se encuentre registrado';
                }
            }
        }
    }
})();

