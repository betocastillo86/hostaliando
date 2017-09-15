(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListLogsController', ListLogsController);

    ListLogsController.$inject = ['logService', 'exceptionService', 'modalService'];

    function ListLogsController(logService, exceptionService, modalService) {
        var vm = this;
        vm.logs = [];

        vm.filter = {
            pageSize: 30,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.getLogs = getLogs;
        vm.showLog = showLog;
        vm.cleanLog = cleanLog;
        
        return activate();

        function activate() {
            getLogs();
        }

        function getLogs() {
            logService.getAll(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.logs = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            getLogs();
        }

        function showLog(log) {
            var message = '<b>Mensaje corto:</b><br>' + log.shortMessage + '<br> <b>Mensaje largo:</b><br>' + log.fullMessage + '<br> <b>Fecha:</b><br>' + log.creationDate + '<br> <b>IP:</b><br>' + log.ipAddress + '<br> <b>Url:</b><br>' + log.pageUrl;
            modalService.show({ message: message, large: true, title: 'Detalle Log' });
        }

        function cleanLog() {
            if (confirm("¿Está seguro de eliminar el log?")) {
                logService.clean()
                    .then(clenCompleted)
                    .catch(exceptionService.handle);
            }

            function clenCompleted() {
                modalService.show({ message: 'Log eliminado correctamente' });
                getLogs();
            }
        }
    }
})();
