(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('RootController', RootController);

    RootController.$inject = ['sessionService'];

    function RootController(sessionService) {
        var vm = this;
        vm.currentUser = undefined;

        activate();

        function activate()
        {
            vm.currentUser = sessionService.getCurrentUser();
        }
    }
})();
