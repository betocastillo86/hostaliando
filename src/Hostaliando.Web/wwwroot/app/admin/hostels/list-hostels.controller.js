
(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListHostelsController', ListHostelsController);

    ListHostelsController.$inject = ['hostelService', 'exceptionService'];

    function ListHostelsController(hostelService, exceptionService) {
        var vm = this;

        vm.hostels = [];
        vm.filter = {
            pageSize: 10,
            page: 0
        };

        vm.changePage = changePage;

        activate();

        function activate() {
            getHostels();
        }

        function getHostels() {
            hostelService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.hostels = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            return getHostels();
        }
    }
})();
