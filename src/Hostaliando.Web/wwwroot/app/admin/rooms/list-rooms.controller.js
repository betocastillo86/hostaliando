(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListRoomsController', ListRoomsController);

    ListRoomsController.$inject = ['roomService', 'exceptionService'];

    function ListRoomsController(roomService, exceptionService) {

        var vm = this;
        vm.rooms = [];
        vm.filter = {
            page: 0,
            pageSize: 2
        };
        vm.pager = {};

        vm.changePage = changePage;

        activate();

        function activate()
        {
            getRooms();
        }

        function getRooms()
        {
            roomService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.rooms = response.results;
                vm.filter.totalCount = response.meta.totalCount;
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            getRooms();
        }

    }
})();
