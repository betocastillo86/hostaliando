(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListNotificationsController', ListNotificationsController);

    ListNotificationsController.$inject = ['notificationService', 'exceptionService'];

    function ListNotificationsController(notificationService, exceptionService) {
        var vm = this;

        vm.notifications = [];
        vm.filter = {
            pageSize: 10,
            page: 0
        };

        vm.changePage = changePage;

        activate();
        
        function activate() {
            getNotifications();
        }

        function getNotifications() {
            notificationService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.notifications = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            return getNotifications();
        }
    }
})();
