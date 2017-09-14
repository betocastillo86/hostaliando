(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('RootController', RootController);

    RootController.$inject = [
        '$rootScope',
        'sessionService',
        'routingService',
        'templateService'];

    function RootController(
        $rootScope,
        sessionService,
        routingService,
        templateService) {

        var vm = this;
        vm.currentUser = undefined;
        vm.getRoute = routingService.getRoute;
        vm.getTemplate = routingService.getTemplate;
        vm.closeSubmenu = closeSubmenu;

        activate();

        function activate()
        {
            vm.currentUser = sessionService.getCurrentUser();
        }

        function closeSubmenu()
        {
            $rootScope.$broadcast('closeSubmenu');
        }
    }
})();
