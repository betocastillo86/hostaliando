(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('RootController', RootController);

    RootController.$inject = [
        'sessionService',
        'routingService'];

    function RootController(
        sessionService,
        routingService) {

        var vm = this;
        vm.currentUser = undefined;
        vm.getRoute = routingService.getRoute;

        activate();

        function activate()
        {
            vm.currentUser = sessionService.getCurrentUser();
        }
    }
})();
