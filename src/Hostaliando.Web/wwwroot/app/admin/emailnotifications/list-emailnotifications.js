(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListEmailNotficationsController', ListEmailNotficationsController);

    ListEmailNotficationsController.$inject = ['emailNotificationService', 'exceptionService'];
    
    function ListEmailNotficationsController(emailNotificationService, exceptionService) {
        var vm = this;
        vm.notifications = [];

        vm.filter = {
            pageSize: 30,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.changeSent = changeSent;

        return activate();

        function activate() {
            getNotifications();
        }

        function getNotifications() {
            emailNotificationService.getAll(vm.filter)
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
            getNotifications();
        }

        function changeSent(sent)
        {
            vm.filter.sent = sent;
            changePage(0);
        }
    }
})();
