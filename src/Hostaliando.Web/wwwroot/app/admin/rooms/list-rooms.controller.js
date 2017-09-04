(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListRoomsController', ListRoomsController);

    ListRoomsController.$inject = ['roomService', 'exceptionService', 'sessionService'];

    function ListRoomsController(roomService, exceptionService, sessionService) {

        var vm = this;
        vm.rooms = [];
        vm.filter = {
            page: 0,
            pageSize: 15
        };
        vm.pager = {};

        vm.changePage = changePage;

        activate();

        function activate()
        {
            vm.filter.hostelId = !sessionService.isAdmin() ? sessionService.getCurrentUser().hostel.id : undefined;

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
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            getRooms();
        }

    }
})();
