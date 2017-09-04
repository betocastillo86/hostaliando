(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListUsersController', ListUsersController);

    ListUsersController.$inject = ['userService', 'exceptionService', 'sessionService'];

    function ListUsersController(
        userService,
        exceptionService,
        sessionService) {

        var vm = this;
        vm.users = [];
        vm.filter = {
            page: 0,
            pageSize: 15
        };
        vm.pager = {};

        vm.changePage = changePage;
        vm.changeHostel = changeHostel;

        activate();

        function activate() {
            getUsers();
        }

        function getUsers() {
            userService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.users = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            getUsers();
        }

        function changeHostel(selected)
        {
            vm.filter.hostelId = selected ? selected.originalObject.id : undefined;
            changePage(0);
        }
    }
})();
