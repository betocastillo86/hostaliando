(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('ListSettingsController', ListSettingsController);

    ListSettingsController.$inject = ['settingService', 'modalService', 'exceptionService'];

    function ListSettingsController(settingService, modalService, exceptionService) {
        var vm = this;
        vm.settings = [];
        vm.isSending = false;

        vm.filter = {
            pageSize: 30,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.getSettings = getSettings;
        vm.toggleEdit = toggleEdit;

        return activate();

        function activate() {
            getSettings();
        }

        function getSettings() {
            settingService.get(vm.filter)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.settings = response.results;
                vm.filter.totalCount = response.meta.totalCount;
                vm.filter.count = response.meta.count;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            getSettings();
        }

        function updateSetting(model) {
            settingService.put(model)
                .then(putCompleted)
                .catch(putError);

            function putCompleted(response) {
                vm.isSending = false;
                modalService.show({ message: 'Llave guardada correctamente' });
            }

            function putError(response) {
                vm.isSending = false;
                exceptionService.handle(response);
            }
        }

        function toggleEdit(setting) {
            if (setting.isEditing) {
                if (!vm.isSending) {
                    vm.isSending = true;
                    updateSetting(setting);
                }
                else {
                    modalService.show({ message: 'El valor no puede ser vacio' });
                }
            }

            setting.isEditing = !setting.isEditing;
        }
    }
})();