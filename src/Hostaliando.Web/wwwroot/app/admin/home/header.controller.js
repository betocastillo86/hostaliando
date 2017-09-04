(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('HeaderController', HeaderController);

    HeaderController.$inject = ['$http', 'sessionService'];

    function HeaderController($http, sessionService) {
        var vm = this;
        vm.logout = logout;

        activate();

        function activate() { }

        function logout()
        {
            sessionService.removeCurrentUser();
            $http.defaults.headers.common.Authorization = '';
            document.location = '/login';
        }
    }
})();
