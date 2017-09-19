(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['authenticationService'];

    function LoginController(authenticationService) {
        var vm = this;
        vm.model = {};
        
        vm.validateAuthentication = validateAuthentication;
        vm.isSending = false;

        activate();

        function activate() { }

        function validateAuthentication()
        {  
            if (vm.form.$valid && !vm.isSending) {

                vm.errorAuthentication = undefined;
                vm.isSending = true;

                authenticationService.post(vm.model)
                    .then(authenticationValid)
                    .catch(authenticationInvalid);
            }

            function authenticationValid(response) {
                vm.isSending = false;
                document.location = '/';
            }

            function authenticationInvalid() {
                vm.isSending = false;
                vm.errorAuthentication = 'Los datos son invalidos';
            }
        }
    }
})();
