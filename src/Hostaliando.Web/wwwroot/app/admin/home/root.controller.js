(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('RootController', RootController);

    RootController.$inject = [
        'sessionService',
        'routingService',
        'templateService'];

    function RootController(
        sessionService,
        routingService,
        templateService) {

        var vm = this;
        vm.currentUser = undefined;
        vm.getRoute = routingService.getRoute;
        vm.getTemplate = routingService.getTemplate;

        activate();

        function activate()
        {
            vm.currentUser = sessionService.getCurrentUser();
        }
    }
})();
